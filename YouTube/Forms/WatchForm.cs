using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace YouTube.Forms
{
    public partial class WatchForm : Form
    {
        public WatchForm()
        {
            InitializeComponent();

            frameConn = new WebFrameConnector();
            frameConn.OnVideoClick += FrameConn_OnVideoClick;
            frameConn.OnActionClick += FrameConn_OnActionClick;
            videoFrame.ObjectForScripting = frameConn;
            detailsFrame.ObjectForScripting = frameConn;
            relatedFrame.ObjectForScripting = frameConn;

            // dummy control to capture key inputs for the player
            playerInputCapture = new Control();
            playerInputCapture.PreviewKeyDown += playerInputCapture_PreviewKeyDown;
            playerInputCapture.KeyDown += playerInputCapture_KeyDown;

            Controls.Add(playerInputCapture);

            player = new AxWMPLib.AxWindowsMediaPlayer();
            player.Dock = DockStyle.Fill;
            player.TabStop = false;
            player.PlayStateChange += Wmp_PlayStateChange;
            player.ClickEvent += Wmp_ClickEvent;
            player.StatusChange += Wmp_StatusChange;
            player.PositionChange += Wmp_PositionChange;
            player.MouseMoveEvent += Player_MouseMoveEvent;
            player.GotFocus += Player_GotFocus; // Fix for ugly focus border on player

            videoPanel.Controls.Add(player);
            player.BringToFront();
            videoFrame.BringToFront();

            playerTimer = new System.Windows.Forms.Timer();
            playerTimer.Interval = 500;
            playerTimer.Tick += PlayerTimer_Tick;
        }

        #region Variables

        private Control playerInputCapture;
        private AxWMPLib.AxWindowsMediaPlayer player;
        private System.Windows.Forms.Timer playerTimer;
        private Data.Entry video;

        private bool prevLargeLayout = true, isFullscreen = false, largePlayerControls = false, enableAutoHide = true;
        private Rectangle prevBounds;
        private FormWindowState prevWindowState;
        private FormBorderStyle prevBorderStyle;

        private static readonly int CONTROLS_AUTO_HIDE_TIMEOUT = 10; // * 500 ms = 5 seconds
        private int controlsAutoHideCounter = CONTROLS_AUTO_HIDE_TIMEOUT;

        private Data.Feed relatedFeed;

        private WebFrameConnector frameConn;

        #endregion

        #region Data Loading

        public void LoadVideo(string videoId)
        {
            video = DataApi.GetVideo(videoId);
            ShowVideoData();
        }

        public void LoadVideo(Data.Entry video)
        {
            this.video = video;
            ShowVideoData();
        }

        private void ShowVideoData()
        {
            if (video == null) return;

            if (!Visible)
            {
                Program.SwitchView();
            }

            Text = video.Title + " - YouTube";
            seekBar.Value = seekBar.BufferValue = 0;
            seekBar.MaxValue = video.Media.Duration.Seconds;

            var description = new Dictionary<string, string>
            {
                { "title", video.Title },
                { "videoId", video.YouTubeId.Id },
                { "description", video.Content },
                { "upload_date", Utils.FormatRelativeDate(video.Published) },
                { "viewcount", video.Statistics.ViewCount.ToString("N0") },
                { "author", video.Author.Name },
                { "thumbnail", "http://i.ytimg.com/vi/" + video.YouTubeId.Id + "/default.jpg" },
                { "thumbnailHQ", "http://i.ytimg.com/vi/" + video.YouTubeId.Id + "/hqdefault.jpg" },
            };

            Utils.LoadTemplate("videoCover", description, videoFrame);
            Utils.LoadTemplate("videoDescription", description, detailsFrame);
            Utils.LoadTemplate("videoRelated", new Dictionary<string, string>(), relatedFrame);

            if (player != null) player.URL = DataApi.GetVideoUrl(video.YouTubeId.Id);
        }

        private void LoadRelated()
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                try
                {
                    var feed = DataApi.GetRelatedVideos(video.YouTubeId.Id);

                    Invoke(new MethodInvoker(delegate
                    {
                        ShowRelatedFeed(feed);
                    }));
                }
                catch (Exception ex)
                {
                    Invoke(new MethodInvoker(delegate
                    {
                        ShowRelatedError("Error: " + ex.Message);
                    }));
                }
            });
        }

        private void ShowRelatedFeed(Data.Feed feed)
        {
            relatedFeed = feed;
            var entriesHtml = new StringBuilder();

            for (int i=0; i < feed.Entries.Count; i++)
            {
                var entry = feed.Entries[i];

                entriesHtml.AppendLine(Utils.CreateTemplateDoc("videoItem", new Dictionary<string, string>
                {
                    { "index", i.ToString() },
                    { "title", entry.Title },
                    { "videoId", entry.YouTubeId.Id },
                    { "author", entry.Author.Name },
                    { "duration", Utils.FormatDurationSeconds(entry.Media.Duration.Seconds) },
                    { "upload_date", Utils.FormatRelativeDate(entry.Published) },
                    { "viewcount", entry.Statistics.ViewCount.ToString("N0") },
                    { "thumbnail", "http://i.ytimg.com/vi/" + entry.YouTubeId.Id + "/default.jpg" }
                }));
            }

            relatedFrame.Document.GetElementById("loading").SetAttribute("className", "hidden");
            relatedFrame.Document.GetElementById("relatedItems").InnerHtml = entriesHtml.ToString();
        }

        private void ShowRelatedError(string message)
        {
            var loadingEl = relatedFrame.Document.GetElementById("loading");
            loadingEl.SetAttribute("className", "hidden");
            var relatedEl = relatedFrame.Document.GetElementById("relatedItems");
            relatedEl.InnerText = message;
        }

        #endregion

        #region View Layout

        private bool LargeVideoLayout
        {
            get { return detailsPanel.Visible; }
            set
            {
                detailsPanel.Visible = detailsSplitter.Visible = relatedPanel.Visible = value;
            }
        }

        private bool LargePlayerControls
        {
            get { return largePlayerControls; }
            set
            {
                if (largePlayerControls != value)
                {
                    largePlayerControls = value;

                    videoControlsPanel.Height = (int)(value ? videoControlsPanel.Height * 1.5 : videoControlsPanel.Height / 1.5);

                    foreach (Control c in videoControlsPanel.Controls)
                    {
                        var btn = c as ExControls.ExButton;
                        if (btn != null)
                        {
                            btn.Width = (int)(value ? btn.Width * 1.5 : btn.Width / 1.5);
                        }
                    }
                }
            }
        }


        private void SetFullScreen(bool value)
        {
            SuspendLayout();

            if (value)
            {
                prevLargeLayout = LargeVideoLayout;
                prevWindowState = WindowState;
                prevBorderStyle = FormBorderStyle;

                prevBounds = WindowState == FormWindowState.Normal
                    ? DesktopBounds
                    : RestoreBounds;

                LargeVideoLayout = false;

                if (WindowState != FormWindowState.Normal)
                    WindowState = FormWindowState.Normal;

                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;

                player.SendToBack();
                playerInputCapture.Focus();
            }
            else
            {
                WindowState = FormWindowState.Normal;
                FormBorderStyle = prevBorderStyle;

                if (prevWindowState == FormWindowState.Normal)
                    DesktopBounds = prevBounds;

                WindowState = prevWindowState;

                LargeVideoLayout = prevLargeLayout;

                ShowPlayerControls();

                player.BringToFront();
            }

            header.Visible = !value;
            isFullscreen = value;
            fullscreenButton.IconKey = value ? "PlayerNormal" : "PlayerLarge";

            if (Settings.Default.LargePlayerControls)
                LargePlayerControls = value;

            ResumeLayout();
        }

        private void ShowCoverScreen(string screenId)
        {
            videoFrame.Document.GetElementById("startScreen").SetAttribute("className", "hidden");
            videoFrame.Document.GetElementById("loadingScreen").SetAttribute("className", "hidden");
            videoFrame.Document.GetElementById("endScreen").SetAttribute("className", "hidden");

            videoFrame.Document.GetElementById(screenId).SetAttribute("className", "overlay");
            videoFrame.Visible = true;
            player.Visible = false;
        }

        private void ShowVideoPlayer()
        {
            videoFrame.Visible = false;
            player.Visible = true;
        }

        #endregion

        #region Video Player

        private void InitVideoPlayer()
        {
            player.uiMode = "none";
            player.stretchToFit = true;
            player.enableContextMenu = false;
            player.settings.enableErrorDialogs = false;
            player.settings.autoStart = false;
            player.settings.volume = 100;
        }

        private DateTime suppressMouseUntil = DateTime.MinValue;

        private void PlayerTimer_Tick(object sender, EventArgs e)
        {
            seekBar.Value = (int)player.Ctlcontrols.currentPosition;
            if (isFullscreen && enableAutoHide)
            {
                controlsAutoHideCounter--;
                if (controlsAutoHideCounter <= 0 && videoControlsPanel.Visible)
                {
                    videoControlsPanel.Visible = false;
                    Cursor.Hide();
                    suppressMouseUntil = DateTime.UtcNow.AddMilliseconds(300);
                }
            }
        }

        private void Player_MouseMoveEvent(object sender, AxWMPLib._WMPOCXEvents_MouseMoveEvent e)
        {
            if (!isFullscreen || !enableAutoHide) return;

            if (DateTime.UtcNow < suppressMouseUntil)
                return;

            ShowPlayerControls();
        }

        private void ShowPlayerControls()
        {
            controlsAutoHideCounter = CONTROLS_AUTO_HIDE_TIMEOUT;
            if (!videoControlsPanel.Visible)
            {
                videoControlsPanel.Visible = true;
                Cursor.Show();
            }
        }

        private void Player_GotFocus(object sender, EventArgs e)
        {
            // Fix for ugly focus border on player
            playerInputCapture.Focus();
        }

        private void Wmp_PositionChange(object sender, AxWMPLib._WMPOCXEvents_PositionChangeEvent e)
        {
            seekBar.Value = (int)e.newPosition;
        }

        private void Wmp_StatusChange(object sender, EventArgs e)
        {
        }

        private void Wmp_ClickEvent(object sender, AxWMPLib._WMPOCXEvents_ClickEvent e)
        {
            if ((e.nButton & 1) == 1)
                PlayPause();
        }

        private void Wmp_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            UpdatePlayerControls();

            switch (player.playState)
            {
                case WMPLib.WMPPlayState.wmppsTransitioning:
                    ShowCoverScreen("loadingScreen");
                    break;

                case WMPLib.WMPPlayState.wmppsPlaying:
                    ShowVideoPlayer();

                    playerTimer.Start();
                    seekBar.Enabled = true;
                    seekBar.MaxValue = (int)player.currentMedia.duration;
                    seekBar.Value = (int)player.Ctlcontrols.currentPosition;
                    break;

                case WMPLib.WMPPlayState.wmppsPaused:
                    playerTimer.Stop();
                    break;

                case WMPLib.WMPPlayState.wmppsStopped:
                    ShowCoverScreen("endScreen");
                    playerTimer.Stop();
                    break;
            }
        }
        
        private void PlayPause()
        {
            if (player == null) return;

            if (player.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                player.Ctlcontrols.pause();
            }
            else
            {
                player.Ctlcontrols.play();
            }
        }

        #endregion

        #region Video Controls

        private void UpdatePlayerControls()
        {
            SuspendLayout();

            playButton.IconKey = player.playState == WMPLib.WMPPlayState.wmppsPlaying ? "PlayerPause" : "PlayerPlay";

            muteButton.IconKey = player.settings.mute ? "PlayerMute" : "PlayerVolumeMax";

            ResumeLayout();
        }

        private void WatchForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (player != null)
            {
                player.Ctlcontrols.stop();
                videoPanel.Controls.Remove(player);
            }
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            PlayPause();
        }

        private void muteButton_Click(object sender, EventArgs e)
        {
            if (player == null) return;

            player.settings.mute = !player.settings.mute;

            UpdatePlayerControls();
        }

        private void fullscreenButton_Click(object sender, EventArgs e)
        {
            SetFullScreen(!isFullscreen);
        }

        #endregion

        #region Events

        private void WatchForm_Load(object sender, EventArgs e)
        {
            InitVideoPlayer();
        }

        private void seekBar_SeekFinished(object sender, ExControls.SeekEventArgs e)
        {
            if (player == null) return;

            player.Ctlcontrols.currentPosition = e.Position;
        }

        private void WatchForm_Shown(object sender, EventArgs e)
        {
            playerInputCapture.Focus();
        }

        private void playerSizeToggle_Click(object sender, EventArgs e)
        {
            LargeVideoLayout = !LargeVideoLayout;

            playerSizeToggle.IconKey = LargeVideoLayout ? "WatchVideoLarge" : "WatchVideoNormal";
        }

        private void relatedFrame_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            // Load related feed for initial video
            if (video != null)
            {
                LoadRelated();
            }
        }

        private void videoControlsPanel_MouseEnter(object sender, EventArgs e)
        {
            if (!isFullscreen) return;

            enableAutoHide = false;
        }

        private void videoControlsPanel_MouseLeave(object sender, EventArgs e)
        {
            if (!isFullscreen) return;

            enableAutoHide = true;
            controlsAutoHideCounter = CONTROLS_AUTO_HIDE_TIMEOUT;
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            new SettingsDialog().ShowDialog();
        }

        private void WatchForm_VisibleChanged(object sender, EventArgs e)
        {
            if (!Visible)
            {
                player.Ctlcontrols.pause();
            }
        }

        private void playerInputCapture_KeyDown(object sender, KeyEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("key press: " + e.KeyCode + " " + e.KeyValue);

            ShowPlayerControls();

            if (48 <= e.KeyValue && e.KeyValue <= 57) // '0' - '9'
            {
                var number = e.KeyValue - 48;
                player.Ctlcontrols.currentPosition = player.currentMedia.duration * (number / 10.0);
                return;
            }

            switch (e.KeyCode)
            {
                case Keys.Space:
                case Keys.K:
                    PlayPause();
                    break;

                case Keys.Escape:
                    if (isFullscreen)
                        SetFullScreen(false);
                    break;

                case Keys.Left:
                case Keys.J:
                    player.Ctlcontrols.currentPosition -= e.Shift ? 1 : 10;
                    break;

                case Keys.Right:
                case Keys.L:
                    player.Ctlcontrols.currentPosition += e.Shift ? 1 : 10;
                    break;

                case Keys.F11:
                case Keys.F:
                case Keys.Enter: 
                    if (e.KeyCode == Keys.Enter && !e.Alt) break;
                    SetFullScreen(!isFullscreen);
                    break;

                case Keys.M:
                    player.settings.mute = !player.settings.mute;
                    UpdatePlayerControls();
                    break;

                case Keys.Up:
                    player.settings.volume = Math.Min(100, player.settings.volume + 10);
                    break;

                case Keys.Down:
                    player.settings.volume = Math.Max(0, player.settings.volume - 10);
                    break;
            }
        }

        private void playerInputCapture_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true; // allow direction keys to raise KeyDown events
        }

        private void watchToBrowseButton_Click(object sender, EventArgs e)
        {
            Program.SwitchView();
        }

        private void FrameConn_OnActionClick(object sender, ActionClickEventArgs e)
        {
            switch (e.Data)
            {
                case "play":
                    player.Ctlcontrols.play();
                    break;

                case "replay":
                    player.Ctlcontrols.play();
                    break;

                case "author":
                    break;
            }
            return;
        }

        private void FrameConn_OnVideoClick(object sender, VideoClickEventArgs e)
        {
            if (relatedFeed == null || e.Index >= relatedFeed.Entries.Count) return;

            var video = relatedFeed.Entries[e.Index];

            if (e.Name == "title")
            {
                LoadVideo(video);
            }
        }

        #endregion

    }
}
