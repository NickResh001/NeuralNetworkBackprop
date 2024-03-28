using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab5
{
    public class Statistic
    {
        public int allAnswers = 0;
        public int rightAnswers = 0;
        public double[] rootMeanSquareErrors;
        public bool done;
        public double percentage => (double)rightAnswers / (double)allAnswers;
    }

    public class NeuralNetwork
    {
        readonly private Random _random = new Random();

        public List<double[]> layers;
        private List<double[]> layersErrors;
        private List<double[,]> weights;
        private List<double[]> biasweights;

        private string[] symbols;
        private string sampleDirectory = ".\\";
        private double educationWeight = 0.75;
        public List<Statistic> statistics;

        public NeuralNetwork(NNSerializeConfiguration nconf) 
        {
            if (nconf != null)
            {
                layers = nconf.layers.ToList();
                layersErrors = nconf.layersErrors.ToList();
                weights = Extensions.ToMultidimensionalArray(nconf.weights).ToList();
                biasweights = nconf.biasweights.ToList();
                symbols = nconf.symbols;
                statistics = nconf.statistics.ToList();
            }
            else
            {
                throw new Exception("nconf is null!");
            }
        }
        public NeuralNetwork(List<int> neuronsCounts, string[] symbolsForRecognize)
        {
            layers = [];
            layersErrors = [];
            weights = [];
            statistics = [];
            biasweights = [];
            symbols = symbolsForRecognize;

            if (neuronsCounts != null && neuronsCounts.Count > 2)
            {
                layers.Add(new double[neuronsCounts[0]]);
                layersErrors.Add(new double[neuronsCounts[0]]);
                biasweights.Add(new double[neuronsCounts[0]]);
                for (int i = 1; i < neuronsCounts.Count; i++)
                {
                    double[] layer = new double[neuronsCounts[i]];
                    double[] biases = new double[neuronsCounts[i]];
                    for (int j = 0; j < layer.Length; j++)
                    {
                        layer[j] = 0;
                        biases[j] = _random.NextDouble() * 2 - 1;
                    }
                    layers.Add(layer);
                    layersErrors.Add(layer);
                    biasweights.Add(biases);

                    double[,] tempweights = new double[neuronsCounts[i - 1], neuronsCounts[i]];
                    for (int j = 0; j < neuronsCounts[i - 1]; j++)
                    {
                        for (int k = 0; k < neuronsCounts[i]; k++)
                        {
                            tempweights[j, k] = _random.NextDouble() * 2 - 1;
                        }
                    }
                    weights.Add(tempweights);
                }
            }
        }
        private double SigmaFunction(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }
        public void BitmapToInputLayer(Bitmap bitmap)
        {
            int imageSize = (int)Math.Pow(layers[0].Length, 0.5);
            if (imageSize * imageSize != layers[0].Length)
                return;

            for (int i = 0; i < imageSize; i++)
            {
                for (int j = 0; j < imageSize; j++)
                {
                    double activation = 0;
                    activation += bitmap.GetPixel(i, j).R;
                    activation += bitmap.GetPixel(i, j).G;
                    activation += bitmap.GetPixel(i, j).B;
                    activation /= (255 * 3);
                    activation = 1 - activation;
                    layers[0][i * imageSize + j] = activation;
                }
            }
        }
        public Bitmap GetBitmapBySymbol(int symbolIndex, int imageNumber)
        {
            string imageNumberString = $"{imageNumber:D5}";
            return new Bitmap(Image.FromFile($".\\{symbols[symbolIndex]}\\{imageNumberString}.png"));
        }
        public string Recognize(Bitmap bitmap)
        {
            // Заполнение input слоя
            BitmapToInputLayer(bitmap);

            // Весь прямой проход
            for (int i = 1; i < layers.Count; i++)
            {
                // Заполнение одного слоя i
                for (int j = 0; j < layers[i].Length; j++)
                {
                    // Заполнение одного нейрона j слоя i
                    double sum = biasweights[i][j];
                    for (int k = 0; k < layers[i - 1].Length; k++)
                    {
                        sum += layers[i - 1][k] * weights[i - 1][k, j];
                    }
                    layers[i][j] = SigmaFunction(sum);
                }
            }

            // Поиск нейрона большей активации
            int maxIndex = -1;
            double maxValue = double.MinValue;
            for (int i = 0; i < layers.Last().Length; i++)
            {
                (maxValue, maxIndex) = maxValue < layers.Last()[i] ? (layers.Last()[i], i) : (maxValue, maxIndex);
            }
            return symbols[maxIndex];
        }
        public double CollectStatistics(int stepCount)
        {
            Statistic stat = new Statistic();
            stat.rootMeanSquareErrors = new double[stepCount];
            for (int step = 0; step < stepCount; step++)
            {
                int outputLayerSize = layers.Last().Length;
                int symbolIndex = step % outputLayerSize;
                int imageNumber = step / outputLayerSize;
                string guessSymbol = Recognize(GetBitmapBySymbol(symbolIndex, imageNumber));

                double[] expectedResult = new double[outputLayerSize];
                for (int i = 0; i < expectedResult.Length; i++)
                {
                    expectedResult[i] = 0;
                }
                expectedResult[symbolIndex] = 1;

                stat.allAnswers++;
                if (symbols[symbolIndex] == guessSymbol)
                    stat.rightAnswers++;

                double error = 0;
                for (int i = 0; i < layers.Last().Length; i++)
                {
                    error += Math.Pow(expectedResult[i] - layers.Last()[i], 2);
                }
                error *= 0.5;
                stat.rootMeanSquareErrors[step] = error;
            }
            statistics.Add(stat);
            return stat.percentage;
        }

        private void EducatePart1(double[] expectedOutput, double[] actualOutput = null)
        {
            if (actualOutput == null)
                actualOutput = layers.Last();

            // Подсчет ошибки выходного слоя
            for (int i = 0; i < actualOutput.Length; i++)
            {
                layersErrors.Last()[i] = (expectedOutput[i] - actualOutput[i]) * actualOutput[i] * (1 - actualOutput[i]);
            }

            // Подсчет ошибок скрытых слоев
            for (int i = layers.Count - 2; i >= 0; i--)
            {
                // Подсчет ошибок одного скрытого слоя
                for (int j = 0; j < layers[i].Length; j++)
                {
                    // Подсчет ошибки одного нейрона
                    double sum = 0;
                    for (int k = 0; k < layers[i + 1].Length; k++)
                    {
                        sum += weights[i][j, k] * layersErrors[i + 1][k];
                    }
                    layersErrors[i][j] = sum * layers[i][j] * (1 - layers[i][j]);
                }
            }
        }
        private void EducatePart2()
        {
            // Пересчет всех весов
            for (int i = 0; i < weights.Count; i++)
            {
                // Пересчет весов одного слоя (обход нейронов слоя "куда")
                for (int k = 0; k < layers[i + 1].Length; k++)
                {
                    // Пересчет весов от одного нейрона слоя "куда", обход нейронов слоя "откуда"
                    for (int j = 0; j < layers[i].Length; j++)
                    {
                        weights[i][j, k] += educationWeight * layersErrors[i + 1][k] * layers[i][j];
                    }
                    biasweights[i + 1][k] += educationWeight * layersErrors[i + 1][k];
                }
            }
        }
        private void Educate(double[] expectedOutput, double[] actualOutput = null)
        {
            EducatePart1(expectedOutput, actualOutput);
            EducatePart2();
        }
        public void Education(int stepCount)
        {
            for (int step = 0; step < stepCount; step++)
            {
                int outputLayerSize = layers.Last().Length;
                int symbolIndex = step % symbols.Length;
                int imageNumber = step / symbols.Length;
                string guessSymbol = Recognize(GetBitmapBySymbol(symbolIndex, imageNumber));

                double[] expectedResult = new double[outputLayerSize];
                for (int i = 0; i < expectedResult.Length; i++)
                {
                    expectedResult[i] = 0;
                }
                expectedResult[symbolIndex] = 1;

                if (symbols[symbolIndex] != guessSymbol)
                {
                    Educate(expectedResult);
                }
            }
        }
        public void GroupEducation(int stepCount, int groupSize)
        {
            int groupCount = stepCount / groupSize;
            int groupElementCount = 0;

            // Начальная инициализация группы - место сбора данных об ошибках на слоях и об активациях на слоях в процессе перебора группы
            List<List<double[]>> layersList = new List<List<double[]>>();
            List<List<double[]>> layerErrorsList = new List<List<double[]>>();
            for (int j = 0; j < groupSize; j++)
            {
                layerErrorsList.Add(new List<double[]>());
                layersList.Add(new List<double[]>());
                for (int k = 0; k < layersErrors.Count; k++)
                {
                    layerErrorsList[j].Add(new double[layersErrors[k].Length]);
                    layersList[j].Add(new double[layersErrors[k].Length]);
                }
            }

            // Начальная инициализация средних значений, на основе которых будут пересчитываться веса
            List<double[]> averageLayerErrors = new List<double[]>();
            List<double[]> averageLayers = new List<double[]>();
            for (int j = 0; j < layersErrors.Count; j++)
            {
                averageLayerErrors.Add(new double[layersErrors[j].Length]);
                averageLayers.Add(new double[layersErrors[j].Length]);
            }

            for (int groupStep = 0; groupStep < groupCount; groupStep++)
            {
                // Перебор группы, сбор данных для подсчета средних значений
                for (int stepInGroup = 0; stepInGroup < groupSize; stepInGroup++)
                {
                    int realStep = stepInGroup + groupStep * groupSize;

                    int outputLayerSize = layers.Last().Length;
                    int symbolIndex = realStep % symbols.Length;
                    int imageNumber = realStep / symbols.Length;
                    string guessSymbol = Recognize(GetBitmapBySymbol(symbolIndex, imageNumber));

                    double[] expectedResult = new double[outputLayerSize];
                    for (int i = 0; i < expectedResult.Length; i++)
                    {
                        expectedResult[i] = 0;
                    }
                    expectedResult[symbolIndex] = 1;

                    EducatePart1(expectedResult);

                    for (int k = 0; k < layersErrors.Count; k++)
                    {
                        for (int m = 0; m < layersErrors[k].Length; m++)
                        {
                            layerErrorsList[stepInGroup][k][m] = layersErrors[k][m];
                            layersList[stepInGroup][k][m] = layers[k][m];
                        }
                    }
                }

                // Подсчет среднего
                for (int j = 0; j < layerErrorsList[0].Count; j++)
                {
                    for (int k = 0; k < layerErrorsList[0][j].Length; k++)
                    {
                        averageLayerErrors[j][k] = 0;
                        averageLayers[j][k] = 0;
                        for (int i = 0; i < groupSize; i++)
                        {
                            averageLayerErrors[j][k] += layerErrorsList[i][j][k];
                            averageLayers[j][k] += layersList[i][j][k];
                        }
                        averageLayerErrors[j][k] /= groupSize;
                        averageLayers[j][k] /= groupSize;
                    }
                }

                layersErrors = averageLayerErrors;
                layers = averageLayers;
                EducatePart2();
            }
        }
        
        public async void Serialize(string filename)
        {
            //XmlSerializer xmlSerializer = new XmlSerializer(typeof(NNSerializeConfiguration));
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                NNSerializeConfiguration nconf = new NNSerializeConfiguration
                    (
                        layers.ToArray(),
                        layersErrors.ToArray(),
                        weights.ToArray(),
                        biasweights.ToArray(),
                        symbols,
                        statistics.ToArray()
                    );
                //xmlSerializer.Serialize(fs, nconf);

                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    IncludeFields = true
                };
                await JsonSerializer.SerializeAsync(fs, nconf, options);
            }
        }
        public async static Task<NeuralNetwork> Deserialize(string filename)
        {
            //XmlSerializer xmlSerializer = new XmlSerializer(typeof(NNSerializeConfiguration));
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    IncludeFields = true
                };
                NNSerializeConfiguration? nconf = await JsonSerializer.DeserializeAsync<NNSerializeConfiguration>(fs, options);

                //NNSerializeConfiguration? nconf = xmlSerializer.Deserialize(fs) as NNSerializeConfiguration;

                if (nconf != null)
                    return new NeuralNetwork(nconf);
                else
                    return null;
            }
        }
    }

    public class NNSerializeConfiguration
    {
        public double[][] layers;
        public double[][] layersErrors;
        public double[][][] weights;
        public double[][] biasweights;
        public string[] symbols;
        public Statistic[] statistics;

        public NNSerializeConfiguration(
            double[][] layers, 
            double[][] layersErrors, 
            double[][,] weights,
            double[][] biasweights, 
            string[] symbols, 
            Statistic[] statistics)
        {
            this.layers = layers;
            this.layersErrors = layersErrors;
            this.weights = Extensions.ToJaggedArray(weights);
            this.biasweights = biasweights;
            this.symbols = symbols;
            this.statistics = statistics;
        }

        public NNSerializeConfiguration() { }

    }

}
