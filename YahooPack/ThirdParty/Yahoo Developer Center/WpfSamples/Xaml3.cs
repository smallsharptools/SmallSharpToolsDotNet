using System;
using System.IO;
using System.Net;
using System.Text;

namespace Yahoo.Samples.Wpf
{
	public class Xaml3 : Yahoo.Samples.Common.ISample
	{

		#region Public properties

		/// <summary>
		/// Gets the sample description.
		/// </summary>
		public string Description
		{
			get { return "An example of binding XML data to a ListBox control."; }
		}

		/// <summary>
		/// Gets the sample name.
		/// </summary>
		public string Name
		{
			get { return "WPF/XAML: ListBox data binding"; }
		}

		/// <summary>
		/// Gets the sample source filename.
		/// </summary>
		public string SourceFile
		{
			get { return "Xaml3Sample.xaml"; }
		}

		#endregion

		#region Public methods

		public void RunSample()
		{
			System.Diagnostics.Process.Start(SourceFile);
		}

		#endregion
	}
}
