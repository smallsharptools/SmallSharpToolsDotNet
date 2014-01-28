using System;
using System.Collections.Generic;
using System.Text;

namespace Yahoo.Samples.Common
{
	public interface ISample
	{
		#region Public properties

		string Description
		{
			get;
		}

		string Name
		{
			get;
		}


		string SourceFile
		{
			get;
		}

		#endregion

		#region Public methods

		void RunSample();

		#endregion
	}
}
