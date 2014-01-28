using System;
using System.IO;
using System.Net;
using System.Text;

namespace Yahoo.Samples.CSharp
{
	public partial class AuthenticatedRequest1
	{

		#region Public methods

		public void RunSample()
		{
			// Create the web request
			HttpWebRequest request 
				= WebRequest.Create("https://api.del.icio.us/v1/posts/recent") as HttpWebRequest;

			// Add authentication to request
			request.Credentials = new NetworkCredential("username", "password");

			// Get response
			using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
			{
				// Get the response stream
				StreamReader reader = new StreamReader(response.GetResponseStream());

				// Console application output
				Console.WriteLine(reader.ReadToEnd());
			}
		}

		#endregion

	}
}
