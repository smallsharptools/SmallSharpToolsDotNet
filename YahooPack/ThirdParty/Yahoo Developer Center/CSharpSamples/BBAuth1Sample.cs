using System;
using System.Web;

namespace Yahoo.Samples.CSharp
{
	public partial class BBAuth1
	{
		// This is only required for the sample to compile
		System.Web.HttpResponse Response = null;

		public void SignInRedirect()
		{
			// Create an instance of Yahoo.Authentication
			Yahoo.Authentication auth = new Authentication("myappid", "mysharedsecret");

			// Redirect the user to the use sign-in page
			Response.Redirect(auth.GetUserLogOnAddress().ToString());
		}
	}
}
