using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace YouTube
{
    public partial class MainForm : Form, IVideoListParent
    {
        public MainForm()
        {
            InitializeComponent();
        }

        #region Variables

        private Data.Feed lastFeed;

        #endregion

        #region Public Methods

        public void WatchVideo(string videoId)
        {
            Process.Start(Settings.Default.VideoPlayer, DataApi.GetVideoUrl(videoId));
        }

        public void DownloadVideo(string videoId, string videoTitle)
        {
            MessageBox.Show("Download not implemented yet! \r\n[" + videoId + "]: " + videoTitle);
        }

        #endregion

        #region Events

        private void MainForm_Load(object sender, EventArgs e)
        {
            Utils.InitalSettings();

            videoResultsBox.Controls.Remove(loadMoreLink);
        }

        private void loadMoreLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoadMoreVideos();
        }

        #region Menu Items

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SettingsDialog().ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutDialog().ShowDialog();
        }

        #endregion

        #region Search Box

        private void searchButton_Click(object sender, EventArgs e)
        {
            SearchVideos(searchTextBox.Text);
        }

        private void searchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                searchButton_Click(sender, e);
            }
        }

        #endregion

        #endregion

        #region Utilities

        private void SetResultsHintText(string text)
        {
            if (text == null || text == "")
            {
                resultsSearchHint.Text = "";
                resultsSearchHint.Visible = false;
                return;
            }

            resultsSearchHint.Text = text;
            resultsSearchHint.Visible = true;
        }

        private void ShowListOfEntries(List<Data.Entry> entries)
        {
            ShowListOfEntries(entries, true);
        }

        private void ShowListOfEntries(List<Data.Entry> entries, bool clear)
        {
            SetResultsHintText(null);
            videoResultsBox.Controls.Remove(loadMoreLink);
            videoResultsBox.SuspendLayout();

            if (clear) videoResultsBox.Controls.Clear();

            if (entries.Count == 0)
                SetResultsHintText("No results.");

            foreach (var entry in entries)
            {
                var vle = new VideoListEntry();
                vle.Entry = entry;
                vle.Anchor = AnchorStyles.Top & AnchorStyles.Left & AnchorStyles.Right;
                videoResultsBox.Controls.Add(vle);
            }

            videoResultsBox.Controls.Add(loadMoreLink);
            videoResultsBox.ResumeLayout();
            videoResultsBox.Focus();
        }

        #endregion

        #region Actions

        private void SearchVideos(string query)
        {
            if (query.Length == 0)
                return;

            searchBox.Enabled = false;
            videoResultsBox.Controls.Clear();
            SetResultsHintText("Loading...");
            UseWaitCursor = true;

            ThreadPool.QueueUserWorkItem(delegate
            {
                try
                {
                    var feed = DataApi.GetSearchResults(query);

                    Invoke(new MethodInvoker(delegate
                    {
                        lastFeed = feed;
                        UseWaitCursor = false;
                        searchBox.Enabled = true;
                        ShowListOfEntries(feed.Entries);
                    }));
                }
                catch (Exception ex)
                {
                    Invoke(new MethodInvoker(delegate
                    {
                        UseWaitCursor = false;
                        searchBox.Enabled = true;
                        SetResultsHintText("Error: " + ex.Message);
                    }));
                }
            });
            Application.DoEvents();
        }

        private void LoadMoreVideos()
        {
            var nextLink = lastFeed.Links.Find(x => x.Relationship == "next");

            if (nextLink == null)
                return;

            var url = Utils.FixUrlDomain(nextLink.Href);

            UseWaitCursor = true;
            loadMoreLink.Enabled = false;

            ThreadPool.QueueUserWorkItem(delegate
            {
                try
                {
                    var feed = DataApi.GetFeed(url);

                    Invoke(new MethodInvoker(delegate
                    {
                        lastFeed = feed;
                        UseWaitCursor = false;
                        loadMoreLink.Enabled = true;
                        ShowListOfEntries(feed.Entries, false);
                    }));
                }
                catch (Exception ex)
                {
                    Invoke(new MethodInvoker(delegate
                    {
                        UseWaitCursor = false;
                        loadMoreLink.Enabled = true;
                        SetResultsHintText("Error: " + ex.Message);
                    }));
                }
            });
        }

        #endregion



    }
}
