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

using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Xml;

namespace SmallSharpTools.Logging
{
    class LoggingProviderConfiguration
    {
        string defaultProvider;
        Hashtable providers = new Hashtable();

        /// <summary>
        /// Providers the configuration
        /// </summary>
        public static LoggingProviderConfiguration GetConfig() {
            //return (LoggingProviderConfiguration)ConfigurationSettings.GetConfig("SmallSharpToolsLoggingProvider");
            return (LoggingProviderConfiguration)ConfigurationManager.GetSection("SmallSharpTools.Logging");
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
						providers.Add(provider.Attributes["name"].Value, new Provider(provider.Attributes) );
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

    /// <summary>
    /// References providers
    /// </summary>
	public class Provider {
		string name;
		string providerType;
		NameValueCollection providerAttributes = new NameValueCollection();

        /// <summary>
        /// References the provider
        /// </summary>
		public Provider (XmlAttributeCollection attributes) {

			// Set the name of the provider
			//
			name = attributes["name"].Value;

			// Set the type of the provider
			//
			providerType = attributes["type"].Value;

			// Store all the attributes in the attributes bucket
			//
			foreach (XmlAttribute attribute in attributes) {

				if ( (attribute.Name != "name") && (attribute.Name != "type") )
					providerAttributes.Add(attribute.Name, attribute.Value);

			}

		}

        /// <summary>
        /// Name
        /// </summary>
		public string Name {
			get {
				return name;
			}
		}

        /// <summary>
        /// Type
        /// </summary>
		public string Type {
			get {
				return providerType;
			}
		}

        /// <summary>
        /// Attributes
        /// </summary>
		public NameValueCollection Attributes {
			get {
				return providerAttributes;
			}
		}

	}

}
