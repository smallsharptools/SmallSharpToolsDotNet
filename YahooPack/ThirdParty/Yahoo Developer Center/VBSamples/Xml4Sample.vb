Imports System.Data
Imports System.IO
Imports System.Net
Imports System.Xml
Imports System.Xml.XPath

Partial Public Class Xml4

	Public Sub RunSample() Implements Common.ISample.RunSample

		Dim request As HttpWebRequest
		Dim response As HttpWebResponse = Nothing
		Dim dsWeather As DataSet

		Try
			' Create the web request
			request = DirectCast(WebRequest.Create( _
			 "http://xml.weather.yahoo.com/forecastrss?p=94704"), HttpWebRequest)

			' Get response
			response = DirectCast(request.GetResponse(), HttpWebResponse)

			' Load data into a dataset
			dsWeather = New DataSet()
			dsWeather.ReadXml(response.GetResponseStream())

			' Print dataset information
			PrintDataSet(dsWeather)
		Finally
			If Not response Is Nothing Then response.Close()
		End Try

	End Sub

	Public Shared Sub PrintDataSet(ByVal ds As DataSet)

		' Print out all tables and their columns
		For Each table As DataTable In ds.Tables
			Console.WriteLine("TABLE '{0}'", table.TableName)
			Console.WriteLine("Total # of rows: {0}", table.Rows.Count)
			Console.WriteLine("---------------------------------------------------------------")

			For Each column As DataColumn In table.Columns
				Console.WriteLine("- {0} ({1})", column.ColumnName, column.DataType.ToString())
			Next  ' For Each column

			Console.WriteLine(System.Environment.NewLine)
		Next  ' For Each table

		' Print out table relations
		For Each relation As DataRelation In ds.Relations
			Console.WriteLine("RELATION: {0}", relation.RelationName)
			Console.WriteLine("---------------------------------------------------------------")
			Console.WriteLine("Parent: {0}", relation.ParentTable.TableName)
			Console.WriteLine("Child: {0}", relation.ChildTable.TableName)
			Console.WriteLine(System.Environment.NewLine)
		Next  ' For Each relation

	End Sub

End Class
