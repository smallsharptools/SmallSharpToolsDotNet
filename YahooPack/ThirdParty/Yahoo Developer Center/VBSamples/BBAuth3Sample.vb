Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Web

Partial Public Class BBAuth3

	Public Sub GetUserCredentials()

		Dim auth As Yahoo.Authentication

		' Create an instance of Yahoo.Authentication
		auth = New Yahoo.Authentication("myappid", "mysharedsecret")

		' You must set the token before calling UpdateCredentials
		auth.Token = "storedusertoken"

		' Attempt to get user credentials
		auth.UpdateCredentials()

	End Sub

End Class
