using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using codegenerator.Models;
using Microsoft.Web.WebView2.Core;

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

        private async void GeneratedCodeForm_Load(object sender, EventArgs e)
        {

            StringBuilder code = new StringBuilder("");
            string curr;
            string FilePath;
            if (bolLoading)
            {
               
                bolLoading = false;
                panel1.Dock = DockStyle.Fill;
                panel2.Dock = DockStyle.Top;
                panel3.Dock = DockStyle.Fill;
                ResizeTextBox();

                GeneratedCodeTextBox.Text = this.GeneratedCodeText;

                code.Append("<!DOCTYPE html>" + Environment.NewLine);
                code.Append("<html>" + Environment.NewLine);
                code.Append("   <head>" + Environment.NewLine);
                code.Append("   <link rel='stylesheet' href='a11y-dark.css'>" + Environment.NewLine);
                code.Append("   </head>" + Environment.NewLine);
                code.Append("   <body>" + Environment.NewLine);
                code.Append("   <pre><code>" + GeneratedCodeTextBox.Text + " </code></pre>" + Environment.NewLine);
                code.Append("   <script type='text/javascript' src='highlight.min.js'></script>" + Environment.NewLine);
                code.Append("   <script>hljs.highlightAll();</script>" + Environment.NewLine);
                code.Append("   </body>" + Environment.NewLine);
                code.Append("</html>");

                curr = Path.GetDirectoryName(Application.ExecutablePath);
                FilePath = Path.Combine(Path.Combine(curr, "Web"), "index.html");
                File.WriteAllText(FilePath, code.ToString());

                await webView2.EnsureCoreWebView2Async(null);
                webView2.NavigationStarting += NavigationStarting;
                webView2.NavigationCompleted += NavigationCompleted;
                webView2.CoreWebView2.SetVirtualHostNameToFolderMapping(
                    "codegeneratorwebview",
                    Path.Combine(curr, "Web"),
                    CoreWebView2HostResourceAccessKind.DenyCors);


                webView2.CoreWebView2.Navigate("https://codegeneratorwebview/index.html");

            }
        }

        void NavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs args)
        {
            Cursor.Current = Cursors.WaitCursor;
        }

        void NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs args)
        {
            Cursor.Current = Cursors.Default;
        }


        private void ResizeTextBox()
        {
            //GeneratedCodeTextBox.Left = 0;
            //GeneratedCodeTextBox.Width = panel3.Width;
            //GeneratedCodeTextBox.Height = panel3.Height - 10;
            //GeneratedCodeTextBox.Top = 10;
            webView2.Left = 0;
            webView2.Width = panel3.Width;
            webView2.Height = panel3.Height - 10;
            webView2.Top = 10;
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            GeneratedCodeTextBox.SelectAll();
            GeneratedCodeTextBox.Copy();
        }

        private void GeneratedCodeForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
            {
                ResizeTextBox();
            }
        }
    }
}
