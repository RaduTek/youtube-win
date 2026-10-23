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
        }


        private WebFrameConnector browseConn, guideConn;
        private Data.Feed browseFeed;
        private string nextFeedUrl;
        private bool loadingMore;
        private Data.Feed pendingFeed; // feed waiting for the results page to load
        private bool browsePageReady;
        private int browseRequestId;

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

        private void LoadSearchFeed(string query)
        {
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

        private void ShowLocalFeed(LocalFeed store)
        {
            LoadBrowsePage(store.Feed.Title);

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
                    { "videoId", entry.YouTubeId.Id },
                    { "author", entry.Author.Name },
                    { "description", entry.Content },
                    { "duration", Utils.FormatDurationSeconds(entry.Media.Duration.Seconds) },
                    { "upload_date", Utils.FormatRelativeDate(entry.Published) },
                    { "viewcount", entry.Statistics.ViewCount.ToString("N0") },
                    { "thumbnail", "http://i.ytimg.com/vi/" + entry.YouTubeId.Id + "/default.jpg" }
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
            }

        }

        private void BrowseConn_OnVideoClick(object sender, VideoClickEventArgs e)
        {
            if (browseFeed == null || e.Index >= browseFeed.Entries.Count) return;

            var video = browseFeed.Entries[e.Index];

            switch (e.Name)
            {
                case "queue":
                    // TODO: add to playback queue (not implemented yet)
                    break;

                default: // "title", "play"
                    WatchVideo(video);
                    break;
            }
        }

        private void guideFrame_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            SelectGuideFeed("most_popular");
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
