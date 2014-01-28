using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Runtime.CompilerServices;

namespace SmallSharpTools.JavaScriptBrowser.Controls.Domain
{
    [Serializable()]
    public class Options
    {

        [XmlIgnore()]
        private Orientation _orientation = Orientation.Vertical;

        public Orientation BrowserOrientation
        {
            get
            {
                return _orientation;
            }
            set
            {
                _orientation = value;
            }
        }

        [XmlIgnore()]
        private int _verticalSplitterDistance = 75;

        public int VerticalSplitterDistance
        {
            get
            {
                return _verticalSplitterDistance;
            }
            set
            {
                _verticalSplitterDistance = value;
            }
        }

        [XmlIgnore()]
        private int _horizontalSplitterDistance = 100;

        public int HorizontalSplitterDistance
        {
            get
            {
                return _horizontalSplitterDistance;
            }
            set
            {
                _horizontalSplitterDistance = value;
            }
        }

        [XmlIgnore()]
        private ScriptLibraryDefinitionCollection _scriptLibraryDefinitions = null;

        public ScriptLibraryDefinitionCollection ScriptLibraryDefinitions
        {
            get
            {
                if (_scriptLibraryDefinitions == null)
                {
                    PopulateDefinitions();
                }
                return _scriptLibraryDefinitions;
            }
            set
            {
                _scriptLibraryDefinitions = value;
            }
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        private void PopulateDefinitions()
        {
            if (_scriptLibraryDefinitions == null)
            {
                ScriptLibraryDefinitionManager manager = new ScriptLibraryDefinitionManager();
                _scriptLibraryDefinitions = manager.GetDefinitions();
            }
        }


    }
}
