namespace YouTube
{
    partial class VideoListEntry
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VideoListEntry));
            this.durationLabel = new System.Windows.Forms.Label();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.titleLink = new System.Windows.Forms.LinkLabel();
            this.dateLabel = new System.Windows.Forms.Label();
            this.viewsLabel = new System.Windows.Forms.Label();
            this.channelLink = new System.Windows.Forms.LinkLabel();
            this.thumbnailBox = new System.Windows.Forms.Panel();
            this.thumbnail = new System.Windows.Forms.Panel();
            this.queueButton = new System.Windows.Forms.PictureBox();
            this.ratingStar5 = new System.Windows.Forms.PictureBox();
            this.ratingStar4 = new System.Windows.Forms.PictureBox();
            this.ratingStar3 = new System.Windows.Forms.PictureBox();
            this.ratingStar2 = new System.Windows.Forms.PictureBox();
            this.ratingStar1 = new System.Windows.Forms.PictureBox();
            this.contextMenu = new NativeToolStrip.NativeContextMenuStrip();
            this.watchNowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToQueueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openInBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.thumbnailBox.SuspendLayout();
            this.thumbnail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.queueButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ratingStar5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ratingStar4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ratingStar3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ratingStar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ratingStar1)).BeginInit();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // durationLabel
            // 
            this.durationLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.durationLabel.AutoSize = true;
            this.durationLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.durationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.75F);
            this.durationLabel.ForeColor = System.Drawing.Color.White;
            this.durationLabel.Location = new System.Drawing.Point(98, 55);
            this.durationLabel.Name = "durationLabel";
            this.durationLabel.Padding = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.durationLabel.Size = new System.Drawing.Size(28, 15);
            this.durationLabel.TabIndex = 2;
            this.durationLabel.Text = "0:00";
            this.durationLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.durationLabel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.thumbnail_MouseClick);
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionLabel.AutoEllipsis = true;
            this.descriptionLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.descriptionLabel.Location = new System.Drawing.Point(136, 21);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(481, 35);
            this.descriptionLabel.TabIndex = 3;
            this.descriptionLabel.Text = "Video Description Goes Here";
            // 
            // titleLink
            // 
            this.titleLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.titleLink.AutoEllipsis = true;
            this.titleLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.titleLink.LinkColor = System.Drawing.SystemColors.HotTrack;
            this.titleLink.Location = new System.Drawing.Point(136, 2);
            this.titleLink.Name = "titleLink";
            this.titleLink.Size = new System.Drawing.Size(481, 17);
            this.titleLink.TabIndex = 9;
            this.titleLink.TabStop = true;
            this.titleLink.Text = "Video Title";
            this.titleLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.titleLink_LinkClicked);
            // 
            // dateLabel
            // 
            this.dateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dateLabel.AutoEllipsis = true;
            this.dateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.dateLabel.Location = new System.Drawing.Point(210, 60);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(90, 13);
            this.dateLabel.TabIndex = 10;
            this.dateLabel.Text = "10 days ago";
            this.dateLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // viewsLabel
            // 
            this.viewsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.viewsLabel.AutoEllipsis = true;
            this.viewsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewsLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.viewsLabel.Location = new System.Drawing.Point(306, 60);
            this.viewsLabel.Name = "viewsLabel";
            this.viewsLabel.Size = new System.Drawing.Size(100, 13);
            this.viewsLabel.TabIndex = 11;
            this.viewsLabel.Text = "123,000,000 views";
            // 
            // channelLink
            // 
            this.channelLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.channelLink.AutoEllipsis = true;
            this.channelLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.channelLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.channelLink.LinkColor = System.Drawing.SystemColors.HotTrack;
            this.channelLink.Location = new System.Drawing.Point(412, 60);
            this.channelLink.Name = "channelLink";
            this.channelLink.Size = new System.Drawing.Size(205, 13);
            this.channelLink.TabIndex = 12;
            this.channelLink.TabStop = true;
            this.channelLink.Text = "Channel Name";
            this.channelLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.channelLink_LinkClicked);
            // 
            // thumbnailBox
            // 
            this.thumbnailBox.BackColor = System.Drawing.Color.White;
            this.thumbnailBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.thumbnailBox.Controls.Add(this.thumbnail);
            this.thumbnailBox.Location = new System.Drawing.Point(0, 0);
            this.thumbnailBox.Name = "thumbnailBox";
            this.thumbnailBox.Padding = new System.Windows.Forms.Padding(1);
            this.thumbnailBox.Size = new System.Drawing.Size(130, 74);
            this.thumbnailBox.TabIndex = 13;
            // 
            // thumbnail
            // 
            this.thumbnail.BackColor = System.Drawing.Color.Transparent;
            this.thumbnail.BackgroundImage = global::YouTube.Properties.Resources.nothumb;
            this.thumbnail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.thumbnail.Controls.Add(this.queueButton);
            this.thumbnail.Controls.Add(this.durationLabel);
            this.thumbnail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.thumbnail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.thumbnail.Location = new System.Drawing.Point(1, 1);
            this.thumbnail.Name = "thumbnail";
            this.thumbnail.Size = new System.Drawing.Size(126, 70);
            this.thumbnail.TabIndex = 3;
            this.thumbnail.MouseClick += new System.Windows.Forms.MouseEventHandler(this.thumbnail_MouseClick);
            // 
            // queueButton
            // 
            this.queueButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.queueButton.Image = global::YouTube.Properties.Resources.AddToList;
            this.queueButton.Location = new System.Drawing.Point(1, 53);
            this.queueButton.Name = "queueButton";
            this.queueButton.Size = new System.Drawing.Size(16, 16);
            this.queueButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.queueButton.TabIndex = 3;
            this.queueButton.TabStop = false;
            this.toolTip.SetToolTip(this.queueButton, "Add to Queue");
            this.queueButton.Click += new System.EventHandler(this.queueButton_Click);
            // 
            // ratingStar5
            // 
            this.ratingStar5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ratingStar5.Image = ((System.Drawing.Image)(resources.GetObject("ratingStar5.Image")));
            this.ratingStar5.Location = new System.Drawing.Point(187, 60);
            this.ratingStar5.Name = "ratingStar5";
            this.ratingStar5.Size = new System.Drawing.Size(12, 12);
            this.ratingStar5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.ratingStar5.TabIndex = 8;
            this.ratingStar5.TabStop = false;
            // 
            // ratingStar4
            // 
            this.ratingStar4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ratingStar4.Image = ((System.Drawing.Image)(resources.GetObject("ratingStar4.Image")));
            this.ratingStar4.Location = new System.Drawing.Point(175, 60);
            this.ratingStar4.Name = "ratingStar4";
            this.ratingStar4.Size = new System.Drawing.Size(12, 12);
            this.ratingStar4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.ratingStar4.TabIndex = 7;
            this.ratingStar4.TabStop = false;
            // 
            // ratingStar3
            // 
            this.ratingStar3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ratingStar3.Image = ((System.Drawing.Image)(resources.GetObject("ratingStar3.Image")));
            this.ratingStar3.Location = new System.Drawing.Point(163, 60);
            this.ratingStar3.Name = "ratingStar3";
            this.ratingStar3.Size = new System.Drawing.Size(12, 12);
            this.ratingStar3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.ratingStar3.TabIndex = 6;
            this.ratingStar3.TabStop = false;
            // 
            // ratingStar2
            // 
            this.ratingStar2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ratingStar2.Image = ((System.Drawing.Image)(resources.GetObject("ratingStar2.Image")));
            this.ratingStar2.Location = new System.Drawing.Point(151, 60);
            this.ratingStar2.Name = "ratingStar2";
            this.ratingStar2.Size = new System.Drawing.Size(12, 12);
            this.ratingStar2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.ratingStar2.TabIndex = 5;
            this.ratingStar2.TabStop = false;
            // 
            // ratingStar1
            // 
            this.ratingStar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ratingStar1.Image = ((System.Drawing.Image)(resources.GetObject("ratingStar1.Image")));
            this.ratingStar1.Location = new System.Drawing.Point(139, 60);
            this.ratingStar1.Name = "ratingStar1";
            this.ratingStar1.Size = new System.Drawing.Size(12, 12);
            this.ratingStar1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.ratingStar1.TabIndex = 4;
            this.ratingStar1.TabStop = false;
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.watchNowToolStripMenuItem,
            this.addToQueueToolStripMenuItem,
            this.downloadToolStripMenuItem,
            this.openInBrowserToolStripMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(162, 92);
            this.contextMenu.Theme = NativeToolStrip.NativeToolStripTheme.Toolbar;
            // 
            // watchNowToolStripMenuItem
            // 
            this.watchNowToolStripMenuItem.Image = global::YouTube.Properties.Resources.control_play_blue;
            this.watchNowToolStripMenuItem.Name = "watchNowToolStripMenuItem";
            this.watchNowToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.watchNowToolStripMenuItem.Text = "&Watch now";
            this.watchNowToolStripMenuItem.Click += new System.EventHandler(this.watchNowToolStripMenuItem_Click);
            // 
            // addToQueueToolStripMenuItem
            // 
            this.addToQueueToolStripMenuItem.Image = global::YouTube.Properties.Resources.AddToList;
            this.addToQueueToolStripMenuItem.Name = "addToQueueToolStripMenuItem";
            this.addToQueueToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.addToQueueToolStripMenuItem.Text = "&Add to queue";
            this.addToQueueToolStripMenuItem.Click += new System.EventHandler(this.addToQueueToolStripMenuItem_Click);
            // 
            // downloadToolStripMenuItem
            // 
            this.downloadToolStripMenuItem.Image = global::YouTube.Properties.Resources.Download;
            this.downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
            this.downloadToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.downloadToolStripMenuItem.Text = "&Download";
            this.downloadToolStripMenuItem.Click += new System.EventHandler(this.downloadToolStripMenuItem_Click);
            // 
            // openInBrowserToolStripMenuItem
            // 
            this.openInBrowserToolStripMenuItem.Image = global::YouTube.Properties.Resources.link;
            this.openInBrowserToolStripMenuItem.Name = "openInBrowserToolStripMenuItem";
            this.openInBrowserToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.openInBrowserToolStripMenuItem.Text = "&Open in browser";
            this.openInBrowserToolStripMenuItem.Click += new System.EventHandler(this.openInBrowserToolStripMenuItem_Click);
            // 
            // toolTip
            // 
            this.toolTip.AutomaticDelay = 1000;
            // 
            // VideoListEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ContextMenuStrip = this.contextMenu;
            this.Controls.Add(this.thumbnailBox);
            this.Controls.Add(this.channelLink);
            this.Controls.Add(this.viewsLabel);
            this.Controls.Add(this.dateLabel);
            this.Controls.Add(this.titleLink);
            this.Controls.Add(this.ratingStar5);
            this.Controls.Add(this.ratingStar4);
            this.Controls.Add(this.ratingStar3);
            this.Controls.Add(this.ratingStar2);
            this.Controls.Add(this.ratingStar1);
            this.Controls.Add(this.descriptionLabel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "VideoListEntry";
            this.Size = new System.Drawing.Size(620, 74);
            this.Load += new System.EventHandler(this.VideoListEntry_Load);
            this.ParentChanged += new System.EventHandler(this.VideoListEntry_ParentChanged);
            this.thumbnailBox.ResumeLayout(false);
            this.thumbnail.ResumeLayout(false);
            this.thumbnail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.queueButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ratingStar5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ratingStar4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ratingStar3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ratingStar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ratingStar1)).EndInit();
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label durationLabel;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.PictureBox ratingStar1;
        private System.Windows.Forms.PictureBox ratingStar2;
        private System.Windows.Forms.PictureBox ratingStar3;
        private System.Windows.Forms.PictureBox ratingStar4;
        private System.Windows.Forms.PictureBox ratingStar5;
        private System.Windows.Forms.LinkLabel titleLink;
        private System.Windows.Forms.Label dateLabel;
        private System.Windows.Forms.Label viewsLabel;
        private System.Windows.Forms.LinkLabel channelLink;
        private System.Windows.Forms.Panel thumbnailBox;
        private NativeToolStrip.NativeContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem watchNowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openInBrowserToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Panel thumbnail;
        private System.Windows.Forms.PictureBox queueButton;
        private System.Windows.Forms.ToolStripMenuItem addToQueueToolStripMenuItem;
    }
}
