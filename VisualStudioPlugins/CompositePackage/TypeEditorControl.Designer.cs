namespace SmallSharpToolscom.CompositePackage
{
    partial class TypeEditorControl
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
            this.nameLabel = new System.Windows.Forms.Label();
            this.classLabel = new System.Windows.Forms.Label();
            this.namespaceLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.classNameTextBox = new System.Windows.Forms.TextBox();
            this.namespaceTextBox = new System.Windows.Forms.TextBox();
            this.errorLabel = new System.Windows.Forms.Label();
            this.typeEditorPanel = new System.Windows.Forms.Panel();
            this.typeEditorPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLabel.Location = new System.Drawing.Point(5, 7);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.nameLabel.Size = new System.Drawing.Size(40, 13);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "Name";
            // 
            // classLabel
            // 
            this.classLabel.AutoSize = true;
            this.classLabel.Location = new System.Drawing.Point(158, 7);
            this.classLabel.Name = "classLabel";
            this.classLabel.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.classLabel.Size = new System.Drawing.Size(37, 13);
            this.classLabel.TabIndex = 1;
            this.classLabel.Text = "Class";
            // 
            // namespaceLabel
            // 
            this.namespaceLabel.AutoSize = true;
            this.namespaceLabel.Location = new System.Drawing.Point(314, 7);
            this.namespaceLabel.Name = "namespaceLabel";
            this.namespaceLabel.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.namespaceLabel.Size = new System.Drawing.Size(69, 13);
            this.namespaceLabel.TabIndex = 2;
            this.namespaceLabel.Text = "Namespace";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(5, 23);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(150, 20);
            this.nameTextBox.TabIndex = 3;
            // 
            // classNameTextBox
            // 
            this.classNameTextBox.Location = new System.Drawing.Point(161, 22);
            this.classNameTextBox.Name = "classNameTextBox";
            this.classNameTextBox.Size = new System.Drawing.Size(150, 20);
            this.classNameTextBox.TabIndex = 4;
            // 
            // namespaceTextBox
            // 
            this.namespaceTextBox.Location = new System.Drawing.Point(317, 22);
            this.namespaceTextBox.Name = "namespaceTextBox";
            this.namespaceTextBox.Size = new System.Drawing.Size(225, 20);
            this.namespaceTextBox.TabIndex = 5;
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.ForeColor = System.Drawing.Color.Red;
            this.errorLabel.Location = new System.Drawing.Point(548, 26);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(11, 13);
            this.errorLabel.TabIndex = 6;
            this.errorLabel.Text = "*";
            // 
            // typeEditorPanel
            // 
            this.typeEditorPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.typeEditorPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.typeEditorPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.typeEditorPanel.Controls.Add(this.classNameTextBox);
            this.typeEditorPanel.Controls.Add(this.errorLabel);
            this.typeEditorPanel.Controls.Add(this.nameLabel);
            this.typeEditorPanel.Controls.Add(this.namespaceTextBox);
            this.typeEditorPanel.Controls.Add(this.classLabel);
            this.typeEditorPanel.Controls.Add(this.namespaceLabel);
            this.typeEditorPanel.Controls.Add(this.nameTextBox);
            this.typeEditorPanel.Location = new System.Drawing.Point(3, 0);
            this.typeEditorPanel.Name = "typeEditorPanel";
            this.typeEditorPanel.Size = new System.Drawing.Size(597, 52);
            this.typeEditorPanel.TabIndex = 7;
            // 
            // TypeEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.typeEditorPanel);
            this.Name = "TypeEditorControl";
            this.Size = new System.Drawing.Size(600, 55);
            this.Load += new System.EventHandler(this.TypeEditorControl_Load);
            this.typeEditorPanel.ResumeLayout(false);
            this.typeEditorPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label classLabel;
        private System.Windows.Forms.Label namespaceLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.TextBox classNameTextBox;
        private System.Windows.Forms.TextBox namespaceTextBox;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.Panel typeEditorPanel;
    }
}
