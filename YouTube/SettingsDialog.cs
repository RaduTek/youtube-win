using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace YouTube
{
    public partial class SettingsDialog : Form
    {
        public SettingsDialog()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            instanceUrlTextBox.Text = Settings.Default.InstanceBaseUrl;
            videoPlayerTextBox.Text = Settings.Default.VideoPlayer;
            downloadFolderTextBox.Text = Settings.Default.DownloadFolder;
            showThumbsCheck.Checked = Settings.Default.ShowThumbs;
            enableHdCheck.Checked = Settings.Default.EnableHd;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Settings.Default.InstanceBaseUrl = instanceUrlTextBox.Text;
            Settings.Default.VideoPlayer = videoPlayerTextBox.Text;
            Settings.Default.DownloadFolder = downloadFolderTextBox.Text;
            Settings.Default.ShowThumbs = showThumbsCheck.Checked;
            Settings.Default.EnableHd = enableHdCheck.Checked;

            Settings.Default.Save();
            this.Close();
        }

        private void videoPlayerOpenBtn_Click(object sender, EventArgs e)
        {
            var fd = new OpenFileDialog();
            fd.Filter = "Executable Files (*.exe)|*.exe";

            if (fd.ShowDialog() == DialogResult.OK)
            {
                videoPlayerTextBox.Text = fd.FileName;
            }
        }

        private void downloadFolderOpenBtn_Click(object sender, EventArgs e)
        {
            var dd = new FolderBrowserDialog();

            if (dd.ShowDialog() == DialogResult.OK)
            {
                downloadFolderTextBox.Text = dd.SelectedPath;
            }
        }
    }
}
