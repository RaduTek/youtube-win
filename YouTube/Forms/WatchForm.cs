using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
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
            detailsFrame.DocumentCompleted += detailsFrame_DocumentCompleted;
            relatedFrame.ObjectForScripting = frameConn;

            PlaybackQueue.Changed += PlaybackQueue_Changed;
            DownloadManager.Changed += DownloadManager_Changed;

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
            playerTimer.Interval = 250;
            playerTimer.Tick += PlayerTimer_Tick;
        }

        #region Variables

        private Control playerInputCapture;
        private AxWMPLib.AxWindowsMediaPlayer player;
        private System.Windows.Forms.Timer playerTimer;
        private Data.Entry video;

        private bool prevLargeLayout = true, isFullscreen = false, largePlayerControls = false, enableAutoHide = false, pauseAutoHide = false;
        private Rectangle prevBounds;
        private FormWindowState prevWindowState;
        private FormBorderStyle prevBorderStyle;

        private static readonly int CONTROLS_AUTO_HIDE_TIMEOUT = 10; // * 500 ms = 5 seconds
        private int controlsAutoHideCounter = CONTROLS_AUTO_HIDE_TIMEOUT;

        private Data.Feed relatedFeed;
        private Dictionary<string, string> detailsValues;
        private string commentsVideoId; // video whose comments were requested

        private bool sidebarLoaded, sidebarReady; // sidebar template written / document loaded
        private Data.Entry pendingVideo; // queued before the Watch window was ever shown, loaded on first show
        private bool historyPending; // preloaded video, added to history once it starts playing
        private bool advancingQueue; // moving on to the next queued video, skip the end screen
        private bool waitingForDownload; // download before play: the video is set once its download finishes
        private bool autoPlayWhenReady; // start playing when the download finishes

        private WebFrameConnector frameConn;

        #endregion

        #region Data Loading

        public string InitialVideoId { get; set; }

        // the Watch window is only reachable once there is a video to show
        public bool HasVideo
        {
            get { return video != null || pendingVideo != null; }
        }

        public void LoadVideo(string videoId)
        {
            pendingVideo = null;
            video = DataApi.GetVideo(videoId);
            ShowVideoData(true, false);
        }

        public void LoadVideo(Data.Entry video)
        {
            pendingVideo = null;
            this.video = video;
            ShowVideoData(false, false);
        }

        public void AddToQueue(Data.Entry entry)
        {
            PlaybackQueue.Add(entry);

            // nothing loaded since app start: make it the current video, without switching view or playing
            if (!HasVideo)
            {
                pendingVideo = entry;
                Program.browseForm.EnableWatchButton = true;
            }
        }

        // Thumbnail queue button: adds the video, or removes it if already queued
        public void ToggleQueued(Data.Entry entry)
        {
            var index = PlaybackQueue.IndexOf(entry.YouTubeId.Id);

            if (index >= 0)
                PlaybackQueue.Remove(index);
            else
                AddToQueue(entry);
        }

        // Thumbnail play button: starts a new queue with this video
        public void PlayInNewQueue(Data.Entry entry)
        {
            if (ShowIfLoaded(entry)) return;

            PlaybackQueue.Clear();
            PlayQueueItem(PlaybackQueue.Add(entry));
        }

        // The video is already loaded (or waiting to be shown): switch to it instead of reloading it
        private bool ShowIfLoaded(Data.Entry entry)
        {
            var current = video ?? pendingVideo;
            if (current == null || current.YouTubeId.Id != entry.YouTubeId.Id) return false;

            if (!Visible) Program.SwitchView();
            return true;
        }

        private void PlayQueueItem(int index)
        {
            if (index < 0 || index >= PlaybackQueue.Items.Count) return;

            if (ShowIfLoaded(PlaybackQueue.Items[index])) return;

            LoadVideo(PlaybackQueue.Items[index]);

            // queue playback starts regardless of the auto play setting
            if (waitingForDownload)
                autoPlayWhenReady = true;
            else
                player.Ctlcontrols.play();
        }

        // detailed: the entry was already loaded from the video API, no need to fetch it again
        // preload: load in the background, without switching to this window or starting playback
        private void ShowVideoData(bool detailed, bool preload)
        {
            if (video == null) return;

            Program.browseForm.EnableWatchButton = true;
            playerInputCapture.Focus();

            if (!Visible && !preload)
            {
                Program.SwitchView();
            }

            Text = video.Title + " - YouTube";
            seekBar.Enabled = false;
            seekBar.Value = seekBar.BufferValue = 0;
            seekBar.MaxValue = video.Media.Duration.Seconds;

            var values = BuildVideoValues(video);

            var downloadFirst = Settings.Default.DownloadBeforePlay && DownloadManager.GetLocalPath(video.YouTubeId.Id) == null;

            if (downloadFirst)
            {
                // show download progress until the video can be played from the local file
                values["startScreenClass"] = "hidden";
                values["loadingScreenClass"] = "hidden";
                values["downloadScreenClass"] = "overlay";
            }
            else if (preload)
            {
                // show the play button instead of the loading indicator
                values["startScreenClass"] = "overlay";
                values["loadingScreenClass"] = "hidden";
            }

            // the details page is reloaded, so comments need to be loaded again
            commentsVideoId = null;

            // details content is filled in once the full video entry is loaded
            detailsValues = detailed ? values : null;

            Utils.LoadTemplate("videoCover", values, videoFrame);
            Utils.LoadTemplate("videoDetails", values, detailsFrame);
            ShowSidebar();

            if (player != null) StartPlayback(!preload && Settings.Default.AutoPlayVideo);

            historyPending = preload;
            if (!preload) LocalFeed.History.Add(video);

            PlaybackQueue.SetCurrent(video.YouTubeId.Id);

            if (!detailed) LoadFullDetails(video);
        }

        // Plays the downloaded file if there is one, otherwise downloads it first (if enabled) or streams it
        private void StartPlayback(bool autoPlay)
        {
            var videoId = video.YouTubeId.Id;
            var localPath = DownloadManager.GetLocalPath(videoId);

            waitingForDownload = false;
            autoPlayWhenReady = autoPlay;

            if (localPath != null)
            {
                SetPlayerUrl(localPath, autoPlay);
            }
            else if (Settings.Default.DownloadBeforePlay)
            {
                // set before clearing the player, so the resulting Stopped state doesn't show the end screen
                waitingForDownload = true;
                player.URL = "";

                var item = DownloadManager.Enqueue(video, true);
                if (item != null) ShowDownloadProgress(item);
            }
            else
            {
                SetPlayerUrl(DataApi.GetVideoUrl(videoId), autoPlay);
            }
        }

        private void SetPlayerUrl(string url, bool autoPlay)
        {
            player.settings.autoStart = false;
            player.URL = url;
            player.settings.autoStart = Settings.Default.AutoPlayVideo;

            if (autoPlay) player.Ctlcontrols.play();
        }

        // Download progress on the cover screen, while waiting to play a video that's being downloaded
        private void ShowDownloadProgress(DownloadItem item)
        {
            var textEl = videoFrame.Document?.GetElementById("downloadText");
            if (textEl == null) return;

            switch (item.Status)
            {
                case DownloadStatus.Waiting:
                    textEl.InnerText = "Waiting for other downloads...";
                    break;

                case DownloadStatus.Downloading:
                    textEl.InnerText = "Downloading... " + item.Progress + "%";
                    break;

                case DownloadStatus.Cancelled:
                case DownloadStatus.Error:
                    textEl.InnerHtml = Utils.HtmlText(item.Status == DownloadStatus.Cancelled ? "Download cancelled." : "Download failed: " + item.Error)
                        + " <a href=\"javascript:window.external.ActionClick('videoCover', 'retryDownload')\">Retry</a>";
                    break;
            }
        }

        private void GetDownloadButtonState(string videoId, out string cssClass, out string text, out string title)
        {
            var item = DownloadManager.Find(videoId);

            if (DownloadManager.GetLocalPath(videoId) != null)
            {
                cssClass = "selected";
                text = "Downloaded";
                title = "Show the downloaded file";
            }
            else if (item != null && (item.Status == DownloadStatus.Waiting || item.Status == DownloadStatus.Downloading))
            {
                cssClass = "";
                text = item.Status == DownloadStatus.Waiting ? "Waiting" : item.Progress + "%";
                title = "Cancel download";
            }
            else
            {
                cssClass = "";
                text = "Download";
                title = "Download this video";
            }
        }

        private void UpdateDownloadButton()
        {
            var button = detailsFrame.Document?.GetElementById("downloadBtn");
            if (button == null || video == null) return;

            string cssClass, text, title;
            GetDownloadButtonState(video.YouTubeId.Id, out cssClass, out text, out title);

            button.SetAttribute("className", cssClass);
            button.SetAttribute("title", title);
            detailsFrame.Document.GetElementById("downloadBtnText").InnerText = text;
        }

        // Download button: download, cancel, or show the downloaded file
        private void DownloadButtonClick()
        {
            if (video == null) return;

            var videoId = video.YouTubeId.Id;
            var item = DownloadManager.Find(videoId);

            if (DownloadManager.GetLocalPath(videoId) != null)
                DownloadManager.OpenFileLocation(videoId);
            else if (item != null && (item.Status == DownloadStatus.Waiting || item.Status == DownloadStatus.Downloading))
                DownloadManager.Cancel(videoId);
            else
                DownloadManager.Enqueue(video, false);
        }

        private void DownloadManager_Changed(object sender, DownloadEventArgs e)
        {
            Utils.UpdateDownloadsButton(downloadsButton, toolTip);

            if (video == null || e.VideoId != video.YouTubeId.Id) return;

            UpdateDownloadButton();

            if (!waitingForDownload) return;

            if (e.Item.Status == DownloadStatus.Downloaded)
            {
                waitingForDownload = false;
                ShowCoverScreen(autoPlayWhenReady ? "loadingScreen" : "startScreen");
                SetPlayerUrl(DownloadManager.GetLocalPath(e.VideoId), autoPlayWhenReady);
            }
            else
            {
                ShowDownloadProgress(e.Item);
            }
        }

        private Dictionary<string, string> BuildVideoValues(Data.Entry video)
        {
            string downloadClass, downloadText, downloadTitle;
            GetDownloadButtonState(video.YouTubeId.Id, out downloadClass, out downloadText, out downloadTitle);

            var likes = video.Rating?.Likes ?? 0;
            var dislikes = video.Rating?.Dislikes ?? 0;
            var ratingTotal = likes + dislikes;

            // leave a small gap between the bars; no ratings -> empty bars
            var ratingP = ratingTotal > 0 ? Math.Max(0, likes / ratingTotal * 100 - 0.1) : 0;
            var ratingN = ratingTotal > 0 ? Math.Max(0, dislikes / ratingTotal * 100 - 0.1) : 0;

            return new Dictionary<string, string>
            {
                { "title", video.Title },
                { "videoId", video.YouTubeId.Id },
                { "description", (video.Content ?? "").Replace("\r\n", "\n").Replace("\n", "<br />") },
                { "upload_date", video.Published.ToLongDateString() },
                { "viewcount", (video.Statistics?.ViewCount ?? 0).ToString("N0") },
                { "author", video.Author.Name },
                { "thumbnail", Utils.GetThumbnailUrl(video.YouTubeId.Id, false) },
                { "thumbnailHQ", Utils.GetThumbnailUrl(video.YouTubeId.Id, true) },
                { "likes", likes.ToString("N0") },
                { "dislikes", dislikes.ToString("N0") },
                { "ratingP", ratingP.ToString("0.###") + "%" },
                { "ratingN", ratingN.ToString("0.###") + "%" },
                { "favoriteClass", LocalFeed.Favorites.Contains(video.YouTubeId.Id) ? "selected" : "" },
                { "downloadClass", downloadClass },
                { "downloadText", downloadText },
                { "downloadTitle", downloadTitle },

                // cover screen only - show loading indicator first when auto play is enabled
                { "startScreenClass", Settings.Default.AutoPlayVideo ? "hidden" : "overlay" },
                { "loadingScreenClass", Settings.Default.AutoPlayVideo ? "overlay" : "hidden" },
                { "downloadScreenClass", "hidden" },
            };
        }

        // Search and related results carry only partial data, so fetch the full entry
        private void LoadFullDetails(Data.Entry partial)
        {
            var videoId = partial.YouTubeId.Id;

            ThreadPool.QueueUserWorkItem(delegate
            {
                Data.Entry full = null;

                try
                {
                    full = DataApi.GetVideo(videoId);
                }
                catch (Exception)
                {
                    // fall back to the data we already have
                }

                Invoke(new MethodInvoker(delegate
                {
                    // another video was selected while loading
                    if (video == null || video.YouTubeId.Id != videoId) return;

                    if (full != null)
                    {
                        video = full;

                        // replace the partial history entry with the full metadata
                        if (LocalFeed.History.Contains(videoId))
                            LocalFeed.History.Add(video);
                    }

                    detailsValues = BuildVideoValues(video);
                    ShowVideoDetails();
                }));
            });
        }

        private void ShowVideoDetails()
        {
            if (detailsValues == null) return;

            var slot = detailsFrame.Document?.GetElementById("detailsContent");
            if (slot == null) return;

            Utils.LoadTemplateSlot("videoDetailsContent", detailsValues, "detailsContent", detailsFrame);

            // stop showing the loading indicator once content is in place
            slot.SetAttribute("className", slot.GetAttribute("className").Replace("loading-32w", "").Trim());
        }

        private void LoadComments()
        {
            if (video == null) return;

            var videoId = video.YouTubeId.Id;

            // already loaded (or loading) for this video
            if (commentsVideoId == videoId) return;

            var slot = detailsFrame.Document?.GetElementById("commentsContent");
            if (slot == null) return;

            commentsVideoId = videoId;
            slot.InnerHtml = "";
            slot.SetAttribute("className", "loading-32w");

            ThreadPool.QueueUserWorkItem(delegate
            {
                try
                {
                    var feed = DataApi.GetVideoComments(videoId);

                    Invoke(new MethodInvoker(delegate
                    {
                        if (commentsVideoId != videoId) return;

                        ShowComments(feed);
                    }));
                }
                catch (Exception)
                {
                    Invoke(new MethodInvoker(delegate
                    {
                        if (commentsVideoId != videoId) return;

                        // allow retrying by reopening the tab
                        commentsVideoId = null;
                        ShowCommentsMessage("Comments couldn't be loaded. Check your network connection and open the tab again to retry.");
                    }));
                }
            });
        }

        private void ShowComments(Data.Feed feed)
        {
            if (feed.Entries == null || feed.Entries.Count == 0)
            {
                ShowCommentsMessage("No comments.");
                return;
            }

            var valuesList = new List<Dictionary<string, string>>();

            foreach (var comment in feed.Entries)
            {
                valuesList.Add(new Dictionary<string, string>
                {
                    { "content", Utils.HtmlText(comment.Content) },
                    { "author", Utils.HtmlText(comment.Author?.Name) },
                    { "likes", (comment.Rating?.Likes ?? 0).ToString("N0") },
                    { "upload_date", Utils.FormatRelativeDate(comment.Published) }
                });
            }

            var slot = detailsFrame.Document?.GetElementById("commentsContent");
            if (slot == null) return;

            Utils.LoadTemplateListSlot("videoComment", valuesList, "commentsContent", detailsFrame);
            slot.SetAttribute("className", slot.GetAttribute("className").Replace("loading-32w", "").Trim());
        }

        private void ShowCommentsMessage(string message)
        {
            var slot = detailsFrame.Document?.GetElementById("commentsContent");
            if (slot == null) return;

            slot.SetAttribute("className", slot.GetAttribute("className").Replace("loading-32w", "").Trim());
            slot.InnerText = message;
        }

        // The sidebar page is loaded once and kept, so the selected tab and the queue list survive video changes
        private void ShowSidebar()
        {
            if (!sidebarLoaded)
            {
                sidebarLoaded = true;
                Utils.LoadTemplate("videoRelated", new Dictionary<string, string>(), relatedFrame);
                return; // related videos are loaded in relatedFrame_DocumentCompleted
            }

            if (!sidebarReady) return;

            relatedFeed = null;
            relatedFrame.Document.GetElementById("relatedItems").InnerHtml = "";
            relatedFrame.Document.GetElementById("loading").SetAttribute("className", "");
            LoadRelated();
        }

        private void LoadRelated()
        {
            var videoId = video.YouTubeId.Id;

            ThreadPool.QueueUserWorkItem(delegate
            {
                try
                {
                    var feed = DataApi.GetRelatedVideos(videoId);

                    Invoke(new MethodInvoker(delegate
                    {
                        // another video was selected while loading
                        if (video == null || video.YouTubeId.Id != videoId) return;

                        ShowRelatedFeed(feed);
                    }));
                }
                catch (Exception)
                {
                    Invoke(new MethodInvoker(delegate
                    {
                        if (video == null || video.YouTubeId.Id != videoId) return;

                        ShowRelatedError("Related videos aren't available right now. Check your network connection.");
                    }));
                }
            });
        }

        private void RenderQueue()
        {
            if (!sidebarReady) return;

            var valuesList = new List<Dictionary<string, string>>();

            for (int i = 0; i < PlaybackQueue.Items.Count; i++)
            {
                var entry = PlaybackQueue.Items[i];
                valuesList.Add(new Dictionary<string, string>
                {
                    { "index", i.ToString() },
                    { "number", (i + 1).ToString() },
                    { "title", entry.Title },
                    { "author", entry.Author.Name },
                    { "duration", Utils.FormatDurationSeconds(entry.Media.Duration.Seconds) },
                    { "thumbnail", Utils.GetThumbnailUrl(entry.YouTubeId.Id, false) },
                    { "currentClass", i == PlaybackQueue.CurrentIndex ? "current" : "" }
                });
            }

            Utils.LoadTemplateListSlot("videoQueueItem", valuesList, "queueItems", relatedFrame);

            var count = PlaybackQueue.Items.Count;
            relatedFrame.Document.GetElementById("queueTab").InnerText = count > 0 ? "Queue (" + count + ")" : "Queue";
            relatedFrame.Document.GetElementById("queueEmpty").SetAttribute("className", count > 0 ? "hidden" : "");
        }

        private void ShowRelatedFeed(Data.Feed feed)
        {
            relatedFeed = feed;

            var valuesList = new List<Dictionary<string, string>>();

            for (int i = 0; i < feed.Entries.Count; i++)
            {
                var entry = feed.Entries[i];
                valuesList.Add(new Dictionary<string, string>
                {
                    { "index", i.ToString() },
                    { "title", entry.Title },
                    { "videoId", entry.YouTubeId.Id },
                    { "author", entry.Author.Name },
                    { "duration", Utils.FormatDurationSeconds(entry.Media.Duration.Seconds) },
                    { "upload_date", Utils.FormatRelativeDate(entry.Published) },
                    { "viewcount", entry.Statistics.ViewCount.ToString("N0") },
                    { "thumbnail", Utils.GetThumbnailUrl(entry.YouTubeId.Id, false) }
                });
            }

            Utils.LoadTemplateListSlot("videoItem", valuesList, "relatedItems", relatedFrame);
            relatedFrame.Document.GetElementById("loading").SetAttribute("className", "hidden");
            //relatedFrame.Document.GetElementById("relatedItems").InnerHtml = entriesHtml.ToString();
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
            get { return !detailsPanel.Visible; }
            set
            {
                playerSizeToggle.IconKey = value ? "WatchVideoNormal" : "WatchVideoLarge";
                detailsPanel.Visible = relatedPanel.Visible = !value;
                enableAutoHide = value;

                if (value)
                {
                    header.BringToFront();
                    videoControlsPanel.BringToFront();
                    playerInputCapture.Focus();
                }
                else
                {
                    header.SendToBack();
                    videoControlsPanel.SendToBack();
                    AdjustPanelSizes();
                }
            }
        }

        private void SetLargePlayerControls(bool value)
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

                LargeVideoLayout = true;

                if (WindowState != FormWindowState.Normal)
                    WindowState = FormWindowState.Normal;

                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
                FormBorderStyle = prevBorderStyle;

                if (prevWindowState == FormWindowState.Normal)
                    DesktopBounds = prevBounds;

                WindowState = prevWindowState;

                ShowPlayerControls();

                LargeVideoLayout = prevLargeLayout;

                AdjustPanelSizes();
            }

            header.Visible = !value;
            isFullscreen = value;
            fullscreenButton.IconKey = value ? "PlayerNormal" : "PlayerLarge";

            if (Settings.Default.LargePlayerControls)
                SetLargePlayerControls(value);

            ResumeLayout();
        }

        private void ShowCoverScreen(string screenId)
        {
            videoFrame.Document.GetElementById("startScreen")?.SetAttribute("className", "hidden");
            videoFrame.Document.GetElementById("loadingScreen")?.SetAttribute("className", "hidden");
            videoFrame.Document.GetElementById("endScreen")?.SetAttribute("className", "hidden");
            videoFrame.Document.GetElementById("helpScreen")?.SetAttribute("className", "hidden");
            videoFrame.Document.GetElementById("downloadScreen")?.SetAttribute("className", "hidden");
            videoFrame.Document.GetElementById(screenId)?.SetAttribute("className", "overlay");
            
            ShowPlayerControls();
            videoFrame.Visible = true;
        }

        private void ShowVideoPlayer()
        {
            videoFrame.Visible = false;
        }

        #endregion

        #region Video Player

        private void InitVideoPlayer()
        {
            player.uiMode = "none";
            player.stretchToFit = true;
            player.enableContextMenu = false;
            player.settings.enableErrorDialogs = false;
            player.settings.autoStart = Settings.Default.AutoPlayVideo;
            player.settings.volume = 100;
        }

        private DateTime suppressMouseUntil = DateTime.MinValue;

        private void PlayerTimer_Tick(object sender, EventArgs e)
        {
            seekBar.Value = player.Ctlcontrols.currentPosition;
            if (enableAutoHide)
            {
                controlsAutoHideCounter--;
                if (controlsAutoHideCounter <= 0 && videoControlsPanel.Visible && !pauseAutoHide)
                {
                    HidePlayerControls();
                    suppressMouseUntil = DateTime.UtcNow.AddMilliseconds(300);
                }
            }
        }

        private void Player_MouseMoveEvent(object sender, AxWMPLib._WMPOCXEvents_MouseMoveEvent e)
        {
            if (!enableAutoHide) return;

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
                if (!isFullscreen) header.Visible = true;
                Cursor.Show();
            }
        }

        private void HidePlayerControls()
        {
            videoControlsPanel.Visible = false;
            if (!isFullscreen) header.Visible = false;
            Cursor.Hide();
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
            //System.Diagnostics.Debug.WriteLine("Play state changed: " + player.playState);
            
            UpdatePlayerControls();

            switch (player.playState)
            {
                //case WMPLib.WMPPlayState.wmppsBuffering:
                case WMPLib.WMPPlayState.wmppsTransitioning:
                    advancingQueue = false;
                    ShowCoverScreen("loadingScreen");
                    break;

                case WMPLib.WMPPlayState.wmppsPlaying:
                    advancingQueue = false;
                    ShowVideoPlayer();

                    if (historyPending)
                    {
                        historyPending = false;
                        LocalFeed.History.Add(video);
                    }

                    playerTimer.Start();
                    if (!seekBar.Enabled)
                    {
                        seekBar.Enabled = true;
                        seekBar.MaxValue = player.currentMedia.duration;
                        seekBar.Value = player.Ctlcontrols.currentPosition;
                    }
                    break;

                case WMPLib.WMPPlayState.wmppsPaused:
                    playerTimer.Stop();
                    break;

                case WMPLib.WMPPlayState.wmppsMediaEnded:
                    // only fires when playback reaches the end (unlike Stopped, which also follows
                    // a user stop, URL change or error), so this is where the queue advances
                    var next = PlaybackQueue.GetNextIndex();
                    if (next >= 0)
                    {
                        advancingQueue = true;

                        // WMP doesn't support changing the media from within PlayStateChange
                        var entry = PlaybackQueue.Items[next];
                        BeginInvoke(new MethodInvoker(delegate
                        {
                            PlayQueueItem(PlaybackQueue.IndexOf(entry.YouTubeId.Id));
                        }));
                    }
                    break;

                case WMPLib.WMPPlayState.wmppsStopped:
                    if (!advancingQueue && !waitingForDownload) ShowCoverScreen("endScreen");
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

        // Prev / next are only shown while playing from the queue, when there is an item before / after the current one
        private void UpdateQueueButtons()
        {
            var index = PlaybackQueue.CurrentIndex;

            SuspendLayout();
            prevButton.Visible = index > 0;
            nextButton.Visible = index >= 0 && index < PlaybackQueue.Items.Count - 1;
            ResumeLayout();
        }

        private void prevButton_Click(object sender, EventArgs e)
        {
            playerInputCapture.Focus();

            if (PlaybackQueue.CurrentIndex > 0)
                PlayQueueItem(PlaybackQueue.CurrentIndex - 1);
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            playerInputCapture.Focus();

            if (PlaybackQueue.CurrentIndex >= 0)
                PlayQueueItem(PlaybackQueue.CurrentIndex + 1);
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            playerInputCapture.Focus();
            PlayPause();
        }

        private void muteButton_Click(object sender, EventArgs e)
        {
            playerInputCapture.Focus();
            if (player == null) return;

            player.settings.mute = !player.settings.mute;

            UpdatePlayerControls();
        }

        private void fullscreenButton_Click(object sender, EventArgs e)
        {
            playerInputCapture.Focus();
            SetFullScreen(!isFullscreen);
        }

        private void seekBar_Click(object sender, EventArgs e)
        {
            playerInputCapture.Focus();
        }

        #endregion

        #region Events

        private void WatchForm_Load(object sender, EventArgs e)
        {
            InitVideoPlayer();

            if (InitialVideoId != null)
            {
                try
                {
                    LoadVideo(InitialVideoId);
                }
                catch (Exception ex)
                {
                    // offline: a downloaded video can still be played
                    var downloaded = LocalFeed.Downloads.Find(InitialVideoId);

                    if (downloaded != null)
                    {
                        LoadVideo(downloaded);
                    }
                    else
                    {
                        MessageBox.Show("Could not load video: " + ex.Message, "YouTube", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        // nothing to watch, go to Browse once this window is shown
                        BeginInvoke(new MethodInvoker(Program.SwitchView));
                    }
                }
            }
            else if (pendingVideo != null)
            {
                // video queued from Browse before this window was first shown
                video = pendingVideo;
                pendingVideo = null;
                ShowVideoData(false, true);
            }
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
        }

        private void detailsFrame_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            ShowVideoDetails();
        }

        private void relatedFrame_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            // may also complete for the intermediate blank document, and only the first load matters
            if (sidebarReady || relatedFrame.Document?.GetElementById("relatedItems") == null) return;

            sidebarReady = true;
            RenderQueue();

            // Load related feed for the current video
            if (video != null)
            {
                LoadRelated();
            }
        }

        private void PlaybackQueue_Changed(object sender, EventArgs e)
        {
            RenderQueue();
            UpdateQueueButtons();
        }

        private void videoControlsPanel_MouseEnter(object sender, EventArgs e)
        {
            if (!isFullscreen) return;

            pauseAutoHide = true;
        }

        private void videoControlsPanel_MouseLeave(object sender, EventArgs e)
        {
            if (!isFullscreen) return;

            pauseAutoHide = false;
            controlsAutoHideCounter = CONTROLS_AUTO_HIDE_TIMEOUT;
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            player.Ctlcontrols.pause();

            new SettingsDialog().ShowDialog();

            // reload settings
            player.settings.autoStart = Settings.Default.AutoPlayVideo;
        }

        private void WatchForm_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                AdjustPanelSizes();
                playerInputCapture.Focus(); 
            }
            else
            {
                if (!Settings.Default.PlayInBrowse)
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

                case Keys.T:
                    if (isFullscreen)
                    {
                        prevLargeLayout = true;
                        SetFullScreen(false);
                        break;
                    }
                    LargeVideoLayout = !LargeVideoLayout;
                    break;

                case Keys.Up:
                    player.settings.volume = Math.Min(100, player.settings.volume + 10);
                    break;

                case Keys.Down:
                    player.settings.volume = Math.Max(0, player.settings.volume - 10);
                    break;

                case Keys.F1:
                    if (videoFrame.Visible)
                        ShowVideoPlayer();
                    else
                        ShowCoverScreen("helpScreen");
                    break;
            }
        }

        private void playerInputCapture_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true; // allow direction keys to raise KeyDown events
        }

        private void WatchForm_Resize(object sender, EventArgs e)
        {
            if (LargeVideoLayout) return;

            AdjustPanelSizes();
        }

        private void AdjustPanelSizes()
        {
            var targetPlayerHeight = (int)Math.Floor(player.Width * 9.0 / 16);
            var idealDetailsHeight = ClientRectangle.Height - header.Height - videoControlsPanel.Height - targetPlayerHeight;
            using (var g = CreateGraphics())
            {
                var minDetailsPanelHeight = (int)(154 * g.DpiX / 96);

                detailsPanel.Height = Math.Max(minDetailsPanelHeight, idealDetailsHeight);
            }
        }

        private void searchBox_Search(object sender, Controls.SearchBoxEventArgs e)
        {
            searchBox.Text = "";
            Program.browseForm.Search(e.Text);
        }

        private void downloadsButton_Click(object sender, EventArgs e)
        {
            Program.browseForm.ShowDownloads();
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

                case "loadComments":
                    LoadComments();
                    break;

                case "download":
                    DownloadButtonClick();
                    break;

                case "retryDownload":
                    if (video != null && waitingForDownload)
                    {
                        var item = DownloadManager.Enqueue(video, true);
                        if (item != null) ShowDownloadProgress(item);
                    }
                    break;

                case "addFavorites":
                    if (video == null) break;

                    var favorited = !LocalFeed.Favorites.Remove(video.YouTubeId.Id);
                    if (favorited)
                        LocalFeed.Favorites.Add(video);

                    detailsFrame.Document?.GetElementById("favoriteBtn")?.SetAttribute("className", favorited ? "selected" : "");
                    break;

                case "author":
                    break;
            }

            playerInputCapture.Focus();
        }

        private void FrameConn_OnVideoClick(object sender, VideoClickEventArgs e)
        {
            switch (e.Name)
            {
                case "queuePlay":
                    PlayQueueItem(e.Index);
                    return;

                case "queueRemove":
                    PlaybackQueue.Remove(e.Index);
                    return;
            }

            // related videos
            if (relatedFeed == null || e.Index >= relatedFeed.Entries.Count) return;

            var video = relatedFeed.Entries[e.Index];

            switch (e.Name)
            {
                case "title":
                case "play":
                    PlayInNewQueue(video);
                    break;

                case "queue":
                    ToggleQueued(video);
                    break;
            }
        }

        #endregion

    }
}
