using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using log4net;
using SmallSharpTools;
using SmallSharpTools.Logging;

/// <summary>
/// Summary description for Utility
/// </summary>
public class Utility
{

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

        string sOut = Protocol + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + Port + HttpContext.Current.Request.ApplicationPath;
        return sOut;
    }

    public static string GetRelativeSiteRoot()
    {
        return HttpContext.Current.Request.ApplicationPath;
    }
    
    public static ILogger GetLogger(Type type)
    {
        return LoggingProvider.Instance.GetLogger(type);
    }

}
