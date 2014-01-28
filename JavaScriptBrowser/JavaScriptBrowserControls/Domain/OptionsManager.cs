
using System.IO;
namespace SmallSharpTools.JavaScriptBrowser.Controls.Domain
{
    public class OptionsManager
    {
        internal const string OptionsFilename = "JavaScriptBrowser.xml";

        public void SaveOptions(Options options)
        {
            SerializeUtility<Options> utility = new SerializeUtility<Options>();
            utility.SerializeData(options, OptionsFilename);
        }

        public Options LoadOptions()
        {
            SerializeUtility<Options> utility = new SerializeUtility<Options>();
            Options options = null;
            try
            {
                if (File.Exists(SerializeUtility<Options>.GetFilePath(OptionsFilename)))
                {
                    options = utility.DeserializeData(OptionsFilename);
                }
            }
            finally
            {
                if (options == null)
                {
                    options = new Options();
                }
            }
            return options;
        }

    }
}
