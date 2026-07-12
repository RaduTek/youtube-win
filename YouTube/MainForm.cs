using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace YouTube
{
    public partial class MainForm : Form, IVideoListParent
    {
        public MainForm()
        {
            InitializeComponent();

            downloadForm = new DownloadForm();
            downloadForm.DownloadStatusChanged += DownloadForm_DownloadStatusChanged;
            queueForm = new QueueForm();
        }

        #region Variables

        private Data.Feed lastFeed;

        private DownloadForm downloadForm;
        private QueueForm queueForm;

        private DownloadStatus downloadStatus;
        private bool hasSearchedOnce = false;

        #endregion

        #region Public Methods

        public void WatchVideo(string videoId, string videoTitle)
        {
            // Queue download if the setting is enabled
            if (Settings.Default.DownloadBeforePlay)
            {
                downloadForm.AddToDownloadList(videoId, videoTitle, true);
                ShowDownloadList();
                return;
            }

            // Play video from local source if possible
            var videoFilePath = Utils.GetVideoFilePath(videoId, videoTitle);
            if (File.Exists(videoFilePath))
            {
                Utils.PlayVideo(videoFilePath);
                return;
            }

            // Play video from the URL
            Utils.PlayVideo(DataApi.GetVideoUrl(videoId));
        }

        public void DownloadVideo(string videoId, string videoTitle)
        {
            downloadForm.AddToDownloadList(videoId, videoTitle, false);
            ShowDownloadList();
        }

        public void QueueVideo(string videoId, string videoTitle, int videoDuration)
        {
            queueForm.QueueVideo(videoId, videoTitle, videoDuration);
            ShowQueueList();
        }

        #endregion

        #region Events

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (hasSearchedOnce)
                return;

            Utils.InitalSettings();

            videoResultsBox.Controls.Remove(loadMoreLink);

            if (Settings.Default.InstanceBaseUrl == null || Settings.Default.InstanceBaseUrl == "")
            {
                SetResultsHintText("Instance Base URL is not configured.\r\nGo to Menu > Settings to set it up.");
                searchBox.Enabled = false;
            } else
            {
                SetResultsHintText("Enter a search term and click Search to find videos.");
                searchBox.Enabled = true;
            }
        }

        private void loadMoreLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoadMoreVideos();
        }

        private void DownloadForm_DownloadStatusChanged(object sender, DownloadForm.DownloadEventArgs e)
        {
            downloadStatus = e.Status;
            switch (e.Status)
            {
                case DownloadStatus.Downloading:
                    downloadStatusButton.Image = Properties.Resources.Download_queued;
                    break;
                case DownloadStatus.Downloaded:
                    downloadStatusButton.Image = Properties.Resources.Download_complete;
                    break;
                case DownloadStatus.Error:
                    downloadStatusButton.Image = Properties.Resources.Download_error;
                    break;
                default:
                    downloadStatusButton.Image = Properties.Resources.Download_idle;
                    break;
            }
        }

        #region Menu Items

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SettingsDialog().ShowDialog();
            MainForm_Load(null, null);
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
            {
                SetResultsHintText("No results.");
                videoResultsBox.ResumeLayout();
                return;
            }

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
            hasSearchedOnce = true;
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

        private void ShowQueueList()
        {
            Point buttonBottomRight = queueListButton.Owner.PointToScreen(
                new Point(queueListButton.Bounds.Right, queueListButton.Bounds.Bottom));

            queueForm.Left = buttonBottomRight.X - queueForm.Width;
            queueForm.Top = buttonBottomRight.Y + 4;

            queueForm.Show();
        }

        private void queueListButton_Click(object sender, EventArgs e)
        {
            ShowQueueList();
        }

        private void ShowDownloadList()
        {
            Point buttonBottomRight = downloadStatusButton.Owner.PointToScreen(
                new Point(downloadStatusButton.Bounds.Right, downloadStatusButton.Bounds.Bottom));

            downloadForm.Left = buttonBottomRight.X - downloadForm.Width;
            downloadForm.Top = buttonBottomRight.Y + 4;

            downloadForm.Show();
        }

        private void downloadStatusButton_Click(object sender, EventArgs e)
        {
            ShowDownloadList();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && downloadStatus == DownloadStatus.Downloading)
            {
                if (MessageBox.Show("Downloads are in progress. Are you sure you want to exit?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
