<%@ WebHandler Language="C#" Class="AutoCompleteHandler" %>

using System;
using System.Web;

public class AutoCompleteHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/xml";
        context.Response.Write("<ul>\n");
        string searchValue = context.Request.Params["searchValue"];
        for (int i=1;i<=5;i++) {
            context.Response.Write("<li>" + searchValue + 
                " " + i.ToString() + "</li>\n");
        }
        context.Response.Write("</ul>\n");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}