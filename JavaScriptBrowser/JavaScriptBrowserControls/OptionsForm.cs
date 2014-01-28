using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SmallSharpTools.JavaScriptBrowser.Controls.Domain;

namespace SmallSharpTools.JavaScriptBrowser.Controls
{
    public partial class OptionsForm : Form
    {

        public OptionsForm()
        {
            InitializeComponent();
            optionsControl.OptionsSaved += new EventHandler(optionsControl_OptionsSaved);
            optionsControl.OptionsCanceled += new EventHandler(optionsControl_OptionsCanceled);
        }

        void optionsControl_OptionsCanceled(object sender, EventArgs e)
        {
            Hide();
        }

        void optionsControl_OptionsSaved(object sender, EventArgs e)
        {
            Hide();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            optionsControl.Cancel();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            optionsControl.Save();
        }

    }
}
