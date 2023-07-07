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
            col5.Name = "Is System Date";
            col5.CellTemplate = new DataGridViewCheckBoxCell();
            col5.ReadOnly = false;
            dataGridView1.Columns.Add(col5);
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
            List<SQLTableKeyFieldModel> keyFieldList;
            KeyValueItem item;

            SetupNewGrid();
            SetupOtherValues();
            item = (KeyValueItem)tablesListBox.SelectedItem;
            fieldList = _dbUtil.GetTableFields(connectionString, Convert.ToInt32(item.Value));
            if (fieldList != null)
            {
                keyFieldList = _dbUtil.GetTableKeyFields(connectionString, item.Text);
                if (keyFieldList != null)
                {
                    for (int i = 0; i <= fieldList.Count - 1; i++)
                    {
                        foreach (var keyfield in keyFieldList)
                        {
                            if (keyfield.COLUMN_NAME == fieldList[i].name)
                            {
                                fieldList[i].isPrimaryKey = true;
                            }
                        }
                    }
                }

                foreach (var field in fieldList)
                {
                    dataGridView1.Rows.Add(
                        field.isPrimaryKey ? "*" + field.name : field.name,
                        field.fieldType,
                        CalculateFieldSizeString(field),
                        field.isnullable == 0 ? false : true,
                        false);
                    //todo remove
                    //just poc
                    if (dataGridView1.Rows.Count % 2 == 0)
                    {
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[4].Value = null;
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[4] = new DataGridViewTextBoxCell();
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[4].ReadOnly = true;
                    }
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
                case "decimal":
                case "numeric":
                    fieldType = field.fieldType + "(" + size + ")";
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
            fieldList = _dbUtil.GetTableFields(connectionString, Convert.ToInt32(item.Value));
            if (fieldList != null)
            {
                foreach (var field in fieldList)
                {
                    code = code + $"         public {MapSQLTypeToCType(field)} {field.name}" + " {get;set;}" + Environment.NewLine;
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
            List<SQLTableKeyFieldModel> keyFieldList;
            KeyValueItem item;
            GeneratedCodeForm form = new GeneratedCodeForm();
            DateTime Today = DateTime.Now;
            item = (KeyValueItem)tablesListBox.SelectedItem;
            fieldList = _dbUtil.GetTableFields(connectionString, Convert.ToInt32(item.Value));
            orderby = "";
            if (fieldList != null)
            {
                keyFieldList = _dbUtil.GetTableKeyFields(connectionString, item.Text);
                if (keyFieldList != null)
                {
                    for (int i = 0; i <= fieldList.Count - 1; i++)
                    {
                        foreach (var keyfield in keyFieldList)
                        {
                            if (keyfield.COLUMN_NAME == fieldList[i].name)
                            {
                                fieldList[i].isPrimaryKey = true;
                            }
                        }
                    }
                }
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
            List<SQLTableFieldModel> fieldList;
            List<SQLTableKeyFieldModel> keyFieldList = null;
            KeyValueItem item;
            GeneratedCodeForm form = new GeneratedCodeForm();
            DateTime Today = DateTime.Now;
            item = (KeyValueItem)tablesListBox.SelectedItem;
            fieldList = _dbUtil.GetTableFields(connectionString, Convert.ToInt32(item.Value));
            parametersKey = "";
            whereClause = "     where ";
            if (fieldList != null)
            {
                keyFieldList = _dbUtil.GetTableKeyFields(connectionString, item.Text);
                if (keyFieldList != null)
                {
                    for (int i = 0; i <= fieldList.Count - 1; i++)
                    {
                        foreach (var keyfield in keyFieldList)
                        {
                            if (keyfield.COLUMN_NAME == fieldList[i].name)
                            {
                                fieldList[i].isPrimaryKey = true;
                            }
                        }
                    }
                }
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
            if (keyFieldList == null)
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

        }

        private void spUpdateByPKButton_Click(object sender, EventArgs e)
        {

        }

        private void spDeleteByPKButton_Click(object sender, EventArgs e)
        {

        }
    }
}