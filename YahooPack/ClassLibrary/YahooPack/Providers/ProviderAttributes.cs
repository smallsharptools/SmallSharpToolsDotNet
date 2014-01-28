using System;
using System.Configuration;
using System.Collections;
using System.Xml;
using System.Web;
using System.Collections.Specialized;

namespace SmallSharpTools.YahooPack.Providers
{

    /// <summary>
    /// References providers
    /// </summary>
    public class ProviderAttributes
    {
        string name;
        string providerType;
        NameValueCollection providerAttributes = new NameValueCollection();

        /// <summary>
        /// References the provider
        /// </summary>
        public ProviderAttributes(XmlAttributeCollection attributes)
        {

            // Set the name of the provider
            //
            name = attributes["name"].Value;

            // Set the type of the provider
            //
            providerType = attributes["type"].Value;

            // Store all the attributes in the attributes bucket
            //
            foreach (XmlAttribute attribute in attributes)
            {

                if ((attribute.Name != "name") && (attribute.Name != "type"))
                    providerAttributes.Add(attribute.Name, attribute.Value);

            }

        }

        /// <summary>
        /// Name
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }

        /// <summary>
        /// Type
        /// </summary>
        public string Type
        {
            get
            {
                return providerType;
            }
        }

        /// <summary>
        /// Attributes
        /// </summary>
        public NameValueCollection Attributes
        {
            get
            {
                return providerAttributes;
            }
        }

    }
}