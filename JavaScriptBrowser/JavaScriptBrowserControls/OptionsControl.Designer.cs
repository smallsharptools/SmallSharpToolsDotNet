namespace SmallSharpTools.JavaScriptBrowser.Controls
{
    partial class OptionsControl
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
            this.librariesPanel = new System.Windows.Forms.Panel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.generalTabPage = new System.Windows.Forms.TabPage();
            this.generalPanel = new System.Windows.Forms.Panel();
            this.innerPanel = new System.Windows.Forms.Panel();
            this.orientationLabel = new System.Windows.Forms.Label();
            this.orientationComboBox = new System.Windows.Forms.ComboBox();
            this.librariesTabPage = new System.Windows.Forms.TabPage();
            this.librariesSplitContainer = new System.Windows.Forms.SplitContainer();
            this.librariesDataGridView = new System.Windows.Forms.DataGridView();
            this.LibraryName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Version = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.File = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.librariesButtonsPanel = new System.Windows.Forms.Panel();
            this.linkLabelGetDefinitionFiles = new System.Windows.Forms.LinkLabel();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.librariesPanel.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.generalTabPage.SuspendLayout();
            this.generalPanel.SuspendLayout();
            this.innerPanel.SuspendLayout();
            this.librariesTabPage.SuspendLayout();
            this.librariesSplitContainer.Panel1.SuspendLayout();
            this.librariesSplitContainer.Panel2.SuspendLayout();
            this.librariesSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.librariesDataGridView)).BeginInit();
            this.librariesButtonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // librariesPanel
            // 
            this.librariesPanel.Controls.Add(this.tabControl);
            this.librariesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.librariesPanel.Location = new System.Drawing.Point(0, 0);
            this.librariesPanel.Name = "librariesPanel";
            this.librariesPanel.Size = new System.Drawing.Size(600, 425);
            this.librariesPanel.TabIndex = 0;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.generalTabPage);
            this.tabControl.Controls.Add(this.librariesTabPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(600, 425);
            this.tabControl.TabIndex = 0;
            // 
            // generalTabPage
            // 
            this.generalTabPage.Controls.Add(this.generalPanel);
            this.generalTabPage.Location = new System.Drawing.Point(4, 22);
            this.generalTabPage.Name = "generalTabPage";
            this.generalTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.generalTabPage.Size = new System.Drawing.Size(592, 399);
            this.generalTabPage.TabIndex = 0;
            this.generalTabPage.Text = "General";
            this.generalTabPage.UseVisualStyleBackColor = true;
            // 
            // generalPanel
            // 
            this.generalPanel.Controls.Add(this.innerPanel);
            this.generalPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.generalPanel.Location = new System.Drawing.Point(3, 3);
            this.generalPanel.Name = "generalPanel";
            this.generalPanel.Size = new System.Drawing.Size(586, 393);
            this.generalPanel.TabIndex = 0;
            // 
            // innerPanel
            // 
            this.innerPanel.Controls.Add(this.orientationLabel);
            this.innerPanel.Controls.Add(this.orientationComboBox);
            this.innerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.innerPanel.Location = new System.Drawing.Point(0, 0);
            this.innerPanel.Name = "innerPanel";
            this.innerPanel.Padding = new System.Windows.Forms.Padding(10);
            this.innerPanel.Size = new System.Drawing.Size(586, 393);
            this.innerPanel.TabIndex = 2;
            // 
            // orientationLabel
            // 
            this.orientationLabel.AutoSize = true;
            this.orientationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.orientationLabel.Location = new System.Drawing.Point(13, 14);
            this.orientationLabel.Name = "orientationLabel";
            this.orientationLabel.Size = new System.Drawing.Size(94, 17);
            this.orientationLabel.TabIndex = 0;
            this.orientationLabel.Text = "Orientation:";
            this.orientationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // orientationComboBox
            // 
            this.orientationComboBox.FormattingEnabled = true;
            this.orientationComboBox.Location = new System.Drawing.Point(113, 13);
            this.orientationComboBox.Name = "orientationComboBox";
            this.orientationComboBox.Size = new System.Drawing.Size(161, 21);
            this.orientationComboBox.TabIndex = 1;
            // 
            // librariesTabPage
            // 
            this.librariesTabPage.Controls.Add(this.librariesSplitContainer);
            this.librariesTabPage.Location = new System.Drawing.Point(4, 22);
            this.librariesTabPage.Name = "librariesTabPage";
            this.librariesTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.librariesTabPage.Size = new System.Drawing.Size(592, 399);
            this.librariesTabPage.TabIndex = 1;
            this.librariesTabPage.Text = "Libraries";
            this.librariesTabPage.UseVisualStyleBackColor = true;
            // 
            // librariesSplitContainer
            // 
            this.librariesSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.librariesSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.librariesSplitContainer.Location = new System.Drawing.Point(3, 3);
            this.librariesSplitContainer.Name = "librariesSplitContainer";
            this.librariesSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // librariesSplitContainer.Panel1
            // 
            this.librariesSplitContainer.Panel1.Controls.Add(this.librariesDataGridView);
            // 
            // librariesSplitContainer.Panel2
            // 
            this.librariesSplitContainer.Panel2.Controls.Add(this.librariesButtonsPanel);
            this.librariesSplitContainer.Size = new System.Drawing.Size(586, 393);
            this.librariesSplitContainer.SplitterDistance = 357;
            this.librariesSplitContainer.TabIndex = 2;
            // 
            // librariesDataGridView
            // 
            this.librariesDataGridView.AllowUserToAddRows = false;
            this.librariesDataGridView.AllowUserToDeleteRows = false;
            this.librariesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.librariesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LibraryName,
            this.Version,
            this.File});
            this.librariesDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.librariesDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.librariesDataGridView.Location = new System.Drawing.Point(0, 0);
            this.librariesDataGridView.MultiSelect = false;
            this.librariesDataGridView.Name = "librariesDataGridView";
            this.librariesDataGridView.ReadOnly = true;
            this.librariesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.librariesDataGridView.ShowCellErrors = false;
            this.librariesDataGridView.ShowEditingIcon = false;
            this.librariesDataGridView.Size = new System.Drawing.Size(586, 357);
            this.librariesDataGridView.TabIndex = 0;
            // 
            // LibraryName
            // 
            this.LibraryName.FillWeight = 250F;
            this.LibraryName.HeaderText = "Name";
            this.LibraryName.MinimumWidth = 100;
            this.LibraryName.Name = "LibraryName";
            this.LibraryName.ReadOnly = true;
            this.LibraryName.Width = 150;
            // 
            // Version
            // 
            this.Version.HeaderText = "Version";
            this.Version.MinimumWidth = 100;
            this.Version.Name = "Version";
            this.Version.ReadOnly = true;
            this.Version.Width = 125;
            // 
            // File
            // 
            this.File.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.File.HeaderText = "File";
            this.File.MinimumWidth = 100;
            this.File.Name = "File";
            this.File.ReadOnly = true;
            // 
            // librariesButtonsPanel
            // 
            this.librariesButtonsPanel.Controls.Add(this.linkLabelGetDefinitionFiles);
            this.librariesButtonsPanel.Controls.Add(this.btnRemove);
            this.librariesButtonsPanel.Controls.Add(this.btnAdd);
            this.librariesButtonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.librariesButtonsPanel.Location = new System.Drawing.Point(0, 0);
            this.librariesButtonsPanel.Name = "librariesButtonsPanel";
            this.librariesButtonsPanel.Size = new System.Drawing.Size(586, 32);
            this.librariesButtonsPanel.TabIndex = 1;
            // 
            // linkLabelGetDefinitionFiles
            // 
            this.linkLabelGetDefinitionFiles.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.linkLabelGetDefinitionFiles.AutoSize = true;
            this.linkLabelGetDefinitionFiles.Location = new System.Drawing.Point(488, 8);
            this.linkLabelGetDefinitionFiles.Name = "linkLabelGetDefinitionFiles";
            this.linkLabelGetDefinitionFiles.Size = new System.Drawing.Size(95, 13);
            this.linkLabelGetDefinitionFiles.TabIndex = 2;
            this.linkLabelGetDefinitionFiles.TabStop = true;
            this.linkLabelGetDefinitionFiles.Text = "Get Definition Files";
            this.linkLabelGetDefinitionFiles.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelGetDefinitionFiles_LinkClicked);
            // 
            // btnRemove
            // 
            this.btnRemove.Image = global::SmallSharpTools.JavaScriptBrowser.Controls.Properties.Resources.script_delete;
            this.btnRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemove.Location = new System.Drawing.Point(89, 3);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(80, 25);
            this.btnRemove.TabIndex = 1;
            this.btnRemove.Text = "Remove";
            this.btnRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRemove.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Image = global::SmallSharpTools.JavaScriptBrowser.Controls.Properties.Resources.script_add;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(3, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(80, 25);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add";
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Script Library Definitions (*.sld)|*.sld|All Files (*.*)|*.*";
            this.openFileDialog.FilterIndex = 0;
            this.openFileDialog.InitialDirectory = "MyDocuments";
            this.openFileDialog.Title = "Add Script Library Definition";
            // 
            // OptionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.librariesPanel);
            this.Name = "OptionsControl";
            this.Size = new System.Drawing.Size(600, 425);
            this.librariesPanel.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.generalTabPage.ResumeLayout(false);
            this.generalPanel.ResumeLayout(false);
            this.innerPanel.ResumeLayout(false);
            this.innerPanel.PerformLayout();
            this.librariesTabPage.ResumeLayout(false);
            this.librariesSplitContainer.Panel1.ResumeLayout(false);
            this.librariesSplitContainer.Panel2.ResumeLayout(false);
            this.librariesSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.librariesDataGridView)).EndInit();
            this.librariesButtonsPanel.ResumeLayout(false);
            this.librariesButtonsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel librariesPanel;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage generalTabPage;
        private System.Windows.Forms.TabPage librariesTabPage;
        private System.Windows.Forms.Panel librariesButtonsPanel;
        private System.Windows.Forms.DataGridView librariesDataGridView;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.DataGridViewTextBoxColumn LibraryName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Version;
        private System.Windows.Forms.DataGridViewTextBoxColumn File;
        private System.Windows.Forms.LinkLabel linkLabelGetDefinitionFiles;
        private System.Windows.Forms.SplitContainer librariesSplitContainer;
        private System.Windows.Forms.Panel generalPanel;
        private System.Windows.Forms.Label orientationLabel;
        private System.Windows.Forms.ComboBox orientationComboBox;
        private System.Windows.Forms.Panel innerPanel;
    }
}
