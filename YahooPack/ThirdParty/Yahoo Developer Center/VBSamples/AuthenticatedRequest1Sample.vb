Imports System
Imports System.IO
Imports System.Net
Imports System.Text

Partial Public Class AuthenticatedRequest1

	Public Sub RunSample() Implements Common.ISample.RunSample

		Dim request As HttpWebRequest
		Dim response As HttpWebResponse = Nothing
		Dim reader As StreamReader

		Try
			' Create the web request
			request = DirectCast(WebRequest.Create("https://api.del.icio.us/v1/posts/recent"), _
			   HttpWebRequest)

			' Add authentication to request
			request.Credentials = New NetworkCredential("username", "password")

			' Get response
			response = DirectCast(request.GetResponse(), HttpWebResponse)

			' Get the response stream into a reader
			reader = New StreamReader(response.GetResponseStream())

			' Console application output
			Console.WriteLine(reader.ReadToEnd())
		Finally
			If Not response Is Nothing Then response.Close()
		End Try

	End Sub

End Class
