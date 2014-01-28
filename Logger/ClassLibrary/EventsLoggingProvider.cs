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
using System.Diagnostics;
using System.Web;
using System.Web.Caching;

namespace SmallSharpTools.Logging
{
    public class EventsLoggingProvider : LoggingProvider
    {
        private static string _eventSource = String.Empty;
        private static bool _debugEnabled = false;
        private static bool _infoEnabled = false;
        private static bool _warnEnabled = true;
        private static bool _errorEnabled = true;
        private static bool _fatalEnabled = true;
        private static bool _isLoggerInitialized = false;

        #region "  Implementation Methods  "
        
        /// <summary>
        /// Events Log implementation
        /// </summary>
        /// <param name="name"></param>
        /// <param name="configValue"></param>
        public override void Initialize(string name, NameValueCollection configValue)
        {
            if (!String.IsNullOrEmpty(configValue["eventSource"]))
            {
                _eventSource = configValue["eventSource"];
            }
            if (!String.IsNullOrEmpty(configValue["debugEnabled"]))
            {
                _debugEnabled = "true".Equals(configValue["debugEnabled"].ToLower());
            }
            if (!String.IsNullOrEmpty(configValue["infoEnabled"]))
            {
                _infoEnabled = "true".Equals(configValue["infoEnabled"].ToLower());
            }
            if (!String.IsNullOrEmpty(configValue["warnEnabled"]))
            {
                _warnEnabled = "true".Equals(configValue["warnEnabled"].ToLower());
            }
            if (!String.IsNullOrEmpty(configValue["errorEnabled"]))
            {
                _errorEnabled = "true".Equals(configValue["errorEnabled"].ToLower());
            }
            if (!String.IsNullOrEmpty(configValue["fatalEnabled"]))
            {
                _fatalEnabled = "true".Equals(configValue["fatalEnabled"].ToLower());
            }
        }
        
        public override ILogger GetLogger(Type type)
        {
            if (!_isLoggerInitialized)
            {
                InitializeLogging();
            }
            return GetCachedLogger(type);
        }
        
        #endregion

        #region "  Private Methods  "
    
        private void InitializeLogging()
        {
            if (_isLoggerInitialized)
            {
                if (!EventLog.SourceExists(EventSource))
                {
                    EventLog.CreateEventSource(EventSource, "Application");
                }
            }
            _isLoggerInitialized = true;
        }

        private ILogger GetCachedLogger(Type type)
        {
            Cache cache = HttpRuntime.Cache;
            string cacheKey = "ILogger::" + type.ToString();
            object loggerObj = cache.Get(cacheKey);
            ILogger logger;
            if (loggerObj != null)
            {
                logger = (ILogger)loggerObj;
            }
            else
            {
                logger = new EventsImpl();
                cache.Insert(cacheKey, logger);
            }
            return logger;
        }
        
        #endregion

        #region "  Properties  "
        
        public static string EventSource
        {
            get
            {
                return _eventSource;
            }
        }
        
        public static bool IsDebugEnabled
        {
            get
            {
                return _debugEnabled;
            }
        }

        public static bool IsInfoEnabled
        {
            get
            {
                return _infoEnabled;
            }
        }

        public static bool IsWarnEnabled
        {
            get
            {
                return _warnEnabled;
            }
        }

        public static bool IsErrorEnabled
        {
            get
            {
                return _errorEnabled;
            }
        }

        public static bool IsFatalEnabled
        {
            get
            {
                return _fatalEnabled;
            }
        }
        
        #endregion

    }
}
