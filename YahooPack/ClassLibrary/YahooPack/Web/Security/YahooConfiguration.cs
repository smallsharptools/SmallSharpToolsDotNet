using System;
using System.Configuration;

namespace SmallSharpTools.YahooPack.Web.Security
{
    public class YahooConfiguration
    {

        public static string ApplicationID
        {
            get
            {
                return GetSetting("YahooApplicationID");
            }
        }

        public static string SharedSecret
        {
            get
            {
                return GetSetting("YahooSharedSecret");
            }
        }

        public static string ApplicationEntryPoint
        {
            get
            {
                return GetSetting("YahooApplicationEntryPoint");
            }
        }

        public static string GetSetting(string key)
        {
            try
            {
                return ConfigurationManager.AppSettings[key];
                //return System.Configuration.ConfigurationSettings.AppSettings[key].ToString();
            }
            catch
            {
                throw new Exception("No " + key + " setting in the web.config.");
            }
        }
        
    }
}
