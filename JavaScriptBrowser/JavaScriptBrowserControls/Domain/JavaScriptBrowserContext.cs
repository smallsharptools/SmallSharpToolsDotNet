using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace SmallSharpTools.JavaScriptBrowser.Controls.Domain
{
    public static class JavaScriptBrowserContext
    {

        private static Options _options = null;

        public static Options Options
        {
            get
            {
                if (_options == null)
                {
                    LoadOptions();
                }
                return _options;
            }
            set
            {
                _options = value;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void LoadOptions()
        {
            if (_options == null)
            {
                OptionsManager manager = new OptionsManager();
                _options = manager.LoadOptions();
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void SaveOptions()
        {
            OptionsManager manager = new OptionsManager();
            manager.SaveOptions(Options);
        }

    }
}
