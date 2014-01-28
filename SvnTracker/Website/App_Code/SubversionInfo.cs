using System;
using System.Xml;

/// <summary>
/// </summary>
using System.Diagnostics;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Collections.Generic;
public class SubversionInfo
{

    public const int FeedLimit = 10;
    public const int FeedDays = 90;
    public const int WaitDelay = 30000; // (ms)
    public const string RootPath = "/log";
    public const string LogEntriesPath = RootPath + "/logentry";

    public string GetRssFeed(HttpContext context, string url, string title, 
        string revision, string username, string password)
    {
        StringBuilder sb = new StringBuilder();

        XmlDocument document = GetLogData(url, revision, username, password);

        if (String.IsNullOrEmpty(title))
        {
            title = "RSS Feed";
        }

        string publicUrl = HttpUtility.HtmlEncode(context.Request.Url.ToString());

        sb.AppendLine("<?xml version=\"1.0\"?>");
        sb.AppendLine("");
        sb.AppendLine("<rss version=\"2.0\">");
        sb.AppendLine("<channel>");
        sb.AppendLine("<title>" + title + "</title>");
        sb.AppendLine("<link>" + publicUrl + "</link>");
        sb.AppendLine("<description>SVN Log</description>");

        XmlNodeList entryNodes = document.SelectNodes(LogEntriesPath);
        List<XmlNode> entries = new List<XmlNode>();
        foreach (XmlNode entryNode in entryNodes)
        {
            entries.Add(entryNode);
        }
        //entries.Reverse();

        bool first = true;
        foreach (XmlNode entryNode in entries)
        {
            XmlNode revisionNode = entryNode.SelectSingleNode("@revision");
            XmlNode authorNode = entryNode.SelectSingleNode("author");
            XmlNode dateNode = entryNode.SelectSingleNode("date");
            XmlNode messageNode = entryNode.SelectSingleNode("msg");

            if (revisionNode != null && authorNode != null && dateNode != null && messageNode != null)
            {

                string revisionValue = revisionNode.Value;
                string revisionUrl = publicUrl + HttpUtility.HtmlEncode("&revision=" + revisionValue);
                //string listData = GetListData(url, revisionValue, username, password);
                string description = "";

                if (messageNode.FirstChild != null)
                {
                    description = HttpUtility.HtmlEncode("<p>" + messageNode.FirstChild.Value + "</p>");
                }
                else
                {
                    description = HttpUtility.HtmlEncode("<p>no comment</p>");
                }

                if (authorNode != null && authorNode.FirstChild != null)
                {
                    description += HttpUtility.HtmlEncode("<p>- " + authorNode.FirstChild.Value + "</p>");
                }

                DateTime commitDate = DateTime.Now;
                DateTime.TryParse(dateNode.FirstChild.Value, out commitDate);

                if (first)
                {
                    sb.AppendLine("<pubDate>" + GetRfc822DateString(commitDate) + "</pubDate>");
                    first = false;
                }

                sb.AppendLine("");
                sb.AppendLine("<item>");
                sb.AppendLine("\t<title>Revision " + revisionValue + "</title>");
                sb.AppendLine("\t<link>" + revisionUrl + "</link>");
                sb.AppendLine("\t<description>" + description + "</description>");
                sb.AppendLine("\t<pubDate>" + GetRfc822DateString(commitDate) + "</pubDate>");
                sb.AppendLine("\t<category><![CDATA[commit]]></category>");
                sb.AppendLine("\t<guid>" + revisionUrl + "</guid>");
                sb.AppendLine("</item>");
                sb.AppendLine("");

            }
        }

        if (first)
        {
            sb.AppendLine("<pubDate>" + GetRfc822DateString(DateTime.Now) + "</pubDate>");
        }

        sb.AppendLine("</channel>");
        sb.AppendLine("</rss>");

        return sb.ToString();
    }

    private XmlDocument GetLogData(string url, string revision, string username, string password)
    {
        Cache cache = HttpRuntime.Cache;
        string cacheKey = "LogData-" + revision + "-" + url;
        if (cache[cacheKey] != null)
        {
            //return (XmlDocument) cache[cacheKey];
        }
        // svn log --xml -r [Revision] --non-interactive --limit [Limit] http://svn.website.com/svn/trunk
        Process proc = new Process();
        proc.StartInfo.FileName = "svn";
        proc.StartInfo.CreateNoWindow = true;
        proc.StartInfo.Arguments = "log --xml " + 
            GetRevisionArguments(revision, false) + 
            GetAuthArguments(username, password) +
            " --non-interactive --limit " + FeedLimit + " " + url;
        proc.StartInfo.UseShellExecute = false;
        proc.StartInfo.RedirectStandardOutput = true;
        proc.Start();
        proc.WaitForExit(WaitDelay);

        XmlDocument document = new XmlDocument();
        document.Load(proc.StandardOutput);
        document.Normalize();

        cache.Insert(cacheKey, document, null, DateTime.Now.AddMinutes(5), 
            Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);

        return document;
    }

    private string GetListData(string url, string revision, string username, string password)
    {
        Cache cache = HttpRuntime.Cache;
        string cacheKey = "ListData-" + revision + "-" + url;
        if (cache[cacheKey] != null)
        {
            return (string)cache[cacheKey];
        }
        // svn list -r [Revision] --non-interactive http://svn.website.com/svn/trunk
        Process proc = new Process();
        proc.StartInfo.FileName = "svn";
        proc.StartInfo.CreateNoWindow = true;
        proc.StartInfo.Arguments = "list " + 
            GetRevisionArguments(revision, false) +
            GetAuthArguments(username, password) +
            " --non-interactive " + url;
        proc.StartInfo.UseShellExecute = false;
        proc.StartInfo.RedirectStandardOutput = true;
        proc.Start();
        proc.WaitForExit(WaitDelay);

        string list = proc.StandardOutput.ReadToEnd();

        cache.Insert(cacheKey, list, null, DateTime.Now.AddHours(4),
            Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);

        return list;
    }

    private string GetRevisionArguments(string revision, bool limitByDate)
    {
        if (limitByDate && String.IsNullOrEmpty(revision))
        {
            revision = GetRevisionDateRange(FeedDays);
        }
        if (!String.IsNullOrEmpty(revision))
        {
            return " -r " + revision;
        }
        else
        {
            return String.Empty;
        }
    }

    private string GetRevisionDateRange(int days)
    {
        DateTime startDate = DateTime.Now.AddDays(-1 * days);
        DateTime endDate = DateTime.Now;

        // start : end
        // {YYYY-MM-DD}:{YYYY-MM-DD} 

        string startString = String.Format("{0}-{1}-{2}",
            startDate.Year, startDate.Month, startDate.Day);

        string endString = String.Format("{0}-{1}-{2}", 
            endDate.Year, endDate.Month, endDate.Day);

        return "{" + startString + "}:{" + endString + "}";
    }

    private string GetAuthArguments(string username, string password)
    {
        if (String.IsNullOrEmpty(username))
        {
            return String.Empty;
        }

        if (String.IsNullOrEmpty(password))
        {
            password = "\"\"";
        }

        return " --username " + username + " --password " + password;
    }

    private string GetRfc822DateString(DateTime date)
    {
        string dateString = date.ToUniversalTime().ToString("ddd, dd MMM yyyy HH:mm:ss +0000");
        return dateString;
    }

}
