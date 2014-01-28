using System;
using System.IO;
using System.Net;
using System.Text;

namespace Yahoo.Samples.CSharp
{
	public partial class SimpleGet2
	{
		public void RunSample()
		{
			PrintSource(new Uri("http://developer.yahoo.com"));
		}

		public static void PrintSource(Uri address)
		{
			HttpWebRequest request;
			HttpWebResponse response = null;
			StreamReader reader;
			StringBuilder sbSource;

			if (address == null) { throw new ArgumentNullException("address"); }

			try
			{
				// Create and initialize the web request
				request = WebRequest.Create(address) as HttpWebRequest;
				request.UserAgent = ".NET Sample";
				request.KeepAlive = false;
				// Set timeout to 15 seconds
				request.Timeout = 15 * 1000;

				// Get response
				response = request.GetResponse() as HttpWebResponse;

				if (request.HaveResponse == true && response != null)
				{
					// Get the response stream
					reader = new StreamReader(response.GetResponseStream());

					// Read it into a StringBuilder
					sbSource = new StringBuilder(reader.ReadToEnd());

					// Console application output
					Console.WriteLine(sbSource.ToString());
				}
			}
			catch (WebException wex)
			{
				// This exception will be raised if the server didn't return 200 - OK
				// Try to retrieve more information about the network error
				if (wex.Response != null)
				{
					using (HttpWebResponse errorResponse = (HttpWebResponse)wex.Response)
					{
						Console.WriteLine("The server returned '{0}' with the status code {1} ({2:d}).",
							errorResponse.StatusDescription, errorResponse.StatusCode,
							errorResponse.StatusCode);
					}
				}
			}
			finally
			{
				if (response != null) { response.Close(); }
			}
		}
	}
}
