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
            this.topPanel = new System.Windows.Forms.Panel();
            this.toolStrip = new NativeToolStrip.NativeToolStrip();
            this.menuButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchBox = new System.Windows.Forms.GroupBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.topPanelDivider = new System.Windows.Forms.GroupBox();
            this.topLogo = new System.Windows.Forms.PictureBox();
            this.videoResultsBox = new System.Windows.Forms.FlowLayoutPanel();
            this.loadMoreLink = new System.Windows.Forms.LinkLabel();
            this.resultsSearchHint = new System.Windows.Forms.Label();
            this.topPanel.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.searchBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.topLogo)).BeginInit();
            this.videoResultsBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.topPanel.Controls.Add(this.toolStrip);
            this.topPanel.Controls.Add(this.searchBox);
            this.topPanel.Controls.Add(this.topPanelDivider);
            this.topPanel.Controls.Add(this.topLogo);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(684, 52);
            this.topPanel.TabIndex = 0;
            // 
            // toolStrip
            // 
            this.toolStrip.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.toolStrip.AutoSize = false;
            this.toolStrip.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuButton});
            this.toolStrip.Location = new System.Drawing.Point(618, 14);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.toolStrip.Size = new System.Drawing.Size(57, 25);
            this.toolStrip.TabIndex = 3;
            this.toolStrip.Text = "toolStrip1";
            this.toolStrip.Theme = NativeToolStrip.NativeToolStripTheme.Transparent;
            // 
            // menuButton
            // 
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
            // searchBox
            // 
            this.searchBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.searchBox.Controls.Add(this.searchButton);
            this.searchBox.Controls.Add(this.searchTextBox);
            this.searchBox.Location = new System.Drawing.Point(141, 1);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(402, 43);
            this.searchBox.TabIndex = 2;
            this.searchBox.TabStop = false;
            // 
            // searchButton
            // 
            this.searchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchButton.Location = new System.Drawing.Point(324, 13);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(70, 22);
            this.searchButton.TabIndex = 1;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // searchTextBox
            // 
            this.searchTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchTextBox.Location = new System.Drawing.Point(9, 14);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(309, 20);
            this.searchTextBox.TabIndex = 0;
            this.searchTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchTextBox_KeyDown);
            // 
            // topPanelDivider
            // 
            this.topPanelDivider.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.topPanelDivider.Location = new System.Drawing.Point(0, 50);
            this.topPanelDivider.Name = "topPanelDivider";
            this.topPanelDivider.Size = new System.Drawing.Size(684, 2);
            this.topPanelDivider.TabIndex = 1;
            this.topPanelDivider.TabStop = false;
            // 
            // topLogo
            // 
            this.topLogo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.topLogo.Image = ((System.Drawing.Image)(resources.GetObject("topLogo.Image")));
            this.topLogo.Location = new System.Drawing.Point(12, 8);
            this.topLogo.Name = "topLogo";
            this.topLogo.Size = new System.Drawing.Size(86, 36);
            this.topLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
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
            this.videoResultsBox.Location = new System.Drawing.Point(17, 67);
            this.videoResultsBox.Name = "videoResultsBox";
            this.videoResultsBox.Padding = new System.Windows.Forms.Padding(2);
            this.videoResultsBox.Size = new System.Drawing.Size(650, 381);
            this.videoResultsBox.TabIndex = 1;
            this.videoResultsBox.WrapContents = false;
            // 
            // loadMoreLink
            // 
            this.loadMoreLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.loadMoreLink.LinkColor = System.Drawing.SystemColors.HotTrack;
            this.loadMoreLink.Location = new System.Drawing.Point(5, 2);
            this.loadMoreLink.Name = "loadMoreLink";
            this.loadMoreLink.Size = new System.Drawing.Size(619, 37);
            this.loadMoreLink.TabIndex = 0;
            this.loadMoreLink.TabStop = true;
            this.loadMoreLink.Text = "Load more videos...";
            this.loadMoreLink.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.loadMoreLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.loadMoreLink_LinkClicked);
            // 
            // resultsSearchHint
            // 
            this.resultsSearchHint.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.resultsSearchHint.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.resultsSearchHint.Location = new System.Drawing.Point(204, 206);
            this.resultsSearchHint.Name = "resultsSearchHint";
            this.resultsSearchHint.Size = new System.Drawing.Size(277, 51);
            this.resultsSearchHint.TabIndex = 5;
            this.resultsSearchHint.Text = "To begin, search for videos";
            this.resultsSearchHint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.resultsSearchHint.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(684, 462);
            this.Controls.Add(this.resultsSearchHint);
            this.Controls.Add(this.videoResultsBox);
            this.Controls.Add(this.topPanel);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "YouTube";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.searchBox.ResumeLayout(false);
            this.searchBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.topLogo)).EndInit();
            this.videoResultsBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.PictureBox topLogo;
        private System.Windows.Forms.GroupBox topPanelDivider;
        private System.Windows.Forms.GroupBox searchBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.FlowLayoutPanel videoResultsBox;
        private NativeToolStrip.NativeToolStrip toolStrip;
        private System.Windows.Forms.ToolStripDropDownButton menuButton;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label resultsSearchHint;
        private System.Windows.Forms.LinkLabel loadMoreLink;
    }
}

