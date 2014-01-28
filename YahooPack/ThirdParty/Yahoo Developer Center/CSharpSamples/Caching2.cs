using System;
using System.IO;
using System.Net;
using System.Text;

namespace Yahoo.Samples.CSharp
{
	public partial class Caching2 : Yahoo.Samples.Common.ISample
	{

		#region Public properties

		/// <summary>
		/// Gets the sample description.
		/// </summary>
		public string Description
		{
			get { return "(CODE ONLY) Code sample that demonstrates adding items to the ASP.NET session state."; }
		}

		/// <summary>
		/// Gets the sample name.
		/// </summary>
		public string Name
		{
			get { return "C#: Session State Caching"; }
		}

		/// <summary>
		/// Gets the sample source filename.
		/// </summary>
		public string SourceFile
		{
			get { return "Caching2Sample.cs"; }
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
