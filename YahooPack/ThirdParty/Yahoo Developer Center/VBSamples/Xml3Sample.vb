Imports System.Data
Imports System.IO
Imports System.Net
Imports System.Xml
Imports System.Xml.XPath

Partial Public Class Xml3

	Public Sub RunSample() Implements Common.ISample.RunSample

		Dim doc As XPathDocument
		Dim ns As XmlNamespaceManager
		Dim navigator As XPathNavigator
		Dim nodes As XPathNodeIterator
		Dim node As XPathNavigator

		' Create a new XmlDocument
		doc = New XPathDocument("http://xml.weather.yahoo.com/forecastrss?p=94704")

		' Create navigator
		navigator = doc.CreateNavigator()

		' Set up namespace manager for XPath
		ns = New XmlNamespaceManager(navigator.NameTable)
		ns.AddNamespace("yweather", "http://xml.weather.yahoo.com/ns/rss/1.0")

		' Get forecast with XPath
		nodes = navigator.Select("/rss/channel/item/yweather:forecast", ns)

		While nodes.MoveNext()
			node = nodes.Current

			Console.WriteLine("{0}: {1}, {2}F - {3}F", _
			   node.GetAttribute("day", ns.DefaultNamespace), _
			   node.GetAttribute("text", ns.DefaultNamespace), _
			   node.GetAttribute("low", ns.DefaultNamespace), _
			   node.GetAttribute("high", ns.DefaultNamespace))
		End While

	End Sub

End Class
