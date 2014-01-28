using System;
using System.IO;
using System.Net;
using System.Text;

namespace Yahoo.Samples.CSharp
{
	public partial class StringGet1
	{
		public void RunSample()
		{
			Console.WriteLine(GetPageAsString(new Uri("http://developer.yahoo.com/")));
		}

		public static string GetPageAsString(Uri address)
		{
			string result = "";

			// Create the web request
			HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;

			// Get response
			using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
			{
				// Get the response stream
				StreamReader reader = new StreamReader(response.GetResponseStream());

				// Read the whole contents and return as a string
				result = reader.ReadToEnd();
			}

			return result;
		}
	}
}
