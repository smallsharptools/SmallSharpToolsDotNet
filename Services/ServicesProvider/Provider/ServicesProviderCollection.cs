using System;
using System.Collections.Generic;
using System.Configuration.Provider;

namespace SmallSharpTools.Services.Provider
{
    public class ServicesProviderCollection : ProviderCollection
    {
        public new ServicesProvider this[string name]
        {
            get { return (ServicesProvider)base[name]; }
        }

        public override void Add(ProviderBase provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            if (!(provider is ServicesProvider))
                throw new ArgumentException
                    ("Invalid provider type", "provider");

            base.Add(provider);
        }
    }
}
