
namespace YouTube.Forms
{
    partial class WatchForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WatchForm));
            this.videoPanel = new System.Windows.Forms.Panel();
            this.videoFrame = new YouTube.ExControls.ExWebBrowser();
            this.videoControlsPanel = new System.Windows.Forms.Panel();
            this.seekBarPanel = new YouTube.ExControls.ExPanel();
            this.seekBar = new YouTube.ExControls.ExSeekBar();
            this.fullscreenButton = new YouTube.ExControls.ExButton();
            this.muteButton = new YouTube.ExControls.ExButton();
            this.playButton = new YouTube.ExControls.ExButton();
            this.detailsPanel = new System.Windows.Forms.Panel();
            this.detailsFrame = new YouTube.ExControls.ExWebBrowser();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.switchToBrowseButton = new YouTube.ExControls.ExButton();
            this.playerSizeToggle = new YouTube.ExControls.ExButton();
            this.settingsButton = new YouTube.ExControls.ExButton();
            this.detailsSplitter = new System.Windows.Forms.Splitter();
            this.relatedPanel = new System.Windows.Forms.Panel();
            this.relatedFrame = new YouTube.ExControls.ExWebBrowser();
            this.header = new System.Windows.Forms.Panel();
            this.topLogo = new System.Windows.Forms.PictureBox();
            this.videoPanel.SuspendLayout();
            this.videoControlsPanel.SuspendLayout();
            this.seekBarPanel.SuspendLayout();
            this.detailsPanel.SuspendLayout();
            this.relatedPanel.SuspendLayout();
            this.header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.topLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // videoPanel
            // 
            this.videoPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.videoPanel.Controls.Add(this.videoFrame);
            this.videoPanel.Controls.Add(this.videoControlsPanel);
            this.videoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.videoPanel.Location = new System.Drawing.Point(0, 48);
            this.videoPanel.Name = "videoPanel";
            this.videoPanel.Size = new System.Drawing.Size(514, 318);
            this.videoPanel.TabIndex = 1;
            // 
            // videoFrame
            // 
            this.videoFrame.AllowWebBrowserDrop = false;
            this.videoFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.videoFrame.IsWebBrowserContextMenuEnabled = false;
            this.videoFrame.Location = new System.Drawing.Point(0, 0);
            this.videoFrame.MinimumSize = new System.Drawing.Size(20, 20);
            this.videoFrame.Name = "videoFrame";
            this.videoFrame.ScriptErrorsSuppressed = true;
            this.videoFrame.ScrollBarsEnabled = false;
            this.videoFrame.Size = new System.Drawing.Size(514, 288);
            this.videoFrame.TabIndex = 1;
            this.videoFrame.Visible = false;
            this.videoFrame.WebBrowserShortcutsEnabled = false;
            this.videoFrame.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.videoFrame_DocumentCompleted);
            // 
            // videoControlsPanel
            // 
            this.videoControlsPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.videoControlsPanel.Controls.Add(this.seekBarPanel);
            this.videoControlsPanel.Controls.Add(this.fullscreenButton);
            this.videoControlsPanel.Controls.Add(this.muteButton);
            this.videoControlsPanel.Controls.Add(this.playButton);
            this.videoControlsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.videoControlsPanel.Location = new System.Drawing.Point(0, 288);
            this.videoControlsPanel.Name = "videoControlsPanel";
            this.videoControlsPanel.Size = new System.Drawing.Size(514, 30);
            this.videoControlsPanel.TabIndex = 0;
            this.videoControlsPanel.MouseEnter += new System.EventHandler(this.videoControlsPanel_MouseEnter);
            this.videoControlsPanel.MouseLeave += new System.EventHandler(this.videoControlsPanel_MouseLeave);
            // 
            // seekBarPanel
            // 
            this.seekBarPanel.BackKey = "PlayerButton_Normal";
            this.seekBarPanel.BackMargins = new System.Windows.Forms.Padding(2);
            this.seekBarPanel.Controls.Add(this.seekBar);
            this.seekBarPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.seekBarPanel.Location = new System.Drawing.Point(90, 0);
            this.seekBarPanel.Name = "seekBarPanel";
            this.seekBarPanel.Size = new System.Drawing.Size(384, 30);
            this.seekBarPanel.TabIndex = 11;
            // 
            // seekBar
            // 
            this.seekBar.BackColor = System.Drawing.Color.Transparent;
            this.seekBar.BufferValue = 0;
            this.seekBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.seekBar.Enabled = false;
            this.seekBar.EnableTransparency = true;
            this.seekBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.seekBar.Location = new System.Drawing.Point(0, 0);
            this.seekBar.MaxValue = 100;
            this.seekBar.Name = "seekBar";
            this.seekBar.ShowLabels = true;
            this.seekBar.Size = new System.Drawing.Size(384, 30);
            this.seekBar.TabIndex = 0;
            this.seekBar.Text = "exSeekBar1";
            this.seekBar.Value = 0;
            this.seekBar.SeekFinished += new System.EventHandler<YouTube.ExControls.SeekEventArgs>(this.seekBar_SeekFinished);
            this.seekBar.MouseEnter += new System.EventHandler(this.videoControlsPanel_MouseEnter);
            this.seekBar.MouseLeave += new System.EventHandler(this.videoControlsPanel_MouseLeave);
            // 
            // fullscreenButton
            // 
            this.fullscreenButton.BackKey = "PlayerButton";
            this.fullscreenButton.BackMargins = new System.Windows.Forms.Padding(2);
            this.fullscreenButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.fullscreenButton.EnableTransparency = true;
            this.fullscreenButton.Icon = ((System.Drawing.Image)(resources.GetObject("fullscreenButton.Icon")));
            this.fullscreenButton.IconKey = "PlayerLarge";
            this.fullscreenButton.IconTransparencyKey = System.Drawing.Color.Magenta;
            this.fullscreenButton.Location = new System.Drawing.Point(474, 0);
            this.fullscreenButton.Name = "fullscreenButton";
            this.fullscreenButton.Size = new System.Drawing.Size(40, 30);
            this.fullscreenButton.TabIndex = 10;
            this.fullscreenButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fullscreenButton.Click += new System.EventHandler(this.fullscreenButton_Click);
            this.fullscreenButton.MouseEnter += new System.EventHandler(this.videoControlsPanel_MouseEnter);
            this.fullscreenButton.MouseLeave += new System.EventHandler(this.videoControlsPanel_MouseLeave);
            // 
            // muteButton
            // 
            this.muteButton.BackKey = "PlayerButton";
            this.muteButton.BackMargins = new System.Windows.Forms.Padding(2);
            this.muteButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.muteButton.EnableTransparency = true;
            this.muteButton.Icon = ((System.Drawing.Image)(resources.GetObject("muteButton.Icon")));
            this.muteButton.IconKey = "PlayerVolumeMax";
            this.muteButton.IconTransparencyKey = System.Drawing.Color.Magenta;
            this.muteButton.Location = new System.Drawing.Point(50, 0);
            this.muteButton.Name = "muteButton";
            this.muteButton.Size = new System.Drawing.Size(40, 30);
            this.muteButton.TabIndex = 9;
            this.muteButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.muteButton.Click += new System.EventHandler(this.muteButton_Click);
            this.muteButton.MouseEnter += new System.EventHandler(this.videoControlsPanel_MouseEnter);
            this.muteButton.MouseLeave += new System.EventHandler(this.videoControlsPanel_MouseLeave);
            // 
            // playButton
            // 
            this.playButton.BackKey = "PlayerButton";
            this.playButton.BackMargins = new System.Windows.Forms.Padding(2);
            this.playButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.playButton.EnableTransparency = true;
            this.playButton.Icon = ((System.Drawing.Image)(resources.GetObject("playButton.Icon")));
            this.playButton.IconKey = "PlayerPlay";
            this.playButton.IconTransparencyKey = System.Drawing.Color.Magenta;
            this.playButton.Location = new System.Drawing.Point(0, 0);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(50, 30);
            this.playButton.TabIndex = 8;
            this.playButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            this.playButton.MouseEnter += new System.EventHandler(this.videoControlsPanel_MouseEnter);
            this.playButton.MouseLeave += new System.EventHandler(this.videoControlsPanel_MouseLeave);
            // 
            // detailsPanel
            // 
            this.detailsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(231)))), ((int)(((byte)(231)))));
            this.detailsPanel.Controls.Add(this.detailsFrame);
            this.detailsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.detailsPanel.Location = new System.Drawing.Point(0, 369);
            this.detailsPanel.Name = "detailsPanel";
            this.detailsPanel.Size = new System.Drawing.Size(514, 112);
            this.detailsPanel.TabIndex = 2;
            // 
            // detailsFrame
            // 
            this.detailsFrame.AllowWebBrowserDrop = false;
            this.detailsFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detailsFrame.IsWebBrowserContextMenuEnabled = false;
            this.detailsFrame.Location = new System.Drawing.Point(0, 0);
            this.detailsFrame.MinimumSize = new System.Drawing.Size(20, 20);
            this.detailsFrame.Name = "detailsFrame";
            this.detailsFrame.ScriptErrorsSuppressed = true;
            this.detailsFrame.Size = new System.Drawing.Size(514, 112);
            this.detailsFrame.TabIndex = 0;
            this.detailsFrame.Visible = false;
            this.detailsFrame.WebBrowserShortcutsEnabled = false;
            // 
            // switchToBrowseButton
            // 
            this.switchToBrowseButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.switchToBrowseButton.BackColor = System.Drawing.Color.Transparent;
            this.switchToBrowseButton.BackKey = "Button";
            this.switchToBrowseButton.BackMargins = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.switchToBrowseButton.EnableTransparency = true;
            this.switchToBrowseButton.Icon = ((System.Drawing.Image)(resources.GetObject("switchToBrowseButton.Icon")));
            this.switchToBrowseButton.IconKey = "WatchToBrowse";
            this.switchToBrowseButton.IconTransparencyKey = System.Drawing.Color.Magenta;
            this.switchToBrowseButton.Location = new System.Drawing.Point(742, 10);
            this.switchToBrowseButton.Name = "switchToBrowseButton";
            this.switchToBrowseButton.Size = new System.Drawing.Size(30, 26);
            this.switchToBrowseButton.TabIndex = 4;
            this.switchToBrowseButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip.SetToolTip(this.switchToBrowseButton, "Switch to Browse");
            this.switchToBrowseButton.Click += new System.EventHandler(this.watchToBrowseButton_Click);
            // 
            // playerSizeToggle
            // 
            this.playerSizeToggle.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.playerSizeToggle.BackColor = System.Drawing.Color.Transparent;
            this.playerSizeToggle.BackKey = "Button";
            this.playerSizeToggle.BackMargins = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.playerSizeToggle.EnableTransparency = true;
            this.playerSizeToggle.Icon = ((System.Drawing.Image)(resources.GetObject("playerSizeToggle.Icon")));
            this.playerSizeToggle.IconKey = "WatchVideoLarge";
            this.playerSizeToggle.IconTransparencyKey = System.Drawing.Color.Magenta;
            this.playerSizeToggle.Location = new System.Drawing.Point(670, 10);
            this.playerSizeToggle.Name = "playerSizeToggle";
            this.playerSizeToggle.Size = new System.Drawing.Size(30, 26);
            this.playerSizeToggle.TabIndex = 3;
            this.playerSizeToggle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip.SetToolTip(this.playerSizeToggle, "Adjust Player Size");
            this.playerSizeToggle.Click += new System.EventHandler(this.playerSizeToggle_Click);
            // 
            // settingsButton
            // 
            this.settingsButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.settingsButton.BackColor = System.Drawing.Color.Transparent;
            this.settingsButton.BackKey = "Button";
            this.settingsButton.BackMargins = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.settingsButton.EnableTransparency = true;
            this.settingsButton.Icon = ((System.Drawing.Image)(resources.GetObject("settingsButton.Icon")));
            this.settingsButton.IconKey = "SettingsDark";
            this.settingsButton.IconTransparencyKey = System.Drawing.Color.Magenta;
            this.settingsButton.Location = new System.Drawing.Point(706, 10);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(30, 26);
            this.settingsButton.TabIndex = 7;
            this.settingsButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip.SetToolTip(this.settingsButton, "Settings");
            this.settingsButton.Click += new System.EventHandler(this.settingsButton_Click);
            // 
            // detailsSplitter
            // 
            this.detailsSplitter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(231)))), ((int)(((byte)(231)))));
            this.detailsSplitter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.detailsSplitter.Location = new System.Drawing.Point(0, 366);
            this.detailsSplitter.Name = "detailsSplitter";
            this.detailsSplitter.Size = new System.Drawing.Size(514, 3);
            this.detailsSplitter.TabIndex = 0;
            this.detailsSplitter.TabStop = false;
            // 
            // relatedPanel
            // 
            this.relatedPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.relatedPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.relatedPanel.Controls.Add(this.relatedFrame);
            this.relatedPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.relatedPanel.Location = new System.Drawing.Point(514, 48);
            this.relatedPanel.Name = "relatedPanel";
            this.relatedPanel.Size = new System.Drawing.Size(270, 433);
            this.relatedPanel.TabIndex = 3;
            // 
            // relatedFrame
            // 
            this.relatedFrame.AllowWebBrowserDrop = false;
            this.relatedFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.relatedFrame.IsWebBrowserContextMenuEnabled = false;
            this.relatedFrame.Location = new System.Drawing.Point(0, 0);
            this.relatedFrame.MinimumSize = new System.Drawing.Size(20, 20);
            this.relatedFrame.Name = "relatedFrame";
            this.relatedFrame.ScriptErrorsSuppressed = true;
            this.relatedFrame.Size = new System.Drawing.Size(270, 433);
            this.relatedFrame.TabIndex = 1;
            this.relatedFrame.Visible = false;
            this.relatedFrame.WebBrowserShortcutsEnabled = false;
            this.relatedFrame.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.relatedFrame_DocumentCompleted);
            // 
            // header
            // 
            this.header.BackgroundImage = global::YouTube.Properties.Resources.WatchHeader;
            this.header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.header.Controls.Add(this.settingsButton);
            this.header.Controls.Add(this.switchToBrowseButton);
            this.header.Controls.Add(this.playerSizeToggle);
            this.header.Controls.Add(this.topLogo);
            this.header.Dock = System.Windows.Forms.DockStyle.Top;
            this.header.Location = new System.Drawing.Point(0, 0);
            this.header.Name = "header";
            this.header.Size = new System.Drawing.Size(784, 48);
            this.header.TabIndex = 0;
            // 
            // topLogo
            // 
            this.topLogo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.topLogo.BackColor = System.Drawing.Color.Transparent;
            this.topLogo.Image = global::YouTube.Properties.Resources.WatchHeaderLogo;
            this.topLogo.Location = new System.Drawing.Point(12, 7);
            this.topLogo.Name = "topLogo";
            this.topLogo.Size = new System.Drawing.Size(80, 32);
            this.topLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.topLogo.TabIndex = 1;
            this.topLogo.TabStop = false;
            // 
            // WatchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.ClientSize = new System.Drawing.Size(784, 481);
            this.Controls.Add(this.videoPanel);
            this.Controls.Add(this.detailsSplitter);
            this.Controls.Add(this.detailsPanel);
            this.Controls.Add(this.relatedPanel);
            this.Controls.Add(this.header);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(640, 420);
            this.Name = "WatchForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "YouTube Video";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WatchForm_FormClosing);
            this.Load += new System.EventHandler(this.WatchForm_Load);
            this.Shown += new System.EventHandler(this.WatchForm_Shown);
            this.VisibleChanged += new System.EventHandler(this.WatchForm_VisibleChanged);
            this.videoPanel.ResumeLayout(false);
            this.videoControlsPanel.ResumeLayout(false);
            this.seekBarPanel.ResumeLayout(false);
            this.detailsPanel.ResumeLayout(false);
            this.relatedPanel.ResumeLayout(false);
            this.header.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.topLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel header;
        private System.Windows.Forms.PictureBox topLogo;
        private System.Windows.Forms.Panel videoPanel;
        private System.Windows.Forms.Panel videoControlsPanel;
        private System.Windows.Forms.Panel detailsPanel;
        private System.Windows.Forms.Panel relatedPanel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Splitter detailsSplitter;
        private ExControls.ExWebBrowser detailsFrame;
        private ExControls.ExWebBrowser relatedFrame;
        private ExControls.ExWebBrowser videoFrame;
        private ExControls.ExButton playButton;
        private ExControls.ExButton muteButton;
        private ExControls.ExButton fullscreenButton;
        private ExControls.ExPanel seekBarPanel;
        private ExControls.ExSeekBar seekBar;
        private ExControls.ExButton playerSizeToggle;
        private ExControls.ExButton switchToBrowseButton;
        private ExControls.ExButton settingsButton;
    }
}