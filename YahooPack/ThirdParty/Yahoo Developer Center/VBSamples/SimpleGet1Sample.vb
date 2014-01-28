Imports System
Imports System.IO
Imports System.Net
Imports System.Text

Partial Public Class SimpleGet1

	Public Sub RunSample() Implements Common.ISample.RunSample

		Dim request As HttpWebRequest
		Dim response As HttpWebResponse = Nothing
		Dim reader As StreamReader

		Try
			' Create the web request
			request = DirectCast(WebRequest.Create("http://developer.yahoo.com/"), HttpWebRequest)

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

	Public Shared Sub PrintSource(ByVal address As Uri)

		Dim request As HttpWebRequest
		Dim response As HttpWebResponse = Nothing
		Dim reader As StreamReader
		Dim sbSource As StringBuilder

		If address Is Nothing Then Throw New ArgumentNullException("address")

		Try
			' Create and initialize the web request
			request = DirectCast(WebRequest.Create(address), HttpWebRequest)
			request.UserAgent = ".NET Sample"
			request.KeepAlive = False
			request.Timeout = 15 * 1000

			' Get response
			response = DirectCast(request.GetResponse(), HttpWebResponse)

			If request.HaveResponse = True AndAlso Not (response Is Nothing) Then

				' Get the response stream
				reader = New StreamReader(response.GetResponseStream())

				' Read it into a StringBuilder
				sbSource = New StringBuilder(reader.ReadToEnd())

				' Console application output
				Console.WriteLine(sbSource.ToString())
			End If
		Catch wex As WebException
			' This exception will be raised if the server didn't return 200 - OK
			' Try to retrieve more information about the network error
			If Not wex.Response Is Nothing Then
				Dim errorResponse As HttpWebResponse = Nothing
				Try
					errorResponse = DirectCast(wex.Response, HttpWebResponse)
					Console.WriteLine("The server returned '{0}' with the status code {1} ({2:d}).", _
					  errorResponse.StatusDescription, errorResponse.StatusCode, _
					  errorResponse.StatusCode)
				Finally
					If Not errorResponse Is Nothing Then errorResponse.Close()
				End Try
			End If
		Finally
			If Not response Is Nothing Then response.Close()
		End Try

	End Sub
End Class
