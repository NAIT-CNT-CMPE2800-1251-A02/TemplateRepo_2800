namespace LAB_FINAL_01
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
            lblHandCount = new Label();
            numHandCount = new NumericUpDown();
            btnStart = new Button();
            lblStatus = new Label();
            lblInterval = new Label();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)numHandCount).BeginInit();
            SuspendLayout();
            // 
            // lblHandCount
            // 
            lblHandCount.AutoSize = true;
            lblHandCount.Location = new Point(14, 13);
            lblHandCount.Name = "lblHandCount";
            lblHandCount.Size = new Size(130, 20);
            lblHandCount.TabIndex = 0;
            lblHandCount.Text = "Number of Hands:";
            // 
            // numHandCount
            // 
            numHandCount.Location = new Point(150, 12);
            numHandCount.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            numHandCount.Name = "numHandCount";
            numHandCount.Size = new Size(150, 27);
            numHandCount.TabIndex = 1;
            numHandCount.Value = new decimal(new int[] { 20, 0, 0, 0 });
            // 
            // btnStart
            // 
            btnStart.Location = new Point(12, 45);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(144, 40);
            btnStart.TabIndex = 2;
            btnStart.Text = "Start Simulation";
            btnStart.UseVisualStyleBackColor = true;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(189, 66);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(101, 20);
            lblStatus.TabIndex = 3;
            lblStatus.Text = "Ready to start";
            // 
            // lblInterval
            // 
            lblInterval.AutoSize = true;
            lblInterval.Location = new Point(327, 19);
            lblInterval.Name = "lblInterval";
            lblInterval.Size = new Size(58, 20);
            lblInterval.TabIndex = 4;
            lblInterval.Text = "Interval";
            // 
            // button1
            // 
            button1.Location = new Point(432, 66);
            button1.Name = "button1";
            button1.Size = new Size(93, 29);
            button1.TabIndex = 5;
            button1.Text = "EXIT";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(615, 119);
            Controls.Add(button1);
            Controls.Add(lblInterval);
            Controls.Add(lblStatus);
            Controls.Add(btnStart);
            Controls.Add(numHandCount);
            Controls.Add(lblHandCount);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)numHandCount).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblHandCount;
        private NumericUpDown numHandCount;
        private Button btnStart;
        private Label lblStatus;
        private Label lblInterval;
        private Button button1;
    }
}
