using System;
using System.Web;

namespace Yahoo.Samples.CSharp
{
	public partial class BBAuth4
	{
		// This is only required for the sample to compile
		System.Web.SessionState.HttpSessionState Session = null;
		System.Web.UI.HtmlControls.HtmlGenericControl Div1 = new System.Web.UI.HtmlControls.HtmlGenericControl("div");

		public void CallAuthenticatedWebService()
		{
			// Retrieve this user's authentication object we've stored in the session state
			Yahoo.Authentication auth = Session["Auth"] as Yahoo.Authentication;

			if (auth != null)
			{
				// Call web service and output result into a DIV tag
				Div1.InnerHtml = auth.GetAuthenticatedServiceString(
					new System.Uri("http://photos.yahooapis.com/V1.0/listServices"));
			}
		}
	}
}
