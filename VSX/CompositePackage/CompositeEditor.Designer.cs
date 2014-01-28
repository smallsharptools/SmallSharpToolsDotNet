namespace SmallSharpTools.VSX.Composite.VSPackage
{
    partial class CompositeEditor
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.removeTypeButton = new System.Windows.Forms.Button();
            this.rootNamespaceLabel = new System.Windows.Forms.Label();
            this.propertiesUserControl1 = new SmallSharpTools.VSX.Composite.VSPackage.PropertiesControl();
            this.typesGridView = new System.Windows.Forms.DataGridView();
            this.CompositeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClassName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Namespace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rootNamespaceTextBox = new System.Windows.Forms.TextBox();
            this.addTypeButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.typesGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.removeTypeButton);
            this.panel1.Controls.Add(this.rootNamespaceLabel);
            this.panel1.Controls.Add(this.propertiesUserControl1);
            this.panel1.Controls.Add(this.typesGridView);
            this.panel1.Controls.Add(this.rootNamespaceTextBox);
            this.panel1.Controls.Add(this.addTypeButton);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(597, 444);
            this.panel1.TabIndex = 0;
            // 
            // removeTypeButton
            // 
            this.removeTypeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.removeTypeButton.Location = new System.Drawing.Point(500, 5);
            this.removeTypeButton.Name = "removeTypeButton";
            this.removeTypeButton.Size = new System.Drawing.Size(94, 23);
            this.removeTypeButton.TabIndex = 15;
            this.removeTypeButton.Text = "Remove";
            this.removeTypeButton.UseVisualStyleBackColor = true;
            // 
            // rootNamespaceLabel
            // 
            this.rootNamespaceLabel.AutoSize = true;
            this.rootNamespaceLabel.Location = new System.Drawing.Point(3, 9);
            this.rootNamespaceLabel.Name = "rootNamespaceLabel";
            this.rootNamespaceLabel.Size = new System.Drawing.Size(90, 13);
            this.rootNamespaceLabel.TabIndex = 13;
            this.rootNamespaceLabel.Text = "Root Namespace";
            // 
            // propertiesUserControl1
            // 
            this.propertiesUserControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propertiesUserControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.propertiesUserControl1.Location = new System.Drawing.Point(0, 150);
            this.propertiesUserControl1.Name = "propertiesUserControl1";
            this.propertiesUserControl1.Size = new System.Drawing.Size(597, 291);
            this.propertiesUserControl1.TabIndex = 12;
            // 
            // typesGridView
            // 
            this.typesGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.typesGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.typesGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.typesGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.typesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.typesGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CompositeName,
            this.ClassName,
            this.Namespace});
            this.typesGridView.Location = new System.Drawing.Point(3, 32);
            this.typesGridView.Name = "typesGridView";
            this.typesGridView.Size = new System.Drawing.Size(594, 112);
            this.typesGridView.TabIndex = 10;
            // 
            // CompositeName
            // 
            this.CompositeName.HeaderText = "Name";
            this.CompositeName.Name = "CompositeName";
            // 
            // ClassName
            // 
            this.ClassName.HeaderText = "Class";
            this.ClassName.Name = "ClassName";
            this.ClassName.ReadOnly = true;
            // 
            // Namespace
            // 
            this.Namespace.HeaderText = "Namespace";
            this.Namespace.Name = "Namespace";
            this.Namespace.ReadOnly = true;
            // 
            // rootNamespaceTextBox
            // 
            this.rootNamespaceTextBox.Location = new System.Drawing.Point(99, 6);
            this.rootNamespaceTextBox.Name = "rootNamespaceTextBox";
            this.rootNamespaceTextBox.Size = new System.Drawing.Size(300, 20);
            this.rootNamespaceTextBox.TabIndex = 8;
            // 
            // addTypeButton
            // 
            this.addTypeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addTypeButton.Location = new System.Drawing.Point(405, 5);
            this.addTypeButton.Name = "addTypeButton";
            this.addTypeButton.Size = new System.Drawing.Size(89, 23);
            this.addTypeButton.TabIndex = 6;
            this.addTypeButton.Text = "Add";
            this.addTypeButton.UseVisualStyleBackColor = true;
            // 
            // CompositeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "CompositeEditor";
            this.Size = new System.Drawing.Size(600, 450);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.typesGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView typesGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn CompositeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClassName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Namespace;
        private PropertiesControl propertiesUserControl1;
        private System.Windows.Forms.Label rootNamespaceLabel;
        private System.Windows.Forms.TextBox rootNamespaceTextBox;
        private System.Windows.Forms.Button addTypeButton;
        private System.Windows.Forms.Button removeTypeButton;
    }
}
