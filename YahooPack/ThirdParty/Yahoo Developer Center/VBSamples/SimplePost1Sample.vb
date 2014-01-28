Imports System
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Web

Partial Public Class SimplePost1

	Public Sub RunSample() Implements Common.ISample.RunSample

		Dim request As HttpWebRequest
		Dim response As HttpWebResponse = Nothing
		Dim reader As StreamReader
		Dim address As Uri
		Dim appId As String
		Dim context As String
		Dim query As String
		Dim data As StringBuilder
		Dim byteData() As Byte
		Dim postStream As Stream = Nothing

		address = New Uri("http://api.search.yahoo.com/ContentAnalysisService/V1/termExtraction")

		' Create the web request
		request = DirectCast(WebRequest.Create(address), HttpWebRequest)

		' Set type to POST
		request.Method = "POST"
		request.ContentType = "application/x-www-form-urlencoded"

		' Create the data we want to send
		appId = "YahooDemo"
		context = "Italian sculptors and painters of the renaissance" _
		   & "favored the Virgin Mary for inspiration"
		query = "madonna"

		data = New StringBuilder()
		data.Append("appid=" + HttpUtility.UrlEncode(appId))
		data.Append("&context=" + HttpUtility.UrlEncode(context))
		data.Append("&query=" + HttpUtility.UrlEncode(query))

		' Create a byte array of the data we want to send
		byteData = UTF8Encoding.UTF8.GetBytes(data.ToString())

		' Set the content length in the request headers
		request.ContentLength = byteData.Length

		' Write data
		Try
			postStream = request.GetRequestStream()
			postStream.Write(byteData, 0, byteData.Length)
		Finally
			If Not postStream Is Nothing Then postStream.Close()
		End Try

		Try
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
