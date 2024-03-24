namespace Lab5
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            rtbxResults = new RichTextBox();
            pictureBox = new PictureBox();
            btnEducation = new Button();
            btnGenerateDataset = new Button();
            numDatasetSize = new NumericUpDown();
            numEducationSteps = new NumericUpDown();
            btnRandomGuess = new Button();
            labelRealValue = new Label();
            labelRecognizedValue = new Label();
            btnRecognize = new Button();
            numRecognitionStepCount = new NumericUpDown();
            labelPercent = new Label();
            btnStatistic = new Button();
            numStatStepCount = new NumericUpDown();
            labelStatResult = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numDatasetSize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numEducationSteps).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numRecognitionStepCount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numStatStepCount).BeginInit();
            SuspendLayout();
            // 
            // rtbxResults
            // 
            rtbxResults.Font = new Font("Consolas", 9F);
            rtbxResults.Location = new Point(289, 12);
            rtbxResults.Name = "rtbxResults";
            rtbxResults.Size = new Size(701, 435);
            rtbxResults.TabIndex = 0;
            rtbxResults.Text = "";
            // 
            // pictureBox
            // 
            pictureBox.Location = new Point(12, 12);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(60, 60);
            pictureBox.TabIndex = 1;
            pictureBox.TabStop = false;
            // 
            // btnEducation
            // 
            btnEducation.Font = new Font("Segoe UI", 14F);
            btnEducation.Location = new Point(545, 453);
            btnEducation.Name = "btnEducation";
            btnEducation.Size = new Size(250, 40);
            btnEducation.TabIndex = 2;
            btnEducation.Text = "Обучение";
            btnEducation.UseVisualStyleBackColor = true;
            btnEducation.Click += btnEducation_Click;
            // 
            // btnGenerateDataset
            // 
            btnGenerateDataset.Font = new Font("Segoe UI", 14F);
            btnGenerateDataset.Location = new Point(545, 499);
            btnGenerateDataset.Name = "btnGenerateDataset";
            btnGenerateDataset.Size = new Size(250, 40);
            btnGenerateDataset.TabIndex = 3;
            btnGenerateDataset.Text = "Сгенерировать датасет";
            btnGenerateDataset.UseVisualStyleBackColor = true;
            btnGenerateDataset.Click += btnGenerateDataset_Click;
            // 
            // numDatasetSize
            // 
            numDatasetSize.Font = new Font("Segoe UI", 14F);
            numDatasetSize.Location = new Point(801, 504);
            numDatasetSize.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numDatasetSize.Name = "numDatasetSize";
            numDatasetSize.Size = new Size(189, 32);
            numDatasetSize.TabIndex = 4;
            // 
            // numEducationSteps
            // 
            numEducationSteps.Font = new Font("Segoe UI", 14F);
            numEducationSteps.Location = new Point(801, 459);
            numEducationSteps.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numEducationSteps.Name = "numEducationSteps";
            numEducationSteps.Size = new Size(189, 32);
            numEducationSteps.TabIndex = 5;
            // 
            // btnRandomGuess
            // 
            btnRandomGuess.Font = new Font("Segoe UI", 14F);
            btnRandomGuess.Location = new Point(12, 169);
            btnRandomGuess.Name = "btnRandomGuess";
            btnRandomGuess.Size = new Size(250, 72);
            btnRandomGuess.TabIndex = 6;
            btnRandomGuess.Text = "Случайное предположение";
            btnRandomGuess.UseVisualStyleBackColor = true;
            btnRandomGuess.Click += btnRandomGuess_Click;
            // 
            // labelRealValue
            // 
            labelRealValue.AutoSize = true;
            labelRealValue.Font = new Font("Segoe UI", 14F);
            labelRealValue.Location = new Point(12, 85);
            labelRealValue.Name = "labelRealValue";
            labelRealValue.Size = new Size(63, 25);
            labelRealValue.TabIndex = 8;
            labelRealValue.Text = "label2";
            // 
            // labelRecognizedValue
            // 
            labelRecognizedValue.AutoSize = true;
            labelRecognizedValue.Font = new Font("Segoe UI", 14F);
            labelRecognizedValue.Location = new Point(12, 124);
            labelRecognizedValue.Name = "labelRecognizedValue";
            labelRecognizedValue.Size = new Size(63, 25);
            labelRecognizedValue.TabIndex = 9;
            labelRecognizedValue.Text = "label2";
            // 
            // btnRecognize
            // 
            btnRecognize.Font = new Font("Segoe UI", 14F);
            btnRecognize.Location = new Point(289, 453);
            btnRecognize.Name = "btnRecognize";
            btnRecognize.Size = new Size(157, 40);
            btnRecognize.TabIndex = 10;
            btnRecognize.Text = "Распознавание";
            btnRecognize.UseVisualStyleBackColor = true;
            btnRecognize.Click += btnRecognize_Click;
            // 
            // numRecognitionStepCount
            // 
            numRecognitionStepCount.Font = new Font("Segoe UI", 14F);
            numRecognitionStepCount.Location = new Point(289, 505);
            numRecognitionStepCount.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numRecognitionStepCount.Name = "numRecognitionStepCount";
            numRecognitionStepCount.Size = new Size(189, 32);
            numRecognitionStepCount.TabIndex = 11;
            // 
            // labelPercent
            // 
            labelPercent.AutoSize = true;
            labelPercent.Font = new Font("Segoe UI", 14F);
            labelPercent.Location = new Point(452, 459);
            labelPercent.Name = "labelPercent";
            labelPercent.Size = new Size(63, 25);
            labelPercent.TabIndex = 12;
            labelPercent.Text = "label2";
            // 
            // btnStatistic
            // 
            btnStatistic.Font = new Font("Segoe UI", 14F);
            btnStatistic.Location = new Point(12, 451);
            btnStatistic.Name = "btnStatistic";
            btnStatistic.Size = new Size(170, 40);
            btnStatistic.TabIndex = 13;
            btnStatistic.Text = "Сбор статистики";
            btnStatistic.UseVisualStyleBackColor = true;
            btnStatistic.Click += btnStatistic_Click;
            // 
            // numStatStepCount
            // 
            numStatStepCount.Font = new Font("Segoe UI", 14F);
            numStatStepCount.Location = new Point(12, 505);
            numStatStepCount.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numStatStepCount.Name = "numStatStepCount";
            numStatStepCount.Size = new Size(189, 32);
            numStatStepCount.TabIndex = 14;
            // 
            // labelStatResult
            // 
            labelStatResult.AutoSize = true;
            labelStatResult.Font = new Font("Segoe UI", 14F);
            labelStatResult.Location = new Point(188, 459);
            labelStatResult.Name = "labelStatResult";
            labelStatResult.Size = new Size(63, 25);
            labelStatResult.TabIndex = 15;
            labelStatResult.Text = "label2";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1002, 548);
            Controls.Add(labelStatResult);
            Controls.Add(numStatStepCount);
            Controls.Add(btnStatistic);
            Controls.Add(labelPercent);
            Controls.Add(numRecognitionStepCount);
            Controls.Add(btnRecognize);
            Controls.Add(labelRecognizedValue);
            Controls.Add(labelRealValue);
            Controls.Add(btnRandomGuess);
            Controls.Add(numEducationSteps);
            Controls.Add(numDatasetSize);
            Controls.Add(btnGenerateDataset);
            Controls.Add(btnEducation);
            Controls.Add(pictureBox);
            Controls.Add(rtbxResults);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)numDatasetSize).EndInit();
            ((System.ComponentModel.ISupportInitialize)numEducationSteps).EndInit();
            ((System.ComponentModel.ISupportInitialize)numRecognitionStepCount).EndInit();
            ((System.ComponentModel.ISupportInitialize)numStatStepCount).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox rtbxResults;
        private PictureBox pictureBox;
        private Button btnEducation;
        private Button btnGenerateDataset;
        private NumericUpDown numDatasetSize;
        private NumericUpDown numEducationSteps;
        private Button btnRandomGuess;
        private Label labelRealValue;
        private Label labelRecognizedValue;
        private Button btnRecognize;
        private NumericUpDown numRecognitionStepCount;
        private Label labelPercent;
        private Button btnStatistic;
        private NumericUpDown numStatStepCount;
        private Label labelStatResult;
    }
}
