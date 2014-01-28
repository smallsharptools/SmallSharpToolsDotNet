using System;
using System.IO;
using System.Net;
using System.Text;

namespace Yahoo.Samples.CSharp
{
	public partial class BBAuth4 : Yahoo.Samples.Common.ISample
	{

		#region Public properties

		/// <summary>
		/// Gets the sample description.
		/// </summary>
		public string Description
		{
			get { return "(CODE ONLY) Code sample that demonstrates calling an authenticated web service."; }
		}

		/// <summary>
		/// Gets the sample name.
		/// </summary>
		public string Name
		{
			get { return "C#: BBAuth - Calling Authenticated Web Services"; }
		}

		/// <summary>
		/// Gets the sample source filename.
		/// </summary>
		public string SourceFile
		{
			get { return "BBAuth4Sample.cs"; }
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
