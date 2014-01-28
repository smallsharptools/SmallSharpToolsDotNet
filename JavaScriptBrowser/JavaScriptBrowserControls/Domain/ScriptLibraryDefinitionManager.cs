using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SmallSharpTools.JavaScriptBrowser.Controls.Domain
{
    [DataObject()]
    public class ScriptLibraryDefinitionManager
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public ScriptLibraryDefinitionCollection GetDefinitions()
        {
            ScriptLibraryDefinitionCollection definitions = new ScriptLibraryDefinitionCollection();
            definitions.Add(new ScriptLibraryDefinition()
            {
                Name = "jQuery",
                Version = "1.2.3",
                FileName = "C:\\ScriptLibraryDefinitions\\jQuery-1.2.3.sld"
            });
            definitions.Add(new ScriptLibraryDefinition()
            {
                Name = "jQuery UI",
                Version = "1.0",
                FileName = "C:\\ScriptLibraryDefinitions\\jQuery-UI-1.0.sld"
            });
            return definitions;
        }

    }
}
