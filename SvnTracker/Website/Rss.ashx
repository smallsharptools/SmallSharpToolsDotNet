<%@ WebHandler Language="C#" Class="Rss" %>

using System;
using System.Web;

public class Rss : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/xml";
        SubversionInfo info = new SubversionInfo();
        string data = info.GetRssFeed(context, context.Request.QueryString["url"],
            context.Request.QueryString["title"], context.Request.QueryString["revision"],
            context.Request.QueryString["username"], context.Request.QueryString["password"]);
        context.Response.Write(data);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}