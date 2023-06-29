namespace codegenerator
{
    partial class GeneratedCodeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            GeneratedCodeTextBox = new TextBox();
            panel1 = new Panel();
            panel3 = new Panel();
            panel2 = new Panel();
            CopyButton = new Button();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // GeneratedCodeTextBox
            // 
            GeneratedCodeTextBox.BackColor = Color.Black;
            GeneratedCodeTextBox.Font = new Font("Courier New", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            GeneratedCodeTextBox.ForeColor = Color.Lime;
            GeneratedCodeTextBox.Location = new Point(26, 29);
            GeneratedCodeTextBox.Multiline = true;
            GeneratedCodeTextBox.Name = "GeneratedCodeTextBox";
            GeneratedCodeTextBox.Size = new Size(106, 59);
            GeneratedCodeTextBox.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(panel2);
            panel1.Location = new Point(28, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(435, 377);
            panel1.TabIndex = 1;
            // 
            // panel3
            // 
            panel3.Controls.Add(GeneratedCodeTextBox);
            panel3.Location = new Point(20, 61);
            panel3.Name = "panel3";
            panel3.Size = new Size(295, 105);
            panel3.TabIndex = 2;
            // 
            // panel2
            // 
            panel2.Controls.Add(CopyButton);
            panel2.Location = new Point(17, 14);
            panel2.Name = "panel2";
            panel2.Size = new Size(210, 35);
            panel2.TabIndex = 1;
            // 
            // CopyButton
            // 
            CopyButton.Location = new Point(3, 3);
            CopyButton.Name = "CopyButton";
            CopyButton.Size = new Size(63, 23);
            CopyButton.TabIndex = 0;
            CopyButton.Text = "Copy";
            CopyButton.UseVisualStyleBackColor = true;
            CopyButton.Click += CopyButton_Click;
            // 
            // GeneratedCodeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            MinimizeBox = false;
            Name = "GeneratedCodeForm";
            Text = "GeneratedCodeForm";
            Load += GeneratedCodeForm_Load;
            Resize += GeneratedCodeForm_Resize;
            panel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TextBox GeneratedCodeTextBox;
        private Panel panel1;
        private Panel panel2;
        private Button CopyButton;
        private Panel panel3;
    }
}