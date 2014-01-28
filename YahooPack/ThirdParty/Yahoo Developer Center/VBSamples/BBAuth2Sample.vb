Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Web

Partial Public Class BBAuth2

	' This is only required for the sample to compile
	Private Request As System.Web.HttpRequest = Nothing
	Private Response As System.Web.HttpResponse = Nothing
	Private Session As System.Web.SessionState.HttpSessionState = Nothing

	Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)

		Dim auth As Yahoo.Authentication = Nothing
		Dim success As Boolean = False

		' Retrieve this user's authentication object we've stored in the session state
		If Not Session("Auth") Is Nothing Then
			auth = DirectCast(Session("Auth"), Yahoo.Authentication)
		End If

		If Not auth Is Nothing Then
			' We have a problem with the current session, abandon and retry
			Session.Abandon()
			Response.Redirect("ErrorPage.aspx")
		End If

		' Check if we are returning from login
		If (Not Request.QueryString("token") Is Nothing) _
		  AndAlso Request.QueryString("token").Length > 0 Then

			' Make sure the call is valid
			If auth.IsValidSignedUrl(Request.Url) = True Then

				success = True

				' Save the user token. It is valid for two weeks
				auth.Token = Request.QueryString("token")

			End If
		End If

		' Redirect if we succeeded
		If success = True Then
			Response.Redirect("Default.aspx")
		Else
			Response.Redirect("SignInError.aspx")
		End If

	End Sub

End Class
