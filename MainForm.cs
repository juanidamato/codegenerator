using codegenerator.BLL;
using codegenerator.Models;
using codegenerator.Models.ViewModels;
using System.Diagnostics;
using System.Drawing;
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
            if (bolLoading)
            {
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

        }

        private void SetupNewGrid()
        {
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
        }

        private void SetupOtherValues()
        {
            EntityNameTextBox.Text = ((KeyValueItem)tablesListBox.SelectedItem).Text;
        }

        private void populateTables()
        {
            List<SQLTableModel> tableList = null;
            tableList = _dbUtil.GetDatabaseTables(connectionString);
            if (tableList == null)
            {
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
        }

        private void tablesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<SQLTableFieldModel> fieldList;
            KeyValueItem item;

            SetupNewGrid();
            SetupOtherValues();
            item = (KeyValueItem)tablesListBox.SelectedItem;
            fieldList = _dbUtil.GetTableFields(connectionString, Convert.ToInt32(item.Value), item.Text);
            if (fieldList != null)
            {
                foreach (var field in fieldList)
                {
                    dataGridView1.Rows.Add(
                        field.isPrimaryKey ? "*" + field.name : field.name,
                        field.fieldType,
                        CalculateFieldSizeString(field),
                        field.isnullable == 0 ? false : true,
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

        private void GenerateEntityButton_Click(object sender, EventArgs e)
        {
            string code;
            List<SQLTableFieldModel> fieldList;
            KeyValueItem item;
            GeneratedCodeForm form = new GeneratedCodeForm();

            code = $"namespace {GeneralNamespaceTextBox.Text}.{EntitiesNamespaceSuffixTextBox.Text}" + Environment.NewLine;
            code = code + "{" + Environment.NewLine;
            code = code + $"    public class {EntityNameTextBox.Text}";
            if (!string.IsNullOrWhiteSpace(EntityInherithsFromTextBox.Text))
            {
                code = code + $" : {EntityInherithsFromTextBox.Text}" + Environment.NewLine;
            }
            else
            {
                code = code + Environment.NewLine;
            }
            code = code + "    {" + Environment.NewLine;
            item = (KeyValueItem)tablesListBox.SelectedItem;
            fieldList = _dbUtil.GetTableFields(connectionString, Convert.ToInt32(item.Value), item.Text);
            if (fieldList != null)
            {
                foreach (var field in fieldList)
                {
                    code = code + $"         public {MapSQLTypeToCType(field)}" + (field.isnullable == 1 ? "?" : "") + $" {field.name}" + " {get;set;}" + Environment.NewLine;
                }
            }
            code = code + "    }" + Environment.NewLine;
            code = code + "}" + Environment.NewLine;
            form.GeneratedCodeText = code;
            form.Show();
        }

        private void spSelectAllButton_Click(object sender, EventArgs e)
        {
            string code;
            string orderby;
            List<SQLTableFieldModel> fieldList;
            KeyValueItem item;
            GeneratedCodeForm form = new GeneratedCodeForm();
            DateTime Today = DateTime.Now;
            item = (KeyValueItem)tablesListBox.SelectedItem;
            fieldList = _dbUtil.GetTableFields(connectionString, Convert.ToInt32(item.Value), item.Text);
            orderby = "";
            if (fieldList != null)
            {
                foreach (var field in fieldList)
                {
                    if (field.isPrimaryKey)
                    {
                        if (orderby != "")
                        {
                            orderby = orderby + ",";
                        }
                        orderby = orderby + $"{field.name}";
                    }
                }
            }
            code = "SET ANSI_NULLS ON" + Environment.NewLine;
            code = code + "GO" + Environment.NewLine;
            code = code + "SET QUOTED_IDENTIFIER ON" + Environment.NewLine;
            code = code + "GO" + Environment.NewLine;
            code = code + "-- =============================================" + Environment.NewLine;
            code = code + "-- Author:" + AuthorTextBox.Text.Trim() + Environment.NewLine;
            code = code + "-- Create date:" + Today.Year.ToString() + "-" + Today.Month.ToString("00") + "-" + Today.Day.ToString("00") + Environment.NewLine;
            code = code + "-- Description:Select all records from table " + item.Text + Environment.NewLine;
            code = code + "-- Generated code by juanidamato/codegenerator" + Environment.NewLine;
            code = code + $"CREATE PROCEDURE [dbo].[{item.Text}_Select_All]" + Environment.NewLine;
            code = code + "AS" + Environment.NewLine;
            code = code + "BEGIN" + Environment.NewLine;
            code = code + "" + Environment.NewLine;
            code = code + "     SET NOCOUNT OFF;" + Environment.NewLine;
            code = code + "" + Environment.NewLine;
            code = code + $"     select * from [{item.Text}]" + Environment.NewLine;
            if (orderby != "")
            {
                code = code + "     order by " + orderby + Environment.NewLine;
            }
            code = code + "END" + Environment.NewLine;
            form.GeneratedCodeText = code;
            form.Show();
        }

        private void spSelectByPKButton_Click(object sender, EventArgs e)
        {
            string code;
            string parametersKey;
            string whereClause = "";
            int KeyCount;
            List<SQLTableFieldModel> fieldList;
            KeyValueItem item;
            GeneratedCodeForm form = new GeneratedCodeForm();
            DateTime Today = DateTime.Now;
            item = (KeyValueItem)tablesListBox.SelectedItem;
            fieldList = _dbUtil.GetTableFields(connectionString, Convert.ToInt32(item.Value), item.Text);
            parametersKey = "";
            whereClause = "     where ";
            if (fieldList != null)
            {
                foreach (var field in fieldList)
                {
                    if (field.isPrimaryKey)
                    {
                        if (parametersKey != "")
                        {
                            parametersKey = parametersKey + "," + Environment.NewLine;
                        }
                        parametersKey = parametersKey + $"      @{field.name} {CalculateFieldTypeString(field)}";
                        if (whereClause != "     where ")
                        {
                            whereClause = whereClause + Environment.NewLine;
                            whereClause = whereClause + "     and ";
                        }
                        whereClause = whereClause + $"{field.name}=@{field.name}";
                    }
                }
                parametersKey = parametersKey + Environment.NewLine;
            }
            KeyCount = (from oneField in fieldList
                        where oneField.isPrimaryKey == true
                        select oneField).Count();
            if (KeyCount == 0)
            {
                MessageBox.Show("This table does not contains primary keys", "Error", MessageBoxButtons.OK);
                return;
            }
            code = "SET ANSI_NULLS ON" + Environment.NewLine;
            code = code + "GO" + Environment.NewLine;
            code = code + "SET QUOTED_IDENTIFIER ON" + Environment.NewLine;
            code = code + "GO" + Environment.NewLine;
            code = code + "-- =============================================" + Environment.NewLine;
            code = code + "-- Author:" + AuthorTextBox.Text.Trim() + Environment.NewLine;
            code = code + "-- Create date:" + Today.Year.ToString() + "-" + Today.Month.ToString("00") + "-" + Today.Day.ToString("00") + Environment.NewLine;
            code = code + "-- Description:Select one record by primary key from table " + item.Text + Environment.NewLine;
            code = code + "-- Generated code by juanidamato/codegenerator" + Environment.NewLine;
            code = code + $"CREATE PROCEDURE [dbo].[{item.Text}_Select_ByPK]" + Environment.NewLine;
            code = code + parametersKey;
            code = code + "AS" + Environment.NewLine;
            code = code + "BEGIN" + Environment.NewLine;
            code = code + "" + Environment.NewLine;
            code = code + "     SET NOCOUNT OFF;" + Environment.NewLine;
            code = code + "" + Environment.NewLine;
            code = code + $"     select * from [{item.Text}]" + Environment.NewLine;
            code = code + $"{whereClause}" + Environment.NewLine;
            code = code + "END" + Environment.NewLine;
            form.GeneratedCodeText = code;
            form.Show();
        }

        private void spInsertButton_Click(object sender, EventArgs e)
        {
            string code;
            string parametersInsert;
            List<SQLTableFieldModel> fieldList;
            KeyValueItem item;
            GeneratedCodeForm form = new GeneratedCodeForm();
            DateTime Today = DateTime.Now;
            item = (KeyValueItem)tablesListBox.SelectedItem;
            fieldList = _dbUtil.GetTableFields(connectionString, Convert.ToInt32(item.Value), item.Text);
            parametersInsert = "";
            if (fieldList != null)
            {
                foreach (var field in fieldList)
                {
                    //todo datefields
                    if (!field.isIdentity)
                    {
                        if (parametersInsert != "")
                        {
                            parametersInsert = parametersInsert + "," + Environment.NewLine;
                        }
                        parametersInsert = parametersInsert + $"      @{field.name} {CalculateFieldTypeString(field)}";

                    }
                }
                parametersInsert = parametersInsert + Environment.NewLine;
            }
            code = "SET ANSI_NULLS ON" + Environment.NewLine;
            code = code + "GO" + Environment.NewLine;
            code = code + "SET QUOTED_IDENTIFIER ON" + Environment.NewLine;
            code = code + "GO" + Environment.NewLine;
            code = code + "-- =============================================" + Environment.NewLine;
            code = code + "-- Author:" + AuthorTextBox.Text.Trim() + Environment.NewLine;
            code = code + "-- Create date:" + Today.Year.ToString() + "-" + Today.Month.ToString("00") + "-" + Today.Day.ToString("00") + Environment.NewLine;
            code = code + "-- Description:Insert one record into table " + item.Text + Environment.NewLine;
            code = code + "-- Generated code by juanidamato/codegenerator" + Environment.NewLine;
            code = code + $"CREATE PROCEDURE [dbo].[{item.Text}_Insert]" + Environment.NewLine;
            code = code + "AS" + Environment.NewLine;
            code = code + "BEGIN" + Environment.NewLine;
            code = code + parametersInsert;
            code = code + "" + Environment.NewLine;
            code = code + "     SET NOCOUNT ON;" + Environment.NewLine;
            code = code + "" + Environment.NewLine;
            code = code + $"     insert into [{item.Text}]" + Environment.NewLine;
            code = code + $"     (" + Environment.NewLine;
            code = code + $"     )" + Environment.NewLine;
            code = code + $"     values" + Environment.NewLine;
            code = code + $"     (" + Environment.NewLine;
            code = code + $"     )" + Environment.NewLine;
            code = code + Environment.NewLine;
            code = code + "     SET NOCOUNT OFF;" + Environment.NewLine;
            code = code + Environment.NewLine;
            code = code + $"     IF @@ROWCOUNT=1" + Environment.NewLine;
            code = code + $"     BEGIN" + Environment.NewLine;
            code = code + $"        select 1 as 'R'" + Environment.NewLine;
            code = code + $"     END" + Environment.NewLine;
            code = code + $"     ELSE" + Environment.NewLine;
            code = code + $"     BEGIN" + Environment.NewLine;
            code = code + $"        select -1 as 'R'" + Environment.NewLine;
            code = code + $"     END" + Environment.NewLine;
            code = code + "END" + Environment.NewLine;
            form.GeneratedCodeText = code;
            form.Show();
        }

        private void spUpdateByPKButton_Click(object sender, EventArgs e)
        {
            string code;
            string parametersUpdate;
            string parametersSet;
            string whereClause = "";
            int KeyCount;
            List<SQLTableFieldModel> fieldList;
            KeyValueItem item;
            GeneratedCodeForm form = new GeneratedCodeForm();
            DateTime Today = DateTime.Now;
            item = (KeyValueItem)tablesListBox.SelectedItem;
            fieldList = _dbUtil.GetTableFields(connectionString, Convert.ToInt32(item.Value), item.Text);
            parametersUpdate = "";
            parametersSet = "";
            whereClause = "     where ";
            if (fieldList != null)
            {
                foreach (var field in fieldList)
                {
                    //todo datefields
                    if (parametersUpdate != "")
                    {
                        parametersUpdate = parametersUpdate + "," + Environment.NewLine;
                    }
                    parametersUpdate = parametersUpdate + $"      @{field.name} {CalculateFieldTypeString(field)}";
                    if (field.isPrimaryKey)
                    {
                        if (whereClause != "     where ")
                        {
                            whereClause = whereClause + Environment.NewLine;
                            whereClause = whereClause + "     and ";
                        }
                        whereClause = whereClause + $"{field.name}=@{field.name}";
                    }
                    else
                    {
                        if (parametersSet != "")
                        {
                            parametersSet = parametersSet + ",";
                            parametersSet = parametersSet + Environment.NewLine;
                        }
                        parametersSet = parametersSet + $"          {field.name}=@{field.name}";
                    }
                }
                parametersUpdate = parametersUpdate + Environment.NewLine;
            }
            KeyCount = (from oneField in fieldList
                        where oneField.isPrimaryKey == true
                        select oneField).Count();
            if (KeyCount == 0)
            {
                MessageBox.Show("This table does not contains primary keys", "Error", MessageBoxButtons.OK);
                return;
            }
            code = "SET ANSI_NULLS ON" + Environment.NewLine;
            code = code + "GO" + Environment.NewLine;
            code = code + "SET QUOTED_IDENTIFIER ON" + Environment.NewLine;
            code = code + "GO" + Environment.NewLine;
            code = code + "-- =============================================" + Environment.NewLine;
            code = code + "-- Author:" + AuthorTextBox.Text.Trim() + Environment.NewLine;
            code = code + "-- Create date:" + Today.Year.ToString() + "-" + Today.Month.ToString("00") + "-" + Today.Day.ToString("00") + Environment.NewLine;
            code = code + "-- Description:Update one record by primary key from table " + item.Text + Environment.NewLine;
            code = code + "-- Generated code by juanidamato/codegenerator" + Environment.NewLine;
            code = code + $"CREATE PROCEDURE [dbo].[{item.Text}_Update_ByPK]" + Environment.NewLine;
            code = code + "AS" + Environment.NewLine;
            code = code + "BEGIN" + Environment.NewLine;
            code = code + parametersUpdate;
            code = code + "" + Environment.NewLine;
            code = code + "     SET NOCOUNT ON;" + Environment.NewLine;
            code = code + "" + Environment.NewLine;
            code = code + $"     update [{item.Text}]" + Environment.NewLine;
            code = code + $"     SET" + Environment.NewLine;
            code = code + $"{parametersSet}" + Environment.NewLine;
            code = code + $"{whereClause}" + Environment.NewLine;
            code = code + Environment.NewLine;
            code = code + "     SET NOCOUNT OFF;" + Environment.NewLine;
            code = code + Environment.NewLine;
            code = code + $"     IF @@ROWCOUNT=1" + Environment.NewLine;
            code = code + $"     BEGIN" + Environment.NewLine;
            code = code + $"        select 1 as 'R'" + Environment.NewLine;
            code = code + $"     END" + Environment.NewLine;
            code = code + $"     ELSE" + Environment.NewLine;
            code = code + $"     BEGIN" + Environment.NewLine;
            code = code + $"        select -1 as 'R'" + Environment.NewLine;
            code = code + $"     END" + Environment.NewLine;
            code = code + "END" + Environment.NewLine;
            form.GeneratedCodeText = code;
            form.Show();
        }

        private void spDeleteByPKButton_Click(object sender, EventArgs e)
        {
            string code;
            string parametersKey;
            string whereClause = "";
            int KeyCount;
            List<SQLTableFieldModel> fieldList;
            KeyValueItem item;
            GeneratedCodeForm form = new GeneratedCodeForm();
            DateTime Today = DateTime.Now;
            item = (KeyValueItem)tablesListBox.SelectedItem;
            fieldList = _dbUtil.GetTableFields(connectionString, Convert.ToInt32(item.Value), item.Text);
            parametersKey = "";
            whereClause = "     where ";
            if (fieldList != null)
            {
                foreach (var field in fieldList)
                {
                    if (field.isPrimaryKey)
                    {
                        if (parametersKey != "")
                        {
                            parametersKey = parametersKey + "," + Environment.NewLine;
                        }
                        parametersKey = parametersKey + $"      @{field.name} {CalculateFieldTypeString(field)}";
                        if (whereClause != "     where ")
                        {
                            whereClause = whereClause + Environment.NewLine;
                            whereClause = whereClause + "     and ";
                        }
                        whereClause = whereClause + $"{field.name}=@{field.name}";
                    }
                }
                parametersKey = parametersKey + Environment.NewLine;
            }
            KeyCount = (from oneField in fieldList
                        where oneField.isPrimaryKey == true
                        select oneField).Count();
            if (KeyCount == 0)
            {
                MessageBox.Show("This table does not contains primary keys", "Error", MessageBoxButtons.OK);
                return;
            }
            code = "SET ANSI_NULLS ON" + Environment.NewLine;
            code = code + "GO" + Environment.NewLine;
            code = code + "SET QUOTED_IDENTIFIER ON" + Environment.NewLine;
            code = code + "GO" + Environment.NewLine;
            code = code + "-- =============================================" + Environment.NewLine;
            code = code + "-- Author:" + AuthorTextBox.Text.Trim() + Environment.NewLine;
            code = code + "-- Create date:" + Today.Year.ToString() + "-" + Today.Month.ToString("00") + "-" + Today.Day.ToString("00") + Environment.NewLine;
            code = code + "-- Description:Delete one record by primary key from table " + item.Text + Environment.NewLine;
            code = code + "-- Generated code by juanidamato/codegenerator" + Environment.NewLine;
            code = code + $"CREATE PROCEDURE [dbo].[{item.Text}_Delete_ByPK]" + Environment.NewLine;
            code = code + parametersKey;
            code = code + "AS" + Environment.NewLine;
            code = code + "BEGIN" + Environment.NewLine;
            code = code + "" + Environment.NewLine;
            code = code + "     SET NOCOUNT OFF;" + Environment.NewLine;
            code = code + "" + Environment.NewLine;
            code = code + $"     delete [{item.Text}]" + Environment.NewLine;
            code = code + $"{whereClause}" + Environment.NewLine;
            code = code + Environment.NewLine;
            code = code + "     SET NOCOUNT OFF;" + Environment.NewLine;
            code = code + Environment.NewLine;
            code = code + $"     IF @@ROWCOUNT=1" + Environment.NewLine;
            code = code + $"     BEGIN" + Environment.NewLine;
            code = code + $"        select 1 as 'R'" + Environment.NewLine;
            code = code + $"     END" + Environment.NewLine;
            code = code + $"     ELSE" + Environment.NewLine;
            code = code + $"     BEGIN" + Environment.NewLine;
            code = code + $"        select -1 as 'R'" + Environment.NewLine;
            code = code + $"     END" + Environment.NewLine;
            code = code + "END" + Environment.NewLine;
            form.GeneratedCodeText = code;
            form.Show();
        }

        private void spAuditTableButton_Click(object sender, EventArgs e)
        {
            string code;
            GeneratedCodeForm form = new GeneratedCodeForm();
            DateTime Today = DateTime.Now;

            code = "SET ANSI_NULLS ON" + Environment.NewLine;
            code = code + "GO" + Environment.NewLine;
            code = code + "SET QUOTED_IDENTIFIER ON" + Environment.NewLine;
            code = code + "GO" + Environment.NewLine;
            code = code + $"CREATE TABLE [dbo].[AuditLogs]" + Environment.NewLine;
            code = code + "(" + Environment.NewLine;
            code = code + "     [IdAudit] [int] IDENTITY(1,1) NOT NULL," + Environment.NewLine;
            code = code + "     [InsertDate] [datetime] NOT NULL," + Environment.NewLine;
            code = code + "     [CurrentUser] [nvarchar](50) NOT NULL," + Environment.NewLine;
            code = code + "     [AuditTable] [nvarchar](50) NOT NULL," + Environment.NewLine;
            code = code + "     [Operation] [nvarchar](20) NOT NULL," + Environment.NewLine;
            code = code + "     [recordId] [nvarchar](20) NOT NULL," + Environment.NewLine;
            code = code + "     [NewData] [nvarchar](max) NULL," + Environment.NewLine;
            code = code + "     [OldData] [nvarchar](max) NULL," + Environment.NewLine;
            code = code + "     CONSTRAINT [PK_AuditLogs] PRIMARY KEY CLUSTERED " + Environment.NewLine;
            code = code + "     (" + Environment.NewLine;
            code = code + "         [IdAudit] ASC" + Environment.NewLine;
            code = code + "     )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]" + Environment.NewLine;
            code = code + ") ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]" + Environment.NewLine;
            code = code + "GO" + Environment.NewLine;
            form.GeneratedCodeText = code;
            form.Show();
        }

        private void spAuditInsertButton_Click(object sender, EventArgs e)
        {
            string code;
            string selectClause = "";
            int KeyCount;
            List<SQLTableFieldModel> fieldList;
            KeyValueItem item;
            GeneratedCodeForm form = new GeneratedCodeForm();
            DateTime Today = DateTime.Now;
            item = (KeyValueItem)tablesListBox.SelectedItem;
            fieldList = _dbUtil.GetTableFields(connectionString, Convert.ToInt32(item.Value), item.Text);
            KeyCount = (from oneField in fieldList
                        where oneField.isPrimaryKey == true
                        select oneField).Count();
            if (KeyCount !=1)
            {
                MessageBox.Show("This table does contains more than one primary key field", "Error", MessageBoxButtons.OK);
                return;
            }
            selectClause = "";
            if (fieldList != null)
            {
                foreach (var field in fieldList)
                {
                    if (field.isPrimaryKey)
                    {
                        selectClause = selectClause + $"{field.name}";
                        break;
                    }
                }
            }
           
            code = "SET ANSI_NULLS ON" + Environment.NewLine;
            code = code + "GO" + Environment.NewLine;
            code = code + "SET QUOTED_IDENTIFIER ON" + Environment.NewLine;
            code = code + "GO" + Environment.NewLine;
            code = code + $"CREATE TRIGGER [dbo].[TR_{item.Text}_Insert_AuditLog]" + Environment.NewLine;
            code = code + "FOR INSERT AS" + Environment.NewLine;
            code = code + "BEGIN" + Environment.NewLine;
            code = code + "" + Environment.NewLine;
            code = code + "     SET NOCOUNT OFF;" + Environment.NewLine;
            code = code + "" + Environment.NewLine;
            code = code + $"     DECLARE @CurrentUser nvarchar(50)" + Environment.NewLine;
            code = code + $"     SELECT @CurrentUser = CAST(SESSION_CONTEXT(N'CurrentUser') AS nvarchar(50))" + Environment.NewLine;
            code = code + Environment.NewLine;
            code = code + $"     IF  @CurrentUser is null" + Environment.NewLine;
            code = code + $"     BEGIN" + Environment.NewLine;
            code = code + $"        SET @CurrentUser=''" + Environment.NewLine;
            code = code + $"     END" + Environment.NewLine;
            code = code + Environment.NewLine;
            code = code + $"     INSERT into AuditLogs (" + Environment.NewLine;
            code = code + $"        InsertDate," + Environment.NewLine;
            code = code + $"        CurrentUser," + Environment.NewLine;
            code = code + $"        AuditTable," + Environment.NewLine;
            code = code + $"        Operation," + Environment.NewLine;
            code = code + $"        recordId," + Environment.NewLine;
            code = code + $"        NewData," + Environment.NewLine;
            code = code + $"        OldData" + Environment.NewLine;
            code = code + $"     )" + Environment.NewLine;
            code = code + $"     values" + Environment.NewLine;
            code = code + $"     (" + Environment.NewLine;
            code = code + $"        GETUTCDATE()," + Environment.NewLine;
            code = code + $"        @CurrentUser," + Environment.NewLine;
            code = code + $"        '{item.Text}'" + Environment.NewLine;
            code = code + $"        'INSERT'" + Environment.NewLine;
            code = code + $"        (SELECT  {selectClause} FROM Inserted)," + Environment.NewLine;
            code = code + $"        (SELECT * FROM Inserted FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)," + Environment.NewLine;
            code = code + $"        null" + Environment.NewLine;
            code = code + $"     )" + Environment.NewLine;
            code = code + "END" + Environment.NewLine;
            form.GeneratedCodeText = code;
            form.Show();
        }
    }
}