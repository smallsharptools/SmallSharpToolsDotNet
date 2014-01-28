using System;
using System.Web;

namespace Yahoo.Samples.CSharp
{
	public partial class Caching2
	{
		// This is only required for the sample to compile
		System.Web.SessionState.HttpSessionState Session = null;

		public void SessionSample()
		{
			string myData = "... sample returned xml data ...";

			// Save data in per-user session state
			Session.Add("UniqueKey", myData);
		}
	}
}
