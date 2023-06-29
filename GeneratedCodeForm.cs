using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace codegenerator
{
    public partial class GeneratedCodeForm : Form
    {
        public string GeneratedCodeText { get; set; }
        private bool bolLoading = true;

        public GeneratedCodeForm()
        {
            InitializeComponent();
        }

        private void GeneratedCodeForm_Load(object sender, EventArgs e)
        {
            if (bolLoading)
            {
                bolLoading = false;
                panel1.Dock = DockStyle.Fill;
                panel2.Dock = DockStyle.Top;
                panel3.Dock = DockStyle.Fill;
                ResizeTextBox();

                GeneratedCodeTextBox.Text = this.GeneratedCodeText;
            }
        }
        private void ResizeTextBox()
        {
            GeneratedCodeTextBox.Left = 0;
            GeneratedCodeTextBox.Width = panel3.Width;
            GeneratedCodeTextBox.Height = panel3.Height - 10;
            GeneratedCodeTextBox.Top = 10;
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            GeneratedCodeTextBox.SelectAll();
            GeneratedCodeTextBox.Copy();
        }

        private void GeneratedCodeForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState!=FormWindowState.Minimized)
            {
                ResizeTextBox();
            }
        }
    }
}
