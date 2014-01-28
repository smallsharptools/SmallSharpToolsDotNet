using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using SmallSharpTools.VisualStudioExtensions;

namespace SmallSharpToolscom.CompositePackage
{
    public partial class CompositeEditor : UserControl
    {

        private CompositeClass _compositeClass;

        public CompositeEditor()
        {
            InitializeComponent();
        }

        public void LoadFile(string filename)
        {
            _compositeClass = new CompositeClass();
            _compositeClass.LoadFile(filename);
        }
    }
}
