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
using System.Configuration;
using System.Xml;

namespace SmallSharpTools.Logging
{
    class LoggingProviderConfigurationHandler : IConfigurationSectionHandler
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
            LoggingProviderConfiguration config = new LoggingProviderConfiguration();
            config.LoadValuesFromConfigurationXml(node);
            return config;
        }
    }
}
