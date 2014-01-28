using System;
using System.Windows.Forms;

namespace SmallSharpTools.JavaScriptBrowser.StandAlone
{
    public partial class BrowserForm : Form
    {
        public BrowserForm()
        {
            InitializeComponent();
        }

        protected override void OnClosed(EventArgs e)
        {
            javaScriptBrowserControl.Close();
            base.OnClosed(e);
        }

    }
}
