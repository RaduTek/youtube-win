using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace YouTube.Forms
{
    public partial class BrowseForm : Form
    {
        public BrowseForm()
        {
            InitializeComponent();

            guideConn = new WebFrameConnector();
            guideConn.OnActionClick += GuideConn_OnActionClick;
            guideFrame.ObjectForScripting = guideConn;

            browseConn = new WebFrameConnector();
            browseConn.OnActionClick += BrowseConn_OnActionClick;
            browseConn.OnVideoClick += BrowseConn_OnVideoClick;
            browseFrame.ObjectForScripting = browseConn;
            browseFrame.DocumentCompleted += browseFrame_DocumentCompleted;

            DownloadManager.Changed += DownloadManager_Changed;
            history.Changed += History_Changed;
            LocalFeed.Subscriptions.Changed += Subscriptions_Changed;
        }

        // A feed (standard or local, by guide name) or a search, for the back history
        private class BrowseLocation
        {
            public bool IsSearch;
            public string Name; // feed name or search query

            public override bool Equals(object obj)
            {
                var other = obj as BrowseLocation;
                return other != null && other.IsSearch == IsSearch && other.Name == Name;
            }

            public override int GetHashCode()
            {
                return (Name ?? "").GetHashCode() ^ IsSearch.GetHashCode();
            }
        }

        private readonly NavigationHistory<BrowseLocation> history = new NavigationHistory<BrowseLocation>();
        private bool navigatingBack;


        private WebFrameConnector browseConn, guideConn;
        private Data.Feed browseFeed;
        private string nextFeedUrl;
        private bool loadingMore;
        private Data.Feed pendingFeed; // feed waiting for the results page to load
        private bool browsePageReady;
        private int browseRequestId;
        private LocalFeed currentLocalFeed; // local feed being shown, null for online feeds

        public bool EnableWatchButton
        {
            get => switchToWatchButton.Enabled;
            set
            {
                switchToWatchButton.Enabled = value;
            }
        }

        #region Methods

        private void WatchVideo(Data.Entry video)
        {
            Program.watchForm.LoadVideo(video);
        }

        private void SelectFeed(string feedName)
        {
            // "search_results" only highlights the guide item, the search itself is recorded in LoadSearchFeed
            if (feedName != "search_results")
                RecordLocation(false, feedName);

            switch (feedName)
            {
                case "search_results":
                    break;
                case "internal_history":
                    ShowLocalFeed(LocalFeed.History);
                    break;
                case "internal_favorites":
                    ShowLocalFeed(LocalFeed.Favorites);
                    break;
                case "internal_downloads":
                    ShowLocalFeed(LocalFeed.Downloads);
                    break;

                default:
                    LoadStandardFeed(feedName);
                    break;
            }
        }

        private void LoadStandardFeed(string feedName)
        {
            LoadFeed(DataApi.StandardFeedUrl(feedName), "");
        }

        public void Search(string query)
        {
            if (!Visible)
                Program.SwitchView();

            searchBox.Text = query;
            LoadSearchFeed(query);
        }

        private void RecordLocation(bool isSearch, string name)
        {
            if (navigatingBack) return;

            var location = new BrowseLocation { IsSearch = isSearch, Name = name };

            // selecting the same feed again doesn't add an entry
            if (location.Equals(history.Current)) return;

            history.Push(location);
        }

        private void GoBack()
        {
            if (!history.CanGoBack) return;

            var location = history.GoBack();

            navigatingBack = true;
            try
            {
                if (location.IsSearch)
                {
                    searchBox.Text = location.Name;
                    LoadSearchFeed(location.Name);
                }
                else
                {
                    // highlights the guide item, the guide then loads the feed through SelectFeed
                    SelectGuideFeed(location.Name);
                }
            }
            finally
            {
                navigatingBack = false;
            }
        }

        private void LoadSearchFeed(string query)
        {
            RecordLocation(true, query);
            SelectGuideFeed("search_results");

            LoadFeed(DataApi.SearchFeedUrl(query), "Search results for <b>" + query + "</b>");
        }

        private void LoadFeed(string feedUrl, string status)
        {
            if (Settings.Default.InstanceBaseUrl.Length == 0)
            {
                ShowBrowseError("Instance URL is not set. Please configure it in settings.");
                return;
            }

            var requestId = LoadBrowsePage(status);

            ThreadPool.QueueUserWorkItem(delegate
            {
                try
                {
                    var feed = DataApi.GetFeed(feedUrl);

                    Invoke(new MethodInvoker(delegate
                    {
                        // another feed was selected while loading
                        if (requestId != browseRequestId) return;

                        ShowBrowseFeedWhenReady(feed);
                    }));
                }
                catch (Exception ex)
                {
                    Invoke(new MethodInvoker(delegate
                    {
                        if (requestId != browseRequestId) return;

                        ShowBrowseError("Error: " + ex.Message);
                    }));
                }
            });
        }

        public void ShowDownloads()
        {
            if (!Visible)
                Program.SwitchView();

            // selects the guide item, which loads the feed through SelectFeed
            SelectGuideFeed("internal_downloads");
        }

        private void ShowLocalFeed(LocalFeed store)
        {
            LoadBrowsePage(store.Feed.Title);
            currentLocalFeed = store;

            // snapshot, so changes to the store (e.g. history updated by watching) don't shift indexes
            ShowBrowseFeedWhenReady(new Data.Feed
            {
                Title = store.Feed.Title,
                Entries = new List<Data.Entry>(store.Feed.Entries)
            });
        }

        // Loads an empty results page; returns an id identifying this request
        private int LoadBrowsePage(string status)
        {
            browseFeed = null;
            nextFeedUrl = null;
            loadingMore = false;
            pendingFeed = null;
            browsePageReady = false;
            currentLocalFeed = null;
            browseRequestId++;

            Utils.LoadTemplate("browseResults", new Dictionary<string, string>
            {
                { "status", status }
            }, browseFrame);

            return browseRequestId;
        }

        // The results page may still be loading (slow machines), so the feed is
        // either shown right away or kept until browseFrame_DocumentCompleted
        private void ShowBrowseFeedWhenReady(Data.Feed feed)
        {
            if (browsePageReady)
                ShowBrowseFeed(feed);
            else
                pendingFeed = feed;
        }

        private void LoadMoreResults()
        {
            if (loadingMore || browseFeed == null || nextFeedUrl == null) return;

            loadingMore = true;
            var url = nextFeedUrl;
            var feedAtRequest = browseFeed;

            browseFrame.Document.GetElementById("browseResultsMore").SetAttribute("className", "hidden");
            browseFrame.Document.GetElementById("loading").SetAttribute("className", "");

            ThreadPool.QueueUserWorkItem(delegate
            {
                try
                {
                    var feed = DataApi.GetFeed(url);

                    Invoke(new MethodInvoker(delegate
                    {
                        loadingMore = false;

                        // Feed was replaced (new search/feed selected) while loading
                        if (browseFeed != feedAtRequest) return;

                        AppendBrowseFeed(feed);
                    }));
                }
                catch (Exception ex)
                {
                    Invoke(new MethodInvoker(delegate
                    {
                        loadingMore = false;

                        if (browseFeed != feedAtRequest) return;

                        browseFrame.Document.GetElementById("loading").SetAttribute("className", "hidden");
                        browseFrame.Document.GetElementById("browseResultsMore").SetAttribute("className", "");
                        MessageBox.Show("Error loading more results: " + ex.Message);
                    }));
                }
            });
        }

        private void AppendBrowseFeed(Data.Feed feed)
        {
            int startIndex = browseFeed.Entries.Count;

            browseFeed.Entries.AddRange(feed.Entries);
            nextFeedUrl = feed.GetNextUrl();

            var items = browseFrame.Document.GetElementById("browseResultsItems");
            items.InnerHtml += BuildEntriesHtml(feed, startIndex);

            browseFrame.Document.GetElementById("loading").SetAttribute("className", "hidden");
            browseFrame.Document.GetElementById("browseResultsMore").SetAttribute("className", nextFeedUrl == null ? "hidden" : "");
        }

        private void ShowBrowseFeed(Data.Feed feed)
        {
            browseFeed = feed;
            nextFeedUrl = feed.GetNextUrl();
            loadingMore = false;

            browseFrame.Document.GetElementById("loading").SetAttribute("className", "hidden");
            browseFrame.Document.GetElementById("browseResultsItems").InnerHtml = BuildEntriesHtml(feed, 0);
            browseFrame.Document.GetElementById("browseResultsMore").SetAttribute("className", nextFeedUrl == null ? "hidden" : "");

            if (currentLocalFeed == LocalFeed.Downloads)
                RenderDownloads();
        }

        // Unfinished downloads, listed above the finished ones on the Downloads feed
        private void RenderDownloads()
        {
            var valuesList = new List<Dictionary<string, string>>();

            foreach (var item in DownloadManager.Items)
            {
                var id = item.VideoId;
                string actions;

                if (item.Status == DownloadStatus.Waiting || item.Status == DownloadStatus.Downloading)
                    actions = DownloadAction("downloadCancel", id, "Cancel");
                else
                    actions = DownloadAction("downloadRetry", id, "Retry") + DownloadAction("downloadDismiss", id, "Remove");

                valuesList.Add(new Dictionary<string, string>
                {
                    { "title", item.Entry.Title },
                    { "thumbnail", Utils.GetThumbnailUrl(id, false) },
                    { "status", Utils.HtmlText(item.StatusText) },
                    { "statusClass", item.Status == DownloadStatus.Error ? "failed" : "" },
                    { "actions", actions }
                });
            }

            Utils.LoadTemplateListSlot("downloadItem", valuesList, "browseDownloads", browseFrame);

            // the In Progress section is only shown while there are unfinished downloads
            browseFrame.Document.GetElementById("browseDownloadsSection")?.SetAttribute("className", valuesList.Count > 0 ? "" : "hidden");
        }

        private static string DownloadAction(string action, string videoId, string text)
        {
            return "<a href=\"javascript:window.external.ActionClick('" + action + "','" + videoId + "')\">" + text + "</a>";
        }

        private string BuildEntriesHtml(Data.Feed feed, int startIndex)
        {
            var entriesHtml = new StringBuilder();

            for (int i = 0; i < feed.Entries.Count; i++)
            {
                var entry = feed.Entries[i];

                entriesHtml.AppendLine(Utils.CreateTemplateDoc("browseResultsItem", new Dictionary<string, string>
                {
                    { "index", (startIndex + i).ToString() },
                    { "title", entry.Title },
                    { "videoId", entry.VideoId },
                    { "author", entry.Author.Name },
                    { "description", entry.Content },
                    { "duration", Utils.FormatDurationSeconds(entry.Media.Duration.Seconds) },
                    { "upload_date", Utils.FormatRelativeDate(entry.Published) },
                    { "viewcount", entry.Statistics.ViewCount.ToString("N0") },
                    { "thumbnail", Utils.GetThumbnailUrl(entry.VideoId, false) }
                }));
            }

            return entriesHtml.ToString();
        }

        private void ShowBrowseError(string message)
        {
            //MessageBox.Show(message);

            Utils.LoadTemplate("browseError", new Dictionary<string, string>()
            {
                { "error_text", message }
            }, browseFrame);
        }

        private void SelectGuideFeed(string feedName)
        {
            guideFrame.Document.InvokeScript("SelectFeed", new string[] { feedName });
        }

        // Subscribed channels, listed in the guide sidebar; the section is hidden while there are none
        private void RenderSubscriptions()
        {
            var guideDoc = guideFrame.Document;
            var slot = guideDoc?.GetElementById("guideSubscriptions");
            if (slot == null) return;

            var channels = new List<Data.Entry>(LocalFeed.Subscriptions.Feed.Entries);
            channels.Sort((a, b) => string.Compare(a.Title, b.Title, StringComparison.CurrentCultureIgnoreCase));

            var html = new StringBuilder();
            foreach (var channel in channels)
            {
                var name = Utils.HtmlText(channel.Title);
                html.AppendLine("<a href=\"javascript:void(0)\" title=\"" + name + "\"><span class=\"feedicon channel\"></span><span class=\"text\">" + name + "</span></a>");
            }

            slot.InnerHtml = html.ToString();
            guideDoc.GetElementById("guideSubscriptionsSection")?.SetAttribute("className", channels.Count > 0 ? "" : "hidden");
        }

        #endregion

        #region Events

        private void BrowseForm_Load(object sender, EventArgs e)
        {
            Utils.InitialSettings();

            rightPanel.Visible = false;

            Utils.LoadTemplate("guide", new Dictionary<string, string>(), guideFrame);
        }

        private void BrowseForm_Shown(object sender, EventArgs e)
        {
        }


        private void GuideConn_OnActionClick(object sender, ActionClickEventArgs e)
        {
            switch (e.Name)
            {
                case "selectFeed":
                    SelectFeed(e.Data);
                    break;
            }
        }

        private void BrowseConn_OnActionClick(object sender, ActionClickEventArgs e)
        {
            switch (e.Name)
            {
                case "loadMore":
                    LoadMoreResults();
                    break;

                case "downloadCancel":
                    DownloadManager.Cancel(e.Data);
                    break;

                case "downloadRetry":
                    DownloadManager.Retry(e.Data);
                    break;

                case "downloadDismiss":
                    DownloadManager.Dismiss(e.Data);
                    break;
            }

        }

        private void BrowseConn_OnVideoClick(object sender, VideoClickEventArgs e)
        {
            if (browseFeed == null || e.Index >= browseFeed.Entries.Count) return;

            var video = browseFeed.Entries[e.Index];

            switch (e.Name)
            {
                case "queue":
                    Program.watchForm.ToggleQueued(video);
                    break;

                case "download":
                    DownloadManager.Enqueue(video, false);
                    break;

                case "title":
                case "play":
                    Program.watchForm.PlayFromFeed(video, browseFeed.Entries, nextFeedUrl);
                    break;

                default: // "author"
                    WatchVideo(video);
                    break;
            }
        }

        private void guideFrame_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            SelectGuideFeed("most_popular");
            RenderSubscriptions();
        }

        private void Subscriptions_Changed(object sender, EventArgs e)
        {
            RenderSubscriptions();
        }

        private void browseFrame_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            // may also complete for the intermediate blank document, wait for the results template
            if (browseFrame.Document?.GetElementById("browseResultsItems") == null) return;

            browsePageReady = true;

            if (pendingFeed == null) return;

            var feed = pendingFeed;
            pendingFeed = null;
            ShowBrowseFeed(feed);
        }

        private void DownloadManager_Changed(object sender, DownloadEventArgs e)
        {
            Utils.UpdateDownloadsButton(downloadsButton, toolTip);

            if (currentLocalFeed != LocalFeed.Downloads || !browsePageReady) return;

            if (e.Item.Status == DownloadStatus.Downloaded)
                ShowLocalFeed(LocalFeed.Downloads); // moves it into the list of downloaded videos
            else
                RenderDownloads();
        }

        private void downloadsButton_Click(object sender, EventArgs e)
        {
            ShowDownloads();
        }

        private void History_Changed(object sender, EventArgs e)
        {
            backButton.Enabled = history.CanGoBack;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            GoBack();
        }

        private void browseToWatchButton_Click(object sender, EventArgs e)
        {
            Program.SwitchView();
        }

        private void searchBox_Search(object sender, Controls.SearchBoxEventArgs e)
        {
            LoadSearchFeed(e.Text);
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            new SettingsDialog().ShowDialog();
        }

        #endregion
    }
}
