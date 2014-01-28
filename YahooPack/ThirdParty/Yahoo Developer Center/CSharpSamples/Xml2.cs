using System;
using System.IO;
using System.Net;
using System.Text;

namespace Yahoo.Samples.CSharp
{
	public partial class Xml2 : Yahoo.Samples.Common.ISample
	{

		#region Public properties

		/// <summary>
		/// Gets the sample description.
		/// </summary>
		public string Description
		{
			get { return "Shows a simple example on using XmlDocument."; }
		}

		/// <summary>
		/// Gets the sample name.
		/// </summary>
		public string Name
		{
			get { return "C#: XmlDocument"; }
		}

		/// <summary>
		/// Gets the sample source filename.
		/// </summary>
		public string SourceFile
		{
			get { return "Xml2Sample.cs"; }
		}

		#endregion

	}
}
