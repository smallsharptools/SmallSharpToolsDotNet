using System;
using System.Collections.Specialized;
using System.Configuration.Provider;
using SmallSharpTools.Services.Provider;

namespace SmallSharpTools.Services.Generic
{
    public class GenericServicesProvider : ServicesProvider
    {

        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            if (String.IsNullOrEmpty(name))
            {
                name = "GenericServicesProvider";
            }

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Generic Service Host Provider");
            }

            base.Initialize(name, config);

            if (config.Count > 0)
            {
                string attr = config.GetKey(0);
                if (!String.IsNullOrEmpty(attr))
                {
                    throw new ProviderException("Unrecognized attribute: " + attr);
                }
            }
        }

        #region "  Implementation Methods  "

        public override Type GetServiceType()
        {
            return typeof(GenericService);
        }

        #endregion
    }
}
