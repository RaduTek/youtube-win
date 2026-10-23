using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using YouTube.Data;

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
        public Entry Entry { get; set; }

        public string VideoId
        {
            get { return Entry.YouTubeId.Id; }
        }

        public DownloadStatus Status { get; set; }

        public int Progress { get; set; } // 0 - 100

        public string Error { get; set; }

        public WebClient Client { get; set; }

        public string StatusText
        {
            get
            {
                switch (Status)
                {
                    case DownloadStatus.Waiting: return "Waiting";
                    case DownloadStatus.Downloading: return "Downloading " + Progress + "%";
                    case DownloadStatus.Downloaded: return "Downloaded";
                    case DownloadStatus.Cancelled: return "Cancelled";
                    case DownloadStatus.Error: return "Failed: " + Error;
                    default: return "";
                }
            }
        }
    }

    public class DownloadEventArgs : EventArgs
    {
        public DownloadItem Item { get; set; }

        public string VideoId
        {
            get { return Item.VideoId; }
        }
    }

    /// <summary>
    /// Downloads videos one at a time. Finished downloads are stored in LocalFeed.Downloads,
    /// unfinished ones (waiting, downloading, failed, cancelled) are kept in Items.
    /// Only meant to be used from the UI thread.
    /// </summary>
    public static class DownloadManager
    {
        public static readonly List<DownloadItem> Items = new List<DownloadItem>();

        public static event EventHandler<DownloadEventArgs> Changed;

        private static DownloadItem current;

        /// <summary>
        /// Number of downloads waiting or in progress
        /// </summary>
        public static int ActiveCount
        {
            get
            {
                int count = 0;
                foreach (var item in Items)
                {
                    if (item.Status == DownloadStatus.Waiting || item.Status == DownloadStatus.Downloading)
                        count++;
                }
                return count;
            }
        }

        public static DownloadItem Current
        {
            get { return current; }
        }

        /// <summary>
        /// Adds a video to the download queue; first puts it in front of the waiting downloads.
        /// Returns null if the video is already downloaded.
        /// </summary>
        public static DownloadItem Enqueue(Entry entry, bool first)
        {
            var videoId = entry.YouTubeId.Id;

            if (GetLocalPath(videoId) != null) return null;

            var item = Find(videoId);

            if (item == null)
            {
                item = new DownloadItem { Entry = entry.Clone() };
                Items.Add(item);
            }
            else if (item.Status == DownloadStatus.Downloading || (item.Status == DownloadStatus.Waiting && !first))
            {
                return item;
            }

            item.Status = DownloadStatus.Waiting;
            item.Progress = 0;
            item.Error = null;

            if (first)
            {
                Items.Remove(item);
                Items.Insert(current != null && Items.Contains(current) ? Items.IndexOf(current) + 1 : 0, item);
            }

            OnChanged(item);
            StartNext();

            return item;
        }

        public static void Cancel(string videoId)
        {
            var item = Find(videoId);
            if (item == null) return;

            if (item.Status == DownloadStatus.Downloading && item.Client != null)
            {
                // finishes in Client_DownloadFileCompleted
                item.Client.CancelAsync();
            }
            else if (item.Status == DownloadStatus.Waiting)
            {
                item.Status = DownloadStatus.Cancelled;
                OnChanged(item);
            }
        }

        public static void Retry(string videoId)
        {
            var item = Find(videoId);
            if (item == null) return;

            if (item.Status == DownloadStatus.Cancelled || item.Status == DownloadStatus.Error)
                Enqueue(item.Entry, false);
        }

        /// <summary>
        /// Removes a cancelled or failed download from the list
        /// </summary>
        public static void Dismiss(string videoId)
        {
            var item = Find(videoId);
            if (item == null || item.Status == DownloadStatus.Downloading || item.Status == DownloadStatus.Waiting) return;

            Items.Remove(item);
            OnChanged(item);
        }

        public static DownloadItem Find(string videoId)
        {
            foreach (var item in Items)
            {
                if (item.VideoId == videoId)
                    return item;
            }

            return null;
        }

        /// <summary>
        /// Path of the downloaded video file, or null if not downloaded
        /// </summary>
        public static string GetLocalPath(string videoId)
        {
            var entry = LocalFeed.Downloads.Find(videoId);

            if (entry == null || string.IsNullOrEmpty(entry.LocalPath) || !File.Exists(entry.LocalPath))
                return null;

            return entry.LocalPath;
        }

        public static void OpenFileLocation(string videoId)
        {
            var path = GetLocalPath(videoId);

            if (path != null)
                Process.Start("explorer.exe", "/select,\"" + path + "\"");
        }

        private static void StartNext()
        {
            if (current != null) return;

            foreach (var item in Items)
            {
                if (item.Status == DownloadStatus.Waiting)
                {
                    Start(item);
                    return;
                }
            }
        }

        private static void Start(DownloadItem item)
        {
            current = item;

            string path;

            try
            {
                Directory.CreateDirectory(Settings.Default.DownloadFolder);
                path = Utils.GetVideoFilePath(item.VideoId, item.Entry.Title);

                // file from an earlier download that isn't registered yet
                if (File.Exists(path))
                {
                    Complete(item, path);
                    return;
                }

                CacheThumbnails(item.VideoId);

                item.Status = DownloadStatus.Downloading;
                item.Client = new WebClient();
                item.Client.DownloadProgressChanged += Client_DownloadProgressChanged;
                item.Client.DownloadFileCompleted += Client_DownloadFileCompleted;
                item.Client.DownloadFileAsync(new Uri(DataApi.GetVideoUrl(item.VideoId)), path + ".part", path);
            }
            catch (Exception ex)
            {
                Fail(item, ex.Message);
                return;
            }

            OnChanged(item);
        }

        private static void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            var item = current;
            if (item == null || item.Client != sender) return;

            // only report actual changes, this event fires very often
            if (e.ProgressPercentage == item.Progress) return;

            item.Progress = e.ProgressPercentage;
            OnChanged(item);
        }

        private static void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            var item = current;
            if (item == null || item.Client != sender) return;

            var path = (string)e.UserState;

            item.Client.Dispose();
            item.Client = null;

            if (e.Cancelled || e.Error != null)
            {
                TryDelete(path + ".part");

                if (e.Cancelled)
                {
                    item.Status = DownloadStatus.Cancelled;
                    current = null;
                    OnChanged(item);
                    StartNext();
                }
                else
                {
                    Fail(item, e.Error.Message);
                }

                return;
            }

            try
            {
                File.Move(path + ".part", path);
            }
            catch (Exception ex)
            {
                TryDelete(path + ".part");
                Fail(item, ex.Message);
                return;
            }

            Complete(item, path);
        }

        private static void Complete(DownloadItem item, string path)
        {
            var entry = item.Entry.Clone();
            entry.LocalPath = path;
            LocalFeed.Downloads.Add(entry);

            item.Status = DownloadStatus.Downloaded;
            item.Progress = 100;
            Items.Remove(item);
            current = null;

            OnChanged(item);
            StartNext();
        }

        private static void Fail(DownloadItem item, string error)
        {
            item.Status = DownloadStatus.Error;
            item.Error = error;
            current = null;

            OnChanged(item);
            StartNext();
        }

        // Thumbnails are stored locally, so downloaded videos show them offline
        private static void CacheThumbnails(string videoId)
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                try
                {
                    Directory.CreateDirectory(Utils.ThumbnailFolder);

                    using (var client = new WebClient())
                    {
                        client.DownloadFile(Utils.GetThumbnailWebUrl(videoId, false), Utils.GetThumbnailCachePath(videoId, false));
                        client.DownloadFile(Utils.GetThumbnailWebUrl(videoId, true), Utils.GetThumbnailCachePath(videoId, true));
                    }
                }
                catch (Exception)
                {
                    // thumbnails are optional
                }
            });
        }

        private static void TryDelete(string path)
        {
            try
            {
                if (File.Exists(path)) File.Delete(path);
            }
            catch (Exception) { }
        }

        private static void OnChanged(DownloadItem item)
        {
            if (Changed != null)
                Changed(null, new DownloadEventArgs { Item = item });
        }
    }
}
