using codegenerator.BLL;
using codegenerator.Models;
using codegenerator.Models.ViewModels;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace codegenerator
{
    public partial class MainForm : Form
    {
        private bool bolLoading;
        private string connectionString;
        private DatabaseUtilities _dbUtil;
        private ODBCConnect _odbcForm;

        public MainForm(DatabaseUtilities dbUtil, ODBCConnect odbcForm)
        {
            _dbUtil = dbUtil;
            _odbcForm = odbcForm;

            bolLoading = true;
            connectionString = "";
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (bolLoading)
            {
                //read configuration  first
                GeneratorConfigurationManager.ReadFromFile();
                AuthorTextBox.Text = GeneratorConfigurationManager._gConfig.Author;
                DomainNamespaceTextBox.Text = GeneratorConfigurationManager._gConfig.DomainNamespace;
                EntitiesNamespaceSuffixTextBox.Text = GeneratorConfigurationManager._gConfig.EntitiesSuffixNamespace;
                if (string.IsNullOrWhiteSpace(EntitiesNamespaceSuffixTextBox.Text))
                {
                    EntitiesNamespaceSuffixTextBox.Text = "Entities";
                    GeneratorConfigurationManager._gConfig.EntitiesSuffixNamespace = EntitiesNamespaceSuffixTextBox.Text;
                    GeneratorConfigurationManager.SaveToFile();
                }
                EntityInherithsFromTextBox.Text = GeneratorConfigurationManager._gConfig.EntitiesInheritsFrom;
                ApplicationNamespaceTextBox.Text = GeneratorConfigurationManager._gConfig.ApplicationNamespace;

                //flag bolLoading
                bolLoading = false;


                panel6.Dock = DockStyle.Fill;
                panel5.Dock = DockStyle.Top;
                panel5.Height = 250;
                dataGridView1.Dock = DockStyle.Fill;
                panel7.Dock = DockStyle.Fill;
                tabControl1.Dock = DockStyle.Fill;

                SetupNewGrid();

                _odbcForm.ShowDialog();

                if (!_odbcForm.returnCode)
                {
                    System.Windows.Forms.Application.Exit();
                    return;
                }
                connectionString = _odbcForm.connectionString;
                populateTables();
            }

            Cursor.Current = Cursors.Default;
        }

        #region General
        private void SetupNewGrid()
        {
            Cursor.Current = Cursors.WaitCursor;

            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();


            DataGridViewColumn col1 = new DataGridViewColumn();
            col1.Name = "Name";
            col1.CellTemplate = new DataGridViewTextBoxCell();
            col1.ReadOnly = true;
            dataGridView1.Columns.Add(col1);

            DataGridViewColumn col2 = new DataGridViewColumn();
            col2.Name = "Type";
            col2.CellTemplate = new DataGridViewTextBoxCell();
            col2.ReadOnly = true;
            dataGridView1.Columns.Add(col2);

            DataGridViewColumn col3 = new DataGridViewColumn();
            col3.Name = "Length";
            col3.CellTemplate = new DataGridViewTextBoxCell();
            col3.ReadOnly = true;
            dataGridView1.Columns.Add(col3);

            DataGridViewColumn col4 = new DataGridViewColumn();
            col4.Name = "Is nullable";
            col4.CellTemplate = new DataGridViewCheckBoxCell();
            col4.ReadOnly = true;
            dataGridView1.Columns.Add(col4);


            DataGridViewColumn col5 = new DataGridViewColumn();
            col5.Name = "Is identity";
            col5.CellTemplate = new DataGridViewCheckBoxCell();
            col5.ReadOnly = true;
            dataGridView1.Columns.Add(col5);


            //DataGridViewColumn col6 = new DataGridViewColumn();
            //col6.Name = "Is System Date";
            //col6.CellTemplate = new DataGridViewCheckBoxCell();
            //col6.ReadOnly = false;
            //dataGridView1.Columns.Add(col6);

            Cursor.Current = Cursors.Default;
        }

        private void SetupOtherValues()
        {
            Cursor.Current = Cursors.WaitCursor;

            DomainEntityNameTextBox.Text = ((KeyValueItem)tablesListBox.SelectedItem).Text;
            ApplicationEntityNameTextBox.Text = ((KeyValueItem)tablesListBox.SelectedItem).Text;

            Cursor.Current = Cursors.Default;
        }

        private void populateTables()
        {
            Cursor.Current = Cursors.WaitCursor;

            List<SQLTableModel> tableList = null;
            tableList = _dbUtil.GetDatabaseTables(connectionString);
            if (tableList == null)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("No tables were fround on current database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (SQLTableModel table in tableList)
            {
                KeyValueItem item = new KeyValueItem();
                item.Text = table.name;
                item.Value = table.id;
                tablesListBox.Items.Add(item);
            }
            tablesListBox.SelectedIndex = 0;

            Cursor.Current = Cursors.Default;
        }

        private void tablesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            List<SQLTableFieldModel> fieldList;
            KeyValueItem item;
            bool isNullable;

            SetupNewGrid();
            SetupOtherValues();
            item = (KeyValueItem)tablesListBox.SelectedItem;
            fieldList = _dbUtil.GetTableFields(connectionString, Convert.ToInt32(item.Value), item.Text);
            if (fieldList != null)
            {
                foreach (var field in fieldList)
                {
                    if (field.isnullable == 0)
                    {
                        isNullable = false;
                    }
                    else
                    {
                        isNullable = true;
                    }

                    dataGridView1.Rows.Add(
                        field.isPrimaryKey ? "*" + field.name : field.name,
                        field.fieldType,
                        CalculateFieldSizeString(field),
                        isNullable,
                        field.isIdentity);

                    //false);
                    //todo remove
                    //just poc
                    //if (dataGridView1.Rows.Count % 2 == 0)
                    //{
                    //    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[4].Value = null;
                    //    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[4] = new DataGridViewTextBoxCell();
                    //    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[4].ReadOnly = true;
                    //}
                }
            }

            Cursor.Current = Cursors.Default;
        }

        private string CalculateFieldSizeString(SQLTableFieldModel field)
        {
            string size = "0";
            switch (field.fieldType)
            {
                case "char":
                case "varchar":
                    size = field.length == -1 ? "max" : field.length.ToString();
                    break;
                case "nchar":
                case "nvarchar":
                    size = field.length == -1 ? "max" : (field.length / 2).ToString();
                    break;
                case "decimal":
                case "numeric":
                    size = "(" + field.xprec.ToString() + "," + field.xscale.ToString() + ")";
                    break;

                default:
                    break;
                    //todo
            }
            return size;
        }

        private string CalculateFieldTypeString(SQLTableFieldModel field)
        {
            string fieldType = "";
            string size;
            size = CalculateFieldSizeString(field);

            switch (field.fieldType)
            {
                case "char":
                case "varchar":
                case "nchar":
                case "nvarchar":
                    fieldType = field.fieldType + "(" + size + ")";
                    break;
                case "decimal":
                case "numeric":
                    fieldType = field.fieldType + size;
                    break;
                default:
                    fieldType = field.fieldType;
                    break;
                    //todo
            }
            return fieldType;
        }

        private string MapSQLTypeToCType(SQLTableFieldModel field)
        {
            string map = "";
            switch (field.fieldType)
            {
                case "char":
                case "varchar":
                case "nchar":
                case "nvarchar":
                    map = "string";
                    break;
                case "int":
                    map = "int";
                    break;
                case "bigint":
                    map = "long";
                    break;
                case "smallint":
                    map = "short";
                    break;
                case "tinyint":
                    map = "byte";
                    break;
                case "bit":
                    map = "bool";
                    break;
                case "decimal":
                    map = "decimal";
                    break;
                case "float":
                    map = "float";
                    break;
                case "double":
                    map = "double";
                    break;
                default:
                    break;
                    //todo
            }
            return map;
        }
        #endregion

        #region Database
        private void spSelectAllButton_Click(object sender, EventArgs e)
        {
            StringBuilder code = new StringBuilder("");
            StringBuilder orderby = new StringBuilder("");
            List<SQLTableFieldModel> fieldList;
            KeyValueItem item;
            GeneratedCodeForm form = new GeneratedCodeForm();
            DateTime Today = DateTime.Now;
            item = (KeyValueItem)tablesListBox.SelectedItem;
            fieldList = _dbUtil.GetTableFields(connectionString, Convert.ToInt32(item.Value), item.Text);

            if (fieldList != null)
            {
                foreach (var field in fieldList)
                {
                    if (field.isPrimaryKey)
                    {
                        if (orderby.ToString() != "")
                        {
                            orderby.Append(",");
                        }
                        orderby.Append($"{field.name}");
                    }
                }
            }
            code.Append("SET ANSI_NULLS ON" + Environment.NewLine);
            code.Append("GO" + Environment.NewLine);
            code.Append("SET QUOTED_IDENTIFIER ON" + Environment.NewLine);
            code.Append("GO" + Environment.NewLine);
            code.Append("-- =============================================" + Environment.NewLine);
            code.Append("-- Author:" + AuthorTextBox.Text.Trim() + Environment.NewLine);
            code.Append("-- Create date:" + Today.Year.ToString() + "-" + Today.Month.ToString("00") + "-" + Today.Day.ToString("00") + Environment.NewLine);
            code.Append("-- Description:Select all records from table " + item.Text + Environment.NewLine);
            code.Append("-- Generated code by juanidamato/codegenerator" + Environment.NewLine);
            code.Append($"CREATE PROCEDURE [dbo].[{item.Text}_Select_All]" + Environment.NewLine);
            code.Append("AS" + Environment.NewLine);
            code.Append("BEGIN" + Environment.NewLine);
            code.Append("" + Environment.NewLine);
            code.Append("     SET NOCOUNT OFF;" + Environment.NewLine);
            code.Append("" + Environment.NewLine);
            code.Append($"     select * from [{item.Text}]" + Environment.NewLine);
            if (orderby.ToString() != "")
            {
                code.Append("     order by " + orderby.ToString() + Environment.NewLine);
            }
            code.Append("END" + Environment.NewLine);
            form.GeneratedCodeText = code.ToString();
            form.Show();
        }

        private void spSelectByPKButton_Click(object sender, EventArgs e)
        {
            StringBuilder code = new StringBuilder("");
            StringBuilder parametersKey = new StringBuilder("");
            StringBuilder whereClause = new StringBuilder("");
            int KeyCount;
            List<SQLTableFieldModel> fieldList;
            KeyValueItem item;
            GeneratedCodeForm form = new GeneratedCodeForm();
            DateTime Today = DateTime.Now;
            item = (KeyValueItem)tablesListBox.SelectedItem;
            fieldList = _dbUtil.GetTableFields(connectionString, Convert.ToInt32(item.Value), item.Text);
            whereClause.Append("     where ");
            if (fieldList != null)
            {
                foreach (var field in fieldList)
                {
                    if (field.isPrimaryKey)
                    {
                        if (parametersKey.ToString() != "")
                        {
                            parametersKey.Append("," + Environment.NewLine);
                        }
                        parametersKey.Append($"      @{field.name} {CalculateFieldTypeString(field)}");
                        if (whereClause.ToString() != "     where ")
                        {
                            whereClause.Append(Environment.NewLine);
                            whereClause.Append("     and ");
                        }
                        whereClause.Append($"{field.name}=@{field.name}");
                    }
                }
                parametersKey.Append(Environment.NewLine);
            }
            KeyCount = (from oneField in fieldList
                        where oneField.isPrimaryKey
                        select oneField).Count();
            if (KeyCount == 0)
            {
                MessageBox.Show("This table does not contains primary keys", "Error", MessageBoxButtons.OK);
                return;
            }
            code.Append("SET ANSI_NULLS ON" + Environment.NewLine);
            code.Append("GO" + Environment.NewLine);
            code.Append("SET QUOTED_IDENTIFIER ON" + Environment.NewLine);
            code.Append("GO" + Environment.NewLine);
            code.Append("-- =============================================" + Environment.NewLine);
            code.Append("-- Author:" + AuthorTextBox.Text.Trim() + Environment.NewLine);
            code.Append("-- Create date:" + Today.Year.ToString() + "-" + Today.Month.ToString("00") + "-" + Today.Day.ToString("00") + Environment.NewLine);
            code.Append("-- Description:Select one record by primary key from table " + item.Text + Environment.NewLine);
            code.Append("-- Generated code by juanidamato/codegenerator" + Environment.NewLine);
            code.Append($"CREATE PROCEDURE [dbo].[{item.Text}_Select_ByPK]" + Environment.NewLine);
            code.Append(parametersKey);
            code.Append("AS" + Environment.NewLine);
            code.Append("BEGIN" + Environment.NewLine);
            code.Append("" + Environment.NewLine);
            code.Append("     SET NOCOUNT OFF;" + Environment.NewLine);
            code.Append("" + Environment.NewLine);
            code.Append($"     select * from [{item.Text}]" + Environment.NewLine);
            code.Append($"{whereClause}" + Environment.NewLine);
            code.Append("END" + Environment.NewLine);
            form.GeneratedCodeText = code.ToString();
            form.Show();
        }

        private void spInsertButton_Click(object sender, EventArgs e)
        {
            StringBuilder code = new StringBuilder("");
            StringBuilder parametersInsert = new StringBuilder("");
            StringBuilder fieldsInsert = new StringBuilder("");
            StringBuilder valuesInsert = new StringBuilder("");
            List<SQLTableFieldModel> fieldList;
            KeyValueItem item;
            GeneratedCodeForm form = new GeneratedCodeForm();
            DateTime Today = DateTime.Now;
            bool HasIdentity = false;
            item = (KeyValueItem)tablesListBox.SelectedItem;
            fieldList = _dbUtil.GetTableFields(connectionString, Convert.ToInt32(item.Value), item.Text);

            if (fieldList != null)
            {
                foreach (var field in fieldList)
                {
                    //todo datefields
                    if (field.isIdentity)
                    {
                        HasIdentity = true;
                    }
                    if (!field.isIdentity)
                    {
                        if (parametersInsert.ToString() != "")
                        {
                            parametersInsert.Append("," + Environment.NewLine);
                        }
                        parametersInsert.Append($"      @{field.name} {CalculateFieldTypeString(field)}");

                        if (fieldsInsert.ToString() != "")
                        {
                            fieldsInsert.Append("," + Environment.NewLine);
                        }
                        fieldsInsert.Append($"      {field.name}");

                        if (valuesInsert.ToString() != "")
                        {
                            valuesInsert.Append("," + Environment.NewLine);
                        }
                        valuesInsert.Append($"      @{field.name}");
                    }

                }
                parametersInsert.Append(Environment.NewLine);
                fieldsInsert.Append(Environment.NewLine);
                valuesInsert.Append(Environment.NewLine);
            }
            code.Append("SET ANSI_NULLS ON" + Environment.NewLine);
            code.Append("GO" + Environment.NewLine);
            code.Append("SET QUOTED_IDENTIFIER ON" + Environment.NewLine);
            code.Append("GO" + Environment.NewLine);
            code.Append("-- =============================================" + Environment.NewLine);
            code.Append("-- Author:" + AuthorTextBox.Text.Trim() + Environment.NewLine);
            code.Append("-- Create date:" + Today.Year.ToString() + "-" + Today.Month.ToString("00") + "-" + Today.Day.ToString("00") + Environment.NewLine);
            code.Append("-- Description:Insert one record into table " + item.Text + Environment.NewLine);
            code.Append("-- Generated code by juanidamato/codegenerator" + Environment.NewLine);
            code.Append($"CREATE PROCEDURE [dbo].[{item.Text}_Insert]" + Environment.NewLine);
            code.Append(parametersInsert.ToString());
            code.Append("AS" + Environment.NewLine);
            code.Append("BEGIN" + Environment.NewLine);
            code.Append("" + Environment.NewLine);
            code.Append("     SET NOCOUNT ON;" + Environment.NewLine);
            code.Append("" + Environment.NewLine);
            code.Append($"     insert into [{item.Text}]" + Environment.NewLine);
            code.Append($"     (" + Environment.NewLine);
            code.Append(fieldsInsert.ToString());
            code.Append($"     )" + Environment.NewLine);
            code.Append($"     values" + Environment.NewLine);
            code.Append($"     (" + Environment.NewLine);
            code.Append(valuesInsert.ToString());
            code.Append($"     )" + Environment.NewLine);
            code.Append(Environment.NewLine);
            code.Append("     SET NOCOUNT OFF;" + Environment.NewLine);
            code.Append(Environment.NewLine);
            code.Append($"     IF @@ROWCOUNT=1" + Environment.NewLine);
            code.Append($"     BEGIN" + Environment.NewLine);
            if (HasIdentity)
            {
                code.Append($"        select @@SCOPE_IDENTITY() as 'R'" + Environment.NewLine);
            }
            else
            {
                code.Append($"        select 1 as 'R'" + Environment.NewLine);
            }
            code.Append($"     END" + Environment.NewLine);
            code.Append($"     ELSE" + Environment.NewLine);
            code.Append($"     BEGIN" + Environment.NewLine);
            code.Append($"        select -1 as 'R'" + Environment.NewLine);
            code.Append($"     END" + Environment.NewLine);
            code.Append("END" + Environment.NewLine);
            form.GeneratedCodeText = code.ToString();
            form.Show();
        }

        private void spUpdateByPKButton_Click(object sender, EventArgs e)
        {
            StringBuilder code = new StringBuilder("");
            StringBuilder parametersUpdate = new StringBuilder("");
            StringBuilder parametersSet = new StringBuilder("");
            StringBuilder whereClause = new StringBuilder("");
            int KeyCount;
            List<SQLTableFieldModel> fieldList;
            KeyValueItem item;
            GeneratedCodeForm form = new GeneratedCodeForm();
            DateTime Today = DateTime.Now;
            item = (KeyValueItem)tablesListBox.SelectedItem;
            fieldList = _dbUtil.GetTableFields(connectionString, Convert.ToInt32(item.Value), item.Text);

            whereClause.Append("     where ");
            if (fieldList != null)
            {
                foreach (var field in fieldList)
                {
                    //todo datefields
                    if (parametersUpdate.ToString() != "")
                    {
                        parametersUpdate.Append("," + Environment.NewLine);
                    }
                    parametersUpdate.Append($"      @{field.name} {CalculateFieldTypeString(field)}");
                    if (field.isPrimaryKey)
                    {
                        if (whereClause.ToString() != "     where ")
                        {
                            whereClause.Append(Environment.NewLine);
                            whereClause.Append("     and ");
                        }
                        whereClause.Append($"{field.name}=@{field.name}");
                    }
                    else
                    {
                        if (parametersSet.ToString() != "")
                        {
                            parametersSet.Append(",");
                            parametersSet.Append(Environment.NewLine);
                        }
                        parametersSet.Append($"          {field.name}=@{field.name}");
                    }
                }
                parametersUpdate.Append(Environment.NewLine);
                whereClause.Append(Environment.NewLine);
            }
            KeyCount = (from oneField in fieldList
                        where oneField.isPrimaryKey
                        select oneField).Count();
            if (KeyCount == 0)
            {
                MessageBox.Show("This table does not contains primary keys", "Error", MessageBoxButtons.OK);
                return;
            }
            code.Append("SET ANSI_NULLS ON" + Environment.NewLine);
            code.Append("GO" + Environment.NewLine);
            code.Append("SET QUOTED_IDENTIFIER ON" + Environment.NewLine);
            code.Append("GO" + Environment.NewLine);
            code.Append("-- =============================================" + Environment.NewLine);
            code.Append("-- Author:" + AuthorTextBox.Text.Trim() + Environment.NewLine);
            code.Append("-- Create date:" + Today.Year.ToString() + "-" + Today.Month.ToString("00") + "-" + Today.Day.ToString("00") + Environment.NewLine);
            code.Append("-- Description:Update one record by primary key from table " + item.Text + Environment.NewLine);
            code.Append("-- Generated code by juanidamato/codegenerator" + Environment.NewLine);
            code.Append($"CREATE PROCEDURE [dbo].[{item.Text}_Update_ByPK]" + Environment.NewLine);
            code.Append(parametersUpdate.ToString());
            code.Append("AS" + Environment.NewLine);
            code.Append("BEGIN" + Environment.NewLine);
            code.Append("" + Environment.NewLine);
            code.Append("     SET NOCOUNT ON;" + Environment.NewLine);
            code.Append("" + Environment.NewLine);
            code.Append($"     update [{item.Text}]" + Environment.NewLine);
            code.Append($"     SET" + Environment.NewLine);
            code.Append($"{parametersSet}" + Environment.NewLine);
            code.Append($"{whereClause}" + Environment.NewLine);
            code.Append(Environment.NewLine);
            code.Append("     SET NOCOUNT OFF;" + Environment.NewLine);
            code.Append(Environment.NewLine);
            code.Append($"     IF @@ROWCOUNT=1" + Environment.NewLine);
            code.Append($"     BEGIN" + Environment.NewLine);
            code.Append($"        select 1 as 'R'" + Environment.NewLine);
            code.Append($"     END" + Environment.NewLine);
            code.Append($"     ELSE" + Environment.NewLine);
            code.Append($"     BEGIN" + Environment.NewLine);
            code.Append($"        select -1 as 'R'" + Environment.NewLine);
            code.Append($"     END" + Environment.NewLine);
            code.Append("END" + Environment.NewLine);
            form.GeneratedCodeText = code.ToString();
            form.Show();
        }

        private void spDeleteByPKButton_Click(object sender, EventArgs e)
        {
            StringBuilder code = new StringBuilder("");
            StringBuilder parametersKey = new StringBuilder("");
            StringBuilder whereClause = new StringBuilder("");
            int KeyCount;
            List<SQLTableFieldModel> fieldList;
            KeyValueItem item;
            GeneratedCodeForm form = new GeneratedCodeForm();
            DateTime Today = DateTime.Now;
            item = (KeyValueItem)tablesListBox.SelectedItem;
            fieldList = _dbUtil.GetTableFields(connectionString, Convert.ToInt32(item.Value), item.Text);
            whereClause.Append("     where ");
            if (fieldList != null)
            {
                foreach (var field in fieldList)
                {
                    if (field.isPrimaryKey)
                    {
                        if (parametersKey.ToString() != "")
                        {
                            parametersKey.Append("," + Environment.NewLine);
                        }
                        parametersKey.Append($"      @{field.name} {CalculateFieldTypeString(field)}");
                        if (whereClause.ToString() != "     where ")
                        {
                            whereClause.Append(Environment.NewLine);
                            whereClause.Append("     and ");
                        }
                        whereClause.Append($"{field.name}=@{field.name}");
                    }
                }
                parametersKey.Append(Environment.NewLine);
            }
            KeyCount = (from oneField in fieldList
                        where oneField.isPrimaryKey
                        select oneField).Count();
            if (KeyCount == 0)
            {
                MessageBox.Show("This table does not contains primary keys", "Error", MessageBoxButtons.OK);
                return;
            }
            code.Append("SET ANSI_NULLS ON" + Environment.NewLine);
            code.Append("GO" + Environment.NewLine);
            code.Append("SET QUOTED_IDENTIFIER ON" + Environment.NewLine);
            code.Append("GO" + Environment.NewLine);
            code.Append("-- =============================================" + Environment.NewLine);
            code.Append("-- Author:" + AuthorTextBox.Text.Trim() + Environment.NewLine);
            code.Append("-- Create date:" + Today.Year.ToString() + "-" + Today.Month.ToString("00") + "-" + Today.Day.ToString("00") + Environment.NewLine);
            code.Append("-- Description:Delete one record by primary key from table " + item.Text + Environment.NewLine);
            code.Append("-- Generated code by juanidamato/codegenerator" + Environment.NewLine);
            code.Append($"CREATE PROCEDURE [dbo].[{item.Text}_Delete_ByPK]" + Environment.NewLine);
            code.Append(parametersKey);
            code.Append("AS" + Environment.NewLine);
            code.Append("BEGIN" + Environment.NewLine);
            code.Append("" + Environment.NewLine);
            code.Append("     SET NOCOUNT OFF;" + Environment.NewLine);
            code.Append("" + Environment.NewLine);
            code.Append($"     delete [{item.Text}]" + Environment.NewLine);
            code.Append($"{whereClause}" + Environment.NewLine);
            code.Append(Environment.NewLine);
            code.Append("     SET NOCOUNT OFF;" + Environment.NewLine);
            code.Append(Environment.NewLine);
            code.Append($"     IF @@ROWCOUNT=1" + Environment.NewLine);
            code.Append($"     BEGIN" + Environment.NewLine);
            code.Append($"        select 1 as 'R'" + Environment.NewLine);
            code.Append($"     END" + Environment.NewLine);
            code.Append($"     ELSE" + Environment.NewLine);
            code.Append($"     BEGIN" + Environment.NewLine);
            code.Append($"        select -1 as 'R'" + Environment.NewLine);
            code.Append($"     END" + Environment.NewLine);
            code.Append("END" + Environment.NewLine);
            form.GeneratedCodeText = code.ToString();
            form.Show();
        }

        private void spAuditTableButton_Click(object sender, EventArgs e)
        {
            StringBuilder code = new StringBuilder("");
            GeneratedCodeForm form = new GeneratedCodeForm();


            code.Append("SET ANSI_NULLS ON" + Environment.NewLine);
            code.Append("GO" + Environment.NewLine);
            code.Append("SET QUOTED_IDENTIFIER ON" + Environment.NewLine);
            code.Append("GO" + Environment.NewLine);
            code.Append($"CREATE TABLE [dbo].[AuditLogs]" + Environment.NewLine);
            code.Append("(" + Environment.NewLine);
            code.Append("     [IdAudit] [int] IDENTITY(1,1) NOT NULL," + Environment.NewLine);
            code.Append("     [InsertDate] [datetime] NOT NULL," + Environment.NewLine);
            code.Append("     [CurrentUser] [nvarchar](50) NOT NULL," + Environment.NewLine);
            code.Append("     [AuditTable] [nvarchar](50) NOT NULL," + Environment.NewLine);
            code.Append("     [Operation] [nvarchar](20) NOT NULL," + Environment.NewLine);
            code.Append("     [recordId] [nvarchar](20) NOT NULL," + Environment.NewLine);
            code.Append("     [NewData] [nvarchar](max) NULL," + Environment.NewLine);
            code.Append("     [OldData] [nvarchar](max) NULL," + Environment.NewLine);
            code.Append("     CONSTRAINT [PK_AuditLogs] PRIMARY KEY CLUSTERED " + Environment.NewLine);
            code.Append("     (" + Environment.NewLine);
            code.Append("         [IdAudit] ASC" + Environment.NewLine);
            code.Append("     )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]" + Environment.NewLine);
            code.Append(") ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]" + Environment.NewLine);
            code.Append("GO" + Environment.NewLine);
            form.GeneratedCodeText = code.ToString();
            form.Show();
        }

        private void spAuditInsertButton_Click(object sender, EventArgs e)
        {
            StringBuilder code = new StringBuilder("");
            StringBuilder selectClause = new StringBuilder("");
            int KeyCount;
            List<SQLTableFieldModel> fieldList;
            KeyValueItem item;
            GeneratedCodeForm form = new GeneratedCodeForm();
            item = (KeyValueItem)tablesListBox.SelectedItem;
            fieldList = _dbUtil.GetTableFields(connectionString, Convert.ToInt32(item.Value), item.Text);
            KeyCount = (from oneField in fieldList
                        where oneField.isPrimaryKey
                        select oneField).Count();
            if (KeyCount != 1)
            {
                MessageBox.Show("This table does contains more than one primary key field", "Error", MessageBoxButtons.OK);
                return;
            }

            if (fieldList != null)
            {
                foreach (var field in fieldList)
                {
                    if (field.isPrimaryKey)
                    {
                        selectClause.Append($"{field.name}");
                        break;
                    }
                }
            }

            code.Append("SET ANSI_NULLS ON" + Environment.NewLine);
            code.Append("GO" + Environment.NewLine);
            code.Append("SET QUOTED_IDENTIFIER ON" + Environment.NewLine);
            code.Append("GO" + Environment.NewLine);
            code.Append($"CREATE TRIGGER [dbo].[TR_{item.Text}_Insert_AuditLog] ON {item.Text}" + Environment.NewLine);
            code.Append("FOR INSERT AS" + Environment.NewLine);
            code.Append("BEGIN" + Environment.NewLine);
            code.Append("" + Environment.NewLine);
            code.Append("     SET NOCOUNT OFF;" + Environment.NewLine);
            code.Append("" + Environment.NewLine);
            code.Append($"     DECLARE @CurrentUser nvarchar(50)" + Environment.NewLine);
            code.Append($"     SELECT @CurrentUser = CAST(SESSION_CONTEXT(N'CurrentUser') AS nvarchar(50))" + Environment.NewLine);
            code.Append(Environment.NewLine);
            code.Append($"     IF  @CurrentUser is null" + Environment.NewLine);
            code.Append($"     BEGIN" + Environment.NewLine);
            code.Append($"        SET @CurrentUser=''" + Environment.NewLine);
            code.Append($"     END" + Environment.NewLine);
            code.Append(Environment.NewLine);
            code.Append($"     INSERT into AuditLogs (" + Environment.NewLine);
            code.Append($"        InsertDate," + Environment.NewLine);
            code.Append($"        CurrentUser," + Environment.NewLine);
            code.Append($"        AuditTable," + Environment.NewLine);
            code.Append($"        Operation," + Environment.NewLine);
            code.Append($"        recordId," + Environment.NewLine);
            code.Append($"        NewData," + Environment.NewLine);
            code.Append($"        OldData" + Environment.NewLine);
            code.Append($"     )" + Environment.NewLine);
            code.Append($"     values" + Environment.NewLine);
            code.Append($"     (" + Environment.NewLine);
            code.Append($"        GETUTCDATE()," + Environment.NewLine);
            code.Append($"        @CurrentUser," + Environment.NewLine);
            code.Append($"        '{item.Text}'," + Environment.NewLine);
            code.Append($"        'INSERT'," + Environment.NewLine);
            code.Append($"        (SELECT  {selectClause} FROM Inserted)," + Environment.NewLine);
            code.Append($"        (SELECT * FROM Inserted FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)," + Environment.NewLine);
            code.Append($"        null" + Environment.NewLine);
            code.Append($"     )" + Environment.NewLine);
            code.Append("END" + Environment.NewLine);
            form.GeneratedCodeText = code.ToString();
            form.Show();
        }

        private void spAuditDeleteButton_Click(object sender, EventArgs e)
        {
            StringBuilder code = new StringBuilder("");
            StringBuilder selectClause = new StringBuilder("");
            int KeyCount;
            List<SQLTableFieldModel> fieldList;
            KeyValueItem item;
            GeneratedCodeForm form = new GeneratedCodeForm();
            item = (KeyValueItem)tablesListBox.SelectedItem;
            fieldList = _dbUtil.GetTableFields(connectionString, Convert.ToInt32(item.Value), item.Text);
            KeyCount = (from oneField in fieldList
                        where oneField.isPrimaryKey
                        select oneField).Count();
            if (KeyCount != 1)
            {
                MessageBox.Show("This table does contains more than one primary key field", "Error", MessageBoxButtons.OK);
                return;
            }
            if (fieldList != null)
            {
                foreach (var field in fieldList)
                {
                    if (field.isPrimaryKey)
                    {
                        selectClause.Append($"{field.name}");
                        break;
                    }
                }
            }

            code.Append("SET ANSI_NULLS ON" + Environment.NewLine);
            code.Append("GO" + Environment.NewLine);
            code.Append("SET QUOTED_IDENTIFIER ON" + Environment.NewLine);
            code.Append("GO" + Environment.NewLine);
            code.Append($"CREATE TRIGGER [dbo].[TR_{item.Text}_Delete_AuditLog] ON {item.Text}" + Environment.NewLine);
            code.Append("FOR DELETE AS" + Environment.NewLine);
            code.Append("BEGIN" + Environment.NewLine);
            code.Append("" + Environment.NewLine);
            code.Append("     SET NOCOUNT OFF;" + Environment.NewLine);
            code.Append("" + Environment.NewLine);
            code.Append($"     DECLARE @CurrentUser nvarchar(50)" + Environment.NewLine);
            code.Append($"     SELECT @CurrentUser = CAST(SESSION_CONTEXT(N'CurrentUser') AS nvarchar(50))" + Environment.NewLine);
            code.Append(Environment.NewLine);
            code.Append($"     IF  @CurrentUser is null" + Environment.NewLine);
            code.Append($"     BEGIN" + Environment.NewLine);
            code.Append($"        SET @CurrentUser=''" + Environment.NewLine);
            code.Append($"     END" + Environment.NewLine);
            code.Append(Environment.NewLine);
            code.Append($"     INSERT into AuditLogs (" + Environment.NewLine);
            code.Append($"        InsertDate," + Environment.NewLine);
            code.Append($"        CurrentUser," + Environment.NewLine);
            code.Append($"        AuditTable," + Environment.NewLine);
            code.Append($"        Operation," + Environment.NewLine);
            code.Append($"        recordId," + Environment.NewLine);
            code.Append($"        NewData," + Environment.NewLine);
            code.Append($"        OldData" + Environment.NewLine);
            code.Append($"     )" + Environment.NewLine);
            code.Append($"     values" + Environment.NewLine);
            code.Append($"     (" + Environment.NewLine);
            code.Append($"        GETUTCDATE()," + Environment.NewLine);
            code.Append($"        @CurrentUser," + Environment.NewLine);
            code.Append($"        '{item.Text}'," + Environment.NewLine);
            code.Append($"        'DELETE'," + Environment.NewLine);
            code.Append($"        (SELECT  {selectClause} FROM Deleted)," + Environment.NewLine);
            code.Append($"        null," + Environment.NewLine);
            code.Append($"        (SELECT * FROM Deleted FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)" + Environment.NewLine);
            code.Append($"     )" + Environment.NewLine);
            code.Append("END" + Environment.NewLine);
            form.GeneratedCodeText = code.ToString();
            form.Show();
        }

        private void spAuditUpdateButton_Click(object sender, EventArgs e)
        {
            StringBuilder code = new StringBuilder("");
            StringBuilder selectClause = new StringBuilder("");
            int KeyCount;
            List<SQLTableFieldModel> fieldList;
            KeyValueItem item;
            GeneratedCodeForm form = new GeneratedCodeForm();
            item = (KeyValueItem)tablesListBox.SelectedItem;
            fieldList = _dbUtil.GetTableFields(connectionString, Convert.ToInt32(item.Value), item.Text);
            KeyCount = (from oneField in fieldList
                        where oneField.isPrimaryKey
                        select oneField).Count();
            if (KeyCount != 1)
            {
                MessageBox.Show("This table does contains more than one primary key field", "Error", MessageBoxButtons.OK);
                return;
            }
            if (fieldList != null)
            {
                foreach (var field in fieldList)
                {
                    if (field.isPrimaryKey)
                    {
                        selectClause.Append($"{field.name}");
                        break;
                    }
                }
            }

            code.Append("SET ANSI_NULLS ON" + Environment.NewLine);
            code.Append("GO" + Environment.NewLine);
            code.Append("SET QUOTED_IDENTIFIER ON" + Environment.NewLine);
            code.Append("GO" + Environment.NewLine);
            code.Append($"CREATE TRIGGER [dbo].[TR_{item.Text}_Update_AuditLog] ON {item.Text}" + Environment.NewLine);
            code.Append("FOR UPDATE AS" + Environment.NewLine);
            code.Append("BEGIN" + Environment.NewLine);
            code.Append("" + Environment.NewLine);
            code.Append("     SET NOCOUNT OFF;" + Environment.NewLine);
            code.Append("" + Environment.NewLine);
            code.Append($"     DECLARE @CurrentUser nvarchar(50)" + Environment.NewLine);
            code.Append($"     SELECT @CurrentUser = CAST(SESSION_CONTEXT(N'CurrentUser') AS nvarchar(50))" + Environment.NewLine);
            code.Append(Environment.NewLine);
            code.Append($"     IF  @CurrentUser is null" + Environment.NewLine);
            code.Append($"     BEGIN" + Environment.NewLine);
            code.Append($"        SET @CurrentUser=''" + Environment.NewLine);
            code.Append($"     END" + Environment.NewLine);
            code.Append(Environment.NewLine);
            code.Append($"     INSERT into AuditLogs (" + Environment.NewLine);
            code.Append($"        InsertDate," + Environment.NewLine);
            code.Append($"        CurrentUser," + Environment.NewLine);
            code.Append($"        AuditTable," + Environment.NewLine);
            code.Append($"        Operation," + Environment.NewLine);
            code.Append($"        recordId," + Environment.NewLine);
            code.Append($"        NewData," + Environment.NewLine);
            code.Append($"        OldData" + Environment.NewLine);
            code.Append($"     )" + Environment.NewLine);
            code.Append($"     values" + Environment.NewLine);
            code.Append($"     (" + Environment.NewLine);
            code.Append($"        GETUTCDATE()," + Environment.NewLine);
            code.Append($"        @CurrentUser," + Environment.NewLine);
            code.Append($"        '{item.Text}'," + Environment.NewLine);
            code.Append($"        'UPDATE'," + Environment.NewLine);
            code.Append($"        (SELECT  {selectClause} FROM Inserted)," + Environment.NewLine);
            code.Append($"        (SELECT * FROM Inserted FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)," + Environment.NewLine);
            code.Append($"        (SELECT * FROM Deleted FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)" + Environment.NewLine);
            code.Append($"     )" + Environment.NewLine);
            code.Append("END" + Environment.NewLine);
            form.GeneratedCodeText = code.ToString();
            form.Show();
        }
        #endregion

        #region Domain
        private void DomainGenerateEntityButton_Click(object sender, EventArgs e)
        {
            StringBuilder code = new StringBuilder("");
            List<SQLTableFieldModel> fieldList;
            KeyValueItem item;
            GeneratedCodeForm form = new GeneratedCodeForm();

            code.Append($"namespace {DomainNamespaceTextBox.Text}.{EntitiesNamespaceSuffixTextBox.Text}" + Environment.NewLine);
            code.Append("{" + Environment.NewLine);
            code.Append($"    public class {DomainEntityNameTextBox.Text}Entity");
            if (!string.IsNullOrWhiteSpace(EntityInherithsFromTextBox.Text))
            {
                code.Append($" : {EntityInherithsFromTextBox.Text}" + Environment.NewLine);
            }
            else
            {
                code.Append(Environment.NewLine);
            }
            code.Append("    {" + Environment.NewLine);
            item = (KeyValueItem)tablesListBox.SelectedItem;
            fieldList = _dbUtil.GetTableFields(connectionString, Convert.ToInt32(item.Value), item.Text);
            if (fieldList != null)
            {
                foreach (var field in fieldList)
                {
                    code.Append($"         public {MapSQLTypeToCType(field)}" + (field.isnullable == 1 ? "?" : "") + $" {field.name}" + " {get;set;}");
                    if (MapSQLTypeToCType(field) == "string" && field.isnullable == 0)
                    {
                        code.Append(" = string.Empty;");
                    }
                    code.Append(Environment.NewLine);
                }
            }
            code.Append("    }" + Environment.NewLine);
            code.Append("}" + Environment.NewLine);
            form.GeneratedCodeText = code.ToString();
            form.Show();
        }

        private void DomainEnumsOperationResultCodesButton_Click(object sender, EventArgs e)
        {
            StringBuilder code = new StringBuilder("");
            GeneratedCodeForm form = new GeneratedCodeForm();

            code.Append($"namespace {DomainNamespaceTextBox.Text}.Enums" + Environment.NewLine);
            code.Append("{" + Environment.NewLine);
            code.Append("       public enum OperationResultCodes" + Environment.NewLine);
            code.Append("       {" + Environment.NewLine);
            code.Append("           OK=200," + Environment.NewLine);
            code.Append("           CREATED=201," + Environment.NewLine);
            code.Append("           ACCEPTED = 202," + Environment.NewLine);
            code.Append("           BAD_REQUEST = 400," + Environment.NewLine);
            code.Append("           NOT_AUTHORIZED =401," + Environment.NewLine);
            code.Append("           FORBIDDEN=403," + Environment.NewLine);
            code.Append("           NOT_FOUND=404," + Environment.NewLine);
            code.Append("           DUPLICATE=409," + Environment.NewLine);
            code.Append("           SERVER_ERROR=500" + Environment.NewLine);
            code.Append("       }" + Environment.NewLine);
            code.Append("}" + Environment.NewLine);
            form.GeneratedCodeText = code.ToString();
            form.Show();
        }

        private void DomainCommonsOperationResultModelButton_Click(object sender, EventArgs e)
        {
            StringBuilder code = new StringBuilder("");
            GeneratedCodeForm form = new GeneratedCodeForm();

            code.Append($"namespace {DomainNamespaceTextBox.Text}.Commons" + Environment.NewLine);
            code.Append("{" + Environment.NewLine);
            code.Append("       public class  OperationResultModel<T>" + Environment.NewLine);
            code.Append("       {" + Environment.NewLine);
            code.Append("           public OperationResultCodes code { get; set; }" + Environment.NewLine);
            code.Append("           public string message { get; set; } = string.Empty;" + Environment.NewLine);
            code.Append("           public T? payload { get; set; }" + Environment.NewLine);
            code.Append("       }" + Environment.NewLine);
            code.Append("}" + Environment.NewLine);
            form.GeneratedCodeText = code.ToString();
            form.Show();
        }

        private void DomainCommonsOperationStatusModelButton_Click(object sender, EventArgs e)
        {
            StringBuilder code = new StringBuilder("");
            GeneratedCodeForm form = new GeneratedCodeForm();

            code.Append($"namespace {DomainNamespaceTextBox.Text}.Commons" + Environment.NewLine);
            code.Append("{" + Environment.NewLine);
            code.Append("       public class  OperationStatusModel" + Environment.NewLine);
            code.Append("       {" + Environment.NewLine);
            code.Append("           public OperationResultCodes code { get; set; }" + Environment.NewLine);
            code.Append("           public string message { get; set; } = string.Empty;" + Environment.NewLine);
            code.Append("       }" + Environment.NewLine);
            code.Append("}" + Environment.NewLine);
            form.GeneratedCodeText = code.ToString();
            form.Show();
        }
        #endregion

        #region Application

        private void ApplicationRepositoryInterfaceButton_Click(object sender, EventArgs e)
        {
            StringBuilder code = new StringBuilder("");
            StringBuilder parametersKey = new StringBuilder("");
            StringBuilder parametersInsert = new StringBuilder("");
            StringBuilder parametersUpdate = new StringBuilder("");

            GeneratedCodeForm form = new GeneratedCodeForm();
            int KeyCount;
            List<SQLTableFieldModel> fieldList;
            KeyValueItem item;
            item = (KeyValueItem)tablesListBox.SelectedItem;
            fieldList = _dbUtil.GetTableFields(connectionString, Convert.ToInt32(item.Value), item.Text);
            if (fieldList != null)
            {
                foreach (var field in fieldList)
                {
                    if (field.isPrimaryKey)
                    {
                        if (parametersKey.ToString() != "")
                        {
                            parametersKey.Append(",");
                        }
                        parametersKey.Append($"{MapSQLTypeToCType(field)} {field.name}");
                    }
                    //todo dates
                    if (!field.isIdentity)
                    {
                        if (parametersInsert.ToString() != "")
                        {
                            parametersInsert.Append(",");
                        }
                        parametersInsert.Append($"{MapSQLTypeToCType(field)} {field.name}");

                        if (parametersUpdate.ToString() != "")
                        {
                            parametersUpdate.Append(",");
                        }
                        parametersUpdate.Append($"{MapSQLTypeToCType(field)} {field.name}");
                    }
                }
            }
            code.Append($"namespace {ApplicationNamespaceTextBox.Text}.Commons.Interfaces.Repositories" + Environment.NewLine);
            code.Append("{" + Environment.NewLine);
            code.Append($"       public interface I{ApplicationEntityNameTextBox.Text}Repository" + Environment.NewLine);
            code.Append("       {" + Environment.NewLine);
            KeyCount = (from oneField in fieldList
                        where oneField.isPrimaryKey
                        select oneField).Count();
            if (KeyCount != 0)
            {
                code.Append($"           public Task<(bool,{ApplicationEntityNameTextBox.Text}Entity?)> GetByPrimaryKeyAsync(" + parametersKey + ");" + Environment.NewLine);
            }
            code.Append("" + Environment.NewLine);
            code.Append($"           public Task<(bool,List<{ApplicationEntityNameTextBox.Text}Entity>)> GetAllAsync();" + Environment.NewLine);
            code.Append("" + Environment.NewLine);
            code.Append($"           public Task<(bool,int)> InsertAsync(" + parametersInsert + ");" + Environment.NewLine);
            code.Append("" + Environment.NewLine);
            code.Append($"           public Task<(bool,int)> UpdateAsync(" + parametersUpdate + ");" + Environment.NewLine);
            code.Append("" + Environment.NewLine);
            if (KeyCount != 0)
            {
                code.Append($"           public Task<(bool,int)> DeleteByPrimaryKeyAsync(" + parametersKey + ");" + Environment.NewLine);
            }
            code.Append(Environment.NewLine);
            code.Append("       }" + Environment.NewLine);
            code.Append("}" + Environment.NewLine);
            form.GeneratedCodeText = code.ToString();
            form.Show();
        }


        private void ApplicationManagerInterfaceButton_Click(object sender, EventArgs e)
        {
            StringBuilder code = new StringBuilder("");
            StringBuilder parametersKey = new StringBuilder("");
            StringBuilder parametersInsert = new StringBuilder("");
            StringBuilder parametersUpdate = new StringBuilder("");

            GeneratedCodeForm form = new GeneratedCodeForm();
            int KeyCount;
            List<SQLTableFieldModel> fieldList;
            KeyValueItem item;
            item = (KeyValueItem)tablesListBox.SelectedItem;
            fieldList = _dbUtil.GetTableFields(connectionString, Convert.ToInt32(item.Value), item.Text);
            if (fieldList != null)
            {
                foreach (var field in fieldList)
                {
                    if (field.isPrimaryKey)
                    {
                        if (parametersKey.ToString() != "")
                        {
                            parametersKey.Append(",");
                        }
                        parametersKey.Append($"{MapSQLTypeToCType(field)} {field.name}");
                    }
                    //todo dates
                    if (!field.isIdentity)
                    {
                        if (parametersInsert.ToString() != "")
                        {
                            parametersInsert.Append(",");
                        }
                        parametersInsert.Append($"{MapSQLTypeToCType(field)} {field.name}");

                        if (parametersUpdate.ToString() != "")
                        {
                            parametersUpdate.Append(",");
                        }
                        parametersUpdate.Append($"{MapSQLTypeToCType(field)} {field.name}");
                    }
                }
            }
            code.Append($"namespace {ApplicationNamespaceTextBox.Text}.Commons.Interfaces.BLL" + Environment.NewLine);
            code.Append("{" + Environment.NewLine);
            code.Append($"       public interface I{ApplicationEntityNameTextBox.Text}Manager" + Environment.NewLine);
            code.Append("       {" + Environment.NewLine);
            KeyCount = (from oneField in fieldList
                        where oneField.isPrimaryKey
                        select oneField).Count();
            if (KeyCount != 0)
            {
                code.Append($"           public Task<(OperationStatusModel,{ApplicationEntityNameTextBox.Text}Entity?)> GetByPrimaryKeyAsync(" + parametersKey + ");" + Environment.NewLine);
            }
            code.Append("" + Environment.NewLine);
            code.Append($"           public Task<(OperationStatusModel,List<{ApplicationEntityNameTextBox.Text}Entity>)> GetAllAsync();" + Environment.NewLine);
            code.Append("" + Environment.NewLine);
            code.Append($"           public Task<(OperationStatusModel)> InsertAsync(" + parametersInsert + ");" + Environment.NewLine);
            code.Append("" + Environment.NewLine);
            code.Append($"           public Task<(OperationStatusModel)> UpdateAsync(" + parametersUpdate + ");" + Environment.NewLine);
            code.Append("" + Environment.NewLine);
            if (KeyCount != 0)
            {
                code.Append($"           public Task<(OperationStatusModel)> DeleteByPrimaryKeyAsync(" + parametersKey + ");" + Environment.NewLine);
            }
            code.Append(Environment.NewLine);
            code.Append("       }" + Environment.NewLine);
            code.Append("}" + Environment.NewLine);
            form.GeneratedCodeText = code.ToString();
            form.Show();
        }

        private void ApplicationGlobalVariablesBLLAndConstantsButton_Click(object sender, EventArgs e)
        {
            StringBuilder code = new StringBuilder("");
            GeneratedCodeForm form = new GeneratedCodeForm();

            code.Append($"namespace {ApplicationNamespaceTextBox.Text}.BLL" + Environment.NewLine);
            code.Append("{" + Environment.NewLine);
            code.Append("       public static class GlobalVariables" + Environment.NewLine);
            code.Append("       {" + Environment.NewLine);
            code.Append("           public static bool EnableTraceTime { get; set; } = true;" + Environment.NewLine);
            code.Append("       }" + Environment.NewLine);
            code.Append("}" + Environment.NewLine);
            form.GeneratedCodeText = code.ToString();
            form.Show();
        }

        private void ApplicationCommonsAttributeTraceAndTimeButton_Click(object sender, EventArgs e)
        {
            StringBuilder code = new StringBuilder("");
            GeneratedCodeForm form = new GeneratedCodeForm();

            code.Append($"namespace {ApplicationNamespaceTextBox.Text}.Commons.Attributes" + Environment.NewLine);
            code.Append("{" + Environment.NewLine);
            code.Append("       [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]" + Environment.NewLine);
            code.Append("       public class TraceAndTimeAttribute:Attribute" + Environment.NewLine);
            code.Append("       {" + Environment.NewLine);
            code.Append(Environment.NewLine);
            code.Append("       }" + Environment.NewLine);
            code.Append("}" + Environment.NewLine);
            form.GeneratedCodeText = code.ToString();
            form.Show();
        }

        private void ApplicationCommonsAttributeTraceAndTimeInterceptorButton_Click(object sender, EventArgs e)
        {
            StringBuilder code = new StringBuilder("");
            GeneratedCodeForm form = new GeneratedCodeForm();
            code.Append("using AutoMapper.Configuration.Annotations;" + Environment.NewLine);
            code.Append("using Castle.Core.Logging;" + Environment.NewLine);
            code.Append("using Castle.DynamicProxy;" + Environment.NewLine);
            code.Append("using Microsoft.Extensions.Logging;" + Environment.NewLine);
            code.Append("using System;" + Environment.NewLine);
            code.Append("using System.Collections.Generic;" + Environment.NewLine);
            code.Append("using System.Linq;" + Environment.NewLine);
            code.Append("using System.Text;" + Environment.NewLine);
            code.Append("using System.Threading.Tasks;" + Environment.NewLine);
            code.Append($"using {ApplicationNamespaceTextBox.Text}.BLL" + Environment.NewLine);
            code.Append(Environment.NewLine);
            code.Append($"namespace {ApplicationNamespaceTextBox.Text}.Commons.Attributes" + Environment.NewLine);
            code.Append("{" + Environment.NewLine);
            code.Append("       public class TraceAndTimeAttibuteInterceptor : IInterceptor" + Environment.NewLine);
            code.Append("       {" + Environment.NewLine);
            code.Append("           private readonly ILogger<TraceAndTimeAttibuteInterceptor> _logger;" + Environment.NewLine);
            code.Append(Environment.NewLine);
            code.Append("           public TraceAndTimeAttibuteInterceptor(ILogger<TraceAndTimeAttibuteInterceptor> logger)" + Environment.NewLine);
            code.Append("           {" + Environment.NewLine);
            code.Append("               _logger = logger;" + Environment.NewLine);
            code.Append("           }" + Environment.NewLine);
            code.Append(Environment.NewLine);
            code.Append("           public void Intercept(IInvocation invocation)" + Environment.NewLine);
            code.Append("           {" + Environment.NewLine);
            code.Append("               string methodName=string.Empty();" + Environment.NewLine);
            code.Append("               if (invocation.Method.ReflectedType is not null " + Environment.NewLine);
            code.Append("                   && invocation.Method.ReflectedType.Name is not null)" + Environment.NewLine);
            code.Append("               {" + Environment.NewLine);
            code.Append("                   methodName = invocation.Method.ReflectedType.Name + \".\";" + Environment.NewLine);
            code.Append("               }" + Environment.NewLine);
            code.Append("               methodName = methodName+invocation.Method.Name;" + Environment.NewLine);
            code.Append("               System.Diagnostics.Stopwatch? watch=null;" + Environment.NewLine);
            code.Append("               try" + Environment.NewLine);
            code.Append("               {" + Environment.NewLine);
            code.Append("                   _logger.LogInformation(\"Enter to method:{@methodName}\", methodName);" + Environment.NewLine);
            code.Append("                   if (GlobalVariables.EnableTraceTime)" + Environment.NewLine);
            code.Append("                   {" + Environment.NewLine);
            code.Append("                       watch = System.Diagnostics.Stopwatch.StartNew();" + Environment.NewLine);
            code.Append("                       watch.Start();" + Environment.NewLine);
            code.Append("                   }" + Environment.NewLine);
            code.Append("                   invocation.Proceed();" + Environment.NewLine);
            code.Append("               }" + Environment.NewLine);
            code.Append("               catch (Exception ex)" + Environment.NewLine);
            code.Append("               {" + Environment.NewLine);
            code.Append("                   _logger.LogError(ex, \"Exception in method:{@methodName}\", methodName);" + Environment.NewLine);
            code.Append("               }" + Environment.NewLine);
            code.Append("               finally" + Environment.NewLine);
            code.Append("               {" + Environment.NewLine);
            code.Append("                   if (GlobalVariables.EnableTraceTime)" + Environment.NewLine);
            code.Append("                   {" + Environment.NewLine);
            code.Append("                       watch!.Stop();" + Environment.NewLine);
            code.Append("                       var elapsedMs = watch.ElapsedMilliseconds;" + Environment.NewLine);
            code.Append("                       _logger.LogInformation(\"Time taken for method:{@methodName} @{elapsedMs} ms\", methodName, elapsedMs);" + Environment.NewLine);
            code.Append("                   }" + Environment.NewLine);
            code.Append("                   _logger.LogInformation(\"Exit from method:{@methodName}\", methodName);" + Environment.NewLine);
            code.Append("               }" + Environment.NewLine);
            code.Append("           }" + Environment.NewLine);
            code.Append("       }" + Environment.NewLine);
            code.Append("}" + Environment.NewLine);

            form.GeneratedCodeText = code.ToString();
            form.Show();
        }

        private void ApplicationCommonsInterfacesInfrastructureDatabaseHelperButton_Click(object sender, EventArgs e)
        {
            StringBuilder code = new StringBuilder("");
            GeneratedCodeForm form = new GeneratedCodeForm();

            code.Append($"namespace {ApplicationNamespaceTextBox.Text}.Commons.Interfaces.Infrastructure" + Environment.NewLine);
            code.Append("{" + Environment.NewLine);
            code.Append("       public interface IDatabaseHelper" + Environment.NewLine);
            code.Append("       {" + Environment.NewLine);
            code.Append("           public Task<IEnumerable<T>> GetArrayDataAsync<T,U>( string command, U parameters,string currentUser=\"\");" + Environment.NewLine);
            code.Append("           public Task DoCommandAsync<U>( string command, U parameters, string currentUser = \"\"););" + Environment.NewLine);
            code.Append("       }" + Environment.NewLine);
            code.Append("}" + Environment.NewLine);
            form.GeneratedCodeText = code.ToString();
            form.Show();
        }

        private void ApplicationCommonsInterfacesUtilsReverseHashButton_Click(object sender, EventArgs e)
        {
            StringBuilder code = new StringBuilder("");
            GeneratedCodeForm form = new GeneratedCodeForm();

            code.Append($"namespace {ApplicationNamespaceTextBox.Text}.Commons.Interfaces.Utils" + Environment.NewLine);
            code.Append("{" + Environment.NewLine);
            code.Append("       public interface IReverseHash" + Environment.NewLine);
            code.Append("       {" + Environment.NewLine);
            code.Append("           public void Init(string salt);" + Environment.NewLine);
            code.Append("           public string Encode(int dataToEncode);" + Environment.NewLine);
            code.Append("           public int[]  Decode(string dataToDecode);" + Environment.NewLine);
            code.Append("       }" + Environment.NewLine);
            code.Append("}" + Environment.NewLine);
            form.GeneratedCodeText = code.ToString();
            form.Show();
        }

        private void ApplicatioConfigureServicesButton_Click(object sender, EventArgs e)
        {
            StringBuilder code = new StringBuilder("");
            GeneratedCodeForm form = new GeneratedCodeForm();
            code.Append("using FluentValidation;" + Environment.NewLine);
            code.Append("using Microsoft.Extensions.DependencyInjection;" + Environment.NewLine);
            code.Append("using System.Reflection;" + Environment.NewLine);
            code.Append("using MediatR;" + Environment.NewLine);
            code.Append("using System;" + Environment.NewLine);
            code.Append("using AutoMapper;" + Environment.NewLine);
            code.Append("using Castle.DynamicProxy;" + Environment.NewLine);
            code.Append(Environment.NewLine);

            code.Append($"namespace {ApplicationNamespaceTextBox.Text}" + Environment.NewLine);
            code.Append("{" + Environment.NewLine);
            code.Append("       public static class ConfigureServices" + Environment.NewLine);
            code.Append("       {" + Environment.NewLine);
            code.Append($"           public static IServiceCollection Add{ApplicationNamespaceTextBox.Text}Services(this IServiceCollection services)" + Environment.NewLine);
            code.Append("           {" + Environment.NewLine);
            code.Append("               services.AddAutoMapper(Assembly.GetExecutingAssembly());" + Environment.NewLine);
            code.Append("               services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());" + Environment.NewLine);
            code.Append("               services.AddMediatR(cfg =>" + Environment.NewLine);
            code.Append("               {" + Environment.NewLine);
            code.Append("                   cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());" + Environment.NewLine);
            code.Append("               });" + Environment.NewLine);
            code.Append("               return services;" + Environment.NewLine);
            code.Append("           }" + Environment.NewLine);
            code.Append(Environment.NewLine);
            code.Append(Environment.NewLine);
            code.Append("           //Website" + Environment.NewLine);
            code.Append("           //http://codethug.com/2021/03/17/Caching-with-Attributes-in-DotNet-Core5/" + Environment.NewLine);
            code.Append("           //service collection extension" + Environment.NewLine);
            code.Append("           public static void AddProxiedTransient<TInterface, TImplementation>" + Environment.NewLine);
            code.Append("                   (this IServiceCollection services)" + Environment.NewLine);
            code.Append("                   where TInterface : class" + Environment.NewLine);
            code.Append("                   where TImplementation : class, TInterface" + Environment.NewLine);
            code.Append("           {" + Environment.NewLine);
            code.Append("               // This registers the underlying class" + Environment.NewLine);
            code.Append("               services.AddTransient<TImplementation>();" + Environment.NewLine);
            code.Append("               services.AddTransient(typeof(TInterface), serviceProvider =>" + Environment.NewLine);
            code.Append("               {" + Environment.NewLine);
            code.Append("                   // Get an instance of the Castle Proxy Generator" + Environment.NewLine);
            code.Append("                   var proxyGenerator = serviceProvider" + Environment.NewLine);
            code.Append("                            .GetRequiredService<ProxyGenerator>();" + Environment.NewLine);
            code.Append("                   // Have DI build out an instance of the class that has methods" + Environment.NewLine);
            code.Append("                   // you want to cache (this is a normal instance of that class " + Environment.NewLine);
            code.Append("                   // without caching added)" + Environment.NewLine);
            code.Append("                   var actual = serviceProvider" + Environment.NewLine);
            code.Append("                       .GetRequiredService<TImplementation>();" + Environment.NewLine);
            code.Append("                   // Find all of the interceptors that have been registered," + Environment.NewLine);
            code.Append("                   // including our caching interceptor.  (you might later add a " + Environment.NewLine);
            code.Append("                   // logging interceptor, etc.)" + Environment.NewLine);
            code.Append("                   var interceptors = serviceProvider" + Environment.NewLine);
            code.Append("                       .GetServices<IInterceptor>().ToArray();" + Environment.NewLine);
            code.Append("                   // Have Castle Proxy build out a proxy object that implements " + Environment.NewLine);
            code.Append("                   // your interface, but adds a caching layer on top of the" + Environment.NewLine);
            code.Append("                   // actual implementation of the class.  This proxy object is" + Environment.NewLine);
            code.Append("                   // what will then get injected into the class that has a" + Environment.NewLine);
            code.Append("                   // dependency on TInterface" + Environment.NewLine);
            code.Append("                   return proxyGenerator.CreateInterfaceProxyWithTarget(" + Environment.NewLine);
            code.Append("                           typeof(TInterface), actual, interceptors);" + Environment.NewLine);
            code.Append("               });" + Environment.NewLine);
            code.Append("           }" + Environment.NewLine);
            code.Append("       }" + Environment.NewLine);
            code.Append("}" + Environment.NewLine);
            form.GeneratedCodeText = code.ToString();
            form.Show();
        }
        #endregion

        #region ConfigChange
        private void AuthorTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!bolLoading)
            {
                GeneratorConfigurationManager._gConfig.Author = AuthorTextBox.Text.Trim();
                GeneratorConfigurationManager.SaveToFile();
            }
        }

        private void DomainNamespaceTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!bolLoading)
            {
                GeneratorConfigurationManager._gConfig.DomainNamespace = DomainNamespaceTextBox.Text.Trim();
                GeneratorConfigurationManager.SaveToFile();
            }
        }

        private void EntitiesNamespaceSuffixTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!bolLoading)
            {
                GeneratorConfigurationManager._gConfig.EntitiesSuffixNamespace = EntitiesNamespaceSuffixTextBox.Text.Trim();
                GeneratorConfigurationManager.SaveToFile();
            }
        }

        private void EntityInherithsFromTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!bolLoading)
            {
                GeneratorConfigurationManager._gConfig.EntitiesInheritsFrom = EntityInherithsFromTextBox.Text.Trim();
                GeneratorConfigurationManager.SaveToFile();
            }
        }

        private void ApplicationNamespaceTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!bolLoading)
            {
                GeneratorConfigurationManager._gConfig.ApplicationNamespace = ApplicationNamespaceTextBox.Text.Trim();
                GeneratorConfigurationManager.SaveToFile();
            }
        }

        #endregion
    }
}