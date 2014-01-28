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
using System.Collections.Specialized;

namespace SmallSharpTools.UrlMapping
{
    public class StaticUrlMappingProvider : UrlMappingProvider
    {
        private static string _virtualPath = String.Empty;
        private static string _mappedPath = String.Empty;
        private static bool _loggingEnabled = false;

        /// <summary>
        /// Log4Net Implementation
        /// </summary>
        public override void Initialize(string name, NameValueCollection configValue)
        {
            if (!String.IsNullOrEmpty(configValue["virtualPath"]))
            {
                _virtualPath = UrlHelper.GetRelativeSiteUrl(configValue["virtualPath"]);
            }
            else
            {
                throw new ApplicationException("Required attribute not defined: virtualPath");
            }
            if (!String.IsNullOrEmpty(configValue["mappedPath"]))
            {
                _mappedPath = UrlHelper.GetRelativeSiteUrl(configValue["mappedPath"]);
            }
            else
            {
                throw new ApplicationException("Required attribute not defined: mappedPath");
            }
            if (!String.IsNullOrEmpty(configValue["loggingEnabled"]))
            {
                _loggingEnabled = "true".Equals(configValue["loggingEnabled"].ToLower());
            }
        }

        public override bool IsMappedPath(string path)
        {
            return path.ToLower().StartsWith(VirtualPath.ToLower());
        }

        public override string GetMappedPath(string path)
        {
            if (IsMappedPath(path))
            {
                return MappedPath;
            }
            else
            {
                return path;
            }
        }
        
        public static string MappedPath
        {
            get
            {
                return _mappedPath;
            }
        }
        
        public static string VirtualPath
        {
            get
            {
                return _virtualPath;
            }
        }
        
        public override bool IsLoggingEnabled
        {
            get
            {
                return _loggingEnabled;
            }
        }
    }
}
