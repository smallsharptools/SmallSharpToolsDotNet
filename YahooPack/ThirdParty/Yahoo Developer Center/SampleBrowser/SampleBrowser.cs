using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Yahoo.Samples
{
    public partial class SampleBrowser : Form
	{

		#region Private fields

		private RichTextBoxWriter _tbwOutput;

		#endregion

		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		public SampleBrowser()
        {
            InitializeComponent();
		}

		#endregion

		#region Private methods, control code

		/// <summary>
		/// Form initialization code.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SampleBrowser_Load(object sender, EventArgs e)
        {

			// Hook up Console redirect to rtbOutput
			_tbwOutput = new RichTextBoxWriter(rtbOutput);
			Console.SetOut(_tbwOutput);

			// Add samples to the list
			bsSamples.Add(new Yahoo.Samples.CSharp.AuthenticatedRequest1());
			bsSamples.Add(new Yahoo.Samples.CSharp.Caching1());
			bsSamples.Add(new Yahoo.Samples.CSharp.Caching2());
			bsSamples.Add(new Yahoo.Samples.CSharp.SimpleGet1());
			bsSamples.Add(new Yahoo.Samples.CSharp.SimpleGet2());
			bsSamples.Add(new Yahoo.Samples.CSharp.SimplePost1());
			bsSamples.Add(new Yahoo.Samples.CSharp.StringGet1());
			bsSamples.Add(new Yahoo.Samples.CSharp.Xml1());
			bsSamples.Add(new Yahoo.Samples.CSharp.Xml2());
			bsSamples.Add(new Yahoo.Samples.CSharp.Xml3());
			bsSamples.Add(new Yahoo.Samples.CSharp.Xml4());
			bsSamples.Add(new Yahoo.Samples.CSharp.BBAuth1());
			bsSamples.Add(new Yahoo.Samples.CSharp.BBAuth2());
			bsSamples.Add(new Yahoo.Samples.CSharp.BBAuth3());
			bsSamples.Add(new Yahoo.Samples.CSharp.BBAuth4());

			bsSamples.Add(new Yahoo.Samples.VB.AuthenticatedRequest1());
			bsSamples.Add(new Yahoo.Samples.VB.Caching1());
			bsSamples.Add(new Yahoo.Samples.VB.Caching2());
			bsSamples.Add(new Yahoo.Samples.VB.SimpleGet1());
			bsSamples.Add(new Yahoo.Samples.VB.SimpleGet2());
			bsSamples.Add(new Yahoo.Samples.VB.SimplePost1());
			bsSamples.Add(new Yahoo.Samples.VB.StringGet1());
			bsSamples.Add(new Yahoo.Samples.VB.Xml1());
			bsSamples.Add(new Yahoo.Samples.VB.Xml2());
			bsSamples.Add(new Yahoo.Samples.VB.Xml3());
			bsSamples.Add(new Yahoo.Samples.VB.Xml4());
			bsSamples.Add(new Yahoo.Samples.VB.BBAuth1());
			bsSamples.Add(new Yahoo.Samples.VB.BBAuth2());
			bsSamples.Add(new Yahoo.Samples.VB.BBAuth3());
			bsSamples.Add(new Yahoo.Samples.VB.BBAuth4());

			bsSamples.Add(new Yahoo.Samples.Wpf.Xaml1());
			bsSamples.Add(new Yahoo.Samples.Wpf.Xaml2());
			bsSamples.Add(new Yahoo.Samples.Wpf.Xaml3());

			// Load settings, ignore all errors
			try { this.Location = Properties.Settings.Default.FormLocation; }
			catch { }
			try { this.ClientSize = Properties.Settings.Default.FormClientSize; }
			catch { }
			try { scMain.SplitterDistance = Properties.Settings.Default.scMainSplitterDistance; }
			catch { }
			try { scSamples.SplitterDistance = Properties.Settings.Default.scSamplesSplitterDistance; }
			catch { }
			try { scSourceOutput.SplitterDistance = Properties.Settings.Default.scSourceOutputSplitterDistance; }
			catch { }
		}

		/// <summary>
		/// Saves the form location when it is moved.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SampleBrowser_LocationChanged(object sender, EventArgs e)
		{
			Properties.Settings.Default.FormLocation = this.Location;
		}

		/// <summary>
		/// Saves the form size when it is resized.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SampleBrowser_SizeChanged(object sender, EventArgs e)
		{
			Properties.Settings.Default.FormClientSize = this.ClientSize;
		}

		/// <summary>
		/// Saves all settings when form is closed.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SampleBrowser_FormClosing(object sender, FormClosingEventArgs e)
		{
			Properties.Settings.Default.Save();
		}

		/// <summary>
		/// Saves splitter location.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void scSamples_SplitterMoved(object sender, SplitterEventArgs e)
		{
			Properties.Settings.Default.scSamplesSplitterDistance = scSamples.SplitterDistance;
		}

		/// <summary>
		/// Saves splitter location.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void scSourceOutput_SplitterMoved(object sender, SplitterEventArgs e)
		{
			Properties.Settings.Default.scSourceOutputSplitterDistance = scSamples.SplitterDistance;
		}

		/// <summary>
		/// Saves splitter location.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void scMain_SplitterMoved(object sender, SplitterEventArgs e)
		{
			Properties.Settings.Default.scMainSplitterDistance = scSamples.SplitterDistance;
		}

		/// <summary>
		/// Saves splitter location.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bsSamples_CurrentChanged(object sender, EventArgs e)
		{
			try
			{
				// Load source code for currently selected item
				using(System.IO.StreamReader reader = new System.IO.StreamReader(System.IO.Path.Combine("Samples", ((Yahoo.Samples.Common.ISample)bsSamples.Current).SourceFile), true))
				{
					rtbSource.Text = reader.ReadToEnd();
				}
			}
			catch(Exception ex)
			{
				rtbSource.Text = ex.ToString();
			}
		}

		/// <summary>
		/// Runs the code for the selected sample.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRun_Click(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			btnRun.Enabled = false;
			rtbOutput.Clear();

			try
			{
				((Yahoo.Samples.Common.ISample)bsSamples.Current).RunSample();
			}
			catch (Exception ex)
			{
				rtbOutput.AppendText(ex.ToString());
			}

			btnRun.Enabled = true;
			this.Cursor = Cursors.Default;
		}

		#endregion

	}
}