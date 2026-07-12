using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Threading;

namespace YouTube
{
    public partial class VideoListEntry : UserControl
    {
        public VideoListEntry()
        {
            InitializeComponent();

            ratingStars = new PictureBox[] { ratingStar1, ratingStar2, ratingStar3, ratingStar4, ratingStar5 };
        }

        #region Variables

        private PictureBox[] ratingStars;

        private IVideoListParent parent;

        #endregion

        #region Properties

        private Data.Entry _entry;
        public Data.Entry Entry
        {
            get
            {
                return _entry;
            }
            set
            {
                _entry = value;
                LoadValues();
            }
        }

        #endregion

        #region Actions

        private void WatchNow()
        {
            if (_entry.YouTubeId.Id != null)
                parent.WatchVideo(_entry.YouTubeId.Id);
        }

        private void DownloadVideo()
        {
            if (_entry.YouTubeId.Id != null)
                parent.DownloadVideo(_entry.YouTubeId.Id, _entry.Title);
        }

        private void OpenInBrowser()
        {
            if (_entry.Media.Player.Url != null)
                Process.Start(Utils.FixUrlDomain(_entry.Media.Player.Url));
        }

        private void OpenChannelInBrowser()
        {
            if (_entry.ChannelId != null)
                Process.Start(DataApi.GetFullUrl("channel/" + _entry.ChannelId));
        }

        #endregion

        #region Data Display

        private void LoadValues()
        {
            if (_entry == null)
                return;

            titleLink.Text = _entry.Title;
            channelLink.Text = _entry.ChannelName;
            descriptionLabel.Text = _entry.Content.Replace('\n', ' ').Replace("\r", "");
            toolTip.SetToolTip(descriptionLabel, _entry.Content);
            dateLabel.Text = Utils.FormatRelativeDate(_entry.Published);

            if (_entry.Media.Thumbnails.Length >= 1)
                LoadThumbnailAsync(_entry.Media.Thumbnails[0].Url);

            SetDurationLabel(_entry.Media.Duration.Seconds);

            if (_entry.Rating != null)
            {
                var rating = (double)_entry.Rating.Likes / (_entry.Rating.Likes + _entry.Rating.Dislikes) * 5.0;
                SetRatingValue(rating);
            }

            if (_entry.Statistics != null)
            {
                viewsLabel.Text = _entry.Statistics.ViewCount.ToString("N0") + " views";
            }
        }

        private void SetRatingValue(double value)
        {
            double truncated = Math.Floor(value * 10) / 10;

            string ratingText = (truncated % 1 == 0)
                ? truncated.ToString("0")
                : truncated.ToString("0.0");

            for (int i = 0; i < 5; i++)
            {
                toolTip.SetToolTip(ratingStars[i], ratingText);

                if (truncated - i <= 0)
                {
                    ratingStars[i].Image = Properties.Resources.star_off;
                }
                else if (truncated - i <= 0.5)
                {
                    ratingStars[i].Image = Properties.Resources.star_half;
                }
                else
                {
                    ratingStars[i].Image = Properties.Resources.star_on;
                }
            }
        }

        private void SetDurationLabel(int totalSeconds)
        {
            TimeSpan duration = TimeSpan.FromSeconds(totalSeconds);

            if (duration.TotalHours >= 1)
                durationLabel.Text = string.Format("{0}:{1:D2}:{2:D2}",
                    (int)duration.TotalHours,
                    duration.Minutes,
                    duration.Seconds);
            else
                durationLabel.Text = string.Format("{0}:{1:D2}",
                    duration.Minutes,
                    duration.Seconds);

            durationLabel.Left = thumbnailBox.Width - durationLabel.Width - thumbnailBox.Padding.Horizontal - 1;
            durationLabel.Top = thumbnailBox.Height - durationLabel.Height - thumbnailBox.Padding.Vertical - 1;
        }

        private void LoadThumbnailAsync(string url)
        {
            if (!Settings.Default.ShowThumbs)
                return;

            var size = thumbnail.Size;

            ThreadPool.QueueUserWorkItem(delegate
            {
                Image image = LoadThumbnailImage(url, size);

                if (thumbnail.InvokeRequired)
                {
                    thumbnail.Invoke(new MethodInvoker(delegate
                    {
                        thumbnail.Image = image;
                    }));
                }
                else
                {
                    thumbnail.Image = image;
                }
            });
        }

        private Image LoadThumbnailImage(string url, Size size)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (Image source = Image.FromStream(stream))
            {
                int targetWidth = size.Width;
                int targetHeight = size.Height;

                float scale = Math.Max(
                    (float)targetWidth / source.Width,
                    (float)targetHeight / source.Height);

                int newWidth = (int)Math.Ceiling(source.Width * scale);
                int newHeight = (int)Math.Ceiling(source.Height * scale);

                Bitmap result = new Bitmap(newWidth, newHeight);

                using (Graphics g = Graphics.FromImage(result))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                    g.DrawImage(source, 0, 0, newWidth, newHeight);
                }

                return result;
            }
        }

        #endregion

        #region Events

        private void VideoListEntry_Load(object sender, EventArgs e)
        {

        }

        private void VideoListEntry_ParentChanged(object sender, EventArgs e)
        {
            parent = ParentForm as IVideoListParent;
        }

        #region Links and Panel Click

        private void titleLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                OpenInBrowser();
        }

        private void channelLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenChannelInBrowser();
        }

        private void thumbnail_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                WatchNow();
        }

        #endregion

        #region Context Menu Click

        private void watchNowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WatchNow();
        }

        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DownloadVideo();
        }

        private void openInBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenInBrowser();
        }

        #endregion

        #endregion

    }
}
