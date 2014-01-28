Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Web

Partial Public Class Caching2

	' This is only required for the sample to compile
	Private Session As System.Web.SessionState.HttpSessionState = Nothing

	Public Sub CacheSample()

		Dim myData As String = "... sample returned xml data ..."

		' Save data in per-user session state
		Session.Add("UniqueKey", myData)

	End Sub

End Class
