using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using SmallSharpTools.YahooPack.Web.Security;

namespace SmallSharpTools.YahooPack.Providers
{
    /// <summary>
    /// Yahoo Authentication Provider
    /// </summary>
    public abstract class YahooAuthenticationProvider : ProviderBase
    {
        
        #region "  Variables  "
        
        /// <summary>
        /// Default Datetime
        /// </summary>
        public readonly static DateTime DefaultDatetime = DateTime.Parse("01/01/1754");

        static YahooAuthenticationProvider _instance;

        static object padLock;

        #endregion

        #region "  Utility Methods  "

        /// <summary>
        /// Instance of provider
        /// </summary>
        public static YahooAuthenticationProvider Instance
        {
            get
            {
                YahooAuthenticationProvider tempInstance;
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
        static YahooAuthenticationProvider LoadProvider()
        {
            // Get the names of the providers
            // Use the cache because the reflection used later is expensive
            Cache cache = HttpRuntime.Cache;
            string cacheKey;

            YahooAuthenticationProvider _instanceLoader;
            YahooAuthenticationProviderConfiguration config = YahooAuthenticationProviderConfiguration.GetConfig();
            cacheKey = "YahooAuthenticationProvider::" + config.DefaultProvider;

            object oProvider = cache.Get(cacheKey);
            if (oProvider != null)
            {
                _instanceLoader = (YahooAuthenticationProvider)oProvider;
            }
            else
            {

                try
                {

                    // Read the configuration specific information for this provider
                    ProviderAttributes providerAttributes = (ProviderAttributes)config.Providers[config.DefaultProvider];

                    // The assembly should be in \bin or GAC
                    Type type = Type.GetType(providerAttributes.Type);
                    _instanceLoader = (YahooAuthenticationProvider)Activator.CreateInstance(type);

                    // Initialize the provider with the attributes.
                    string cStringName = providerAttributes.Attributes["connectionStringName"];

                    if (!String.IsNullOrEmpty(cStringName) &&
                        ConfigurationManager.ConnectionStrings != null &&
                        ConfigurationManager.ConnectionStrings[cStringName] != null)
                    {
                        string cString = ConfigurationManager.ConnectionStrings[cStringName].ConnectionString;
                        providerAttributes.Attributes.Add("connectionString", cString);
                    }
                    _instanceLoader.Initialize(providerAttributes.Name, providerAttributes.Attributes);

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

        #region "  Base Methods  "
        
        public void ClearSession()
        {
            YahooSession yahooSession = LoadSession();
            yahooSession.ClearAuthentication();
            SaveSession(yahooSession);
        }
        
        #endregion

        #region "  Interface Methods  "

        public abstract YahooSession LoadSession();
        
        public abstract void SaveSession(YahooSession yahooSession);

        public abstract bool IsLoggingEnabled { get; }

        #endregion

        #region "  Properties  "
        
        public static Guid UserID
        {
            get
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    return (Guid)Membership.GetUser().ProviderUserKey;
                }
                else
                {
                    Guid userId = new Guid(HttpContext.Current.Request.AnonymousID.Substring(0, 36));
                    return userId;
                }
            }
        }
        
        #endregion
        
    }
}
