Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Web

Partial Public Class Caching1

	' This is only required for the sample to compile
	Private Cache As System.Web.Caching.Cache = Nothing

	Public Sub CacheSample1()

		Dim myData = "... sample returned xml data ..."

		' Cache the data for 5 minutes
		Cache.Insert("UniqueKey", myData, Nothing, _
		 DateTime.Now.AddMinutes(5), _
		 System.Web.Caching.Cache.NoSlidingExpiration)

		' Cache the data for 5 minutes from the previous time it was accessed
		Cache.Insert("UniqueKey", myData, Nothing, _
		 System.Web.Caching.Cache.NoAbsoluteExpiration, _
		 TimeSpan.FromMinutes(5))

	End Sub

End Class
