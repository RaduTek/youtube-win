using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace YouTube
{
    public partial class QueueForm : Form
    {
        public QueueForm()
        {
            InitializeComponent();
        }

        private void QueueForm_Deactivate(object sender, EventArgs e)
        {
            Hide();
        }

        private void QueueForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        public void QueueVideo(string videoId, string videoTitle, int videoDuration)
        {
            var playlistItem = new PlaylistItem();
            playlistItem.VideoId = videoId;
            playlistItem.Title = videoTitle;
            playlistItem.Duration = videoDuration;

            var listItem = new ListViewItem(videoTitle);
            listItem.Tag = playlistItem;
            listItem.SubItems.Add(Utils.FormatDurationSeconds(videoDuration));

            queueList.Items.Add(listItem);
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            queueList.Items.Clear();
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            var playlist = new List<PlaylistItem>();
            foreach (ListViewItem item in queueList.Items)
            {
                playlist.Add((PlaylistItem)item.Tag);
            }

            var tempFolder = Environment.GetEnvironmentVariable("TEMP");
            var filePath = Path.Combine(tempFolder, $"yt_queue_{DateTime.Now.ToFileTime()}.m3u");

            PlaylistUtil.WriteToM3U(playlist, filePath, true);

            Utils.PlayVideo(filePath);
        }
    }
}
