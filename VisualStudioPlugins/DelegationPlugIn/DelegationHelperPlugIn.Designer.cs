namespace DelegationPlugIn
{
    partial class DelegationHelperPlugIn
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public DelegationHelperPlugIn()
        {
            /// <summary>
            /// Required for Windows.Forms Class Composition Designer support
            /// </summary>
            InitializeComponent();
        }

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DelegationHelperPlugIn));
            this.actExpandProperties = new DevExpress.CodeRush.Core.Action(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.actExpandProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // actExpandProperties
            // 
            this.actExpandProperties.ActionName = "Expand Properties";
            this.actExpandProperties.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
            this.actExpandProperties.Description = "Expands properties for the selected private variable";
            this.actExpandProperties.Image = ((System.Drawing.Bitmap)(resources.GetObject("actExpandProperties.Image")));
            this.actExpandProperties.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
            this.actExpandProperties.ParentMenu = "Tools";
            this.actExpandProperties.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.actExpandProperties_Execute);
            ((System.ComponentModel.ISupportInitialize)(this.actExpandProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.CodeRush.Core.Action actExpandProperties;
    }
}