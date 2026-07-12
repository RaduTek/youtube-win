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
                parent.WatchVideo(_entry.YouTubeId.Id, _entry.Title);
        }

        private void DownloadVideo()
        {
            if (_entry.YouTubeId.Id != null)
                parent.DownloadVideo(_entry.YouTubeId.Id, _entry.Title);
        }

        private void AddToQueue()
        {
            if (_entry.YouTubeId.Id != null)
                parent.QueueVideo(_entry.YouTubeId.Id, _entry.Title, _entry.Media.Duration.Seconds);
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
            toolTip.SetToolTip(dateLabel, _entry.Published.ToLongDateString());

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
            durationLabel.Text = Utils.FormatDurationSeconds(totalSeconds);
            durationLabel.Left = thumbnail.Width - durationLabel.Width;
            durationLabel.Top = thumbnail.Height - durationLabel.Height;
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
                        thumbnail.BackgroundImage = image;
                    }));
                }
                else
                {
                    thumbnail.BackgroundImage = image;
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

                int scaledWidth = (int)Math.Ceiling(source.Width * scale);
                int scaledHeight = (int)Math.Ceiling(source.Height * scale);

                int offsetX = (scaledWidth - targetWidth) / 2;
                int offsetY = (scaledHeight - targetHeight) / 2;

                Bitmap result = new Bitmap(targetWidth, targetHeight);
                result.SetResolution(source.HorizontalResolution, source.VerticalResolution);

                using (Graphics g = Graphics.FromImage(result))
                {
                    //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    //g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                    g.DrawImage(source, -offsetX, -offsetY, scaledWidth, scaledHeight);
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
        private void queueButton_Click(object sender, EventArgs e)
        {
            AddToQueue();
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
        private void addToQueueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddToQueue();
        }

        #endregion

        #endregion
    }
}
