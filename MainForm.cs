using codegenerator.BLL;
using codegenerator.Models;
using System.Diagnostics;

namespace codegenerator
{
    public partial class MainForm : Form
    {
        private bool bolLoading;
        private string connectionString;

        public MainForm()
        {
            bolLoading = true;
            connectionString = "";
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (bolLoading)
            {
                bolLoading = false;
                ODBCConnect connectForm = new ODBCConnect();
                connectForm.ShowDialog();

                if (!connectForm.returnCode)
                {
                    System.Windows.Forms.Application.Exit();
                    return;
                }
                connectionString = connectForm.connectionString;
                populateTables();
            }

        }

        private void populateTables()
        {
            List<SQLTableModel> tableList = null;
            tableList = DatabaseUtilities.GetDatabaseTables(connectionString);
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
            fieldList = DatabaseUtilities.GetTableFields(connectionString, Convert.ToInt32(item.Value));
        }
    }
}