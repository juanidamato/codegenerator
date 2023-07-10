namespace codegenerator
{
    partial class MainForm
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
            panel1 = new Panel();
            panel3 = new Panel();
            tablesListBox = new ListBox();
            panel2 = new Panel();
            label1 = new Label();
            panel4 = new Panel();
            label2 = new Label();
            panel5 = new Panel();
            dataGridView1 = new DataGridView();
            panel6 = new Panel();
            panel7 = new Panel();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            AuthorTextBox = new TextBox();
            label7 = new Label();
            GeneralNamespaceTextBox = new TextBox();
            label3 = new Label();
            tabPage2 = new TabPage();
            EntityNameTextBox = new TextBox();
            label6 = new Label();
            GenerateEntityButton = new Button();
            EntityInherithsFromTextBox = new TextBox();
            label5 = new Label();
            EntitiesNamespaceSuffixTextBox = new TextBox();
            label4 = new Label();
            tabPage4 = new TabPage();
            spAuditDeleteButton = new Button();
            spAuditInsertButton = new Button();
            spAuditTableButton = new Button();
            label9 = new Label();
            label8 = new Label();
            spDeleteByPKButton = new Button();
            spUpdateByPKButton = new Button();
            spInsertButton = new Button();
            SelectByPKButton = new Button();
            spSelectAllButton = new Button();
            tabPage3 = new TabPage();
            spAuditUpdateButton = new Button();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel6.SuspendLayout();
            panel7.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage4.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(200, 662);
            panel1.TabIndex = 1;
            // 
            // panel3
            // 
            panel3.Controls.Add(tablesListBox);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 33);
            panel3.Name = "panel3";
            panel3.Size = new Size(200, 629);
            panel3.TabIndex = 3;
            // 
            // tablesListBox
            // 
            tablesListBox.Dock = DockStyle.Fill;
            tablesListBox.FormattingEnabled = true;
            tablesListBox.ItemHeight = 15;
            tablesListBox.Location = new Point(0, 0);
            tablesListBox.Name = "tablesListBox";
            tablesListBox.Size = new Size(200, 629);
            tablesListBox.TabIndex = 0;
            tablesListBox.SelectedIndexChanged += tablesListBox_SelectedIndexChanged;
            // 
            // panel2
            // 
            panel2.Controls.Add(label1);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(200, 33);
            panel2.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(0, 9);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 1;
            label1.Text = "Tables:";
            // 
            // panel4
            // 
            panel4.Controls.Add(label2);
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(200, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(780, 33);
            panel4.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 9);
            label2.Name = "label2";
            label2.Size = new Size(40, 15);
            label2.TabIndex = 2;
            label2.Text = "Fields:";
            // 
            // panel5
            // 
            panel5.Controls.Add(dataGridView1);
            panel5.Location = new Point(3, 3);
            panel5.Name = "panel5";
            panel5.Size = new Size(55, 41);
            panel5.TabIndex = 4;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridView1.Location = new Point(3, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridView1.Size = new Size(34, 25);
            dataGridView1.TabIndex = 3;
            // 
            // panel6
            // 
            panel6.Controls.Add(panel7);
            panel6.Controls.Add(panel5);
            panel6.Location = new Point(206, 39);
            panel6.Name = "panel6";
            panel6.Size = new Size(762, 611);
            panel6.TabIndex = 5;
            // 
            // panel7
            // 
            panel7.Controls.Add(tabControl1);
            panel7.Location = new Point(6, 50);
            panel7.Name = "panel7";
            panel7.Size = new Size(742, 538);
            panel7.TabIndex = 5;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Location = new Point(13, 16);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(642, 371);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(AuthorTextBox);
            tabPage1.Controls.Add(label7);
            tabPage1.Controls.Add(GeneralNamespaceTextBox);
            tabPage1.Controls.Add(label3);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(634, 343);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "General";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // AuthorTextBox
            // 
            AuthorTextBox.Location = new Point(119, 43);
            AuthorTextBox.Name = "AuthorTextBox";
            AuthorTextBox.Size = new Size(413, 23);
            AuthorTextBox.TabIndex = 3;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(8, 43);
            label7.Name = "label7";
            label7.Size = new Size(47, 15);
            label7.TabIndex = 2;
            label7.Text = "Author:";
            // 
            // GeneralNamespaceTextBox
            // 
            GeneralNamespaceTextBox.Location = new Point(119, 14);
            GeneralNamespaceTextBox.Name = "GeneralNamespaceTextBox";
            GeneralNamespaceTextBox.Size = new Size(413, 23);
            GeneralNamespaceTextBox.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(8, 14);
            label3.Name = "label3";
            label3.Size = new Size(72, 15);
            label3.TabIndex = 0;
            label3.Text = "Namespace:";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(EntityNameTextBox);
            tabPage2.Controls.Add(label6);
            tabPage2.Controls.Add(GenerateEntityButton);
            tabPage2.Controls.Add(EntityInherithsFromTextBox);
            tabPage2.Controls.Add(label5);
            tabPage2.Controls.Add(EntitiesNamespaceSuffixTextBox);
            tabPage2.Controls.Add(label4);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(634, 343);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Domain";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // EntityNameTextBox
            // 
            EntityNameTextBox.Location = new Point(217, 42);
            EntityNameTextBox.Name = "EntityNameTextBox";
            EntityNameTextBox.Size = new Size(363, 23);
            EntityNameTextBox.TabIndex = 4;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(18, 42);
            label6.Name = "label6";
            label6.Size = new Size(80, 15);
            label6.TabIndex = 7;
            label6.Text = "Entitity name:";
            // 
            // GenerateEntityButton
            // 
            GenerateEntityButton.Location = new Point(18, 127);
            GenerateEntityButton.Name = "GenerateEntityButton";
            GenerateEntityButton.Size = new Size(72, 24);
            GenerateEntityButton.TabIndex = 6;
            GenerateEntityButton.Text = "Generate";
            GenerateEntityButton.UseVisualStyleBackColor = true;
            GenerateEntityButton.Click += GenerateEntityButton_Click;
            // 
            // EntityInherithsFromTextBox
            // 
            EntityInherithsFromTextBox.Location = new Point(217, 71);
            EntityInherithsFromTextBox.Name = "EntityInherithsFromTextBox";
            EntityInherithsFromTextBox.Size = new Size(363, 23);
            EntityInherithsFromTextBox.TabIndex = 5;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(18, 71);
            label5.Name = "label5";
            label5.Size = new Size(173, 15);
            label5.TabIndex = 4;
            label5.Text = "Entitity inherits from (optional):";
            // 
            // EntitiesNamespaceSuffixTextBox
            // 
            EntitiesNamespaceSuffixTextBox.Location = new Point(217, 13);
            EntitiesNamespaceSuffixTextBox.Name = "EntitiesNamespaceSuffixTextBox";
            EntitiesNamespaceSuffixTextBox.Size = new Size(363, 23);
            EntitiesNamespaceSuffixTextBox.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(18, 13);
            label4.Name = "label4";
            label4.Size = new Size(143, 15);
            label4.TabIndex = 2;
            label4.Text = "Entities namespace suffix:";
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(spAuditUpdateButton);
            tabPage4.Controls.Add(spAuditDeleteButton);
            tabPage4.Controls.Add(spAuditInsertButton);
            tabPage4.Controls.Add(spAuditTableButton);
            tabPage4.Controls.Add(label9);
            tabPage4.Controls.Add(label8);
            tabPage4.Controls.Add(spDeleteByPKButton);
            tabPage4.Controls.Add(spUpdateByPKButton);
            tabPage4.Controls.Add(spInsertButton);
            tabPage4.Controls.Add(SelectByPKButton);
            tabPage4.Controls.Add(spSelectAllButton);
            tabPage4.Location = new Point(4, 24);
            tabPage4.Name = "tabPage4";
            tabPage4.Size = new Size(634, 343);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "Stored Procedures/Audit logs";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // spAuditDeleteButton
            // 
            spAuditDeleteButton.Location = new Point(303, 144);
            spAuditDeleteButton.Name = "spAuditDeleteButton";
            spAuditDeleteButton.Size = new Size(136, 25);
            spAuditDeleteButton.TabIndex = 9;
            spAuditDeleteButton.Text = "Delete audit trigger";
            spAuditDeleteButton.UseVisualStyleBackColor = true;
            spAuditDeleteButton.Click += spAuditDeleteButton_Click;
            // 
            // spAuditInsertButton
            // 
            spAuditInsertButton.Location = new Point(161, 144);
            spAuditInsertButton.Name = "spAuditInsertButton";
            spAuditInsertButton.Size = new Size(136, 25);
            spAuditInsertButton.TabIndex = 8;
            spAuditInsertButton.Text = "Insert audit trigger";
            spAuditInsertButton.UseVisualStyleBackColor = true;
            spAuditInsertButton.Click += spAuditInsertButton_Click;
            // 
            // spAuditTableButton
            // 
            spAuditTableButton.Location = new Point(19, 144);
            spAuditTableButton.Name = "spAuditTableButton";
            spAuditTableButton.Size = new Size(136, 25);
            spAuditTableButton.TabIndex = 7;
            spAuditTableButton.Text = "Audit table";
            spAuditTableButton.UseVisualStyleBackColor = true;
            spAuditTableButton.Click += spAuditTableButton_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(19, 126);
            label9.Name = "label9";
            label9.Size = new Size(36, 15);
            label9.TabIndex = 6;
            label9.Text = "Audit";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(19, 26);
            label8.Name = "label8";
            label8.Size = new Size(38, 15);
            label8.TabIndex = 5;
            label8.Text = "CRUD";
            // 
            // spDeleteByPKButton
            // 
            spDeleteByPKButton.Location = new Point(303, 44);
            spDeleteByPKButton.Name = "spDeleteByPKButton";
            spDeleteByPKButton.Size = new Size(136, 25);
            spDeleteByPKButton.TabIndex = 4;
            spDeleteByPKButton.Text = "Delete by PK";
            spDeleteByPKButton.UseVisualStyleBackColor = true;
            spDeleteByPKButton.Click += spDeleteByPKButton_Click;
            // 
            // spUpdateByPKButton
            // 
            spUpdateByPKButton.Location = new Point(161, 75);
            spUpdateByPKButton.Name = "spUpdateByPKButton";
            spUpdateByPKButton.Size = new Size(136, 25);
            spUpdateByPKButton.TabIndex = 3;
            spUpdateByPKButton.Text = "Update by PK";
            spUpdateByPKButton.UseVisualStyleBackColor = true;
            spUpdateByPKButton.Click += spUpdateByPKButton_Click;
            // 
            // spInsertButton
            // 
            spInsertButton.Location = new Point(19, 75);
            spInsertButton.Name = "spInsertButton";
            spInsertButton.Size = new Size(136, 25);
            spInsertButton.TabIndex = 2;
            spInsertButton.Text = "Insert";
            spInsertButton.UseVisualStyleBackColor = true;
            spInsertButton.Click += spInsertButton_Click;
            // 
            // SelectByPKButton
            // 
            SelectByPKButton.Location = new Point(161, 44);
            SelectByPKButton.Name = "SelectByPKButton";
            SelectByPKButton.Size = new Size(136, 25);
            SelectByPKButton.TabIndex = 1;
            SelectByPKButton.Text = "Select by PK";
            SelectByPKButton.UseVisualStyleBackColor = true;
            SelectByPKButton.Click += spSelectByPKButton_Click;
            // 
            // spSelectAllButton
            // 
            spSelectAllButton.Location = new Point(19, 44);
            spSelectAllButton.Name = "spSelectAllButton";
            spSelectAllButton.Size = new Size(136, 25);
            spSelectAllButton.TabIndex = 0;
            spSelectAllButton.Text = "Select All";
            spSelectAllButton.UseVisualStyleBackColor = true;
            spSelectAllButton.Click += spSelectAllButton_Click;
            // 
            // tabPage3
            // 
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(634, 343);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Features";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // spAuditUpdateButton
            // 
            spAuditUpdateButton.Location = new Point(445, 144);
            spAuditUpdateButton.Name = "spAuditUpdateButton";
            spAuditUpdateButton.Size = new Size(136, 25);
            spAuditUpdateButton.TabIndex = 10;
            spAuditUpdateButton.Text = "Update audit trigger";
            spAuditUpdateButton.UseVisualStyleBackColor = true;
            spAuditUpdateButton.Click += spAuditUpdateButton_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(980, 662);
            Controls.Add(panel6);
            Controls.Add(panel4);
            Controls.Add(panel1);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Code Generator";
            Load += MainForm_Load;
            panel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel6.ResumeLayout(false);
            panel7.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            tabPage4.ResumeLayout(false);
            tabPage4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Panel panel1;
        private ListBox tablesListBox;
        private Panel panel2;
        private Label label1;
        private Panel panel3;
        private Panel panel4;
        private Label label2;
        private Panel panel5;
        private DataGridView dataGridView1;
        private Panel panel6;
        private Panel panel7;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TextBox GeneralNamespaceTextBox;
        private Label label3;
        private TabPage tabPage3;
        private TextBox EntitiesNamespaceSuffixTextBox;
        private Label label4;
        private TextBox EntityInherithsFromTextBox;
        private Label label5;
        private Button GenerateEntityButton;
        private TextBox EntityNameTextBox;
        private Label label6;
        private TextBox AuthorTextBox;
        private Label label7;
        private TabPage tabPage4;
        private Button spSelectAllButton;
        private Button SelectByPKButton;
        private Button spInsertButton;
        private Button spDeleteByPKButton;
        private Button spUpdateByPKButton;
        private Label label9;
        private Label label8;
        private Button spAuditTableButton;
        private Button spAuditInsertButton;
        private Button spAuditDeleteButton;
        private Button spAuditUpdateButton;
    }
}