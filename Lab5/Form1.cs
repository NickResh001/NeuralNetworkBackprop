namespace Lab5
{
    public partial class Form1 : Form
    {
        public DatasetGenerator datasetGenerator;
        public NeuralNetwork neuro;
        public string[] symbols =
            {
                "0",
                "1",
                "2",
                "3",
                "4",
                "5",
                "6",
                "7",
                "8",
                "9"
            };
        public Form1()
        {
            InitializeComponent();

            rtbxResults.Text = string.Empty;
            numDatasetSize.Value = 15000;
            numEducationSteps.Value = 150000;
            numStatStepCount.Value = 100000;

            datasetGenerator = new DatasetGenerator(".\\");
            neuro = new NeuralNetwork([28 * 28, 32, 32, symbols.Length], symbols);

            tbxNnToJson.Text = @"test1.xml";
            tbxJsonToNn.Text = @"test1.xml";

            //{
            //    int[][,] a = new int[2][,];
            //    int counter = 0;
            //    for (int i = 0; i < 2; i++)
            //    {
            //        a[i] = new int[3, 4];
            //        for (int j = 0; j < 3; j++)
            //        {
            //            for (int k = 0; k < 4; k++)
            //            {
            //                a[i][j, k] = counter;
            //                counter++;
            //            }
            //        }
            //    }

            //    int[][][] b = a.ToJaggedArray();
            //}
            //{
            //    Bitmap bitmap = neuro.GetBitmapBySymbol("4", 0);
            //    neuro.BitmapToInputLayer(bitmap);
            //    pictureBox.Image = bitmap;
            //    string output = "";
            //    for (int i = 0; i < 28; i++)
            //    {
            //        for (int j = 0; j < 28; j++)
            //        {
            //            if (neuro.layers[0][i * 28 + j] >= 0.5)
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
            for (int i = 0; i < symbols.Length; i++)
            {
                datasetGenerator.CreateSymbol(symbols[i], (int)numDatasetSize.Value, 28);
            }
        }

        private void btnEducation_Click(object sender, EventArgs e)
        {
            int stepCount = 0;
            if (int.TryParse(numEducationSteps.Value.ToString(), out stepCount))
            {
                neuro.Education(stepCount);
                //neuro.GroupEducation(stepCount, symbols.Length * 10);
            }
        }


        private void btnStatistic_Click(object sender, EventArgs e)
        {
            labelStatResult.Text = neuro.CollectStatistics((int)numStatStepCount.Value).ToString();
        }

        private async void btnJsonToNn_Click(object sender, EventArgs e)
        {
            this.Text = tbxJsonToNn.Text;
            neuro = await NeuralNetwork.Deserialize(tbxJsonToNn.Text) ?? throw new Exception("Name cannot be null");
        }

        private void btnNnToJson_Click(object sender, EventArgs e)
        {
            neuro.Serialize(tbxNnToJson.Text);
            this.Text = tbxNnToJson.Text;
        }
    }
}
