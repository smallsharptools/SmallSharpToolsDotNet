using System;
using System.Web;

namespace Yahoo.Samples.CSharp
{
	public partial class BBAuth2
	{
		// This is only required for the sample to compile
		System.Web.HttpRequest Request = null;
		System.Web.HttpResponse Response = null;
		System.Web.SessionState.HttpSessionState Session = null;

		private void Page_Load(object sender, System.EventArgs e)
		{
			bool success = false;

			// Retrieve this user's authentication object we've stored in the session state
			Yahoo.Authentication auth = Session["Auth"] as Yahoo.Authentication;

			if (auth == null)
			{
				// We have a problem with the current session, abandon and retry
				Session.Abandon();
				Response.Redirect("ErrorPage.aspx");
			}

			// Check if we are returning from login
			if (Request.QueryString["token"] != null && Request.QueryString["token"].Length > 0)
			{
				// Make sure the call is valid
				if (auth.IsValidSignedUrl(Request.Url) == true)
				{
					success = true;

					// Save the user token. It is valid for two weeks
					auth.Token = Request.QueryString["token"];
				}
			}

			// Redirect if we succeeded
			if (success == true)
			{
				Response.Redirect("Default.aspx");
			}
			else
			{
				Response.Redirect("SignInError.aspx");
			}
		}

	}
}
