<%@ WebHandler Language="C#" Class="ScriptsHandler" %>

using System;
using System.Globalization;
using System.Web;
using System.Web.Caching;

public class ScriptsHandler : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        DateTime lastModifiedUtc = SiteUtility.GetJavaScriptLastModified();

        string ifModifiedSinceStr = context.Request.Headers["If-Modified-Since"];
        if (!String.IsNullOrEmpty(ifModifiedSinceStr))
        {
            DateTime ifModifiedSinceUtc = DateTime.Parse(ifModifiedSinceStr).ToUniversalTime();
            if (lastModifiedUtc <= ifModifiedSinceUtc)
            {
                context.Response.StatusCode = 304;
                context.Response.StatusDescription = "Not Modified";
                context.Response.AddHeader("Content-Length", "0");
            }
        }

        if (context.Response.StatusCode != 304)
        {
            // Set Last-Modified Header
            context.Response.AddHeader("Last-Modified", lastModifiedUtc.ToString("R", DateTimeFormatInfo.InvariantInfo));

            context.Response.ContentType = "text/javascript";
            string script = SiteUtility.GetCompactedJavaScript();
            context.Response.Write(script);
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}