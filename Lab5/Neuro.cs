using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Printing;

namespace Lab5
{
    public class Neuro
    {
        private Random _random = new Random();

        private int imageSize;              // размерность изображения
        private int inputLayerSize;
        private int hiddenLayerSize;
        private int outputLayerSize;
        private double educationWeight;     // вес обучения

        private double[] inputLayer;
        private double[,] weightsInputHidden;
        private double[] hiddenLayer;
        private double[,] weightsHiddenOutput;
        private double[] outputLayer;

        private double[] hiddenLayerErrors;
        private double[] outputLayerErrors;

        public List<double> averageErrors;

        public Neuro()
        {
            imageSize = 60;
            inputLayerSize = imageSize * imageSize;
            hiddenLayerSize = 60;
            outputLayerSize = 10;
            educationWeight = 0.5;
            Initialize();
        }
        private void Initialize()
        {
            inputLayer = new double[inputLayerSize];
            weightsInputHidden = new double[inputLayerSize, hiddenLayerSize];
            hiddenLayer = new double[hiddenLayerSize];
            weightsHiddenOutput = new double[hiddenLayerSize, outputLayerSize];
            outputLayer = new double[outputLayerSize];

            hiddenLayerErrors = new double[hiddenLayerSize];
            outputLayerErrors = new double[outputLayerSize];

            averageErrors = new List<double>();

            for (int j = 0; j < hiddenLayerSize; j++)
            {
                for (int i = 0; i < inputLayerSize; i++)
                {
                    weightsInputHidden[i, j] = _random.NextDouble();
                }
                for (int k = 0; k < outputLayerSize; k++)
                {
                    weightsHiddenOutput[j, k] = _random.NextDouble();
                }
            }
        }
        private void BitmapToInputLayer(Bitmap bitmap)
        {
            for (int i = 0; i < imageSize; i++)
            {
                for (int j = 0; j < imageSize; j++)
                {
                    //bool enough = true;
                    //enough &= bitmap.GetPixel(i, j).R < 128;
                    //enough &= bitmap.GetPixel(i, j).G < 128;
                    //enough &= bitmap.GetPixel(i, j).B < 128;
                    //if (enough)
                    //    receptorLayer[i, j] = 1;
                    //else
                    //    receptorLayer[i, j] = 0;

                    inputLayer[i * imageSize + j] = (bitmap.GetPixel(i, j).R + bitmap.GetPixel(i, j).G + bitmap.GetPixel(i, j).B) / (255 * 3);
                }
            }
        }
        private double SigmaFunction(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }
        public Bitmap GetBitmapBySymbol(string symbol, int step)
        {
            string strStep = $"{step:D5}";
            return new Bitmap(Image.FromFile($".\\{symbol}\\{strStep}.png"));
        }
        public int Recognize(Bitmap bitmap)
        {
            // Определим содержимое для inputLayer
            BitmapToInputLayer(bitmap);

            // Определим содержимое для hiddenLayer
            for (int i = 0; i < hiddenLayer.Length; i++)
            {
                double sum = 0;
                for (int j = 0; j < inputLayer.Length; j++)
                {
                    sum += inputLayer[j] * weightsInputHidden[j, i];
                }
                hiddenLayer[i] = SigmaFunction(sum);
            }

            // Определим содержимое для outputLayer
            for (int i = 0; i < outputLayer.Length; i++)
            {
                double sum = 0;
                for (int j = 0; j < hiddenLayer.Length; j++)
                {
                    sum += hiddenLayer[j] * weightsHiddenOutput[j, i];
                }
                outputLayer[i] = SigmaFunction(sum);
            }

            // Ищем выходной нейрон с максимальной активацией
            int answer = 0;
            double maxValue = double.MinValue;
            for (int i = 0; i < outputLayer.Length; i++)
            {
                if (maxValue < outputLayer[i])
                {
                    maxValue = outputLayer[i];
                    answer = i;
                }
            }

            return answer;
        }

        private void Educate(double[] expectedOutputs)
        {
            // Подсчет среднеквадратичной ошибки
            double error = 0;
            for (int i = 0; i < outputLayer.Length; i++)
            {
                error += Math.Pow(expectedOutputs[i] - outputLayer[i], 2);
            }
            error *= 0.5;
            averageErrors.Add(error);

            // // Рекурсивный подсчет ошибок на нейронах
            // Подсчет ошибок на выходных нейронах
            for (int i = 0; i < outputLayer.Length; i++)
            {
                outputLayerErrors[i] = (expectedOutputs[i] - outputLayer[i]) * outputLayer[i] * (1 - outputLayer[i]);
            }

            // Подсчет ошибок на скрытых нейронах
            for (int i = 0; i < hiddenLayer.Length; i++)
            {
                double sum = 0;
                for (int j = 0; j < outputLayer.Length; j++)
                {
                    sum += outputLayerErrors[j] * weightsHiddenOutput[i, j];
                }
                hiddenLayerErrors[i] = sum * hiddenLayer[i] * (1 - hiddenLayer[i]);
            }

            // // Коррекция весов
            // Коррекция весов между скрытым и выходным слоем
            for (int i = 0; i < hiddenLayer.Length; i++)
            {
                for (int j = 0; j < outputLayer.Length; j++)
                {
                    weightsHiddenOutput[i, j] += educationWeight * outputLayerErrors[j] * hiddenLayer[i];
                }
            }

            // Коррекция весов между входным и скрытым слоем
            for (int i = 0; i < inputLayer.Length; i++)
            {
                for (int j = 0; j < hiddenLayer.Length; j++)
                {
                    weightsInputHidden[i, j] += educationWeight * hiddenLayerErrors[j] * inputLayer[i];
                }
            }
        }
        public void Education(int stepCount)
        {
            for (int step = 0; step < stepCount; step++)
            {
                int realSymbol = step % outputLayerSize;
                int guessSymbol = Recognize(GetBitmapBySymbol(realSymbol.ToString(), step / outputLayerSize));

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
