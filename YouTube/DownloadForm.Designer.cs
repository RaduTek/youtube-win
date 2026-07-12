namespace YouTube
{
    partial class DownloadForm
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
            this.rootPanel = new System.Windows.Forms.Panel();
            this.downloadListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.itemContextMenu = new NativeToolStrip.NativeContextMenuStrip();
            this.playVideoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showInFolderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.retryMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip = new NativeToolStrip.NativeToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.removeAllButton = new System.Windows.Forms.ToolStripButton();
            this.openDownloadsButton = new System.Windows.Forms.ToolStripButton();
            this.rootPanel.SuspendLayout();
            this.itemContextMenu.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // rootPanel
            // 
            this.rootPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rootPanel.Controls.Add(this.downloadListView);
            this.rootPanel.Controls.Add(this.toolStrip);
            this.rootPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rootPanel.Location = new System.Drawing.Point(0, 0);
            this.rootPanel.Name = "rootPanel";
            this.rootPanel.Padding = new System.Windows.Forms.Padding(2);
            this.rootPanel.Size = new System.Drawing.Size(240, 240);
            this.rootPanel.TabIndex = 0;
            // 
            // downloadListView
            // 
            this.downloadListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.downloadListView.ContextMenuStrip = this.itemContextMenu;
            this.downloadListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.downloadListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.downloadListView.HideSelection = false;
            this.downloadListView.Location = new System.Drawing.Point(2, 26);
            this.downloadListView.MultiSelect = false;
            this.downloadListView.Name = "downloadListView";
            this.downloadListView.Size = new System.Drawing.Size(234, 210);
            this.downloadListView.SmallImageList = this.imageList;
            this.downloadListView.TabIndex = 1;
            this.downloadListView.UseCompatibleStateImageBehavior = false;
            this.downloadListView.View = System.Windows.Forms.View.Details;
            this.downloadListView.DoubleClick += new System.EventHandler(this.downloadListView_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Video";
            this.columnHeader1.Width = 140;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Status";
            this.columnHeader2.Width = 65;
            // 
            // itemContextMenu
            // 
            this.itemContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playVideoMenuItem,
            this.showInFolderMenuItem,
            this.retryMenuItem,
            this.cancelMenuItem,
            this.removeMenuItem});
            this.itemContextMenu.Name = "itemContextMenu";
            this.itemContextMenu.Size = new System.Drawing.Size(181, 136);
            this.itemContextMenu.Theme = NativeToolStrip.NativeToolStripTheme.Toolbar;
            this.itemContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.itemContextMenu_Opening);
            // 
            // playVideoMenuItem
            // 
            this.playVideoMenuItem.Image = global::YouTube.Properties.Resources.control_play_blue;
            this.playVideoMenuItem.Name = "playVideoMenuItem";
            this.playVideoMenuItem.Size = new System.Drawing.Size(180, 22);
            this.playVideoMenuItem.Text = "Play Video";
            this.playVideoMenuItem.Click += new System.EventHandler(this.playVideoMenuItem_Click);
            // 
            // showInFolderMenuItem
            // 
            this.showInFolderMenuItem.Image = global::YouTube.Properties.Resources.folder;
            this.showInFolderMenuItem.Name = "showInFolderMenuItem";
            this.showInFolderMenuItem.Size = new System.Drawing.Size(180, 22);
            this.showInFolderMenuItem.Text = "Show in folder";
            this.showInFolderMenuItem.Click += new System.EventHandler(this.showInFolderMenuItem_Click);
            // 
            // retryMenuItem
            // 
            this.retryMenuItem.Name = "retryMenuItem";
            this.retryMenuItem.Size = new System.Drawing.Size(150, 22);
            this.retryMenuItem.Text = "Retry";
            this.retryMenuItem.Click += new System.EventHandler(this.retryMenuItem_Click);
            // 
            // cancelMenuItem
            // 
            this.cancelMenuItem.Image = global::YouTube.Properties.Resources.cross;
            this.cancelMenuItem.Name = "cancelMenuItem";
            this.cancelMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cancelMenuItem.Text = "Cancel";
            this.cancelMenuItem.Click += new System.EventHandler(this.cancelMenuItem_Click);
            // 
            // removeMenuItem
            // 
            this.removeMenuItem.Image = global::YouTube.Properties.Resources.bin_closed;
            this.removeMenuItem.Name = "removeMenuItem";
            this.removeMenuItem.Size = new System.Drawing.Size(180, 22);
            this.removeMenuItem.Text = "Remove";
            this.removeMenuItem.Click += new System.EventHandler(this.removeMenuItem_Click);
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // toolStrip
            // 
            this.toolStrip.AutoSize = false;
            this.toolStrip.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.removeAllButton,
            this.openDownloadsButton});
            this.toolStrip.Location = new System.Drawing.Point(2, 2);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.toolStrip.Size = new System.Drawing.Size(234, 24);
            this.toolStrip.TabIndex = 2;
            this.toolStrip.Text = "nativeToolStrip1";
            this.toolStrip.Theme = NativeToolStrip.NativeToolStripTheme.Transparent;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Image = global::YouTube.Properties.Resources.Download;
            this.toolStripLabel1.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(82, 23);
            this.toolStripLabel1.Text = "Downloads";
            // 
            // removeAllButton
            // 
            this.removeAllButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.removeAllButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.removeAllButton.Image = global::YouTube.Properties.Resources.bin_closed;
            this.removeAllButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.removeAllButton.Margin = new System.Windows.Forms.Padding(0);
            this.removeAllButton.Name = "removeAllButton";
            this.removeAllButton.Size = new System.Drawing.Size(23, 23);
            this.removeAllButton.Text = "Remove All";
            this.removeAllButton.Click += new System.EventHandler(this.removeAllButton_Click);
            // 
            // openDownloadsButton
            // 
            this.openDownloadsButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.openDownloadsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openDownloadsButton.Image = global::YouTube.Properties.Resources.folder;
            this.openDownloadsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openDownloadsButton.Margin = new System.Windows.Forms.Padding(0);
            this.openDownloadsButton.Name = "openDownloadsButton";
            this.openDownloadsButton.Size = new System.Drawing.Size(23, 23);
            this.openDownloadsButton.Text = "Open Downloads Folder";
            this.openDownloadsButton.Click += new System.EventHandler(this.openDownloadsButton_Click);
            // 
            // DownloadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(240, 240);
            this.ControlBox = false;
            this.Controls.Add(this.rootPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DownloadForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Download";
            this.Deactivate += new System.EventHandler(this.DownloadForm_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DownloadForm_FormClosing);
            this.rootPanel.ResumeLayout(false);
            this.itemContextMenu.ResumeLayout(false);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel rootPanel;
        private System.Windows.Forms.ListView downloadListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ImageList imageList;
        private NativeToolStrip.NativeContextMenuStrip itemContextMenu;
        private System.Windows.Forms.ToolStripMenuItem playVideoMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showInFolderMenuItem;
        private System.Windows.Forms.ToolStripMenuItem retryMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeMenuItem;
        private NativeToolStrip.NativeToolStrip toolStrip;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton openDownloadsButton;
        private System.Windows.Forms.ToolStripButton removeAllButton;
    }
}