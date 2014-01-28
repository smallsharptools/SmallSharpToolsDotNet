using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SmallSharpToolscom.CompositePackage
{
    public partial class TypeEditorControl : UserControl
    {
        public TypeEditorControl()
        {
            InitializeComponent();
        }

        private void TypeEditorControl_Load(object sender, EventArgs e)
        {
            errorLabel.Visible = false;
        }

        public void LoadForm(string name, string className, string theNamespace)
        {
            NameX = name;
            ClassName = className;
            Namespace = theNamespace;
        }

        public string NameX
        {
            get { return nameTextBox.Text; }
            set { nameTextBox.Text = value; }
        }

        public string ClassName
        {
            get { return classNameTextBox.Text; }
            set { classNameTextBox.Text = value; }
        }

        public string Namespace
        {
            get { return namespaceTextBox.Text; }
            set { namespaceTextBox.Text = value; }
        }


    }
}
