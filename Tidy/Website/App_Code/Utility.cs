using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace SmallSharpTools.TidyWebite
{
    /// <summary>
    /// Summary description for Utility
    /// </summary>
    public class Utility
    {
        public Utility()
        {
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

        public static Guid UserID
        {
            get
            {
                if (IsUserAuthenticated)
                {
                    return (Guid)Membership.GetUser().ProviderUserKey;
                }
                else
                {
                    Guid userId = new Guid(HttpContext.Current.Request.AnonymousID.Substring(0, 36));
                    return userId;
                }
            }
        }

        public static string GetSiteUrl(string url)
        {
            if (String.IsNullOrEmpty(url))
            {
                return GetSiteRoot() + "/";
            }
            if (url.StartsWith("~/"))
            {
                url = url.Substring(1, url.Length - 1);
            }
            return GetSiteRoot() + url;
        }

        public static string GetUrlPath(string url)
        {
            return HttpContext.Current.Request.MapPath(url);
        }

        public static string GetRelativeSiteUrl(string url)
        {
            if (!String.IsNullOrEmpty(url))
            {
                if (url.StartsWith("~/"))
                {
                    url = url.Substring(1, url.Length - 1);
                }
                return GetRelativeSiteRoot() + url;
            }
            return GetRelativeSiteRoot() + "/";
        }

        public static string GetSiteRoot()
        {
            string Port = HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
            if (Port == null || Port == "80" || Port == "443")
                Port = "";
            else
                Port = ":" + Port;

            string Protocol = HttpContext.Current.Request.ServerVariables["SERVER_PORT_SECURE"];
            if (Protocol == null || Protocol == "0")
                Protocol = "http://";
            else
                Protocol = "https://";
            string sOut;
            if ("/".Equals(HttpContext.Current.Request.ApplicationPath))
            {
                sOut = Protocol + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + Port;
            }
            else
            {
                sOut = Protocol + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + Port + HttpContext.Current.Request.ApplicationPath;
            }
            return sOut;
        }

        public static string GetRelativeSiteRoot()
        {
            if ("/".Equals(HttpContext.Current.Request.ApplicationPath))
            {
                return String.Empty;
            }
            return HttpContext.Current.Request.ApplicationPath;
        }

    }
}
