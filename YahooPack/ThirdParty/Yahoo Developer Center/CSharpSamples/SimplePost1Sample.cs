using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace Yahoo.Samples.CSharp
{
	public partial class SimplePost1
	{
		public void RunSample()
		{
			Uri address = new Uri("http://api.search.yahoo.com/ContentAnalysisService/V1/termExtraction");

			// Create the web request
			HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;

			// Set type to POST
			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded";

			// Create the data we want to send
			string appId = "YahooDemo";
			string context = "Italian sculptors and painters of the renaissance"
								+ "favored the Virgin Mary for inspiration";
			string query = "madonna";

			StringBuilder data = new StringBuilder();
			data.Append("appid=" + HttpUtility.UrlEncode(appId));
			data.Append("&context=" + HttpUtility.UrlEncode(context));
			data.Append("&query=" + HttpUtility.UrlEncode(query));

			// Create a byte array of the data we want to send
			byte[] byteData = UTF8Encoding.UTF8.GetBytes(data.ToString());

			// Set the content length in the request headers
			request.ContentLength = byteData.Length;
			
			// Write data
			using (Stream postStream = request.GetRequestStream())
			{
				postStream.Write(byteData, 0, byteData.Length);
			}

			// Get response
			using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
			{
				// Get the response stream
				StreamReader reader = new StreamReader(response.GetResponseStream());

				// Console application output
				Console.WriteLine(reader.ReadToEnd());
			}
		}
	}
}
