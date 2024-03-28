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
            btnEducation = new Button();
            btnGenerateDataset = new Button();
            numDatasetSize = new NumericUpDown();
            numEducationSteps = new NumericUpDown();
            btnStatistic = new Button();
            numStatStepCount = new NumericUpDown();
            labelStatResult = new Label();
            btnNnToJson = new Button();
            btnJsonToNn = new Button();
            tbxJsonToNn = new TextBox();
            tbxNnToJson = new TextBox();
            ((System.ComponentModel.ISupportInitialize)numDatasetSize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numEducationSteps).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numStatStepCount).BeginInit();
            SuspendLayout();
            // 
            // rtbxResults
            // 
            rtbxResults.Font = new Font("Consolas", 9F);
            rtbxResults.Location = new Point(801, 12);
            rtbxResults.Name = "rtbxResults";
            rtbxResults.Size = new Size(189, 524);
            rtbxResults.TabIndex = 0;
            rtbxResults.Text = "";
            // 
            // btnEducation
            // 
            btnEducation.Font = new Font("Segoe UI", 14F);
            btnEducation.Location = new Point(572, 12);
            btnEducation.Name = "btnEducation";
            btnEducation.Size = new Size(223, 40);
            btnEducation.TabIndex = 2;
            btnEducation.Text = "Обучение";
            btnEducation.UseVisualStyleBackColor = true;
            btnEducation.Click += btnEducation_Click;
            // 
            // btnGenerateDataset
            // 
            btnGenerateDataset.Font = new Font("Segoe UI", 14F);
            btnGenerateDataset.Location = new Point(572, 124);
            btnGenerateDataset.Name = "btnGenerateDataset";
            btnGenerateDataset.Size = new Size(223, 40);
            btnGenerateDataset.TabIndex = 3;
            btnGenerateDataset.Text = "Сгенерировать датасет";
            btnGenerateDataset.UseVisualStyleBackColor = true;
            btnGenerateDataset.Click += btnGenerateDataset_Click;
            // 
            // numDatasetSize
            // 
            numDatasetSize.Font = new Font("Segoe UI", 14F);
            numDatasetSize.Location = new Point(572, 170);
            numDatasetSize.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numDatasetSize.Name = "numDatasetSize";
            numDatasetSize.Size = new Size(223, 32);
            numDatasetSize.TabIndex = 4;
            // 
            // numEducationSteps
            // 
            numEducationSteps.Font = new Font("Segoe UI", 14F);
            numEducationSteps.Location = new Point(572, 58);
            numEducationSteps.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numEducationSteps.Name = "numEducationSteps";
            numEducationSteps.Size = new Size(223, 32);
            numEducationSteps.TabIndex = 5;
            // 
            // btnStatistic
            // 
            btnStatistic.Font = new Font("Segoe UI", 14F);
            btnStatistic.Location = new Point(572, 232);
            btnStatistic.Name = "btnStatistic";
            btnStatistic.Size = new Size(223, 40);
            btnStatistic.TabIndex = 13;
            btnStatistic.Text = "Сбор статистики";
            btnStatistic.UseVisualStyleBackColor = true;
            btnStatistic.Click += btnStatistic_Click;
            // 
            // numStatStepCount
            // 
            numStatStepCount.Font = new Font("Segoe UI", 14F);
            numStatStepCount.Location = new Point(572, 278);
            numStatStepCount.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numStatStepCount.Name = "numStatStepCount";
            numStatStepCount.Size = new Size(223, 32);
            numStatStepCount.TabIndex = 14;
            // 
            // labelStatResult
            // 
            labelStatResult.AutoSize = true;
            labelStatResult.Font = new Font("Segoe UI", 14F);
            labelStatResult.Location = new Point(572, 313);
            labelStatResult.Name = "labelStatResult";
            labelStatResult.Size = new Size(63, 25);
            labelStatResult.TabIndex = 15;
            labelStatResult.Text = "label2";
            // 
            // btnNnToJson
            // 
            btnNnToJson.Font = new Font("Segoe UI", 14F);
            btnNnToJson.Location = new Point(12, 496);
            btnNnToJson.Name = "btnNnToJson";
            btnNnToJson.Size = new Size(223, 40);
            btnNnToJson.TabIndex = 16;
            btnNnToJson.Text = "Записать в файл";
            btnNnToJson.UseVisualStyleBackColor = true;
            btnNnToJson.Click += btnNnToJson_Click;
            // 
            // btnJsonToNn
            // 
            btnJsonToNn.Font = new Font("Segoe UI", 14F);
            btnJsonToNn.Location = new Point(12, 450);
            btnJsonToNn.Name = "btnJsonToNn";
            btnJsonToNn.Size = new Size(223, 40);
            btnJsonToNn.TabIndex = 17;
            btnJsonToNn.Text = "Считать из файла";
            btnJsonToNn.UseVisualStyleBackColor = true;
            btnJsonToNn.Click += btnJsonToNn_Click;
            // 
            // tbxJsonToNn
            // 
            tbxJsonToNn.Font = new Font("Segoe UI", 14F);
            tbxJsonToNn.Location = new Point(241, 455);
            tbxJsonToNn.Name = "tbxJsonToNn";
            tbxJsonToNn.Size = new Size(212, 32);
            tbxJsonToNn.TabIndex = 18;
            // 
            // tbxNnToJson
            // 
            tbxNnToJson.Font = new Font("Segoe UI", 14F);
            tbxNnToJson.Location = new Point(241, 501);
            tbxNnToJson.Name = "tbxNnToJson";
            tbxNnToJson.Size = new Size(212, 32);
            tbxNnToJson.TabIndex = 19;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1002, 548);
            Controls.Add(tbxNnToJson);
            Controls.Add(tbxJsonToNn);
            Controls.Add(btnJsonToNn);
            Controls.Add(btnNnToJson);
            Controls.Add(labelStatResult);
            Controls.Add(numStatStepCount);
            Controls.Add(btnStatistic);
            Controls.Add(numEducationSteps);
            Controls.Add(numDatasetSize);
            Controls.Add(btnGenerateDataset);
            Controls.Add(btnEducation);
            Controls.Add(rtbxResults);
            Name = "Form1";
            Text = "Form1s";
            ((System.ComponentModel.ISupportInitialize)numDatasetSize).EndInit();
            ((System.ComponentModel.ISupportInitialize)numEducationSteps).EndInit();
            ((System.ComponentModel.ISupportInitialize)numStatStepCount).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox rtbxResults;
        private Button btnEducation;
        private Button btnGenerateDataset;
        private NumericUpDown numDatasetSize;
        private NumericUpDown numEducationSteps;
        private Button btnStatistic;
        private NumericUpDown numStatStepCount;
        private Label labelStatResult;
        private Button btnNnToJson;
        private Button btnJsonToNn;
        private TextBox tbxJsonToNn;
        private TextBox tbxNnToJson;
    }
}
