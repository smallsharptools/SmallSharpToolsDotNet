Imports System.Data
Imports System.IO
Imports System.Net
Imports System.Xml
Imports System.Xml.XPath

Partial Public Class Xml2

	Public Sub RunSample() Implements Common.ISample.RunSample

		Dim doc As XmlDocument
		Dim ns As XmlNamespaceManager
		Dim nodes As XmlNodeList

		' Create a new XmlDocument
		doc = New XmlDocument()

		' Load data
		doc.Load("http://xml.weather.yahoo.com/forecastrss?p=94704")

		' Set up namespace manager for XPath
		ns = New XmlNamespaceManager(doc.NameTable)
		ns.AddNamespace("yweather", "http://xml.weather.yahoo.com/ns/rss/1.0")

		' Get forecast with XPath
		nodes = doc.SelectNodes("/rss/channel/item/yweather:forecast", ns)

		' You can also get elements based on their tag name and namespace,
		' though this isn't recommended
		'nodes = doc.GetElementsByTagName("forecast", _
		'							"http://xml.weather.yahoo.com/ns/rss/1.0")

		For Each node As XmlNode In nodes
			Console.WriteLine("{0}: {1}, {2}F - {3}F", _
			  node.Attributes("day").InnerText, _
			  node.Attributes("text").InnerText, _
			  node.Attributes("low").InnerText, _
			  node.Attributes("high").InnerText)
		Next

	End Sub

End Class
