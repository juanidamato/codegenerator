using codegenerator.BLL;
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
            List<string> tableList = null;
            tableList = DatabaseUtilities.GetDatabaseTables(connectionString);
            if (tableList == null)
            {
                MessageBox.Show("No tables were fround on current database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (string table in tableList)
            {
                tablesListBox.Items.Add(table);
            }
            tablesListBox.SelectedIndex = 0;
        }

        private void tablesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}