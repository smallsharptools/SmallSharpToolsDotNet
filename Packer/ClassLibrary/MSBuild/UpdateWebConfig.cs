#region Copyright © 2007-2009 Brennan Stehling. SmallSharpTools.com. All rights reserved.
/*
Copyright © 2007-2009 Brennan Stehling. SmallSharpTools.com. All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions
are met:

1. Redistributions of source code must retain the above copyright
   notice, this list of conditions and the following disclaimer.
2. Redistributions in binary form must reproduce the above copyright
   notice, this list of conditions and the following disclaimer in the
   documentation and/or other materials provided with the distribution.
3. The name of the author may not be used to endorse or promote products
   derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE AUTHOR "AS IS" AND ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT, INDIRECT,
INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE. 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System.IO;
using Properties = SmallSharpTools.Packer.Utilities.Properties;

namespace SmallSharpTools.Packer.MSBuild
{

    public class UpdateWebConfig : Task
    {

        #region "  Constants  "

        public const string CustomErrorsModeXPath = "/configuration/system.web/customErrors/@mode";
        public const string DebugXPath = "/configuration/system.web/compilation/@debug";
        public const string ConnectionStringConfigSourceXPath = "/configuration/connectionStrings/@configSource";
        public const string AppSettingXPath = "/configuration/appSettings/add[@key='{0}']/@value";
        public const string MailHostXPath = "/configuration/system.net/mailSettings/smtp/network/@host";

        #endregion

        #region  "  Properties  "

        /// <summary>
        /// The input and possibly the output file depending on value of OutputPath.
        /// </summary>
        public string WebConfigPath { get; set; }

        /// <summary>
        /// Option value for the output file. When not defined output is written to WebConfigPath.
        /// </summary>
        public string OutputPath { get; set; }

        /// <summary>
        /// Sets customer errors mode: /configuration/system.web/customErrors/@mode
        /// </summary>
        public string CustomErrorsMode { get; set; }

        /// <summary>
        /// Sets debug mode: /configuration/system.web/compilation/@debug
        /// </summary>
        public string Debug { get; set; }
        
        /// <summary>
        /// Sets the configSource attribute on connectionStrings element: /configuration/connectionStrings/@configSource
        /// </summary>
        public string ConnectionStringConfigSource { get; set; }

        /// <summary>
        /// Sets multiple appSetting values using name/value pairs, like Key1=Value1;Key2=Value2.
        /// Whitespace across multiple lines is allowed as all values are trimmed of whitespace.
        /// </summary>
        public string AppSettings { get; set; }

        /// <summary>
        /// Sets mail host: /configuration/system.net/mailSettings/smtp/network/@host
        /// </summary>
        public string MailHost { get; set; }

        /// <summary>
        /// Controls whether additional details are printed to the console
        /// </summary>
        public bool Verbose { get; set; }

        #endregion

        #region "  Methods  "

        private string Trim(string str)
        {
            if (!String.IsNullOrEmpty(str))
            {
                return str.Trim();
            }
            else
            {
                return str;
            }
        }

        #endregion

        #region " Task Implementation  "

        public override bool Execute()
        {
            string outputFilename = WebConfigPath;
            if (!String.IsNullOrEmpty(OutputPath))
            {
                outputFilename = OutputPath;
            }

            try
            {
                if (!File.Exists(WebConfigPath))
                {
                    throw new ApplicationException(String.Format(Properties.Resources.WebConfigUpdateFileNotFound, WebConfigPath));
                }

                Log.LogMessage(Properties.Resources.WebConfigUpdateDocument, Trim(WebConfigPath));
                XmlDocument document = new XmlDocument();
                document.Load(WebConfigPath);

                XPathNavigator navigator = document.CreateNavigator();
                XmlNamespaceManager manager = new XmlNamespaceManager(navigator.NameTable);

                if (!String.IsNullOrEmpty(Trim(Debug)))
                {
                    if (ChangeValue(navigator, manager, DebugXPath, Trim(Debug)) && Verbose)
                    {
                        Log.LogMessage(Properties.Resources.WebConfigUpdateDebug, Trim(Debug));
                    }
                }

                if (!String.IsNullOrEmpty(Trim(CustomErrorsMode)))
                {
                    if (ChangeValue(navigator, manager, CustomErrorsModeXPath, Trim(CustomErrorsMode)) && Verbose)
                    {
                        Log.LogMessage(Properties.Resources.WebConfigUpdateCustomErrorsMode, Trim(CustomErrorsMode));
                    }
                }

                if (!String.IsNullOrEmpty(Trim(ConnectionStringConfigSource)))
                {
                    if (ChangeValue(navigator, manager, ConnectionStringConfigSourceXPath, Trim(ConnectionStringConfigSource)) && Verbose)
                    {
                        Log.LogMessage(Properties.Resources.WebConfigUpdateConnectionStringConfigSource,
                            Trim(ConnectionStringConfigSource));
                    }
                }

                // AppSettings
                string appSettings = Trim(AppSettings);
                int index = 0;

                if (!String.IsNullOrEmpty(appSettings))
                {
                    string[] pairs = appSettings.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    foreach (string pair in pairs)
                    {
                        if (!String.IsNullOrEmpty(pair))
                        {
                            string trimmedPair = pair.Trim();
                            if (!String.IsNullOrEmpty(trimmedPair))
                            {
                                index = trimmedPair.IndexOf("=");
                                if (index <= 0)
                                {
                                    throw new ApplicationException(String.Format("AppSetting pair is invalid: ({0})", trimmedPair));
                                }
                                string key = trimmedPair.Substring(0, index);
                                string val = trimmedPair.Substring(index + 1);
                                if (ChangeValue(navigator, manager, String.Format(AppSettingXPath, Trim(key)), Trim(val)) && Verbose)
                                {
                                    Log.LogMessage(Properties.Resources.WebConfigUpdateAppSetting, Trim(key), Trim(val));
                                }
                            }
                        }
                    }
                }

                if (!String.IsNullOrEmpty(Trim(MailHost)) && Verbose)
                {
                    if (ChangeValue(navigator, manager, MailHostXPath, Trim(MailHost)) && Verbose)
                    {
                        Log.LogMessage(Properties.Resources.WebConfigUpdateMailHost, Trim(MailHost));
                    }
                }

                using (XmlTextWriter writer = new XmlTextWriter(outputFilename, Encoding.UTF8))
                {
                    writer.Formatting = Formatting.Indented;
                    document.Save(writer);
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                Log.LogErrorFromException(ex);
                return false;
            }

            Log.LogMessage(Properties.Resources.WebConfigUpdateResult, WebConfigPath, outputFilename);
            return true;
        }

        private bool ChangeValue(XPathNavigator navigator, XmlNamespaceManager manager, string xpath, string value)
        {
            XPathExpression expression = XPathExpression.Compile(xpath, manager);
            XPathNodeIterator nodes = navigator.Select(expression);

            if (nodes.MoveNext())
            {
                nodes.Current.SetValue(value ?? String.Empty);
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

    }

}
