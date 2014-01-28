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
using System.IO;
using System.Web;
using System.Web.Caching;
using log4net;
using log4net.Config;

namespace SmallSharpTools.Logging
{
    class Log4NetLoggingProvider : LoggingProvider
    {

        #region "  Variables  "
        private static string _configurationFile = "";
        private static bool _configureAndWatch = false;
        private static bool _isLoggerInitialized = false;
        #endregion

        #region "  Implementation Methods  "

        /// <summary>
        /// Log4Net Implementation
        /// </summary>
        public override void Initialize(string name, NameValueCollection configValue)
        {
            string configurationFile = configValue["configurationFile"];
            string configureAndWatch = configValue["configureAndWatch"];

            if (!String.IsNullOrEmpty(configurationFile))
            {
                _configurationFile = configurationFile;
            }
            if (!String.IsNullOrEmpty(configureAndWatch))
            {
                _configureAndWatch = "true".Equals(configureAndWatch.ToLower());
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

        private static ILogger GetCachedLogger(Type type)
        {
            Cache cache = HttpRuntime.Cache;
            string cacheKey = "ILogger::" + type.ToString();
            object loggerObj = cache.Get(cacheKey);
            ILog logger;
            if (loggerObj != null)
            {
                logger = (ILog)loggerObj;
            }
            else
            {
                logger = LogManager.GetLogger(type);
                cache.Insert(cacheKey, logger);
            }
            Log4NetImpl wrapper = new Log4NetImpl(logger);
            return wrapper;
        }

        private void InitializeLogging()
        {
            try
            {
                FileInfo config = new FileInfo(ConfigurationFile);
                if (!config.Exists)
                {
                    throw new ApplicationException("Logger configuration does not exist: " + config.FullName);
                }
                if (ConfigureAndWatch)
                {
                    XmlConfigurator.ConfigureAndWatch(config);
                }
                else
                {
                    XmlConfigurator.Configure(config);
                }
                _isLoggerInitialized = true;
                ILogger newLogger = GetLogger(typeof(Log4NetLoggingProvider));
                if (newLogger != null)
                {
                    newLogger.Info("Logging configured");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to configure logging: " + ex.Message);
            }
        }
        
        #endregion

        #region "  Properties  "

        public string ConfigurationFile
        {
            get
            {
                if (!String.IsNullOrEmpty(_configurationFile))
                {
                    return _configurationFile;
                }
                else
                {
                    // attempt to make it work for the command-line
                    string configFile = Path.Combine(Environment.CurrentDirectory, "log4net.config");
                    if (File.Exists(configFile))
                    {
                        return configFile;
                    }
                    // default value
                    return Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "log4net.config");
                }
            }
        }
        
        public bool ConfigureAndWatch
        {
            get
            {
                return _configureAndWatch;
            }
        }
        
        #endregion

        #region "  Utility Methods  "

        #endregion

    }
}
