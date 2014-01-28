using System;
using System.Configuration;
using System.Xml;

namespace SmallSharpTools.YahooPack.Providers
{
    class YahooAuthenticationProviderConfigurationHandler : IConfigurationSectionHandler
    {
        /// <summary>
        /// Creates photo provider configuration
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="context"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public virtual object Create(Object parent, Object context, XmlNode node)
        {
            YahooAuthenticationProviderConfiguration config = new YahooAuthenticationProviderConfiguration();
            config.LoadValuesFromConfigurationXml(node);
            return config;
        }
    }
}
