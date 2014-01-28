using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using SmallSharpTools.Packer.Utilities;

/// <summary>
/// </summary>
public static class SiteUtility
{

    private static List<string> _scripts;

    public static List<string> Scripts
    {
        get
        {
            if (_scripts == null)
            {
                _scripts = new List<string>();
                _scripts.Add("~/core.js");
                _scripts.Add("~/script1.js");
                _scripts.Add("~/script2.js");
                _scripts.Add("~/script3.js");
            }
            return _scripts;
        }
    }

    public static void RegisterJavaScript(Page page)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(page);

        if (HttpContext.Current.IsDebuggingEnabled)
        {
            // reference each script without shrinking
            foreach (string script in Scripts)
            {
                scriptManager.Scripts.Add(new ScriptReference(script));
            }
        }
        else
        {
            // reference the scripts handler which shrinks all scripts into one
            scriptManager.Scripts.Add(new ScriptReference("~/ScriptsHandler.ashx"));
        }
    }

    public static DateTime GetJavaScriptLastModified()
    {
        DateTime lm = DateTime.MinValue;

        foreach (string script in SiteUtility.Scripts)
        {
            FileInfo fi = new FileInfo(HttpContext.Current.Request.MapPath(script));
            if (fi.LastWriteTimeUtc > lm)
            {
                lm = fi.LastWriteTimeUtc;
            }
        }

        // strip the milliseconds since the Last-Modified header only precise to seconds
        return new DateTime(lm.Year, lm.Month, lm.Day, lm.Hour, lm.Minute, lm.Second, DateTimeKind.Utc);
    }

    public static string GetCompactedJavaScript()
    {
        List<string> scripts = new List<string>();
        foreach (string script in SiteUtility.Scripts)
        {
            scripts.Add(HttpContext.Current.Request.MapPath(script));
        }

        // set to expire cache when any of the source files changes
        CacheDependency cacheDependency = new CacheDependency(scripts.ToArray());

        Cache cache = HttpRuntime.Cache;
        string cacheKey = "CompactedJavaScript";
        if (cache[cacheKey] != null)
        {
            return cache[cacheKey] as string;
        }

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("/*  Generated " + DateTime.Now.ToUniversalTime().ToString("R", DateTimeFormatInfo.InvariantInfo) + "  */\n");
        sb.AppendLine(FileProcessor.Run(PackMode.JSMin, scripts.ToArray(), false));

        string output = sb.ToString();

        // hold for 30 minutes
        cache.Insert(cacheKey, output, cacheDependency, DateTime.Now.AddMinutes(30),
            Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);

        return output;
    }

}
