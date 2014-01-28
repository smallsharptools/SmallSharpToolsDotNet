using System;
using System.IO;
using System.Net;
using System.Text;

namespace Yahoo.Samples.Wpf
{
	public class Xaml1 : Yahoo.Samples.Common.ISample
	{

		#region Public properties

		/// <summary>
		/// Gets the sample description.
		/// </summary>
		public string Description
		{
			get { return "(CODE ONLY) XAML code for XmlDataProvider."; }
		}

		/// <summary>
		/// Gets the sample name.
		/// </summary>
		public string Name
		{
			get { return "WPF/XAML: XmlDataProvider"; }
		}

		/// <summary>
		/// Gets the sample source filename.
		/// </summary>
		public string SourceFile
		{
			get { return "Xaml1Sample.xaml"; }
		}

		#endregion

		#region Public methods

		public void RunSample()
		{
			Console.WriteLine("This sample does not produce any results. Please refer to the sample source.");
		}

		#endregion
	}
}
