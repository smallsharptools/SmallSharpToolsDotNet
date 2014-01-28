using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SmallSharpTools.JavaScriptBrowser.Controls.Domain;

namespace SmallSharpTools.JavaScriptBrowser.Controls
{
    public partial class OptionsControl : UserControl
    {
        [Category("Options")]
        public event EventHandler OptionsSaved;

        [Category("Options")]
        public event CancelEventHandler OptionsSaving;

        [Category("Options")]
        public event EventHandler OptionsCanceled;

        public OptionsControl()
        {
            InitializeComponent();
            foreach (ScriptLibraryDefinition definition in JavaScriptBrowserContext.Options.ScriptLibraryDefinitions)
            {
                librariesDataGridView.Rows.Add(new object[]{
                    definition.Name, definition.Version, definition.FileName
                });
            }
            orientationComboBox.Items.Clear();
            orientationComboBox.Items.Add(Orientation.Horizontal);
            orientationComboBox.Items.Add(Orientation.Vertical);
            orientationComboBox.SelectedIndex = 0;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // TODO set the form here?
            //if (!this.DesignMode)
            //{
            //    foreach (object item in orientationComboBox.Items)
            //    {
            //        if (item.Equals(JavaScriptBrowserContext.Options.BrowserOrientation))
            //        {
            //            orientationComboBox.SelectedItem = item;
            //        }
            //    }
            //}
            if (!this.DesignMode)
            {
                orientationComboBox.SelectedItem = JavaScriptBrowserContext.Options.BrowserOrientation;
            }
        }

        public void Save()
        {
            CancelEventArgs ce = new CancelEventArgs();
            OnOptionsSaving(ce);
            if (!ce.Cancel)
            {
                JavaScriptBrowserContext.Options.BrowserOrientation = (Orientation)orientationComboBox.SelectedItem;
                JavaScriptBrowserContext.SaveOptions();
                OnOptionsSaved(EventArgs.Empty);
            }
        }

        public void Cancel()
        {
            OnOptionsCanceled(EventArgs.Empty);
        }

        protected virtual void OnOptionsSaving(CancelEventArgs e)
        {
            if (OptionsSaving != null)
            {
                OptionsSaving(this, e);
            }
        }

        protected virtual void OnOptionsSaved(EventArgs e)
        {
            if (OptionsSaved != null)
            {
                OptionsSaved(this, e);
            }
        }

        protected virtual void OnOptionsCanceled(EventArgs e)
        {
            if (OptionsCanceled != null)
            {
                OptionsCanceled(this, e);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = String.Empty;
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                // TODO verify the file is a valid SLD format
                MessageBox.Show(openFileDialog.FileName);
            }
        }

        private void linkLabelGetDefinitionFiles_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.xmldoccomments.com/");
        }

    }
}
