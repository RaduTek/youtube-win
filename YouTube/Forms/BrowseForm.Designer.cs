
namespace YouTube.Forms
{
    partial class BrowseForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrowseForm));
            this.header = new System.Windows.Forms.Panel();
            this.settingsButton = new YouTube.ExControls.ExButton();
            this.switchToWatchButton = new YouTube.ExControls.ExButton();
            this.searchBox = new YouTube.ExControls.ExPanel();
            this.searchButton = new YouTube.ExControls.ExButton();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.topLogo = new System.Windows.Forms.PictureBox();
            this.leftPanel = new System.Windows.Forms.Panel();
            this.guideFrame = new YouTube.ExControls.ExWebBrowser();
            this.leftSplitter = new System.Windows.Forms.Splitter();
            this.middlePanel = new System.Windows.Forms.Panel();
            this.browseFrame = new YouTube.ExControls.ExWebBrowser();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.queueFrame = new YouTube.ExControls.ExWebBrowser();
            this.rightSplitter = new System.Windows.Forms.Splitter();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.header.SuspendLayout();
            this.searchBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.topLogo)).BeginInit();
            this.leftPanel.SuspendLayout();
            this.middlePanel.SuspendLayout();
            this.rightPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // header
            // 
            this.header.BackgroundImage = global::YouTube.Properties.Resources.BrowseHeader;
            this.header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.header.Controls.Add(this.settingsButton);
            this.header.Controls.Add(this.switchToWatchButton);
            this.header.Controls.Add(this.searchBox);
            this.header.Controls.Add(this.topLogo);
            this.header.Dock = System.Windows.Forms.DockStyle.Top;
            this.header.Location = new System.Drawing.Point(0, 0);
            this.header.Name = "header";
            this.header.Size = new System.Drawing.Size(784, 48);
            this.header.TabIndex = 1;
            // 
            // settingsButton
            // 
            this.settingsButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.settingsButton.BackColor = System.Drawing.Color.Transparent;
            this.settingsButton.BackKey = "Button";
            this.settingsButton.BackMargins = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.settingsButton.EnableTransparency = true;
            this.settingsButton.Icon = ((System.Drawing.Image)(resources.GetObject("settingsButton.Icon")));
            this.settingsButton.IconKey = "SettingsLight";
            this.settingsButton.IconTransparencyKey = System.Drawing.Color.Magenta;
            this.settingsButton.Location = new System.Drawing.Point(706, 10);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(30, 26);
            this.settingsButton.TabIndex = 6;
            this.settingsButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip.SetToolTip(this.settingsButton, "Settings");
            this.settingsButton.Click += new System.EventHandler(this.settingsButton_Click);
            // 
            // switchToWatchButton
            // 
            this.switchToWatchButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.switchToWatchButton.BackColor = System.Drawing.Color.Transparent;
            this.switchToWatchButton.BackKey = "Button";
            this.switchToWatchButton.BackMargins = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.switchToWatchButton.Enabled = false;
            this.switchToWatchButton.EnableTransparency = true;
            this.switchToWatchButton.Icon = ((System.Drawing.Image)(resources.GetObject("switchToWatchButton.Icon")));
            this.switchToWatchButton.IconKey = "BrowseToWatch";
            this.switchToWatchButton.IconTransparencyKey = System.Drawing.Color.Magenta;
            this.switchToWatchButton.Location = new System.Drawing.Point(742, 10);
            this.switchToWatchButton.Name = "switchToWatchButton";
            this.switchToWatchButton.Size = new System.Drawing.Size(30, 26);
            this.switchToWatchButton.TabIndex = 5;
            this.switchToWatchButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip.SetToolTip(this.switchToWatchButton, "Switch to Watch");
            this.switchToWatchButton.Click += new System.EventHandler(this.browseToWatchButton_Click);
            // 
            // searchBox
            // 
            this.searchBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.searchBox.BackColor = System.Drawing.Color.Transparent;
            this.searchBox.BackKey = "SearchBoxText";
            this.searchBox.BackMargins = new System.Windows.Forms.Padding(4);
            this.searchBox.Controls.Add(this.searchButton);
            this.searchBox.Controls.Add(this.searchTextBox);
            this.searchBox.Location = new System.Drawing.Point(259, 12);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(267, 23);
            this.searchBox.TabIndex = 2;
            // 
            // searchButton
            // 
            this.searchButton.BackKey = "SearchButton";
            this.searchButton.BackMargins = new System.Windows.Forms.Padding(4);
            this.searchButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.searchButton.EnableTransparency = true;
            this.searchButton.Icon = null;
            this.searchButton.IconKey = "";
            this.searchButton.IconTransparencyKey = System.Drawing.Color.Magenta;
            this.searchButton.Location = new System.Drawing.Point(207, 0);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(60, 23);
            this.searchButton.TabIndex = 1;
            this.searchButton.Text = "Search";
            this.searchButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // searchTextBox
            // 
            this.searchTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.searchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.searchTextBox.Location = new System.Drawing.Point(5, 5);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(199, 13);
            this.searchTextBox.TabIndex = 0;
            this.searchTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchTextBox_KeyDown);
            // 
            // topLogo
            // 
            this.topLogo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.topLogo.BackColor = System.Drawing.Color.Transparent;
            this.topLogo.Image = global::YouTube.Properties.Resources.BrowseHeaderLogo;
            this.topLogo.Location = new System.Drawing.Point(12, 7);
            this.topLogo.Name = "topLogo";
            this.topLogo.Size = new System.Drawing.Size(80, 32);
            this.topLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.topLogo.TabIndex = 1;
            this.topLogo.TabStop = false;
            // 
            // leftPanel
            // 
            this.leftPanel.Controls.Add(this.guideFrame);
            this.leftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftPanel.Location = new System.Drawing.Point(0, 48);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Size = new System.Drawing.Size(140, 433);
            this.leftPanel.TabIndex = 2;
            // 
            // guideFrame
            // 
            this.guideFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guideFrame.IsWebBrowserContextMenuEnabled = false;
            this.guideFrame.Location = new System.Drawing.Point(0, 0);
            this.guideFrame.MinimumSize = new System.Drawing.Size(20, 20);
            this.guideFrame.Name = "guideFrame";
            this.guideFrame.ScriptErrorsSuppressed = true;
            this.guideFrame.Size = new System.Drawing.Size(140, 433);
            this.guideFrame.TabIndex = 0;
            this.guideFrame.Visible = false;
            this.guideFrame.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.guideFrame_DocumentCompleted);
            // 
            // leftSplitter
            // 
            this.leftSplitter.Location = new System.Drawing.Point(140, 48);
            this.leftSplitter.Name = "leftSplitter";
            this.leftSplitter.Size = new System.Drawing.Size(3, 433);
            this.leftSplitter.TabIndex = 3;
            this.leftSplitter.TabStop = false;
            // 
            // middlePanel
            // 
            this.middlePanel.Controls.Add(this.browseFrame);
            this.middlePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.middlePanel.Location = new System.Drawing.Point(143, 48);
            this.middlePanel.Name = "middlePanel";
            this.middlePanel.Size = new System.Drawing.Size(458, 433);
            this.middlePanel.TabIndex = 4;
            // 
            // browseFrame
            // 
            this.browseFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browseFrame.IsWebBrowserContextMenuEnabled = false;
            this.browseFrame.Location = new System.Drawing.Point(0, 0);
            this.browseFrame.MinimumSize = new System.Drawing.Size(20, 20);
            this.browseFrame.Name = "browseFrame";
            this.browseFrame.ScriptErrorsSuppressed = true;
            this.browseFrame.Size = new System.Drawing.Size(458, 433);
            this.browseFrame.TabIndex = 0;
            this.browseFrame.Visible = false;
            // 
            // rightPanel
            // 
            this.rightPanel.Controls.Add(this.queueFrame);
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.rightPanel.Location = new System.Drawing.Point(604, 48);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(180, 433);
            this.rightPanel.TabIndex = 5;
            // 
            // queueFrame
            // 
            this.queueFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.queueFrame.IsWebBrowserContextMenuEnabled = false;
            this.queueFrame.Location = new System.Drawing.Point(0, 0);
            this.queueFrame.MinimumSize = new System.Drawing.Size(20, 20);
            this.queueFrame.Name = "queueFrame";
            this.queueFrame.ScriptErrorsSuppressed = true;
            this.queueFrame.Size = new System.Drawing.Size(180, 433);
            this.queueFrame.TabIndex = 0;
            this.queueFrame.Visible = false;
            // 
            // rightSplitter
            // 
            this.rightSplitter.Dock = System.Windows.Forms.DockStyle.Right;
            this.rightSplitter.Location = new System.Drawing.Point(601, 48);
            this.rightSplitter.Name = "rightSplitter";
            this.rightSplitter.Size = new System.Drawing.Size(3, 433);
            this.rightSplitter.TabIndex = 6;
            this.rightSplitter.TabStop = false;
            // 
            // BrowseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(784, 481);
            this.Controls.Add(this.middlePanel);
            this.Controls.Add(this.rightSplitter);
            this.Controls.Add(this.rightPanel);
            this.Controls.Add(this.leftSplitter);
            this.Controls.Add(this.leftPanel);
            this.Controls.Add(this.header);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(640, 420);
            this.Name = "BrowseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "YouTube";
            this.Load += new System.EventHandler(this.BrowseForm_Load);
            this.Shown += new System.EventHandler(this.BrowseForm_Shown);
            this.header.ResumeLayout(false);
            this.searchBox.ResumeLayout(false);
            this.searchBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.topLogo)).EndInit();
            this.leftPanel.ResumeLayout(false);
            this.middlePanel.ResumeLayout(false);
            this.rightPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel header;
        private System.Windows.Forms.PictureBox topLogo;
        private System.Windows.Forms.Panel leftPanel;
        private System.Windows.Forms.Splitter leftSplitter;
        private System.Windows.Forms.Panel middlePanel;
        private System.Windows.Forms.Panel rightPanel;
        private System.Windows.Forms.Splitter rightSplitter;
        private ExControls.ExPanel searchBox;
        private ExControls.ExWebBrowser guideFrame;
        private ExControls.ExWebBrowser browseFrame;
        private ExControls.ExWebBrowser queueFrame;
        private System.Windows.Forms.TextBox searchTextBox;
        private ExControls.ExButton searchButton;
        private ExControls.ExButton switchToWatchButton;
        private System.Windows.Forms.ToolTip toolTip;
        private ExControls.ExButton settingsButton;
    }
}