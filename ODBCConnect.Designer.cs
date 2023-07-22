namespace codegenerator
{
    partial class ODBCConnect
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
            ConnectionStringLabel = new Label();
            ConnectionStringTextBox = new TextBox();
            PreserveConnectionStringCheckBox = new CheckBox();
            AcceptButton = new Button();
            CancelButton = new Button();
            SuspendLayout();
            // 
            // ConnectionStringLabel
            // 
            ConnectionStringLabel.AutoSize = true;
            ConnectionStringLabel.Location = new Point(32, 44);
            ConnectionStringLabel.Name = "ConnectionStringLabel";
            ConnectionStringLabel.Size = new Size(105, 15);
            ConnectionStringLabel.TabIndex = 0;
            ConnectionStringLabel.Text = "Connection string:";
            // 
            // ConnectionStringTextBox
            // 
            ConnectionStringTextBox.Location = new Point(150, 44);
            ConnectionStringTextBox.Margin = new Padding(3, 2, 3, 2);
            ConnectionStringTextBox.Name = "ConnectionStringTextBox";
            ConnectionStringTextBox.PasswordChar = '*';
            ConnectionStringTextBox.Size = new Size(484, 23);
            ConnectionStringTextBox.TabIndex = 1;
            // 
            // PreserveConnectionStringCheckBox
            // 
            PreserveConnectionStringCheckBox.AutoSize = true;
            PreserveConnectionStringCheckBox.Location = new Point(42, 95);
            PreserveConnectionStringCheckBox.Margin = new Padding(3, 2, 3, 2);
            PreserveConnectionStringCheckBox.Name = "PreserveConnectionStringCheckBox";
            PreserveConnectionStringCheckBox.Size = new Size(508, 19);
            PreserveConnectionStringCheckBox.TabIndex = 2;
            PreserveConnectionStringCheckBox.Text = "Preserve connection string (warning: connection string will be saved in \"config.txt\" text file)";
            PreserveConnectionStringCheckBox.UseVisualStyleBackColor = true;
            // 
            // AcceptButton
            // 
            AcceptButton.Location = new Point(244, 129);
            AcceptButton.Margin = new Padding(3, 2, 3, 2);
            AcceptButton.Name = "AcceptButton";
            AcceptButton.Size = new Size(95, 28);
            AcceptButton.TabIndex = 3;
            AcceptButton.Text = "Accept";
            AcceptButton.UseVisualStyleBackColor = true;
            AcceptButton.Click += AcceptButton_Click;
            // 
            // CancelButton
            // 
            CancelButton.Location = new Point(345, 129);
            CancelButton.Margin = new Padding(3, 2, 3, 2);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new Size(95, 28);
            CancelButton.TabIndex = 4;
            CancelButton.Text = "Cancel";
            CancelButton.UseVisualStyleBackColor = true;
            CancelButton.Click += CancelButton_Click;
            // 
            // ODBCConnect
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 177);
            ControlBox = false;
            Controls.Add(CancelButton);
            Controls.Add(AcceptButton);
            Controls.Add(PreserveConnectionStringCheckBox);
            Controls.Add(ConnectionStringTextBox);
            Controls.Add(ConnectionStringLabel);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ODBCConnect";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Connection";
            Load += ODBCConnect_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label ConnectionStringLabel;
        private TextBox ConnectionStringTextBox;
        private CheckBox PreserveConnectionStringCheckBox;
        private new Button AcceptButton;
        private new Button CancelButton;
    }
}