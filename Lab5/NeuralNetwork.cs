using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    public class Statistic
    {
        public int allAnswers = 0;
        public int rightAnswers = 0;
        public List<double> rootMeanSquareErrors = [];
        public double percentage => (double)rightAnswers / (double)allAnswers;
    }

    public class NeuralNetwork
    {
        readonly private Random _random = new Random();
        public List<double[]> layers;
        private List<double[]> layersErrors;
        private List<double[,]> weights;

        private double educationWeight = 0.1;

        public List<Statistic> statistics;
        public NeuralNetwork(List<int> neuronsCounts)
        {
            layers = [];
            layersErrors = [];
            weights = [];
            statistics = [];

            if (neuronsCounts != null && neuronsCounts.Count > 2)
            {
                layers.Add(new double[neuronsCounts[0]]);
                layersErrors.Add(new double[neuronsCounts[0]]);
                for (int i = 1; i < neuronsCounts.Count; i++)
                {
                    double[] layer = new double[neuronsCounts[i]];
                    for (int j = 0; j < layer.Length; j++)
                    {
                        layer[j] = 0;
                    }
                    layers.Add(layer);
                    layersErrors.Add(layer);

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
        public Bitmap GetBitmapBySymbol(string symbol, int step)
        {
            string strStep = $"{step:D5}";
            return new Bitmap(Image.FromFile($".\\{symbol}\\{strStep}.png"));
        }
        public int Recognize(Bitmap bitmap)
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
                    double sum = 0;
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
            return maxIndex;
        }
        public double CollectStatistics(int stepCount)
        {
            Statistic stat = new Statistic();
            for (int step = 0; step < stepCount; step++)
            {
                int outputLayerSize = layers.Last().Length;
                int realSymbol = step % outputLayerSize;
                int imageNumber = step / outputLayerSize;
                int guessSymbol = Recognize(GetBitmapBySymbol(realSymbol.ToString(), imageNumber));

                double[] expectedResult = new double[outputLayerSize];
                for (int i = 0; i < expectedResult.Length; i++)
                {
                    expectedResult[i] = 0;
                }
                expectedResult[realSymbol] = 1;

                stat.allAnswers++;
                if (realSymbol == guessSymbol)
                    stat.rightAnswers++;

                double error = 0;
                for (int i = 0; i < layers.Last().Length; i++)
                {
                    error += Math.Pow(expectedResult[i] - layers.Last()[i], 2);
                }
                error *= 0.5;
                stat.rootMeanSquareErrors.Add(error);
            }
            statistics.Add(stat);
            return stat.percentage;
        }

        private void Educate(double[] expectedOutput)
        {
            // Подсчет среднеквадратичной ошибки
            double error = 0;
            for (int i = 0; i < layers.Last().Length; i++)
            {
                error += Math.Pow(expectedOutput[i] - layers.Last()[i], 2);
            }
            error /= 2;

            // Подсчет ошибки выходного слоя
            for (int i = 0; i < layers.Last().Length; i++)
            {
                layersErrors.Last()[i] = (expectedOutput[i] - layers.Last()[i]) * layers.Last()[i] * (1 - layers.Last()[i]);
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
                    layersErrors[i][j] = SigmaFunction(sum * layers[i][j] * (1 - layers[i][j]));
                }
            }

            // Пересчет всех весов
            for (int i = 0; i < weights.Count; i++)
            {
                // Пересчет весов одного слоя
                for (int j = 0; j < layers[i].Length; j++)
                {
                    // Пересчет весов от одного нейрона
                    for (int k = 0; k < layers[i + 1].Length; k++)
                    {
                        weights[i][j, k] += educationWeight * layersErrors[i + 1][k] * layers[i][j];
                    }
                }
            }
        }
        public void Education(int stepCount)
        {
            for (int step = 0; step < stepCount; step++)
            {
                int outputLayerSize = layers.Last().Length;
                int realSymbol = step % outputLayerSize;
                int imageNumber = step / outputLayerSize;
                int guessSymbol = Recognize(GetBitmapBySymbol(realSymbol.ToString(), imageNumber));

                double[] expectedResult = new double[outputLayerSize];
                for (int i = 0; i < expectedResult.Length; i++)
                {
                    expectedResult[i] = 0;
                }
                expectedResult[realSymbol] = 1;

                if (realSymbol != guessSymbol)
                {
                    Educate(expectedResult);
                }
            }
        }

    }
}
