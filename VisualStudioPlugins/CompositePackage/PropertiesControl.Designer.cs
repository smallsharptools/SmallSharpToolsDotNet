namespace SmallSharpToolscom.CompositePackage
{
    partial class PropertiesControl
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
            this.propertiesControlPanel = new System.Windows.Forms.Panel();
            this.properitesGridView = new System.Windows.Forms.DataGridView();
            this.PropertyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Alias = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsReadOnly = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsHidden = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.propertiesControlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.properitesGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // propertiesControlPanel
            // 
            this.propertiesControlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propertiesControlPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.propertiesControlPanel.Controls.Add(this.properitesGridView);
            this.propertiesControlPanel.Location = new System.Drawing.Point(3, 3);
            this.propertiesControlPanel.Name = "propertiesControlPanel";
            this.propertiesControlPanel.Size = new System.Drawing.Size(594, 144);
            this.propertiesControlPanel.TabIndex = 0;
            // 
            // properitesGridView
            // 
            this.properitesGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.properitesGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.properitesGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.properitesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.properitesGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PropertyName,
            this.Alias,
            this.IsReadOnly,
            this.IsHidden});
            this.properitesGridView.Location = new System.Drawing.Point(-2, -5);
            this.properitesGridView.Name = "properitesGridView";
            this.properitesGridView.Size = new System.Drawing.Size(600, 142);
            this.properitesGridView.TabIndex = 0;
            // 
            // PropertyName
            // 
            this.PropertyName.HeaderText = "Name";
            this.PropertyName.Name = "PropertyName";
            // 
            // Alias
            // 
            this.Alias.HeaderText = "Alias";
            this.Alias.Name = "Alias";
            // 
            // IsReadOnly
            // 
            this.IsReadOnly.HeaderText = "Read Only";
            this.IsReadOnly.Name = "IsReadOnly";
            // 
            // IsHidden
            // 
            this.IsHidden.HeaderText = "Hidden";
            this.IsHidden.Name = "IsHidden";
            // 
            // PropertiesUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.propertiesControlPanel);
            this.Name = "PropertiesUserControl";
            this.Size = new System.Drawing.Size(600, 150);
            this.propertiesControlPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.properitesGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel propertiesControlPanel;
        private System.Windows.Forms.DataGridView properitesGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn PropertyName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Alias;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsReadOnly;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsHidden;
    }
}
