using System;
using System.Web;

namespace Yahoo.Samples.CSharp
{
	public partial class Caching1
	{
		// This only required for the sample to compile
		System.Web.Caching.Cache Cache = null;

		public void CacheSample()
		{
			string myData = "... sample returned xml data ...";

			// Cache the data for 5 minutes
			Cache.Insert("UniqueKey", myData, null, 
							DateTime.Now.AddMinutes(5), 
							System.Web.Caching.Cache.NoSlidingExpiration);

			// Cache the data for 5 minutes from the previous time it was accessed
			Cache.Insert("UniqueKey", myData, null, 
							System.Web.Caching.Cache.NoAbsoluteExpiration, 
							TimeSpan.FromMinutes(5));
		}
	}
}
