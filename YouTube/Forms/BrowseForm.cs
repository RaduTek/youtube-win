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
        }


        private WebFrameConnector browseConn, guideConn;
        private Data.Feed browseFeed;

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
                case "internal_favorites":
                    break;
                case "internal_downloads":
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

            Utils.LoadTemplate("browseResults", new Dictionary<string, string>
            {
                { "status", status }
            }, browseFrame);

            ThreadPool.QueueUserWorkItem(delegate
            {
                try
                {
                    var feed = DataApi.GetFeed(feedUrl);

                    Invoke(new MethodInvoker(delegate
                    {
                        ShowBrowseFeed(feed);
                    }));
                }
                catch (Exception ex)
                {
                    Invoke(new MethodInvoker(delegate
                    {
                        ShowBrowseError("Error: " + ex.Message);
                    }));
                }
            });
        }

        private void ShowBrowseFeed(Data.Feed feed)
        {
            browseFeed = feed;
            var entriesHtml = new StringBuilder();

            for (int i = 0; i < feed.Entries.Count; i++)
            {
                var entry = feed.Entries[i];

                entriesHtml.AppendLine(Utils.CreateTemplateDoc("browseResultsItem", new Dictionary<string, string>
                {
                    { "index", i.ToString() },
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

            browseFrame.Document.GetElementById("loading").SetAttribute("className", "hidden");
            browseFrame.Document.GetElementById("browseResultsItems").InnerHtml = entriesHtml.ToString();
            browseFrame.Document.GetElementById("browseResultsMore").SetAttribute("className", "");
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
            rightSplitter.Visible = false;

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

        }

        private void BrowseConn_OnVideoClick(object sender, VideoClickEventArgs e)
        {
            if (browseFeed == null || e.Index > browseFeed.Entries.Count) return;

            var video = browseFeed.Entries[e.Index];

            WatchVideo(video);
        }

        private void guideFrame_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            SelectGuideFeed("most_popular");
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
