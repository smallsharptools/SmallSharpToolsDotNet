namespace Yahoo.Samples
{
    partial class SampleBrowser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			this.scMain = new System.Windows.Forms.SplitContainer();
			this.scSamples = new System.Windows.Forms.SplitContainer();
			this.lstSample = new System.Windows.Forms.ListBox();
			this.bsSamples = new System.Windows.Forms.BindingSource(this.components);
			this.lblSamples = new System.Windows.Forms.Label();
			this.tlpSampleInfoBorder = new System.Windows.Forms.TableLayoutPanel();
			this.tlpSampleInfo = new System.Windows.Forms.TableLayoutPanel();
			this.lblName = new System.Windows.Forms.Label();
			this.lblDescription = new System.Windows.Forms.Label();
			this.btnRun = new System.Windows.Forms.Button();
			this.scSourceOutput = new System.Windows.Forms.SplitContainer();
			this.rtbSource = new System.Windows.Forms.RichTextBox();
			this.lblSource = new System.Windows.Forms.Label();
			this.lblResult = new System.Windows.Forms.Label();
			this.rtbOutput = new System.Windows.Forms.RichTextBox();
			this.scMain.Panel1.SuspendLayout();
			this.scMain.Panel2.SuspendLayout();
			this.scMain.SuspendLayout();
			this.scSamples.Panel1.SuspendLayout();
			this.scSamples.Panel2.SuspendLayout();
			this.scSamples.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.bsSamples)).BeginInit();
			this.tlpSampleInfoBorder.SuspendLayout();
			this.tlpSampleInfo.SuspendLayout();
			this.scSourceOutput.Panel1.SuspendLayout();
			this.scSourceOutput.Panel2.SuspendLayout();
			this.scSourceOutput.SuspendLayout();
			this.SuspendLayout();
			// 
			// scMain
			// 
			this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scMain.ForeColor = System.Drawing.SystemColors.ControlText;
			this.scMain.Location = new System.Drawing.Point(0, 0);
			this.scMain.Margin = new System.Windows.Forms.Padding(0);
			this.scMain.Name = "scMain";
			// 
			// scMain.Panel1
			// 
			this.scMain.Panel1.Controls.Add(this.scSamples);
			this.scMain.Panel1.Controls.Add(this.btnRun);
			this.scMain.Panel1MinSize = 150;
			// 
			// scMain.Panel2
			// 
			this.scMain.Panel2.Controls.Add(this.scSourceOutput);
			this.scMain.Size = new System.Drawing.Size(618, 458);
			this.scMain.SplitterDistance = 238;
			this.scMain.SplitterWidth = 5;
			this.scMain.TabIndex = 0;
			this.scMain.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.scMain_SplitterMoved);
			// 
			// scSamples
			// 
			this.scSamples.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.scSamples.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.scSamples.Location = new System.Drawing.Point(8, 0);
			this.scSamples.Margin = new System.Windows.Forms.Padding(0);
			this.scSamples.Name = "scSamples";
			this.scSamples.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// scSamples.Panel1
			// 
			this.scSamples.Panel1.Controls.Add(this.lstSample);
			this.scSamples.Panel1.Controls.Add(this.lblSamples);
			// 
			// scSamples.Panel2
			// 
			this.scSamples.Panel2.Controls.Add(this.tlpSampleInfoBorder);
			this.scSamples.Size = new System.Drawing.Size(230, 423);
			this.scSamples.SplitterDistance = 328;
			this.scSamples.TabIndex = 0;
			this.scSamples.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.scSamples_SplitterMoved);
			// 
			// lstSample
			// 
			this.lstSample.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lstSample.DataSource = this.bsSamples;
			this.lstSample.DisplayMember = "Name";
			this.lstSample.FormattingEnabled = true;
			this.lstSample.IntegralHeight = false;
			this.lstSample.Location = new System.Drawing.Point(0, 22);
			this.lstSample.Name = "lstSample";
			this.lstSample.Size = new System.Drawing.Size(230, 306);
			this.lstSample.TabIndex = 1;
			// 
			// bsSamples
			// 
			this.bsSamples.DataSource = typeof(Yahoo.Samples.Common.ISample);
			this.bsSamples.CurrentChanged += new System.EventHandler(this.bsSamples_CurrentChanged);
			// 
			// lblSamples
			// 
			this.lblSamples.AutoSize = true;
			this.lblSamples.Location = new System.Drawing.Point(0, 6);
			this.lblSamples.Name = "lblSamples";
			this.lblSamples.Size = new System.Drawing.Size(96, 13);
			this.lblSamples.TabIndex = 0;
			this.lblSamples.Text = "Available Samples:";
			// 
			// tlpSampleInfoBorder
			// 
			this.tlpSampleInfoBorder.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.tlpSampleInfoBorder.ColumnCount = 1;
			this.tlpSampleInfoBorder.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tlpSampleInfoBorder.Controls.Add(this.tlpSampleInfo, 0, 0);
			this.tlpSampleInfoBorder.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlpSampleInfoBorder.Location = new System.Drawing.Point(0, 0);
			this.tlpSampleInfoBorder.Margin = new System.Windows.Forms.Padding(0);
			this.tlpSampleInfoBorder.Name = "tlpSampleInfoBorder";
			this.tlpSampleInfoBorder.RowCount = 1;
			this.tlpSampleInfoBorder.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tlpSampleInfoBorder.Size = new System.Drawing.Size(230, 91);
			this.tlpSampleInfoBorder.TabIndex = 0;
			// 
			// tlpSampleInfo
			// 
			this.tlpSampleInfo.ColumnCount = 1;
			this.tlpSampleInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpSampleInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tlpSampleInfo.Controls.Add(this.lblName, 0, 0);
			this.tlpSampleInfo.Controls.Add(this.lblDescription, 0, 1);
			this.tlpSampleInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlpSampleInfo.Location = new System.Drawing.Point(1, 1);
			this.tlpSampleInfo.Margin = new System.Windows.Forms.Padding(0);
			this.tlpSampleInfo.Name = "tlpSampleInfo";
			this.tlpSampleInfo.RowCount = 2;
			this.tlpSampleInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpSampleInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpSampleInfo.Size = new System.Drawing.Size(228, 89);
			this.tlpSampleInfo.TabIndex = 0;
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSamples, "Name", true));
			this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblName.Location = new System.Drawing.Point(3, 0);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(39, 13);
			this.lblName.TabIndex = 0;
			this.lblName.Text = "Name";
			// 
			// lblDescription
			// 
			this.lblDescription.AutoSize = true;
			this.lblDescription.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSamples, "Description", true));
			this.lblDescription.Location = new System.Drawing.Point(3, 13);
			this.lblDescription.Name = "lblDescription";
			this.lblDescription.Size = new System.Drawing.Size(60, 13);
			this.lblDescription.TabIndex = 1;
			this.lblDescription.Text = "Description";
			// 
			// btnRun
			// 
			this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRun.Location = new System.Drawing.Point(160, 429);
			this.btnRun.Name = "btnRun";
			this.btnRun.Size = new System.Drawing.Size(75, 23);
			this.btnRun.TabIndex = 1;
			this.btnRun.Text = "&Run";
			this.btnRun.UseVisualStyleBackColor = true;
			this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
			// 
			// scSourceOutput
			// 
			this.scSourceOutput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scSourceOutput.Location = new System.Drawing.Point(0, 0);
			this.scSourceOutput.Margin = new System.Windows.Forms.Padding(0);
			this.scSourceOutput.Name = "scSourceOutput";
			this.scSourceOutput.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// scSourceOutput.Panel1
			// 
			this.scSourceOutput.Panel1.Controls.Add(this.rtbSource);
			this.scSourceOutput.Panel1.Controls.Add(this.lblSource);
			// 
			// scSourceOutput.Panel2
			// 
			this.scSourceOutput.Panel2.Controls.Add(this.lblResult);
			this.scSourceOutput.Panel2.Controls.Add(this.rtbOutput);
			this.scSourceOutput.Size = new System.Drawing.Size(375, 458);
			this.scSourceOutput.SplitterDistance = 288;
			this.scSourceOutput.TabIndex = 3;
			this.scSourceOutput.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.scSourceOutput_SplitterMoved);
			// 
			// rtbSource
			// 
			this.rtbSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.rtbSource.Location = new System.Drawing.Point(0, 22);
			this.rtbSource.Name = "rtbSource";
			this.rtbSource.ReadOnly = true;
			this.rtbSource.Size = new System.Drawing.Size(367, 265);
			this.rtbSource.TabIndex = 2;
			this.rtbSource.Text = "";
			this.rtbSource.WordWrap = false;
			// 
			// lblSource
			// 
			this.lblSource.AutoSize = true;
			this.lblSource.Location = new System.Drawing.Point(1, 6);
			this.lblSource.Name = "lblSource";
			this.lblSource.Size = new System.Drawing.Size(71, 13);
			this.lblSource.TabIndex = 1;
			this.lblSource.Text = "Source code:";
			// 
			// lblResult
			// 
			this.lblResult.AutoSize = true;
			this.lblResult.Location = new System.Drawing.Point(1, 2);
			this.lblResult.Name = "lblResult";
			this.lblResult.Size = new System.Drawing.Size(78, 13);
			this.lblResult.TabIndex = 0;
			this.lblResult.Text = "Sample output:";
			// 
			// rtbOutput
			// 
			this.rtbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.rtbOutput.Location = new System.Drawing.Point(0, 18);
			this.rtbOutput.Name = "rtbOutput";
			this.rtbOutput.ReadOnly = true;
			this.rtbOutput.Size = new System.Drawing.Size(367, 142);
			this.rtbOutput.TabIndex = 1;
			this.rtbOutput.Text = "";
			this.rtbOutput.WordWrap = false;
			// 
			// SampleBrowser
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(618, 458);
			this.Controls.Add(this.scMain);
			this.Name = "SampleBrowser";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Yahoo! Developer Network - .NET Sample Browser";
			this.SizeChanged += new System.EventHandler(this.SampleBrowser_SizeChanged);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SampleBrowser_FormClosing);
			this.LocationChanged += new System.EventHandler(this.SampleBrowser_LocationChanged);
			this.Load += new System.EventHandler(this.SampleBrowser_Load);
			this.scMain.Panel1.ResumeLayout(false);
			this.scMain.Panel2.ResumeLayout(false);
			this.scMain.ResumeLayout(false);
			this.scSamples.Panel1.ResumeLayout(false);
			this.scSamples.Panel1.PerformLayout();
			this.scSamples.Panel2.ResumeLayout(false);
			this.scSamples.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.bsSamples)).EndInit();
			this.tlpSampleInfoBorder.ResumeLayout(false);
			this.tlpSampleInfo.ResumeLayout(false);
			this.tlpSampleInfo.PerformLayout();
			this.scSourceOutput.Panel1.ResumeLayout(false);
			this.scSourceOutput.Panel1.PerformLayout();
			this.scSourceOutput.Panel2.ResumeLayout(false);
			this.scSourceOutput.Panel2.PerformLayout();
			this.scSourceOutput.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.SplitContainer scMain;
		private System.Windows.Forms.Label lblSamples;
		private System.Windows.Forms.Button btnRun;
		private System.Windows.Forms.ListBox lstSample;
		private System.Windows.Forms.Label lblResult;
		private System.Windows.Forms.RichTextBox rtbOutput;
		private System.Windows.Forms.SplitContainer scSamples;
		private System.Windows.Forms.BindingSource bsSamples;
		private System.Windows.Forms.TableLayoutPanel tlpSampleInfo;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Label lblDescription;
		private System.Windows.Forms.TableLayoutPanel tlpSampleInfoBorder;
		private System.Windows.Forms.SplitContainer scSourceOutput;
		private System.Windows.Forms.Label lblSource;
		private System.Windows.Forms.RichTextBox rtbSource;
    }
}

