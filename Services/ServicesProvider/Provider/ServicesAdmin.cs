using System.Configuration.Provider;
using System.Diagnostics;
using System.Web.Configuration;

namespace SmallSharpTools.Services.Provider
{
    public class ServicesAdmin
    {
        private static ServicesAdmin _instance = null;
        private static ServicesProvider _defaultProvider = null;
        private static ServicesProviderCollection _providers = null;
        private static object _lock = new object();

        private ServicesAdmin() {}

        public ServicesProvider DefaultProvider
        {
            get { return _defaultProvider; }
        }

        public ServicesProvider GetProvider(string name)
        {
            return _providers[name];
        }

        public static ServicesAdmin Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ServicesAdmin();
                    LoadProviders();
                }
                return _instance;
            }
        }

        private static void LoadProviders()
        {
            // Avoid claiming lock if providers are already loaded
            if (_defaultProvider == null)
            {
                lock (_lock)
                {
                    // Do this again to make sure _defaultProvider is still null
                    if (_defaultProvider == null)
                    {
                        ServicesSection section = (ServicesSection)
                            WebConfigurationManager.GetSection("servicesProvider");

                        _providers = new ServicesProviderCollection();

                        ProvidersHelper.InstantiateProviders(
                            section.Providers, _providers, 
                            typeof(ServicesProvider));

                        _defaultProvider = _providers[section.DefaultProvider];

                        if (_defaultProvider == null)
                            throw new ProviderException
                                ("Unable to load default ServiceHostProvider");
                    }
                }
            }
        }

        public ServicesProviderCollection Providers
        {
            get
            {
                return _providers;
            }
        }

        public void Startup()
        {
            Trace.WriteLine("ServicesAdmin - Startup()");
            foreach (ServicesProvider servicesProvider in Providers)
            {
                if (servicesProvider.AutoStart)
                {
                    servicesProvider.Start();
                }
            }
        }

        public void Shutdown()
        {
            Trace.WriteLine("ServicesAdmin - Shutdown()");
            foreach (ServicesProvider servicesProvider in Providers)
            {
                if (servicesProvider.IsActive)
                {
                    servicesProvider.Stop();
                }
            }
        }
    }
}
