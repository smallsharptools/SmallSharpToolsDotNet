using SmallSharpTools.JavaScriptBrowser.Controls;

namespace SmallSharpTools.JavaScriptBrowser.VSPackage
{
    partial class BrowserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                if(components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose( disposing );
        }


        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.javaScriptBrowserControl = new SmallSharpTools.JavaScriptBrowser.Controls.JavaScriptBrowserControl();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.javaScriptBrowserControl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(868, 324);
            this.panel1.TabIndex = 0;
            // 
            // javaScriptBrowserControl
            // 
            this.javaScriptBrowserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.javaScriptBrowserControl.Location = new System.Drawing.Point(0, 0);
            this.javaScriptBrowserControl.Name = "javaScriptBrowserControl";
            this.javaScriptBrowserControl.Size = new System.Drawing.Size(868, 324);
            this.javaScriptBrowserControl.TabIndex = 0;
            // 
            // MyControl
            // 
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.panel1);
            this.Name = "MyControl";
            this.Size = new System.Drawing.Size(868, 324);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Panel panel1;
        private JavaScriptBrowserControl javaScriptBrowserControl;


    }
}
