Imports System.Data
Imports System.IO
Imports System.Net
Imports System.Xml
Imports System.Xml.XPath

Partial Public Class Xml1

	Public Sub RunSample() Implements Common.ISample.RunSample

		' Retrieve XML document
		Dim reader As XmlTextReader = New XmlTextReader("http://xml.weather.yahoo.com/forecastrss?p=94704")

		' Skip non-significant whitespace
		reader.WhitespaceHandling = WhitespaceHandling.Significant

		' Read nodes one at a time
		While reader.Read()

			' Print out info on node
			Console.WriteLine("{0}: {1}", reader.NodeType.ToString(), reader.Name)

		End While

	End Sub

End Class
