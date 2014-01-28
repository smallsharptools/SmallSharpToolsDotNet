/*=======================================================================
  Copyright (C) SmallSharpTools.com.  All rights reserved.
 
  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
  PARTICULAR PURPOSE.
  
  Brennan Stehling
  brennan@smallsharptools.com
  http://www.smallsharptools.com/
=======================================================================*/

using System;
using System.Web;

namespace SmallSharpTools.UrlMapping
{
    public class UrlHelper
    {

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

        public static string GetRelativeSiteRoot()
        {
            if ("/".Equals(HttpContext.Current.Request.ApplicationPath))
            {
                return String.Empty;
            }
            return HttpContext.Current.Request.ApplicationPath;
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

        public static string GetUrlPath(string url)
        {
            return HttpContext.Current.Request.MapPath(url);
        }
        
    }
}
