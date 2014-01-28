Imports System
Imports System.IO
Imports System.Net
Imports System.Text

Partial Public Class StringGet1

	Public Sub RunSample() Implements Common.ISample.RunSample

		Console.WriteLine(GetPageAsString(New Uri("http://developer.yahoo.com/")))

	End Sub

	Public Shared Function GetPageAsString(ByVal address As Uri) As String

		Dim request As HttpWebRequest
		Dim response As HttpWebResponse = Nothing
		Dim reader As StreamReader
		Dim result As String

		Try
			' Create the web request
			request = DirectCast(WebRequest.Create(address), HttpWebRequest)

			' Get response
			response = DirectCast(request.GetResponse(), HttpWebResponse)

			' Get the response stream into a reader
			reader = New StreamReader(response.GetResponseStream())

			' Read the whole contents and return as a string
			result = reader.ReadToEnd()
		Finally
			If Not response Is Nothing Then response.Close()
		End Try

		Return result

	End Function

End Class
