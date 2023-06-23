using codegenerator.BLL;
using codegenerator.Models;
using codegenerator.Models.ViewModels;
using System.Diagnostics;
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
            item = (KeyValueItem)tablesListBox.SelectedItem;
            fieldList = _dbUtil.GetTableFields(connectionString, Convert.ToInt32(item.Value));
            if (fieldList != null)
            {
                foreach (var field in fieldList)
                {
                    dataGridView1.Rows.Add(
                        field.name,
                        field.fieldType,
                        CalculateFieldSizeString(field),
                        field.isnullable == 0 ? false : true,
                        false);
                    if (dataGridView1.Rows.Count % 2 ==0)
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
                    size = "xx";
                    break;
            }
            return size;
        }

       
    }
}