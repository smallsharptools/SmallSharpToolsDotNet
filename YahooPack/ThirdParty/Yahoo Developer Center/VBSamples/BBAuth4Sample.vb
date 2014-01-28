Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Web

Partial Public Class BBAuth4

	' This is only required for the sample to compile
	Private Session As System.Web.SessionState.HttpSessionState = Nothing
	Private Div1 As System.Web.UI.HtmlControls.HtmlGenericControl = New System.Web.UI.HtmlControls.HtmlGenericControl("div")

	Private Sub CallAuthenticatedWebService()

		Dim auth As Yahoo.Authentication = Nothing

		' Retrieve this user's authentication object we've stored in the session state
		If Not Session("Auth") Is Nothing Then
			auth = DirectCast(Session("Auth"), Yahoo.Authentication)

			' Call web service and output result into a DIV tag
			Div1.InnerHtml = auth.GetAuthenticatedServiceString( _
			 New System.Uri("http://photos.yahooapis.com/V1.0/listServices"))
		End If

	End Sub

End Class
