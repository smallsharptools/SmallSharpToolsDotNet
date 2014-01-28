using System;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace Yahoo.Samples.CSharp
{
	public partial class Xml2
	{
		public void RunSample()
		{
			// Create a new XmlDocument
			XmlDocument doc = new XmlDocument();

			// Load data
			doc.Load("http://xml.weather.yahoo.com/forecastrss?p=94704");

			// Set up namespace manager for XPath
			XmlNamespaceManager ns = new XmlNamespaceManager(doc.NameTable);
			ns.AddNamespace("yweather", "http://xml.weather.yahoo.com/ns/rss/1.0");

			// Get forecast with XPath
			XmlNodeList nodes = doc.SelectNodes("/rss/channel/item/yweather:forecast", ns);

			// You can also get elements based on their tag name and namespace,
			// though this isn't recommended
			//XmlNodeList nodes = doc.GetElementsByTagName("forecast", 
			//							"http://xml.weather.yahoo.com/ns/rss/1.0");

			foreach(XmlNode node in nodes)
			{
				Console.WriteLine("{0}: {1}, {2}F - {3}F",
									node.Attributes["day"].InnerText,
									node.Attributes["text"].InnerText,
									node.Attributes["low"].InnerText,
									node.Attributes["high"].InnerText);
			}
		}
	}
}
