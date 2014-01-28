namespace SmallSharpTools.JavaScriptBrowser.StandAlone
{
    partial class BrowserForm
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
            SmallSharpTools.JavaScriptBrowser.Controls.Domain.Options options1 = new SmallSharpTools.JavaScriptBrowser.Controls.Domain.Options();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrowserForm));
            this.javaScriptBrowserControl = new SmallSharpTools.JavaScriptBrowser.Controls.JavaScriptBrowserControl();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // javaScriptBrowserControl
            // 
            this.javaScriptBrowserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.javaScriptBrowserControl.Location = new System.Drawing.Point(0, 0);
            this.javaScriptBrowserControl.Name = "javaScriptBrowserControl";
            //options1.BrowserOrientation = System.Windows.Forms.Orientation.Vertical;
            //this.javaScriptBrowserControl.Options = options1;
            this.javaScriptBrowserControl.Size = new System.Drawing.Size(715, 286);
            this.javaScriptBrowserControl.TabIndex = 0;
            // 
            // notifyIcon
            // 
            this.notifyIcon.Text = "Notify Icon";
            this.notifyIcon.Visible = true;
            // 
            // BrowserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 286);
            this.Controls.Add(this.javaScriptBrowserControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BrowserForm";
            this.Text = "JavaScript Browser";
            this.ResumeLayout(false);

        }

        #endregion

        private SmallSharpTools.JavaScriptBrowser.Controls.JavaScriptBrowserControl javaScriptBrowserControl;
        private System.Windows.Forms.NotifyIcon notifyIcon;
    }
}

