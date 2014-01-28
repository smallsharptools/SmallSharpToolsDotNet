using System;
using System.Configuration;
using System.Collections;
using System.Xml;
using System.Web;
using System.Collections.Specialized;

namespace SmallSharpTools.YahooPack.Providers
{
    class YahooAuthenticationProviderConfiguration
    {
        string defaultProvider;
        Hashtable providers = new Hashtable();

        /// <summary>
        /// Provider configuration
        /// </summary>
        public static YahooAuthenticationProviderConfiguration GetConfig() {
            return (YahooAuthenticationProviderConfiguration)ConfigurationManager.GetSection("SmallSharpTools.YahooPack.Authentication");
        }

        /// <summary>
        /// Loads XML files
        /// </summary>
		public void LoadValuesFromConfigurationXml(XmlNode node) {
			XmlAttributeCollection attributeCollection = node.Attributes;

			// Get the default provider
			defaultProvider = attributeCollection["defaultProvider"].Value;

			// Read child nodes
			foreach (XmlNode child in node.ChildNodes) {
				if (child.Name == "providers")
					GetProviders(child);
			}
		}

		void GetProviders(XmlNode node) {
			foreach (XmlNode provider in node.ChildNodes) {
				switch (provider.Name) {
					case "add" :
						providers.Add(provider.Attributes["name"].Value, new ProviderAttributes(provider.Attributes) );
						break;

					case "remove" :
						providers.Remove(provider.Attributes["name"].Value);
						break;

					case "clear" :
						providers.Clear();
						break;
				}
			}
		}

		// Properties
		//
        /// <summary>
        /// The default provider
        /// </summary>
		public string DefaultProvider { get { return defaultProvider; } }
		public Hashtable Providers { get { return providers; } } 

	}


}
