﻿namespace ModGenerator
{
    partial class ModBuilder
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sADXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sA2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CheckAllButton = new System.Windows.Forms.Button();
            this.CheckModifiedButton = new System.Windows.Forms.Button();
            this.UncheckAllButton = new System.Windows.Forms.Button();
            this.OldCPlusExportButton = new System.Windows.Forms.Button();
            this.CPlusExportButton = new System.Windows.Forms.Button();
            this.IniExportButton = new System.Windows.Forms.Button();
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(584, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameToolStripMenuItem,
            this.projectToolStripMenuItem,
            this.openToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sADXToolStripMenuItem,
            this.sA2ToolStripMenuItem});
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.gameToolStripMenuItem.Text = "Game";
            // 
            // sADXToolStripMenuItem
            // 
            this.sADXToolStripMenuItem.Checked = true;
            this.sADXToolStripMenuItem.CheckOnClick = true;
            this.sADXToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.sADXToolStripMenuItem.Name = "sADXToolStripMenuItem";
            this.sADXToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.sADXToolStripMenuItem.Text = "SADX";
            this.sADXToolStripMenuItem.Click += new System.EventHandler(this.sADXToolStripMenuItem_Click);
            // 
            // sA2ToolStripMenuItem
            // 
            this.sA2ToolStripMenuItem.CheckOnClick = true;
            this.sA2ToolStripMenuItem.Name = "sA2ToolStripMenuItem";
            this.sA2ToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.sA2ToolStripMenuItem.Text = "SA2";
            this.sA2ToolStripMenuItem.Click += new System.EventHandler(this.sA2ToolStripMenuItem_Click);
            // 
            // projectToolStripMenuItem
            // 
            this.projectToolStripMenuItem.Name = "projectToolStripMenuItem";
            this.projectToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.projectToolStripMenuItem.Text = "&Project";
            this.projectToolStripMenuItem.Click += new System.EventHandler(this.projectToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.openToolStripMenuItem.Text = "&Open Specific INI";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listView1.FullRowSelect = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.Location = new System.Drawing.Point(12, 27);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(560, 484);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 240;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Type";
            this.columnHeader2.Width = 120;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Changed";
            // 
            // CheckAllButton
            // 
            this.CheckAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CheckAllButton.AutoSize = true;
            this.CheckAllButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CheckAllButton.Location = new System.Drawing.Point(12, 527);
            this.CheckAllButton.Name = "CheckAllButton";
            this.CheckAllButton.Size = new System.Drawing.Size(62, 23);
            this.CheckAllButton.TabIndex = 2;
            this.CheckAllButton.Text = "Check All";
            this.CheckAllButton.UseVisualStyleBackColor = true;
            this.CheckAllButton.Click += new System.EventHandler(this.CheckAll_Click);
            // 
            // CheckModifiedButton
            // 
            this.CheckModifiedButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CheckModifiedButton.AutoSize = true;
            this.CheckModifiedButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CheckModifiedButton.Location = new System.Drawing.Point(80, 527);
            this.CheckModifiedButton.Name = "CheckModifiedButton";
            this.CheckModifiedButton.Size = new System.Drawing.Size(91, 23);
            this.CheckModifiedButton.TabIndex = 3;
            this.CheckModifiedButton.Text = "Check Modified";
            this.CheckModifiedButton.UseVisualStyleBackColor = true;
            this.CheckModifiedButton.Click += new System.EventHandler(this.CheckModified_Click);
            // 
            // UncheckAllButton
            // 
            this.UncheckAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.UncheckAllButton.AutoSize = true;
            this.UncheckAllButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.UncheckAllButton.Location = new System.Drawing.Point(177, 527);
            this.UncheckAllButton.Name = "UncheckAllButton";
            this.UncheckAllButton.Size = new System.Drawing.Size(75, 23);
            this.UncheckAllButton.TabIndex = 4;
            this.UncheckAllButton.Text = "Uncheck All";
            this.UncheckAllButton.UseVisualStyleBackColor = true;
            this.UncheckAllButton.Click += new System.EventHandler(this.UnCheckAll_Click);
            // 
            // OldCPlusExportButton
            // 
            this.OldCPlusExportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OldCPlusExportButton.AutoSize = true;
            this.OldCPlusExportButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.OldCPlusExportButton.Location = new System.Drawing.Point(258, 527);
            this.OldCPlusExportButton.Name = "OldCPlusExportButton";
            this.OldCPlusExportButton.Size = new System.Drawing.Size(92, 23);
            this.OldCPlusExportButton.TabIndex = 5;
            this.OldCPlusExportButton.Text = "Export C++ (old)";
            this.OldCPlusExportButton.UseVisualStyleBackColor = true;
            this.OldCPlusExportButton.Click += new System.EventHandler(this.ExportOldCPP_Click);
            // 
            // CPlusExportButton
            // 
            this.CPlusExportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CPlusExportButton.AutoSize = true;
            this.CPlusExportButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CPlusExportButton.Location = new System.Drawing.Point(356, 527);
            this.CPlusExportButton.Name = "CPlusExportButton";
            this.CPlusExportButton.Size = new System.Drawing.Size(98, 23);
            this.CPlusExportButton.TabIndex = 6;
            this.CPlusExportButton.Text = "Export C++ (new)";
            this.CPlusExportButton.UseVisualStyleBackColor = true;
            this.CPlusExportButton.Click += new System.EventHandler(this.ExportNewCPP_Click);
            // 
            // IniExportButton
            // 
            this.IniExportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.IniExportButton.AutoSize = true;
            this.IniExportButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.IniExportButton.Location = new System.Drawing.Point(460, 527);
            this.IniExportButton.Name = "IniExportButton";
            this.IniExportButton.Size = new System.Drawing.Size(85, 23);
            this.IniExportButton.TabIndex = 7;
            this.IniExportButton.Text = "Export File List";
            this.IniExportButton.UseVisualStyleBackColor = true;
            this.IniExportButton.Click += new System.EventHandler(this.INIExport_Click);
            // 
            // ModBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 562);
            this.Controls.Add(this.IniExportButton);
            this.Controls.Add(this.CPlusExportButton);
            this.Controls.Add(this.OldCPlusExportButton);
            this.Controls.Add(this.UncheckAllButton);
            this.Controls.Add(this.CheckModifiedButton);
            this.Controls.Add(this.CheckAllButton);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ModBuilder";
            this.Text = "Struct Converter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button CheckAllButton;
        private System.Windows.Forms.Button CheckModifiedButton;
        private System.Windows.Forms.Button UncheckAllButton;
        private System.Windows.Forms.Button OldCPlusExportButton;
		private System.Windows.Forms.Button CPlusExportButton;
        private System.Windows.Forms.Button IniExportButton;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sADXToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sA2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowser;
        private System.Windows.Forms.ToolStripMenuItem projectToolStripMenuItem;
    }
}

