using System;
using System.Configuration;
using System.Web;

namespace SmallSharpTools.EventsBanner.Website
{
    /// <summary>
    /// Summary description for SiteConfiguration
    /// </summary>
    public class SiteConfiguration
    {
        public static string AdminUserName
        {
            get
            {
                return GetSetting("AdminUserName", true);
            }
        }

        public static string AdminUserPassword
        {
            get
            {
                return GetSetting("AdminUserPassword", true);
            }
        }

        public static string GetUserName()
        {
            if (IsUserAuthenticated)
            {
                return HttpContext.Current.User.Identity.Name;
            }
            return String.Empty;
        }

        public static bool IsUserAuthenticated
        {
            get
            {
                return HttpContext.Current.User.Identity.IsAuthenticated;
            }
        }

        public static bool IsAdminUser
        {
            get
            {
                return HttpContext.Current.User.IsInRole("Admin");
            }
        }

        public static string HomeUrl
        {
            get
            {
                return Utility.GetRelativeSiteUrl("~/");
            }
        }

        public static string ErrorUrl
        {
            get
            {
                return Utility.GetRelativeSiteUrl("~/Error.aspx");
            }
        }

        public static string GetSetting(string key, bool isRequired)
        {
            try
            {
                string setting = ConfigurationManager.AppSettings[key].ToString();
                if (isRequired && String.IsNullOrEmpty(setting))
                {
                    throw new ApplicationException("Required field is undefined: " + key);
                }
                return setting;
            }
            catch
            {
                throw new Exception("No " + key + " setting in the web.config.");
            }
        }
    }
}