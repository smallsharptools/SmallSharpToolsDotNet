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
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Provider;
using System.Web;
using System.Web.Caching;

namespace SmallSharpTools.UrlMapping
{
    /// <summary>
    /// Photo Album Provider
    /// </summary>
    public abstract class UrlMappingProvider : ProviderBase
    {

        /// <summary>
        /// Default Datetime
        /// </summary>
        public readonly static DateTime DefaultDatetime = DateTime.Parse("01/01/1754");

        static UrlMappingProvider _instance;

        static object padLock;

        #region "  Utility Methods  "

        /// <summary>
        /// Instance of provider
        /// </summary>
        public static UrlMappingProvider Instance
        {
            get
            {
                UrlMappingProvider tempInstance;
                if (_instance == null)
                    padLock = new object();
                lock (padLock)
                {
                    tempInstance = LoadProvider();
                    _instance = tempInstance;
                }

                return _instance;
            }
        }

        /// <summary>
        /// Loads provider
        /// </summary>
        static UrlMappingProvider LoadProvider()
        {
            // Get the names of the providers
            // Use the cache because the reflection used later is expensive
            Cache cache = HttpRuntime.Cache;
            string cacheKey;

            UrlMappingProvider _instanceLoader;
            UrlMappingProviderConfiguration config = UrlMappingProviderConfiguration.GetConfig();
            cacheKey = "UrlMappingProvider::" + config.DefaultProvider;

            object oProvider = cache.Get(cacheKey);
            if (oProvider != null)
            {
                _instanceLoader = (UrlMappingProvider)oProvider;
            }
            else
            {

                try
                {

                    // Read the configuration specific information for this provider
                    Provider provider = (Provider)config.Providers[config.DefaultProvider];

                    // The assembly should be in \bin or GAC
                    Type type = Type.GetType(provider.Type);
                    _instanceLoader = (UrlMappingProvider)Activator.CreateInstance(type);

                    // Initialize the provider with the attributes.
                    string cStringName = provider.Attributes["connectionStringName"];

                    if (!String.IsNullOrEmpty(cStringName) &&
                        ConfigurationManager.ConnectionStrings != null &&
                        ConfigurationManager.ConnectionStrings[cStringName] != null)
                    {
                        string cString = ConfigurationManager.ConnectionStrings[cStringName].ConnectionString;
                        provider.Attributes.Add("connectionString", cString);
                    }
                    _instanceLoader.Initialize(provider.Name, provider.Attributes);

                    //pop it into the cache to keep out site from running into the ground :)
                    cache.Insert(cacheKey, _instanceLoader);
                }
                catch (Exception e)
                {
                    throw new Exception("Unable to load provider", e);
                }
            }
            return _instanceLoader;
        }

        #endregion

        #region "  Interface Methods  "

        public abstract bool IsMappedPath(string path);

        public abstract string GetMappedPath(string path);

        public abstract bool IsLoggingEnabled { get; }

        #endregion

    }
}
