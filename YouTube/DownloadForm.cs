using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace YouTube
{

    public partial class DownloadForm : Form
    {
        public DownloadForm()
        {
            InitializeComponent();

            DownloadUtil.PopulateImageList(imageList);
        }

        #region Variables

        private readonly List<DownloadItem> downloadItems = new List<DownloadItem>();
        private DownloadItem currentDownload;
        private readonly object queueLock = new object();

        #endregion

        #region Public Events

        public class DownloadEventArgs : EventArgs
        {
            public DownloadStatus Status { get; set; }
        }

        public event EventHandler<DownloadEventArgs> DownloadStatusChanged;

        #endregion

        #region Public Methods

        public void AddToDownloadList(string videoId, string videoTitle, bool playAfterDownload)
        {
            DownloadItem item;

            if (downloadItems.Exists(x => x.VideoId == videoId))
            {
                item = downloadItems.Find(x => x.VideoId == videoId);
                item.ListItem.Selected = true;
                item.PlayAfterDownload = playAfterDownload;

                if (item.Status == DownloadStatus.Downloaded && playAfterDownload)
                {
                    item.PlayVideo();
                }
                return;
            }

            item = new DownloadItem();

            item.VideoId = videoId;
            item.Title = videoTitle;
            item.Url = DataApi.GetVideoUrl(videoId);
            item.Status = DownloadStatus.Waiting;
            item.PlayAfterDownload = playAfterDownload;
            item.Done += Item_Done;

            downloadItems.Add(item);
            item.UpdateListViewItem();

            downloadListView.Items.Add(item.ListItem);
            item.ListItem.Selected = true;

            DoQueueCycle();
        }

        #endregion

        #region Private Methods

        private void CompleteCurrent(DownloadItem item)
        {
            lock (queueLock)
            {
                if (currentDownload != item)
                    return;

                currentDownload = null;
            }

            DoQueueCycle();
        }

        private void Item_Done(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(Item_Done), sender, e);
                return;
            }

            CompleteCurrent((DownloadItem)sender);
        }

        private void DoQueueCycle()
        {
            UpdateDownloadItemStatus();

            lock (queueLock)
            {
                if (currentDownload != null)
                    return;

                var next = downloadItems.Find(x => x.Status == DownloadStatus.Waiting);
                if (next == null)
                    return;

                currentDownload = next;
            }

            try
            {
                currentDownload.Start();
            }
            catch
            {
                currentDownload.Status = DownloadStatus.Error;
                currentDownload.UpdateListViewItem();
                CompleteCurrent(currentDownload);
            }
        }

        private void UpdateDownloadItemStatus()
        {
            int waitingCount = downloadItems.FindAll(x => x.Status == DownloadStatus.Waiting).Count;
            int errorCount = downloadItems.FindAll(x => x.Status == DownloadStatus.Error).Count;
            int downloadedCount = downloadItems.FindAll(x => x.Status == DownloadStatus.Downloaded).Count;

            DownloadStatus status = DownloadStatus.Idle;

            if (errorCount > 0)
                status = DownloadStatus.Error;
            else if (waitingCount > 0)
                status = DownloadStatus.Downloading;
            else if (downloadedCount > 0)
                status = DownloadStatus.Downloaded;

            DownloadStatusChanged?.Invoke(this, new DownloadEventArgs { Status = status });
        }

        private DownloadItem GetSelectedDownloadItem()
        {
            if (downloadListView.SelectedItems.Count > 0)
            {
                return (DownloadItem)downloadListView.SelectedItems[0].Tag;
            }
            return null;
        }

        private void RemoveDownloadItem(DownloadItem item)
        {
            if (item == null) return;
            if (item.Status == DownloadStatus.Downloading)
            {
                item.Cancel();
            }
            downloadListView.Items.Remove(item.ListItem);
            downloadItems.Remove(item);
        }

        #endregion

        #region Events

        private void DownloadForm_Deactivate(object sender, EventArgs e)
        {
            Hide();
        }

        private void DownloadForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        #region Context Menu

        private void itemContextMenu_Opening(object sender, CancelEventArgs e)
        {
            var item = GetSelectedDownloadItem();
            if (item == null) return;

            switch (item.Status)
            {
                case DownloadStatus.Waiting:
                    playVideoMenuItem.Visible = showInFolderMenuItem.Visible = false;
                    retryMenuItem.Visible = false;
                    removeMenuItem.Visible = true;
                    cancelMenuItem.Visible = false;
                    break;
                case DownloadStatus.Downloading:
                    playVideoMenuItem.Visible = showInFolderMenuItem.Visible = false;
                    retryMenuItem.Visible = false;
                    removeMenuItem.Visible = false;
                    cancelMenuItem.Visible = true;
                    break;
                case DownloadStatus.Downloaded:
                    playVideoMenuItem.Visible = showInFolderMenuItem.Visible = true;
                    retryMenuItem.Visible = false;
                    removeMenuItem.Visible = true;
                    cancelMenuItem.Visible = false;
                    break;
                case DownloadStatus.Error:
                    playVideoMenuItem.Visible = showInFolderMenuItem.Visible = false;
                    retryMenuItem.Visible = true;
                    removeMenuItem.Visible = true;
                    cancelMenuItem.Visible = false;
                    break;
            }
        }

        private void playVideoMenuItem_Click(object sender, EventArgs e)
        {
            var item = GetSelectedDownloadItem();
            if (item == null) return;

            item.PlayVideo();
        }

        private void showInFolderMenuItem_Click(object sender, EventArgs e)
        {
            var item = GetSelectedDownloadItem();
            if (item == null) return;

            item.OpenFileLocation();
        }

        private void retryMenuItem_Click(object sender, EventArgs e)
        {
            var item = GetSelectedDownloadItem();
            if (item == null) return;

            item.Retry();
            DoQueueCycle();
        }

        private void cancelMenuItem_Click(object sender, EventArgs e)
        {
            var item = GetSelectedDownloadItem();
            if (item == null) return;

            item.Cancel();
        }

        private void removeMenuItem_Click(object sender, EventArgs e)
        {
            var item = GetSelectedDownloadItem();
            if (item == null) return;

            RemoveDownloadItem(item);
        }

        private void downloadListView_DoubleClick(object sender, EventArgs e)
        {
            var item = GetSelectedDownloadItem();
            if (item == null) return;

            item.PlayVideo();
        }

        #endregion

        #endregion

        private void openDownloadsButton_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Settings.Default.DownloadFolder);
        }

        private void removeAllButton_Click(object sender, EventArgs e)
        {
            downloadListView.Items.Clear();

            foreach (var item in downloadItems)
            {
                if (item.Status == DownloadStatus.Downloading)
                {
                    item.Cancel();
                }
            }

            downloadItems.Clear();
        }
    }
}
