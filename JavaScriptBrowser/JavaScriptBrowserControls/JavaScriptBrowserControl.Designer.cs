namespace SmallSharpTools.JavaScriptBrowser.Controls
{
    partial class JavaScriptBrowserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JavaScriptBrowserControl));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.treeView = new System.Windows.Forms.TreeView();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripOptionsButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripOrientationButton = new System.Windows.Forms.ToolStripButton();
            this.pnlMain.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMain.Controls.Add(this.splitContainer);
            this.pnlMain.Location = new System.Drawing.Point(3, 28);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(594, 269);
            this.pnlMain.TabIndex = 0;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.treeView);
            this.splitContainer.Panel1.Margin = new System.Windows.Forms.Padding(3);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.webBrowser);
            this.splitContainer.Panel2.Margin = new System.Windows.Forms.Padding(3);
            this.splitContainer.Size = new System.Drawing.Size(594, 269);
            this.splitContainer.SplitterDistance = 163;
            this.splitContainer.TabIndex = 0;
            // 
            // treeView
            // 
            this.treeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(163, 269);
            this.treeView.TabIndex = 0;
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(0, 0);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.ScriptErrorsSuppressed = true;
            this.webBrowser.Size = new System.Drawing.Size(427, 269);
            this.webBrowser.TabIndex = 0;
            this.webBrowser.WebBrowserShortcutsEnabled = false;
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox,
            this.toolStripOptionsButton,
            this.toolStripOrientationButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(600, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip1";
            // 
            // toolStripComboBox
            // 
            this.toolStripComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.toolStripComboBox.Items.AddRange(new object[] {
            "jQuery",
            "Prototype",
            "script.aculo.us",
            "MochiKit",
            "YUI",
            "moo.fx",
            "Ext JS",
            "Dojo"});
            this.toolStripComboBox.Name = "toolStripComboBox";
            this.toolStripComboBox.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripComboBox.Size = new System.Drawing.Size(150, 25);
            this.toolStripComboBox.ToolTipText = "Library";
            this.toolStripComboBox.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox1_SelectedIndexChanged);
            // 
            // toolStripOptionsButton
            // 
            this.toolStripOptionsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripOptionsButton.Image = ((System.Drawing.Image)(resources.GetObject("toolStripOptionsButton.Image")));
            this.toolStripOptionsButton.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.toolStripOptionsButton.Name = "toolStripOptionsButton";
            this.toolStripOptionsButton.Size = new System.Drawing.Size(23, 22);
            this.toolStripOptionsButton.Text = "Options";
            this.toolStripOptionsButton.Click += new System.EventHandler(this.toolStripOptionsButton_Click);
            // 
            // toolStripOrientationButton
            // 
            this.toolStripOrientationButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripOrientationButton.Image = ((System.Drawing.Image)(resources.GetObject("toolStripOrientationButton.Image")));
            this.toolStripOrientationButton.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.toolStripOrientationButton.Name = "toolStripOrientationButton";
            this.toolStripOrientationButton.Size = new System.Drawing.Size(23, 22);
            this.toolStripOrientationButton.Text = "Change Orientation";
            this.toolStripOrientationButton.ToolTipText = "Change Orientation";
            this.toolStripOrientationButton.Click += new System.EventHandler(this.toolStripOrientationButton_Click);
            // 
            // JavaScriptBrowserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.pnlMain);
            this.Name = "JavaScriptBrowserControl";
            this.Size = new System.Drawing.Size(600, 300);
            this.pnlMain.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox;
        private System.Windows.Forms.ToolStripButton toolStripOptionsButton;
        private System.Windows.Forms.ToolStripButton toolStripOrientationButton;

    }
}
