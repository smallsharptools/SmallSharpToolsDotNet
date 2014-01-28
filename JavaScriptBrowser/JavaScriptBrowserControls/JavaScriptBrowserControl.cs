using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SmallSharpTools.JavaScriptBrowser.Controls.Domain;
using SmallSharpTools.JavaScriptBrowser.Documentation;
using System.IO;

namespace SmallSharpTools.JavaScriptBrowser.Controls
{
    public partial class JavaScriptBrowserControl : UserControl
    {
        delegate void DisplayItemDelegate(object item);
        delegate void NavigateDelegate(string url);
        delegate void UpdateRootNodeDelegate(TreeNode rootNode);

        private OptionsForm optionsForm;

        public JavaScriptBrowserControl()
        {
            InitializeComponent();
            CreateDefaultDocuments();
            OptionsManager manager = new OptionsManager();

            this.Load += new EventHandler(JavaScriptBrowserControl_Load);
            optionsForm = new OptionsForm();

            webBrowser.ScriptErrorsSuppressed = true;
            webBrowser.NewWindow += new CancelEventHandler(webBrowser_NewWindow);
            ChangeOrientation();
        }

        private void CreateDefaultDocuments()
        {
            SerializeUtility<string>.CreateStorageDirectory();
            foreach (string name in DocumentHelper.DefaultFiles)
            {
                string filename = SerializeUtility<string>.GetFilePath(name);
                if (!File.Exists(filename))
                {
                    string content = DocumentHelper.GetDocumentAsString(name);
                    File.WriteAllText(filename, content);
                }
            }
        }

        void webBrowser_NewWindow(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }

        void JavaScriptBrowserControl_Load(object sender, EventArgs e)
        {
            toolStripComboBox.Items.Clear();
            toolStripComboBox.Items.Add("JavaScript Core");
            toolStripComboBox.Items.Add("jQuery");
            toolStripComboBox.Items.Add("Prototype");
            //toolStripComboBox.Items.Add("script.aculo.us");
            //toolStripComboBox.Items.Add("MochiKit");
            //toolStripComboBox.Items.Add("YUI");
            //toolStripComboBox.Items.Add("moo.fx");
            //toolStripComboBox.Items.Add("Ext JS");
            //toolStripComboBox.Items.Add("Dojo");

            toolStripComboBox.SelectedIndex = 0;
        }

        private void toolStripOptionsButton_Click(object sender, EventArgs e)
        {
            optionsForm.ShowDialog();
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayItemDelegate del = delegate(object item)
            {
                DisplayItem(item);
            };
            del.BeginInvoke(toolStripComboBox.SelectedItem, null, null);
        }

        private void DisplayItem(object item)
        {
            UpdateRootNodeDelegate del = delegate(TreeNode node)
            {
                SetRootNode(node);
            };

            NavigateDelegate del2 = delegate(string url)
            {
                Navigate(url);
            };

            TreeNode rootNode = null;

            if ("JavaScript Core".Equals(item))
            {
                rootNode = new TreeNode("JavaScript Core");
                rootNode.Nodes.Add("show");
                rootNode.Nodes.Add("hide");
                string filename = SerializeUtility<string>.GetFilePath(DocumentHelper.JavaScriptCoreDocument);
                webBrowser.Invoke(del2, new object[] { GetFileUrl(filename) + "#classes" });
            }
            else if ("jQuery".Equals(item))
            {
                rootNode = new TreeNode("jQuery");
                rootNode.Nodes.Add("show");
                rootNode.Nodes.Add("hide");
                rootNode.Nodes.Add("abc");
                string filename = SerializeUtility<string>.GetFilePath(DocumentHelper.JQueryDocument);
                webBrowser.Invoke(del2, new object[] { GetFileUrl(filename) + "#classes" });
            }
            else if ("Prototype".Equals(item))
            {
                rootNode = new TreeNode("Prototype");
                rootNode.Nodes.Add("abc");
                rootNode.Nodes.Add("xyz");
                string filename = SerializeUtility<string>.GetFilePath(DocumentHelper.PrototypeDocument);
                webBrowser.Invoke(del2, new object[] { GetFileUrl(filename) });
            }
            else if ("script.aculo.us".Equals(item))
            {
                rootNode = new TreeNode("script.aculo.us");
                rootNode.Nodes.Add("abc");
                rootNode.Nodes.Add("xyz");
                webBrowser.Invoke(del2, new object[] { "http://script.aculo.us/" });
            }
            else if ("Dojo".Equals(item))
            {
                rootNode = new TreeNode("Dojo");
                rootNode.Nodes.Add("abc");
                rootNode.Nodes.Add("xyz");
                webBrowser.Invoke(del2, new object[] { "http://dojotoolkit.org/" });
            }
            else
            {
                MessageBox.Show("Item not recognized!");
            }

            treeView.Invoke(del, new object[]{rootNode});
        }

        private void SetRootNode(TreeNode rootNode)
        {
            treeView.Nodes.Clear();
            if (rootNode != null)
            {
                treeView.Nodes.Add(rootNode);
            }
        }

        private void Navigate(string url)
        {
            webBrowser.Navigate(url);
        }

        private void toolStripOrientationButton_Click(object sender, EventArgs e)
        {
            ChangeOrientation();
        }

        private void ChangeOrientation()
        {
            if (splitContainer.Orientation == Orientation.Vertical)
            {
                JavaScriptBrowserContext.Options.VerticalSplitterDistance = splitContainer.SplitterDistance;
                JavaScriptBrowserContext.Options.BrowserOrientation = Orientation.Horizontal;
                SetOrientation(Orientation.Horizontal);
            }
            else
            {
                JavaScriptBrowserContext.Options.HorizontalSplitterDistance = splitContainer.SplitterDistance;
                JavaScriptBrowserContext.Options.BrowserOrientation = Orientation.Vertical;
                SetOrientation(Orientation.Vertical);
            }
        }

        private void SetOrientation(Orientation orientation)
        {
            if (orientation == Orientation.Vertical)
            {
                splitContainer.SplitterDistance = JavaScriptBrowserContext.Options.VerticalSplitterDistance;
            }
            else
            {
                splitContainer.SplitterDistance = JavaScriptBrowserContext.Options.HorizontalSplitterDistance;
            }
            splitContainer.Orientation = orientation;
        }

        public void Close()
        {
            JavaScriptBrowserContext.SaveOptions();
        }

        private string GetFileUrl(string filename)
        {
            return filename.Replace(Path.DirectorySeparatorChar, "/".ToCharArray()[0]);
        }

    }
}
