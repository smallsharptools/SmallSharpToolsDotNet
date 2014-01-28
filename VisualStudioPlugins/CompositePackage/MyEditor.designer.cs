namespace SmallSharpToolscom.CompositePackage
{
    partial class MyEditor
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
            this.myEditorPanel = new System.Windows.Forms.Panel();
            this.typesGridView = new System.Windows.Forms.DataGridView();
            this.CompositeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClassName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Namespace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typesComboBox = new System.Windows.Forms.ComboBox();
            this.rootNamespaceTextBox = new System.Windows.Forms.TextBox();
            this.removeTypeButton = new System.Windows.Forms.Button();
            this.addTypeButton = new System.Windows.Forms.Button();
            this.myEditorPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.typesGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // myEditorPanel
            // 
            this.myEditorPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.myEditorPanel.Controls.Add(this.typesGridView);
            this.myEditorPanel.Controls.Add(this.typesComboBox);
            this.myEditorPanel.Controls.Add(this.rootNamespaceTextBox);
            this.myEditorPanel.Controls.Add(this.removeTypeButton);
            this.myEditorPanel.Controls.Add(this.addTypeButton);
            this.myEditorPanel.Location = new System.Drawing.Point(3, 3);
            this.myEditorPanel.Name = "myEditorPanel";
            this.myEditorPanel.Size = new System.Drawing.Size(594, 423);
            this.myEditorPanel.TabIndex = 0;
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
            this.typesGridView.Location = new System.Drawing.Point(4, 32);
            this.typesGridView.Name = "typesGridView";
            this.typesGridView.Size = new System.Drawing.Size(587, 216);
            this.typesGridView.TabIndex = 5;
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
            // typesComboBox
            // 
            this.typesComboBox.FormattingEnabled = true;
            this.typesComboBox.Location = new System.Drawing.Point(210, 3);
            this.typesComboBox.Name = "typesComboBox";
            this.typesComboBox.Size = new System.Drawing.Size(200, 21);
            this.typesComboBox.TabIndex = 4;
            // 
            // rootNamespaceTextBox
            // 
            this.rootNamespaceTextBox.Location = new System.Drawing.Point(4, 4);
            this.rootNamespaceTextBox.Name = "rootNamespaceTextBox";
            this.rootNamespaceTextBox.Size = new System.Drawing.Size(200, 20);
            this.rootNamespaceTextBox.TabIndex = 3;
            // 
            // removeTypeButton
            // 
            this.removeTypeButton.Location = new System.Drawing.Point(497, 3);
            this.removeTypeButton.Name = "removeTypeButton";
            this.removeTypeButton.Size = new System.Drawing.Size(75, 23);
            this.removeTypeButton.TabIndex = 1;
            this.removeTypeButton.Text = "Remove";
            this.removeTypeButton.UseVisualStyleBackColor = true;
            // 
            // addTypeButton
            // 
            this.addTypeButton.Location = new System.Drawing.Point(416, 4);
            this.addTypeButton.Name = "addTypeButton";
            this.addTypeButton.Size = new System.Drawing.Size(75, 23);
            this.addTypeButton.TabIndex = 0;
            this.addTypeButton.Text = "Add";
            this.addTypeButton.UseVisualStyleBackColor = true;
            // 
            // MyEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.myEditorPanel);
            this.Name = "MyEditor";
            this.Size = new System.Drawing.Size(600, 429);
            this.myEditorPanel.ResumeLayout(false);
            this.myEditorPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.typesGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel myEditorPanel;
        private System.Windows.Forms.Button removeTypeButton;
        private System.Windows.Forms.Button addTypeButton;
        private System.Windows.Forms.ComboBox typesComboBox;
        private System.Windows.Forms.TextBox rootNamespaceTextBox;
        private System.Windows.Forms.DataGridView typesGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn CompositeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClassName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Namespace;



    }
}
