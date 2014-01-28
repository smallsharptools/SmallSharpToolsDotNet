using System;
using System.IO;
using System.Net;
using System.Text;

namespace Yahoo.Samples.CSharp
{
	public partial class StringGet1 : Yahoo.Samples.Common.ISample
	{

		#region Public properties

		/// <summary>
		/// Gets the sample description.
		/// </summary>
		public string Description
		{
			get { return "Retrieves the contents of a page or web service and returns the result as a string."; }
		}

		/// <summary>
		/// Gets the sample name.
		/// </summary>
		public string Name
		{
			get { return "C#: Return data as string"; }
		}

		/// <summary>
		/// Gets the sample source filename.
		/// </summary>
		public string SourceFile
		{
			get { return "StringGet1Sample.cs"; }
		}

		#endregion

	}
}
