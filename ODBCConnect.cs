using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using codegenerator.BLL;
namespace codegenerator
{
    public partial class ODBCConnect : Form
    {

        public bool returnCode { get; set; }
        public string connectionString { get; set; }

        public ODBCConnect()
        {
            InitializeComponent();

            returnCode = false;
            connectionString = "";
            //todo load from file if exists
            string curr;
            curr = Path.GetDirectoryName(Application.ExecutablePath);
            try
            {
                if (File.Exists(Path.Combine(curr, "connection.txt")))
                {
                    connectionString = File.ReadAllText(Path.Combine(curr, "connection.txt"));
                    PreserveConnectionStringCheckBox.Checked = true;
                }
            }
            catch (Exception ex)
            {
            }

            ConnectionStringTextBox.Text = connectionString;

        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            returnCode = false;
            connectionString = "";
            this.Close();
        }

        private void AcceptButton_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(ConnectionStringTextBox.Text))
            {
                MessageBox.Show("Must supply a connection string", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!DatabaseUtilities.TestConnection(ConnectionStringTextBox.Text))
            {
                MessageBox.Show("Invalid connection string", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (PreserveConnectionStringCheckBox.Checked)
            {
                string curr;
                curr = Path.GetDirectoryName(Application.ExecutablePath);
                File.WriteAllText(Path.Combine(curr, "connection.txt"), ConnectionStringTextBox.Text);
            }

            returnCode = true;
            connectionString = ConnectionStringTextBox.Text;
            this.Close();
        }

        private void ODBCConnect_Load(object sender, EventArgs e)
        {

        }
    }
}
