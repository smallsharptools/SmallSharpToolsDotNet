using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using SmallSharpTools.VSX.Composite;

namespace SmallSharpTools.VSX.Composite.VSPackage
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
            _compositeClass = new CompositeClass(null);
            _compositeClass.LoadFile(filename);
        }
    }
}
