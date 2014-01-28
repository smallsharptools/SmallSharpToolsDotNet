using System;
using System.Web;

namespace Yahoo.Samples.CSharp
{
	public partial class BBAuth3
	{
		public static void GetUserCredentials()
		{
			// Create an instance of Yahoo.Authentication
			Yahoo.Authentication auth = new Authentication("myappid", "mysharedsecret");

			// You must set the token before calling UpdateCredentials
			auth.Token = "storedusertoken";

			// Attempt to get user credentials
			auth.UpdateCredentials();
		}
	}
}
