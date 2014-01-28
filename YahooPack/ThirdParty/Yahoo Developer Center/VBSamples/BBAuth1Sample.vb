Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Web

Partial Public Class BBAuth1

	' This is only required for the sample to compile
	Private Response As System.Web.HttpResponse = Nothing

	Public Sub SignInRedirect()

		Dim auth As Yahoo.Authentication

		' Create an instance of Yahoo.Authentication
		auth = New Yahoo.Authentication("myappid", "mysharedsecret")

		' Redirect the user to the use sign-in page
		Response.Redirect(auth.GetUserLogOnAddress().ToString())

	End Sub

End Class
