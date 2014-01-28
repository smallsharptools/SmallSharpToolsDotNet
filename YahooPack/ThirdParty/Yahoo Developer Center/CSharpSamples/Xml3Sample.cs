using System;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace Yahoo.Samples.CSharp
{
	public partial class Xml3
	{
		public void RunSample()
		{
			// Create a new XmlDocument
			XPathDocument doc = new XPathDocument("http://xml.weather.yahoo.com/forecastrss?p=94704");

			// Create navigator
			XPathNavigator navigator = doc.CreateNavigator();

			// Set up namespace manager for XPath
			XmlNamespaceManager ns = new XmlNamespaceManager(navigator.NameTable);
			ns.AddNamespace("yweather", "http://xml.weather.yahoo.com/ns/rss/1.0");

			// Get forecast with XPath
			XPathNodeIterator nodes = navigator.Select("/rss/channel/item/yweather:forecast", ns);

			while(nodes.MoveNext())
			{
				XPathNavigator node = nodes.Current;

				Console.WriteLine("{0}: {1}, {2}F - {3}F",
									node.GetAttribute("day", ns.DefaultNamespace),
									node.GetAttribute("text", ns.DefaultNamespace),
									node.GetAttribute("low", ns.DefaultNamespace),
									node.GetAttribute("high", ns.DefaultNamespace));
			}
		}
	}
}
