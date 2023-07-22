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
            tabPage4 = new TabPage();
            groupBox6 = new GroupBox();
            spAuditUpdateButton = new Button();
            spAuditDeleteButton = new Button();
            spAuditInsertButton = new Button();
            spAuditTableButton = new Button();
            groupBox5 = new GroupBox();
            spDeleteByPKButton = new Button();
            spUpdateByPKButton = new Button();
            spInsertButton = new Button();
            SelectByPKButton = new Button();
            spSelectAllButton = new Button();
            tabPage2 = new TabPage();
            groupBox3 = new GroupBox();
            DomainCommonsOperationStatusModelButton = new Button();
            DomainCommonsOperationResultModelButton = new Button();
            groupBox2 = new GroupBox();
            DomainEnumsOperationResultCodesButton = new Button();
            groupBox1 = new GroupBox();
            DomainGenerateEntityButton = new Button();
            DomainEntityNameTextBox = new TextBox();
            label6 = new Label();
            EntityInherithsFromTextBox = new TextBox();
            label5 = new Label();
            EntitiesNamespaceSuffixTextBox = new TextBox();
            label4 = new Label();
            DomainNamespaceTextBox = new TextBox();
            label3 = new Label();
            tabPage3 = new TabPage();
            groupBox7 = new GroupBox();
            ApplicationManagerInterfaceButton = new Button();
            ApplicationRepositoryInterfaceButton = new Button();
            ApplicationEntityNameTextBox = new TextBox();
            label8 = new Label();
            groupBox4 = new GroupBox();
            ApplicationCommonsInterfacesUtilsReverseHashButton = new Button();
            button4 = new Button();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            ApplicationCommonsInterfacesInfrastructureDatabaseHelperButton = new Button();
            ApplicatioConfigureServicesButton = new Button();
            ApplicationCommonsAttributeTraceAndTimeInterceptorButton = new Button();
            ApplicationCommonsAttributeTraceAndTimeButton = new Button();
            ApplicationGlobalVariablesBLLAndConstantsButton = new Button();
            ApplicationNamespaceTextBox = new TextBox();
            label12 = new Label();
            tabPage5 = new TabPage();
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
            tabPage4.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox5.SuspendLayout();
            tabPage2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            tabPage3.SuspendLayout();
            groupBox7.SuspendLayout();
            groupBox4.SuspendLayout();
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
            panel4.Size = new Size(873, 33);
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
            panel6.Size = new Size(855, 611);
            panel6.TabIndex = 5;
            // 
            // panel7
            // 
            panel7.Controls.Add(tabControl1);
            panel7.Location = new Point(6, 50);
            panel7.Name = "panel7";
            panel7.Size = new Size(833, 538);
            panel7.TabIndex = 5;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage5);
            tabControl1.Location = new Point(3, 3);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(827, 479);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(AuthorTextBox);
            tabPage1.Controls.Add(label7);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(819, 451);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "General";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // AuthorTextBox
            // 
            AuthorTextBox.Location = new Point(70, 17);
            AuthorTextBox.Name = "AuthorTextBox";
            AuthorTextBox.Size = new Size(413, 23);
            AuthorTextBox.TabIndex = 3;
            AuthorTextBox.TextChanged += AuthorTextBox_TextChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(17, 17);
            label7.Name = "label7";
            label7.Size = new Size(47, 15);
            label7.TabIndex = 2;
            label7.Text = "Author:";
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(groupBox6);
            tabPage4.Controls.Add(groupBox5);
            tabPage4.Location = new Point(4, 24);
            tabPage4.Name = "tabPage4";
            tabPage4.Size = new Size(819, 451);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "Stored Procedures/Audit logs";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(spAuditUpdateButton);
            groupBox6.Controls.Add(spAuditDeleteButton);
            groupBox6.Controls.Add(spAuditInsertButton);
            groupBox6.Controls.Add(spAuditTableButton);
            groupBox6.Location = new Point(19, 114);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(732, 90);
            groupBox6.TabIndex = 12;
            groupBox6.TabStop = false;
            groupBox6.Text = "Audit";
            // 
            // spAuditUpdateButton
            // 
            spAuditUpdateButton.Location = new Point(451, 32);
            spAuditUpdateButton.Name = "spAuditUpdateButton";
            spAuditUpdateButton.Size = new Size(136, 25);
            spAuditUpdateButton.TabIndex = 14;
            spAuditUpdateButton.Text = "Update audit trigger";
            spAuditUpdateButton.UseVisualStyleBackColor = true;
            // 
            // spAuditDeleteButton
            // 
            spAuditDeleteButton.Location = new Point(309, 32);
            spAuditDeleteButton.Name = "spAuditDeleteButton";
            spAuditDeleteButton.Size = new Size(136, 25);
            spAuditDeleteButton.TabIndex = 13;
            spAuditDeleteButton.Text = "Delete audit trigger";
            spAuditDeleteButton.UseVisualStyleBackColor = true;
            // 
            // spAuditInsertButton
            // 
            spAuditInsertButton.Location = new Point(167, 32);
            spAuditInsertButton.Name = "spAuditInsertButton";
            spAuditInsertButton.Size = new Size(136, 25);
            spAuditInsertButton.TabIndex = 12;
            spAuditInsertButton.Text = "Insert audit trigger";
            spAuditInsertButton.UseVisualStyleBackColor = true;
            // 
            // spAuditTableButton
            // 
            spAuditTableButton.Location = new Point(25, 32);
            spAuditTableButton.Name = "spAuditTableButton";
            spAuditTableButton.Size = new Size(136, 25);
            spAuditTableButton.TabIndex = 11;
            spAuditTableButton.Text = "Audit table";
            spAuditTableButton.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(spDeleteByPKButton);
            groupBox5.Controls.Add(spUpdateByPKButton);
            groupBox5.Controls.Add(spInsertButton);
            groupBox5.Controls.Add(SelectByPKButton);
            groupBox5.Controls.Add(spSelectAllButton);
            groupBox5.Location = new Point(19, 12);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(732, 96);
            groupBox5.TabIndex = 11;
            groupBox5.TabStop = false;
            groupBox5.Text = "CRUD";
            // 
            // spDeleteByPKButton
            // 
            spDeleteByPKButton.Location = new Point(302, 22);
            spDeleteByPKButton.Name = "spDeleteByPKButton";
            spDeleteByPKButton.Size = new Size(136, 25);
            spDeleteByPKButton.TabIndex = 9;
            spDeleteByPKButton.Text = "Delete by PK";
            spDeleteByPKButton.UseVisualStyleBackColor = true;
            // 
            // spUpdateByPKButton
            // 
            spUpdateByPKButton.Location = new Point(586, 22);
            spUpdateByPKButton.Name = "spUpdateByPKButton";
            spUpdateByPKButton.Size = new Size(136, 25);
            spUpdateByPKButton.TabIndex = 8;
            spUpdateByPKButton.Text = "Update by PK";
            spUpdateByPKButton.UseVisualStyleBackColor = true;
            // 
            // spInsertButton
            // 
            spInsertButton.Location = new Point(444, 22);
            spInsertButton.Name = "spInsertButton";
            spInsertButton.Size = new Size(136, 25);
            spInsertButton.TabIndex = 7;
            spInsertButton.Text = "Insert";
            spInsertButton.UseVisualStyleBackColor = true;
            // 
            // SelectByPKButton
            // 
            SelectByPKButton.Location = new Point(160, 22);
            SelectByPKButton.Name = "SelectByPKButton";
            SelectByPKButton.Size = new Size(136, 25);
            SelectByPKButton.TabIndex = 6;
            SelectByPKButton.Text = "Select by PK";
            SelectByPKButton.UseVisualStyleBackColor = true;
            // 
            // spSelectAllButton
            // 
            spSelectAllButton.Location = new Point(18, 22);
            spSelectAllButton.Name = "spSelectAllButton";
            spSelectAllButton.Size = new Size(136, 25);
            spSelectAllButton.TabIndex = 5;
            spSelectAllButton.Text = "Select All";
            spSelectAllButton.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(groupBox3);
            tabPage2.Controls.Add(groupBox2);
            tabPage2.Controls.Add(groupBox1);
            tabPage2.Controls.Add(DomainNamespaceTextBox);
            tabPage2.Controls.Add(label3);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(819, 451);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Domain";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(DomainCommonsOperationStatusModelButton);
            groupBox3.Controls.Add(DomainCommonsOperationResultModelButton);
            groupBox3.Location = new Point(15, 193);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(368, 93);
            groupBox3.TabIndex = 16;
            groupBox3.TabStop = false;
            groupBox3.Text = "Commons";
            // 
            // DomainCommonsOperationStatusModelButton
            // 
            DomainCommonsOperationStatusModelButton.Location = new Point(6, 52);
            DomainCommonsOperationStatusModelButton.Name = "DomainCommonsOperationStatusModelButton";
            DomainCommonsOperationStatusModelButton.Size = new Size(344, 24);
            DomainCommonsOperationStatusModelButton.TabIndex = 18;
            DomainCommonsOperationStatusModelButton.Text = "OperationStatusModel";
            DomainCommonsOperationStatusModelButton.UseVisualStyleBackColor = true;
            DomainCommonsOperationStatusModelButton.Click += DomainCommonsOperationStatusModelButton_Click;
            // 
            // DomainCommonsOperationResultModelButton
            // 
            DomainCommonsOperationResultModelButton.Location = new Point(6, 22);
            DomainCommonsOperationResultModelButton.Name = "DomainCommonsOperationResultModelButton";
            DomainCommonsOperationResultModelButton.Size = new Size(344, 24);
            DomainCommonsOperationResultModelButton.TabIndex = 8;
            DomainCommonsOperationResultModelButton.Text = "OperationResultModel";
            DomainCommonsOperationResultModelButton.UseVisualStyleBackColor = true;
            DomainCommonsOperationResultModelButton.Click += DomainCommonsOperationResultModelButton_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(DomainEnumsOperationResultCodesButton);
            groupBox2.Location = new Point(390, 35);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(366, 75);
            groupBox2.TabIndex = 15;
            groupBox2.TabStop = false;
            groupBox2.Text = "Enums";
            // 
            // DomainEnumsOperationResultCodesButton
            // 
            DomainEnumsOperationResultCodesButton.Location = new Point(6, 22);
            DomainEnumsOperationResultCodesButton.Name = "DomainEnumsOperationResultCodesButton";
            DomainEnumsOperationResultCodesButton.Size = new Size(354, 24);
            DomainEnumsOperationResultCodesButton.TabIndex = 16;
            DomainEnumsOperationResultCodesButton.Text = "OperationResultCodes";
            DomainEnumsOperationResultCodesButton.UseVisualStyleBackColor = true;
            DomainEnumsOperationResultCodesButton.Click += DomainEnumsOperationResultCodesButton_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(DomainGenerateEntityButton);
            groupBox1.Controls.Add(DomainEntityNameTextBox);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(EntityInherithsFromTextBox);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(EntitiesNamespaceSuffixTextBox);
            groupBox1.Controls.Add(label4);
            groupBox1.Location = new Point(15, 35);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(366, 152);
            groupBox1.TabIndex = 14;
            groupBox1.TabStop = false;
            groupBox1.Text = "Entity";
            // 
            // DomainGenerateEntityButton
            // 
            DomainGenerateEntityButton.Location = new Point(189, 120);
            DomainGenerateEntityButton.Name = "DomainGenerateEntityButton";
            DomainGenerateEntityButton.Size = new Size(161, 24);
            DomainGenerateEntityButton.TabIndex = 14;
            DomainGenerateEntityButton.Text = "Generate Entity";
            DomainGenerateEntityButton.UseVisualStyleBackColor = true;
            DomainGenerateEntityButton.Click += DomainGenerateEntityButton_Click;
            // 
            // DomainEntityNameTextBox
            // 
            DomainEntityNameTextBox.Location = new Point(189, 61);
            DomainEntityNameTextBox.Name = "DomainEntityNameTextBox";
            DomainEntityNameTextBox.Size = new Size(161, 23);
            DomainEntityNameTextBox.TabIndex = 10;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 60);
            label6.Name = "label6";
            label6.Size = new Size(80, 15);
            label6.TabIndex = 13;
            label6.Text = "Entitity name:";
            // 
            // EntityInherithsFromTextBox
            // 
            EntityInherithsFromTextBox.Location = new Point(189, 91);
            EntityInherithsFromTextBox.Name = "EntityInherithsFromTextBox";
            EntityInherithsFromTextBox.Size = new Size(161, 23);
            EntityInherithsFromTextBox.TabIndex = 12;
            EntityInherithsFromTextBox.TextChanged += EntityInherithsFromTextBox_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 98);
            label5.Name = "label5";
            label5.Size = new Size(173, 15);
            label5.TabIndex = 11;
            label5.Text = "Entitity inherits from (optional):";
            // 
            // EntitiesNamespaceSuffixTextBox
            // 
            EntitiesNamespaceSuffixTextBox.Location = new Point(189, 28);
            EntitiesNamespaceSuffixTextBox.Name = "EntitiesNamespaceSuffixTextBox";
            EntitiesNamespaceSuffixTextBox.Size = new Size(161, 23);
            EntitiesNamespaceSuffixTextBox.TabIndex = 9;
            EntitiesNamespaceSuffixTextBox.TextChanged += EntitiesNamespaceSuffixTextBox_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 31);
            label4.Name = "label4";
            label4.Size = new Size(143, 15);
            label4.TabIndex = 8;
            label4.Text = "Entities namespace suffix:";
            // 
            // DomainNamespaceTextBox
            // 
            DomainNamespaceTextBox.Location = new Point(204, 6);
            DomainNamespaceTextBox.Name = "DomainNamespaceTextBox";
            DomainNamespaceTextBox.Size = new Size(363, 23);
            DomainNamespaceTextBox.TabIndex = 11;
            DomainNamespaceTextBox.TextChanged += DomainNamespaceTextBox_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(21, 6);
            label3.Name = "label3";
            label3.Size = new Size(117, 15);
            label3.TabIndex = 10;
            label3.Text = "Domain Namespace:";
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(groupBox7);
            tabPage3.Controls.Add(groupBox4);
            tabPage3.Controls.Add(ApplicationNamespaceTextBox);
            tabPage3.Controls.Add(label12);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(819, 451);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Application";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(ApplicationManagerInterfaceButton);
            groupBox7.Controls.Add(ApplicationRepositoryInterfaceButton);
            groupBox7.Controls.Add(ApplicationEntityNameTextBox);
            groupBox7.Controls.Add(label8);
            groupBox7.Location = new Point(13, 39);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new Size(366, 345);
            groupBox7.TabIndex = 15;
            groupBox7.TabStop = false;
            groupBox7.Text = "Entity";
            // 
            // ApplicationManagerInterfaceButton
            // 
            ApplicationManagerInterfaceButton.Location = new Point(6, 83);
            ApplicationManagerInterfaceButton.Name = "ApplicationManagerInterfaceButton";
            ApplicationManagerInterfaceButton.Size = new Size(344, 24);
            ApplicationManagerInterfaceButton.TabIndex = 15;
            ApplicationManagerInterfaceButton.Text = "Commons - Interfaces -BLL - Manager";
            ApplicationManagerInterfaceButton.TextAlign = ContentAlignment.MiddleLeft;
            ApplicationManagerInterfaceButton.UseVisualStyleBackColor = true;
            ApplicationManagerInterfaceButton.Click += ApplicationManagerInterfaceButton_Click;
            // 
            // ApplicationRepositoryInterfaceButton
            // 
            ApplicationRepositoryInterfaceButton.Location = new Point(6, 53);
            ApplicationRepositoryInterfaceButton.Name = "ApplicationRepositoryInterfaceButton";
            ApplicationRepositoryInterfaceButton.Size = new Size(344, 24);
            ApplicationRepositoryInterfaceButton.TabIndex = 14;
            ApplicationRepositoryInterfaceButton.Text = "Commons - Interfaces -Repositories - Repository";
            ApplicationRepositoryInterfaceButton.TextAlign = ContentAlignment.MiddleLeft;
            ApplicationRepositoryInterfaceButton.UseVisualStyleBackColor = true;
            ApplicationRepositoryInterfaceButton.Click += ApplicationRepositoryInterfaceButton_Click;
            // 
            // ApplicationEntityNameTextBox
            // 
            ApplicationEntityNameTextBox.Location = new Point(189, 22);
            ApplicationEntityNameTextBox.Name = "ApplicationEntityNameTextBox";
            ApplicationEntityNameTextBox.Size = new Size(161, 23);
            ApplicationEntityNameTextBox.TabIndex = 10;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(6, 21);
            label8.Name = "label8";
            label8.Size = new Size(80, 15);
            label8.TabIndex = 13;
            label8.Text = "Entitity name:";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(ApplicationCommonsInterfacesUtilsReverseHashButton);
            groupBox4.Controls.Add(button4);
            groupBox4.Controls.Add(button3);
            groupBox4.Controls.Add(button2);
            groupBox4.Controls.Add(button1);
            groupBox4.Controls.Add(ApplicationCommonsInterfacesInfrastructureDatabaseHelperButton);
            groupBox4.Controls.Add(ApplicatioConfigureServicesButton);
            groupBox4.Controls.Add(ApplicationCommonsAttributeTraceAndTimeInterceptorButton);
            groupBox4.Controls.Add(ApplicationCommonsAttributeTraceAndTimeButton);
            groupBox4.Controls.Add(ApplicationGlobalVariablesBLLAndConstantsButton);
            groupBox4.Location = new Point(401, 39);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(402, 345);
            groupBox4.TabIndex = 14;
            groupBox4.TabStop = false;
            groupBox4.Text = "Miscellaneous";
            // 
            // ApplicationCommonsInterfacesUtilsReverseHashButton
            // 
            ApplicationCommonsInterfacesUtilsReverseHashButton.Location = new Point(6, 262);
            ApplicationCommonsInterfacesUtilsReverseHashButton.Name = "ApplicationCommonsInterfacesUtilsReverseHashButton";
            ApplicationCommonsInterfacesUtilsReverseHashButton.Size = new Size(390, 24);
            ApplicationCommonsInterfacesUtilsReverseHashButton.TabIndex = 26;
            ApplicationCommonsInterfacesUtilsReverseHashButton.Text = "Commons  - Interfaces - Utils - IReverseHash";
            ApplicationCommonsInterfacesUtilsReverseHashButton.TextAlign = ContentAlignment.MiddleLeft;
            ApplicationCommonsInterfacesUtilsReverseHashButton.UseVisualStyleBackColor = true;
            ApplicationCommonsInterfacesUtilsReverseHashButton.Click += ApplicationCommonsInterfacesUtilsReverseHashButton_Click;
            // 
            // button4
            // 
            button4.Location = new Point(6, 232);
            button4.Name = "button4";
            button4.Size = new Size(390, 24);
            button4.TabIndex = 25;
            button4.Text = "Commons  - Interfaces - Infrastructure - IStorageHelper";
            button4.TextAlign = ContentAlignment.MiddleLeft;
            button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(6, 202);
            button3.Name = "button3";
            button3.Size = new Size(390, 24);
            button3.TabIndex = 24;
            button3.Text = "Commons  - Interfaces - Infrastructure - IQueueHelper";
            button3.TextAlign = ContentAlignment.MiddleLeft;
            button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(6, 172);
            button2.Name = "button2";
            button2.Size = new Size(390, 24);
            button2.TabIndex = 23;
            button2.Text = "Commons  - Interfaces - Infrastructure - IPublisherSubscriptionHelper";
            button2.TextAlign = ContentAlignment.MiddleLeft;
            button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(6, 142);
            button1.Name = "button1";
            button1.Size = new Size(390, 24);
            button1.TabIndex = 22;
            button1.Text = "Commons  - Interfaces - Infrastructure - ICacheHelper";
            button1.TextAlign = ContentAlignment.MiddleLeft;
            button1.UseVisualStyleBackColor = true;
            // 
            // ApplicationCommonsInterfacesInfrastructureDatabaseHelperButton
            // 
            ApplicationCommonsInterfacesInfrastructureDatabaseHelperButton.Location = new Point(6, 112);
            ApplicationCommonsInterfacesInfrastructureDatabaseHelperButton.Name = "ApplicationCommonsInterfacesInfrastructureDatabaseHelperButton";
            ApplicationCommonsInterfacesInfrastructureDatabaseHelperButton.Size = new Size(390, 24);
            ApplicationCommonsInterfacesInfrastructureDatabaseHelperButton.TabIndex = 21;
            ApplicationCommonsInterfacesInfrastructureDatabaseHelperButton.Text = "Commons  - Interfaces - Infrastructure - IDatabaseHelper";
            ApplicationCommonsInterfacesInfrastructureDatabaseHelperButton.TextAlign = ContentAlignment.MiddleLeft;
            ApplicationCommonsInterfacesInfrastructureDatabaseHelperButton.UseVisualStyleBackColor = true;
            ApplicationCommonsInterfacesInfrastructureDatabaseHelperButton.Click += ApplicationCommonsInterfacesInfrastructureDatabaseHelperButton_Click;
            // 
            // ApplicatioConfigureServicesButton
            // 
            ApplicatioConfigureServicesButton.Location = new Point(6, 315);
            ApplicatioConfigureServicesButton.Name = "ApplicatioConfigureServicesButton";
            ApplicatioConfigureServicesButton.Size = new Size(390, 24);
            ApplicatioConfigureServicesButton.TabIndex = 20;
            ApplicatioConfigureServicesButton.Text = "ConfigureServices";
            ApplicatioConfigureServicesButton.TextAlign = ContentAlignment.MiddleLeft;
            ApplicatioConfigureServicesButton.UseVisualStyleBackColor = true;
            ApplicatioConfigureServicesButton.Click += ApplicatioConfigureServicesButton_Click;
            // 
            // ApplicationCommonsAttributeTraceAndTimeInterceptorButton
            // 
            ApplicationCommonsAttributeTraceAndTimeInterceptorButton.Location = new Point(6, 82);
            ApplicationCommonsAttributeTraceAndTimeInterceptorButton.Name = "ApplicationCommonsAttributeTraceAndTimeInterceptorButton";
            ApplicationCommonsAttributeTraceAndTimeInterceptorButton.Size = new Size(390, 24);
            ApplicationCommonsAttributeTraceAndTimeInterceptorButton.TabIndex = 19;
            ApplicationCommonsAttributeTraceAndTimeInterceptorButton.Text = "Commons  - Attributes -TraceAndTimeAtrributeInterceptor";
            ApplicationCommonsAttributeTraceAndTimeInterceptorButton.TextAlign = ContentAlignment.MiddleLeft;
            ApplicationCommonsAttributeTraceAndTimeInterceptorButton.UseVisualStyleBackColor = true;
            ApplicationCommonsAttributeTraceAndTimeInterceptorButton.Click += ApplicationCommonsAttributeTraceAndTimeInterceptorButton_Click;
            // 
            // ApplicationCommonsAttributeTraceAndTimeButton
            // 
            ApplicationCommonsAttributeTraceAndTimeButton.Location = new Point(6, 52);
            ApplicationCommonsAttributeTraceAndTimeButton.Name = "ApplicationCommonsAttributeTraceAndTimeButton";
            ApplicationCommonsAttributeTraceAndTimeButton.Size = new Size(390, 24);
            ApplicationCommonsAttributeTraceAndTimeButton.TabIndex = 18;
            ApplicationCommonsAttributeTraceAndTimeButton.Text = "Commons  - Attributes -TraceAndTimeAtrribute";
            ApplicationCommonsAttributeTraceAndTimeButton.TextAlign = ContentAlignment.MiddleLeft;
            ApplicationCommonsAttributeTraceAndTimeButton.UseVisualStyleBackColor = true;
            ApplicationCommonsAttributeTraceAndTimeButton.Click += ApplicationCommonsAttributeTraceAndTimeButton_Click;
            // 
            // ApplicationGlobalVariablesBLLAndConstantsButton
            // 
            ApplicationGlobalVariablesBLLAndConstantsButton.Location = new Point(6, 22);
            ApplicationGlobalVariablesBLLAndConstantsButton.Name = "ApplicationGlobalVariablesBLLAndConstantsButton";
            ApplicationGlobalVariablesBLLAndConstantsButton.Size = new Size(390, 24);
            ApplicationGlobalVariablesBLLAndConstantsButton.TabIndex = 17;
            ApplicationGlobalVariablesBLLAndConstantsButton.Text = "BLL  - Global variables and constants";
            ApplicationGlobalVariablesBLLAndConstantsButton.TextAlign = ContentAlignment.MiddleLeft;
            ApplicationGlobalVariablesBLLAndConstantsButton.UseVisualStyleBackColor = true;
            ApplicationGlobalVariablesBLLAndConstantsButton.Click += ApplicationGlobalVariablesBLLAndConstantsButton_Click;
            // 
            // ApplicationNamespaceTextBox
            // 
            ApplicationNamespaceTextBox.Location = new Point(196, 10);
            ApplicationNamespaceTextBox.Name = "ApplicationNamespaceTextBox";
            ApplicationNamespaceTextBox.Size = new Size(363, 23);
            ApplicationNamespaceTextBox.TabIndex = 13;
            ApplicationNamespaceTextBox.TextChanged += ApplicationNamespaceTextBox_TextChanged;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(13, 10);
            label12.Name = "label12";
            label12.Size = new Size(136, 15);
            label12.TabIndex = 12;
            label12.Text = "Application Namespace:";
            // 
            // tabPage5
            // 
            tabPage5.Location = new Point(4, 24);
            tabPage5.Name = "tabPage5";
            tabPage5.Size = new Size(819, 451);
            tabPage5.TabIndex = 4;
            tabPage5.Text = "Infrastructure";
            tabPage5.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1073, 662);
            Controls.Add(panel6);
            Controls.Add(panel4);
            Controls.Add(panel1);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Code Generator";
            WindowState = FormWindowState.Maximized;
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
            tabPage4.ResumeLayout(false);
            groupBox6.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            groupBox4.ResumeLayout(false);
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
        private TabPage tabPage3;
        private TextBox AuthorTextBox;
        private Label label7;
        private TabPage tabPage4;
        private Button DomainCommonsOperationResultModelButton;
        private TextBox DomainNamespaceTextBox;
        private Label label3;
        private GroupBox groupBox1;
        private Button DomainGenerateEntityButton;
        private TextBox DomainEntityNameTextBox;
        private Label label6;
        private TextBox EntityInherithsFromTextBox;
        private Label label5;
        private TextBox EntitiesNamespaceSuffixTextBox;
        private Label label4;
        private GroupBox groupBox3;
        private GroupBox groupBox2;
        private Button DomainEnumsOperationResultCodesButton;
        private Button DomainCommonsOperationStatusModelButton;
        private TextBox ApplicationNamespaceTextBox;
        private Label label12;
        private GroupBox groupBox4;
        private Button ApplicationGlobalVariablesBLLAndConstantsButton;
        private GroupBox groupBox5;
        private Button spDeleteByPKButton;
        private Button spUpdateByPKButton;
        private Button spInsertButton;
        private Button SelectByPKButton;
        private Button spSelectAllButton;
        private GroupBox groupBox6;
        private Button spAuditUpdateButton;
        private Button spAuditDeleteButton;
        private Button spAuditInsertButton;
        private Button spAuditTableButton;
        private Button ApplicationCommonsAttributeTraceAndTimeButton;
        private Button ApplicationCommonsAttributeTraceAndTimeInterceptorButton;
        private Button ApplicatioConfigureServicesButton;
        private GroupBox groupBox7;
        private Button ApplicationRepositoryInterfaceButton;
        private TextBox ApplicationEntityNameTextBox;
        private Label label8;
        private Button ApplicationManagerInterfaceButton;
        private Button ApplicationCommonsInterfacesInfrastructureDatabaseHelperButton;
        private Button button4;
        private Button button3;
        private Button button2;
        private Button button1;
        private Button ApplicationCommonsInterfacesUtilsReverseHashButton;
        private TabPage tabPage5;
    }
}