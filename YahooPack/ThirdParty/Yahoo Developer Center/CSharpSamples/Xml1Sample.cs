using System;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace Yahoo.Samples.CSharp
{
	public partial class Xml1
	{
		public void RunSample()
		{
			// Retrieve XML document
			XmlTextReader reader = new XmlTextReader("http://xml.weather.yahoo.com/forecastrss?p=94704");

			// Skip non-significant whitespace
			reader.WhitespaceHandling = WhitespaceHandling.Significant;
			
			// Read nodes one at a time
			while (reader.Read())
			{
				// Print out info on node
				Console.WriteLine("{0}: {1}", reader.NodeType.ToString(), reader.Name);
			}
		}
	}
}
