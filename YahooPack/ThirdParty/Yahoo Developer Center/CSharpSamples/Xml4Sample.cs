using System;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace Yahoo.Samples.CSharp
{
	public partial class Xml4
	{
		public void RunSample()
		{
			// Create the web request
			HttpWebRequest request 
				= WebRequest.Create("http://xml.weather.yahoo.com/forecastrss?p=94704") as HttpWebRequest;

			// Get response
			using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
			{
				// Load data into a dataset
				DataSet dsWeather = new DataSet();
				dsWeather.ReadXml(response.GetResponseStream());

				// Print dataset information
				PrintDataSet(dsWeather);
			}
		}

		public static void PrintDataSet(DataSet ds)
		{
			// Print out all tables and their columns
			foreach (DataTable table in ds.Tables)
			{
				Console.WriteLine("TABLE '{0}'", table.TableName);
				Console.WriteLine("Total # of rows: {0}", table.Rows.Count);
				Console.WriteLine("---------------------------------------------------------------");

				foreach (DataColumn column in table.Columns)
				{
					Console.WriteLine("- {0} ({1})", column.ColumnName, column.DataType.ToString());
				}  // foreach column

				Console.WriteLine(System.Environment.NewLine);
			}  // foreach table

			// Print out table relations
			foreach (DataRelation relation in ds.Relations)
			{
				Console.WriteLine("RELATION: {0}", relation.RelationName);
				Console.WriteLine("---------------------------------------------------------------");
				Console.WriteLine("Parent: {0}", relation.ParentTable.TableName);
				Console.WriteLine("Child: {0}", relation.ChildTable.TableName);
				Console.WriteLine(System.Environment.NewLine);
			}  // foreach relation
		}

	}
}
