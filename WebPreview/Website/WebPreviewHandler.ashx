<%@ WebHandler Language="C#" Class="WebPreviewHandler" %>

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.Caching;
using SmallSharpTools.WebPreview;

public class WebPreviewHandler : IHttpHandler 
{
    
    public void ProcessRequest (HttpContext context) 
    {
        string url = context.Request.QueryString["url"];
        
        int width;
        if (!int.TryParse(context.Request.QueryString["width"], out width))
        {
            width = 150;
        }
        int height;
        if (!int.TryParse(context.Request.QueryString["height"], out height))
        {
            height = 100;
        }

        ThumbnailBuilder tb = new ThumbnailBuilder();
        
        string sourceFilename = tb.GetSourceFilename(url);
        FileInfo file = new FileInfo(sourceFilename);
        
        if (!file.Exists || file.CreationTime < DateTime.Now.AddHours(-4))
        {
            PreviewBuilder pb = new PreviewBuilder(url, sourceFilename);
            pb.CreatePreview();
        }

        string cachedFilename = tb.GetCachedFilename(sourceFilename, width, height);
        
        if (!File.Exists(cachedFilename))
        {
            // handle the error by throwing it
            tb.ExceptionCatching += delegate { throw tb.CurrentError; };
            tb.CreateThumbnail(sourceFilename, cachedFilename, width, height);
        }  
        
        context.Response.ContentType = tb.GetContentType(cachedFilename);
        context.Response.WriteFile(cachedFilename);
    }
    
    public bool IsReusable 
    {
        get {
            return false;
        }
    }

}