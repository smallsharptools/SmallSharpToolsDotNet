using System;
using System.IO;
using System.Net;
using System.Text;

namespace Yahoo.Samples.CSharp
{
	public partial class AuthenticatedRequest1 : Yahoo.Samples.Common.ISample
	{

		#region Public properties

		/// <summary>
		/// Gets the sample description.
		/// </summary>
		public string Description
		{
			get { return "Performs an HTTP authenticated request to del.icio.us."; }
		}

		/// <summary>
		/// Gets the sample name.
		/// </summary>
		public string Name
		{
			get { return "C#: Authenticated Request"; }
		}

		/// <summary>
		/// Gets the sample source filename.
		/// </summary>
		public string SourceFile
		{
			get { return "AuthenticatedRequest1Sample.cs"; }
		}

		#endregion

	}
}
