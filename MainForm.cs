using codegenerator.BLL;
using codegenerator.Models;
using System.Diagnostics;

namespace codegenerator
{
    public partial class MainForm : Form
    {
        private bool bolLoading;
        private string connectionString;
        private DatabaseUtilities _dbUtil;
        private ODBCConnect _odbcForm;

        public MainForm(DatabaseUtilities dbUtil,ODBCConnect odbcForm)
        {
            _dbUtil=dbUtil;
            _odbcForm=odbcForm;

            bolLoading = true;
            connectionString = "";
            InitializeComponent();

            DataGridViewColumn col1=new DataGridViewColumn();
            col1.Name="Name";
            dataGridView1.Columns.Add(col1);

            DataGridViewColumn col2=new DataGridViewColumn();
            col2.Name="Type";
            dataGridView1.Columns.Add(col2);

            DataGridViewColumn col3=new DataGridViewColumn();
            col3.Name="Length";
            dataGridView1.Columns.Add(col3);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (bolLoading)
            {
                bolLoading = false;
                
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
            KeyValueItem item = (KeyValueItem)tablesListBox.SelectedItem;
            fieldList = _dbUtil.GetTableFields(connectionString, Convert.ToInt32(item.Value));
        }
    }
}