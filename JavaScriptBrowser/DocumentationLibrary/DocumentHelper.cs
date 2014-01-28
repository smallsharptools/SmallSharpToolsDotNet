using System;
using System.Reflection;
using System.IO;

namespace SmallSharpTools.JavaScriptBrowser.Documentation
{
    public class DocumentHelper
    {
        public const string DocumentRoot = "SmallSharpTools.JavaScriptBrowser.Documentation.";
        public const string XslDocument = "XmlDocComments.xsl";
        public const string XsdDocument = "XmlDocComments.xsd";
        public const string CssDocument = "XmlDocComments.css";
        public const string JavaScriptCoreDocument = "JavaScriptCore.xml";
        public const string JQueryDocument = "jQuery.xml";
        public const string PrototypeDocument = "Prototype.xml";
        public static readonly string[] DefaultFiles = 
        {
            XslDocument, XsdDocument, CssDocument, JQueryDocument, PrototypeDocument, JavaScriptCoreDocument
        };

        private DocumentHelper()
        {
        }

        public static string GetDocumentAsString(string name)
        {
            // set the prefix
            name = DocumentRoot + name;
            if (IsNameValid(name))
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                StreamReader sr = new StreamReader(assembly.GetManifestResourceStream(name));
                return sr.ReadToEnd();
            }
            return String.Empty;
        }

        public static bool IsNameValid(string name)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            foreach (string resourceName in assembly.GetManifestResourceNames())
            {
                if (resourceName.Equals(name))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
