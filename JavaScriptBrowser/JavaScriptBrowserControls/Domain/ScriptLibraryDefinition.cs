using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallSharpTools.JavaScriptBrowser.Controls.Domain
{

    [Serializable()]
    public class ScriptLibraryDefinition
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string FileName { get; set; }
    }

    [Serializable()]
    public class ScriptLibraryDefinitionCollection : List<ScriptLibraryDefinition>
    {
    }

}

