using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.ServiceModel;

namespace SmallSharpTools.Services.Provider
{
    public abstract class ServicesProvider : ProviderBase
    {

        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            if (String.IsNullOrEmpty(config["isManager"]))
            {
                config.Remove("isManager");
                config.Add("isManager", "false");
            }

            bool isManager;
            Boolean.TryParse(config["isManager"], out isManager);
            IsManager = isManager;
            config.Remove("isManager");

            if (String.IsNullOrEmpty(config["autoStart"]))
            {
                config.Remove("autoStart");
                config.Add("autoStart", "false");
            }

            bool autoStart;
            Boolean.TryParse(config["autoStart"], out autoStart);
            AutoStart = autoStart;
            config.Remove("autoStart");

            if (String.IsNullOrEmpty(config["serviceUri"]))
            {
                config.Remove("serviceUri");
                config.Add("serviceUri", "");
            }

            ServiceUri = config["serviceUri"];
            config.Remove("serviceUri");

            base.Initialize(name, config);
        }

        private string _serviceUri = String.Empty;
        public string ServiceUri
        {
            get
            {
                return _serviceUri;
            }
            set
            {
                _serviceUri = value;
            }
        }

        #region "  Base Methods and Properties  "

        private ServiceHost serviceHost = null;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public virtual void Start()
        {
            if (!IsActive)
            {
                // start the service
                Trace.WriteLine("Starting " + Name + " on " + ServiceUri);
                Type serviceType = GetServiceType();
                serviceHost = new ServiceHost(serviceType, new Uri(ServiceUri));
                serviceHost.Open();
                IsActive = true;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public virtual void Stop()
        {
            if (IsActive)
            {
                // stop the service
                Trace.WriteLine("Stopping " + Name + " on " + ServiceUri);
                serviceHost.Close();
                IsActive = false;
            }
        }

        private bool _isManager = false;
        public virtual bool IsManager
        {
            get { return _isManager; }
            protected set { _isManager = value; }
        }

        private bool _isActive = false;
        public virtual bool IsActive
        {
            get { return _isActive; }
            protected set { _isActive = value; }
        }

        private bool _autoStart = false;
        public virtual bool AutoStart
        {
            get
            {
                return _autoStart;
            }
            set
            {
                _autoStart = value;
            }
        }

        #endregion

        #region "  Abstract Methods  "

        public abstract Type GetServiceType();
        
        #endregion

    }
}
