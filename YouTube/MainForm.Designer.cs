namespace YouTube
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.resultsSearchHint = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.topPanel = new System.Windows.Forms.Panel();
            this.topLogo = new System.Windows.Forms.PictureBox();
            this.videoResultsBox = new YouTube.CustomFlowLayoutPanel();
            this.loadMoreLink = new System.Windows.Forms.LinkLabel();
            this.searchBox = new ImageControls.ImagePanel();
            this.searchButton = new ImageControls.ImageButton();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.toolStrip = new NativeToolStrip.NativeToolStrip();
            this.menuButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.queueListButton = new System.Windows.Forms.ToolStripButton();
            this.downloadStatusButton = new System.Windows.Forms.ToolStripButton();
            this.topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.topLogo)).BeginInit();
            this.videoResultsBox.SuspendLayout();
            this.searchBox.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // resultsSearchHint
            // 
            this.resultsSearchHint.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.resultsSearchHint.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.resultsSearchHint.Location = new System.Drawing.Point(138, 206);
            this.resultsSearchHint.Name = "resultsSearchHint";
            this.resultsSearchHint.Size = new System.Drawing.Size(408, 51);
            this.resultsSearchHint.TabIndex = 5;
            this.resultsSearchHint.Text = "Search Box Hint Text";
            this.resultsSearchHint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::YouTube.Properties.Resources.HeaderShadow;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 52);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(684, 20);
            this.panel1.TabIndex = 6;
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.topPanel.BackgroundImage = global::YouTube.Properties.Resources.Header;
            this.topPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.topPanel.Controls.Add(this.searchBox);
            this.topPanel.Controls.Add(this.toolStrip);
            this.topPanel.Controls.Add(this.topLogo);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(684, 52);
            this.topPanel.TabIndex = 0;
            // 
            // topLogo
            // 
            this.topLogo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.topLogo.BackColor = System.Drawing.Color.Transparent;
            this.topLogo.Image = ((System.Drawing.Image)(resources.GetObject("topLogo.Image")));
            this.topLogo.Location = new System.Drawing.Point(12, 8);
            this.topLogo.Name = "topLogo";
            this.topLogo.Size = new System.Drawing.Size(86, 36);
            this.topLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.topLogo.TabIndex = 0;
            this.topLogo.TabStop = false;
            // 
            // videoResultsBox
            // 
            this.videoResultsBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.videoResultsBox.AutoScroll = true;
            this.videoResultsBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.videoResultsBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.videoResultsBox.Controls.Add(this.loadMoreLink);
            this.videoResultsBox.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.videoResultsBox.Location = new System.Drawing.Point(17, 71);
            this.videoResultsBox.Name = "videoResultsBox";
            this.videoResultsBox.Padding = new System.Windows.Forms.Padding(2);
            this.videoResultsBox.Size = new System.Drawing.Size(650, 375);
            this.videoResultsBox.TabIndex = 1;
            this.videoResultsBox.WrapContents = false;
            // 
            // loadMoreLink
            // 
            this.loadMoreLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.loadMoreLink.LinkColor = System.Drawing.SystemColors.HotTrack;
            this.loadMoreLink.Location = new System.Drawing.Point(5, 2);
            this.loadMoreLink.Name = "loadMoreLink";
            this.loadMoreLink.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.loadMoreLink.Size = new System.Drawing.Size(444, 37);
            this.loadMoreLink.TabIndex = 0;
            this.loadMoreLink.TabStop = true;
            this.loadMoreLink.Text = "Load more videos...";
            this.loadMoreLink.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.loadMoreLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.loadMoreLink_LinkClicked);
            // 
            // searchBox
            // 
            this.searchBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.searchBox.BackColor = System.Drawing.Color.Transparent;
            this.searchBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("searchBox.BackgroundImage")));
            this.searchBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.searchBox.BackImageNormal = global::YouTube.Properties.Resources.SearchBoxText;
            this.searchBox.BackImageSlice = new System.Windows.Forms.Padding(4);
            this.searchBox.Controls.Add(this.searchButton);
            this.searchBox.Controls.Add(this.searchTextBox);
            this.searchBox.Location = new System.Drawing.Point(177, 14);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(330, 24);
            this.searchBox.TabIndex = 4;
            // 
            // searchButton
            // 
            this.searchButton.BackColor = System.Drawing.Color.Transparent;
            this.searchButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("searchButton.BackgroundImage")));
            this.searchButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.searchButton.BackImageDisabled = global::YouTube.Properties.Resources.SearchButton_Disabled;
            this.searchButton.BackImageFocus = null;
            this.searchButton.BackImageHover = global::YouTube.Properties.Resources.SearchButton_Hover;
            this.searchButton.BackImageNormal = global::YouTube.Properties.Resources.SearchButton_Normal;
            this.searchButton.BackImagePressed = global::YouTube.Properties.Resources.SearchButton_Press;
            this.searchButton.BackImageSlice = new System.Windows.Forms.Padding(4);
            this.searchButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.searchButton.FlatAppearance.BorderSize = 0;
            this.searchButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.searchButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.searchButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.searchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchButton.Location = new System.Drawing.Point(272, 0);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(58, 24);
            this.searchButton.TabIndex = 1;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = false;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // searchTextBox
            // 
            this.searchTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.searchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.searchTextBox.Location = new System.Drawing.Point(5, 6);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(261, 13);
            this.searchTextBox.TabIndex = 0;
            this.searchTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchTextBox_KeyDown);
            // 
            // toolStrip
            // 
            this.toolStrip.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.toolStrip.AutoSize = false;
            this.toolStrip.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuButton,
            this.queueListButton,
            this.downloadStatusButton});
            this.toolStrip.Location = new System.Drawing.Point(546, 14);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.toolStrip.Size = new System.Drawing.Size(129, 25);
            this.toolStrip.TabIndex = 3;
            this.toolStrip.Text = "toolStrip1";
            this.toolStrip.Theme = NativeToolStrip.NativeToolStripTheme.Transparent;
            // 
            // menuButton
            // 
            this.menuButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.menuButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuButton.Image = ((System.Drawing.Image)(resources.GetObject("menuButton.Image")));
            this.menuButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuButton.Name = "menuButton";
            this.menuButton.Size = new System.Drawing.Size(51, 22);
            this.menuButton.Text = "Menu";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Text = "&Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // queueListButton
            // 
            this.queueListButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.queueListButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.queueListButton.Image = global::YouTube.Properties.Resources.List_empty;
            this.queueListButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.queueListButton.Name = "queueListButton";
            this.queueListButton.Size = new System.Drawing.Size(23, 22);
            this.queueListButton.Text = "Queue";
            this.queueListButton.Click += new System.EventHandler(this.queueListButton_Click);
            // 
            // downloadStatusButton
            // 
            this.downloadStatusButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.downloadStatusButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.downloadStatusButton.Image = global::YouTube.Properties.Resources.Download_idle;
            this.downloadStatusButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.downloadStatusButton.Name = "downloadStatusButton";
            this.downloadStatusButton.Size = new System.Drawing.Size(23, 22);
            this.downloadStatusButton.Text = "Downloads";
            this.downloadStatusButton.Click += new System.EventHandler(this.downloadStatusButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(684, 462);
            this.Controls.Add(this.resultsSearchHint);
            this.Controls.Add(this.videoResultsBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.topPanel);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "YouTube";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.topPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.topLogo)).EndInit();
            this.videoResultsBox.ResumeLayout(false);
            this.searchBox.ResumeLayout(false);
            this.searchBox.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.PictureBox topLogo;
        private CustomFlowLayoutPanel videoResultsBox;
        private NativeToolStrip.NativeToolStrip toolStrip;
        private System.Windows.Forms.ToolStripDropDownButton menuButton;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label resultsSearchHint;
        private System.Windows.Forms.LinkLabel loadMoreLink;
        private System.Windows.Forms.ToolStripButton queueListButton;
        private System.Windows.Forms.ToolStripButton downloadStatusButton;
        private ImageControls.ImagePanel searchBox;
        private ImageControls.ImageButton searchButton;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Panel panel1;
    }
}

