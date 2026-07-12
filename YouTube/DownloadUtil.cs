using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace YouTube
{
    public enum DownloadStatus
    {
        Idle,
        Waiting,
        Downloading,
        Downloaded,
        Cancelled,
        Error
    }

    public class DownloadItem
    {
        public DownloadItem()
        {
            Status = DownloadStatus.Waiting;

            UpdateListViewItem();
        }

        #region Properties

        public string VideoId { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }

        public string DestinationPath { get; set; }

        public bool PlayAfterDownload { get; set; }

        public DownloadStatus Status { get; set; }

        public string StatusText { get; set; }

        public ListViewItem ListItem { get; set; }

        public WebClient Client { get; set; }

        #endregion

        #region Public Events

        public event EventHandler Done;

        #endregion

        #region Public Methods

        public void UpdateListViewItem()
        {
            if (ListItem == null)
            {
                ListItem = new ListViewItem();
                ListItem.SubItems.Add(Status.ToString());
            }

            ListItem.Text = Title;
            ListItem.ImageKey = Status.ToString().ToLower();
            ListItem.Tag = this;

            if (StatusText != null)
                ListItem.SubItems[1].Text = StatusText;
            else
                ListItem.SubItems[1].Text = Status.ToString();
        }

        public void PlayVideo()
        {
            if (File.Exists(DestinationPath))
            {
                Utils.PlayVideo(DestinationPath);
            }
        }

        public void OpenFileLocation()
        {
            if (System.IO.File.Exists(DestinationPath))
            {
                Process.Start("explorer.exe", "/select,\"" + DestinationPath + "\"");
            }
        }

        public void Retry()
        {
            if (Status == DownloadStatus.Downloaded)
                return;

            Status = DownloadStatus.Waiting;
            UpdateListViewItem();
        }

        public void Cancel()
        {
            if (Status == DownloadStatus.Downloading && Client != null && Client.IsBusy)
            {
                Client.CancelAsync();
            }
        }

        public void Start()
        {
            Directory.CreateDirectory(Settings.Default.DownloadFolder);
            DestinationPath = Utils.GetVideoFilePath(VideoId, Title);

            if (File.Exists(DestinationPath))
            {
                Status = DownloadStatus.Downloaded;
                StatusText = "Already downloaded";
                DownloadComplete();
                return;
            }

            Client = new WebClient();

            Client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(Client_DownloadProgressChanged);
            Client.DownloadFileCompleted += new AsyncCompletedEventHandler(Client_DownloadFileCompleted);

            Client.DownloadFileAsync(new Uri(Url), DestinationPath);

            StatusText = "0%";
            UpdateListViewItem();
        }

        #endregion

        #region Private Methods

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            StatusText = e.ProgressPercentage + "%";
            UpdateListViewItem();
        }

        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            StatusText = null;
            if (e.Cancelled)
            {
                Status = DownloadStatus.Cancelled;
            }
            else if (e.Error != null)
            {
                Status = DownloadStatus.Error;
                StatusText = "Error: " + e.Error.Message;
            }
            else
            {
                Status = DownloadStatus.Downloaded;
            }

            Client.Dispose();
            DownloadComplete();
        }

        private void DownloadComplete()
        {
            if (Status == DownloadStatus.Downloaded && PlayAfterDownload)
            {
                PlayVideo();
            }

            UpdateListViewItem();
            Done?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }

    public static class DownloadUtil
    {
        public static void PopulateImageList(ImageList imageList)
        {
            imageList.Images.Clear();
            imageList.Images.Add("waiting", Properties.Resources.time);
            imageList.Images.Add("downloading", Properties.Resources.Download);
            imageList.Images.Add("downloaded", Properties.Resources.accept);
            imageList.Images.Add("cancelled", Properties.Resources.error);
            imageList.Images.Add("error", Properties.Resources.error);
        }
    }
}
