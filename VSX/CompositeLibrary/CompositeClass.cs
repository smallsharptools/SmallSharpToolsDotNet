using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

namespace SmallSharpTools.VSX.Composite
{
    public class CompositeClass
    {
        
        #region "  Variable  "

        public const string CompositeXPath = "/composite";
        public const string TypesXPath = "/composite/types/type";
        public const string PropertiesXPath = "properties/add";

        private Assembly[] _assemblies;

        #endregion
        
        #region "  Constructor  "

        public CompositeClass(Assembly[] assmemblies)
        {
            _assemblies = assmemblies;
        }

        #endregion

        #region "  Methods  "

        public void LoadFile(string filename)
        {
            XmlDocument document = new XmlDocument();
            document.Load(filename);

            XmlNode compositeNode = document.SelectSingleNode(CompositeXPath);
            string rootNamespace = GetNodeValue(compositeNode, "@rootNamespace");
            RootNamespace = rootNamespace;

            XmlNodeList types = document.SelectNodes(TypesXPath);
            foreach (XmlNode typeNode in types)
            {
                string typeName = GetNodeValue(typeNode, "@name");
                string className = GetNodeValue(typeNode, "@className");
                string theNamespace = GetNodeValue(typeNode, "@namespace");
                if (!Namespaces.Contains(theNamespace))
                {
                    Namespaces.Add(theNamespace);
                }
                CompositeType compositeType = new CompositeType(typeName, className, theNamespace);

                Type foundType = FindType(className, theNamespace);

                foreach (PropertyInfo pi in foundType.GetProperties())
                {
                    if (pi.CanRead && pi.CanWrite)
                    {
                        CompositeProperty property = new CompositeProperty(typeName, pi.Name, pi.PropertyType);
                        compositeType.Properties.Add(property);
                    }
                }

                XmlNodeList hiddenNodes = typeNode.SelectNodes(PropertiesXPath);
                foreach (XmlNode propertyNode in hiddenNodes)
                {
                    string propertyName = GetNodeValue(propertyNode, "@name");
                    string alias = GetNodeValue(propertyNode, "@alias");
                    bool isReadOnly = GetBooleanNodeValue(propertyNode, "@isReadOnly");
                    bool isHidden = GetBooleanNodeValue(propertyNode, "@isHidden");

                    CompositeProperty property = compositeType.Properties.FindCompositeProperty(typeName, propertyName);

                    if (property != null)
                    {
                        if (!String.IsNullOrEmpty(alias))
                        {
                            property.Alias = alias;
                        }
                        property.IsReadOnly = isReadOnly;
                        property.IsHidden = isHidden;
                    }
                }
            }

        }

        private string GetNodeValue(XmlNode node, string xpath)
        {
            XmlNode valueNode = node.SelectSingleNode(xpath);
            if (valueNode != null)
            {
                return valueNode.Value;
            }
            else
            {
                return String.Empty;
            }
        }

        private bool GetBooleanNodeValue(XmlNode node, string xpath)
        {
            string value = GetNodeValue(node, xpath);
            if (String.IsNullOrEmpty(value))
            {
                return false;
            }
            return bool.Parse(value);
        }
        
        private Type FindType(string typeName, string namespaceValue)
        {
            Type type = null;
            foreach (Assembly assembly in _assemblies)
            {
                type = assembly.GetType(namespaceValue + "." + typeName);
                if (type != null)
                {
                    break;
                }
            }
            return type;
        }

        #endregion

        #region "  Properties  "

        private string _rootNamespace;

        public string RootNamespace
        {
            get { return _rootNamespace; }
            set { _rootNamespace = value; }
        }

        private List<string> _namespaces = new List<string>();

        public List<string> Namespaces
        {
            get { return _namespaces; }
        }

        private CompositeTypeCollection _types = new CompositeTypeCollection();

        public CompositeTypeCollection Types
        {
            get { return _types; }
        }

        #endregion

    }
}
