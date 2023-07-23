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
            webView2 = new Microsoft.Web.WebView2.WinForms.WebView2();
            panel2 = new Panel();
            CopyButton = new Button();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webView2).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // GeneratedCodeTextBox
            // 
            GeneratedCodeTextBox.BackColor = Color.White;
            GeneratedCodeTextBox.Font = new Font("Consolas", 12F, FontStyle.Bold, GraphicsUnit.Point);
            GeneratedCodeTextBox.ForeColor = SystemColors.Highlight;
            GeneratedCodeTextBox.Location = new Point(26, 29);
            GeneratedCodeTextBox.Multiline = true;
            GeneratedCodeTextBox.Name = "GeneratedCodeTextBox";
            GeneratedCodeTextBox.ScrollBars = ScrollBars.Both;
            GeneratedCodeTextBox.Size = new Size(106, 59);
            GeneratedCodeTextBox.TabIndex = 0;
            GeneratedCodeTextBox.Visible = false;
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
            panel3.Controls.Add(webView2);
            panel3.Controls.Add(GeneratedCodeTextBox);
            panel3.Location = new Point(17, 61);
            panel3.Name = "panel3";
            panel3.Size = new Size(373, 188);
            panel3.TabIndex = 2;
            // 
            // webView2
            // 
            webView2.AllowExternalDrop = true;
            webView2.CreationProperties = null;
            webView2.DefaultBackgroundColor = Color.White;
            webView2.Location = new Point(138, 29);
            webView2.Name = "webView2";
            webView2.Size = new Size(177, 117);
            webView2.TabIndex = 3;
            webView2.ZoomFactor = 1D;
            // 
            // panel2
            // 
            panel2.Controls.Add(CopyButton);
            panel2.Location = new Point(17, 14);
            panel2.Name = "panel2";
            panel2.Size = new Size(116, 31);
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
            WindowState = FormWindowState.Maximized;
            Load += GeneratedCodeForm_Load;
            Resize += GeneratedCodeForm_Resize;
            panel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)webView2).EndInit();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TextBox GeneratedCodeTextBox;
        private Panel panel1;
        private Panel panel2;
        private Button CopyButton;
        private Panel panel3;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView2;
    }
}