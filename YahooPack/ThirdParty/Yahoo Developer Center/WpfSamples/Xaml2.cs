using System;
using System.IO;
using System.Net;
using System.Text;

namespace Yahoo.Samples.Wpf
{
	public class Xaml2 : Yahoo.Samples.Common.ISample
	{

		#region Public properties

		/// <summary>
		/// Gets the sample description.
		/// </summary>
		public string Description
		{
			get { return "Displays weather conditions for Berkeley, CA."; }
		}

		/// <summary>
		/// Gets the sample name.
		/// </summary>
		public string Name
		{
			get { return "WPF/XAML: Weather  Badge"; }
		}

		/// <summary>
		/// Gets the sample source filename.
		/// </summary>
		public string SourceFile
		{
			get { return "Xaml2Sample.xaml"; }
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
