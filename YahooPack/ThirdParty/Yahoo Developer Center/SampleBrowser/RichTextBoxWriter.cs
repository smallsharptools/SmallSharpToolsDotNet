using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Yahoo.Samples
{
	/// <summary>
	/// TextWriter implementation that outputs all text to a RichTextBox.
	/// </summary>
	public class RichTextBoxWriter  : TextWriter
	{

		#region Private fields

		private System.Text.Encoding _encoding = new System.Text.UTF8Encoding();
		private System.Windows.Forms.RichTextBox _rtbOutput;

		#endregion

		#region Constructors

		/// <summary>
		/// 
		/// </summary>
		/// <param name="outputControl"></param>
		public RichTextBoxWriter(System.Windows.Forms.RichTextBox outputControl) 
		{
			if (outputControl == null) { throw new ArgumentNullException("outputControl"); }

			_rtbOutput = outputControl;
		}

		#endregion

		#region Public properties

		/// <summary>
		/// Gets the Encoding in which the output is written.
		/// </summary>
		public override System.Text.Encoding Encoding
		{
			get { return _encoding; }
		}

		/// <summary>
		/// Gets or sets the RichTextBox control to output to.
		/// </summary>
		public System.Windows.Forms.RichTextBox OutputControl
		{
			get { return _rtbOutput; }
			set
			{
				if (value == null) { throw new ArgumentNullException("value"); }
				_rtbOutput = value;
			}
		}

		#endregion

		#region Overridden methods

		/// <summary>
		/// Writes the given data to the specified RichTextBox.
		/// </summary>
		/// <param name="value"></param>
		public override void Write(char value)
		{
			base.Write(value);

			if (_rtbOutput != null)
			{
				_rtbOutput.AppendText(value.ToString());
			}
		}

		#endregion
	}
}
