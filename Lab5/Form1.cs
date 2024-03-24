namespace Lab5
{
    public partial class Form1 : Form
    {
        public DatasetGenerator datasetGenerator;
        public NeuralNetwork neuro;
        public Form1()
        {
            InitializeComponent();

            rtbxResults.Text = string.Empty;
            numDatasetSize.Value = 5000;
            numEducationSteps.Value = 1000;
            numStatStepCount.Value = 1000;

            datasetGenerator = new DatasetGenerator(".\\");
            neuro = new NeuralNetwork([28 * 28, 16, 16, 10]);

            //{
            //    Bitmap bitmap = neuro.GetBitmapBySymbol("2", 1);
            //    neuro.BitmapToInputLayer(bitmap);
            //    pictureBox.Image = bitmap;
            //    string output = "";
            //    for (int i = 0; i < 60; i++)
            //    {
            //        for (int j = 0; j < 60; j++)
            //        {
            //            if (neuro.layers[0][i * 60 + j] >= 0.5)
            //                output += "0";
            //            else
            //                output += "-";
            //        }
            //        output += "\n";
            //    }
            //    rtbxResults.Text = output;
            //}

        }

        private void btnGenerateDataset_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                datasetGenerator.CreateSymbol(i.ToString(), (int)numDatasetSize.Value, 28);
            }
        }

        private void btnEducation_Click(object sender, EventArgs e)
        {
            int stepCount = 0;
            if (int.TryParse(numEducationSteps.Value.ToString(), out stepCount))
                neuro.Education(stepCount);
        }

        private void btnRandomGuess_Click(object sender, EventArgs e)
        {

        }

        private void btnRecognize_Click(object sender, EventArgs e)
        {
            int rightAnswersCount = 0;
            int stepCount = (int)numRecognitionStepCount.Value;
            for (int i = 0; i < stepCount; i++)
            {
                int step = i / 10;
                int symbol = i % 10;
                int guess = neuro.Recognize(neuro.GetBitmapBySymbol(symbol.ToString(), step));
                if (guess == symbol)
                    rightAnswersCount++;
            }
            labelPercent.Text = "";
            labelPercent.Text += ((double)rightAnswersCount / stepCount * 100).ToString();
            labelPercent.Text += " %";
        }

        private void btnStatistic_Click(object sender, EventArgs e)
        {
            labelStatResult.Text = neuro.CollectStatistics((int)numStatStepCount.Value).ToString();
        }
    }
}
