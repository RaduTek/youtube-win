using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
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
            LoadSettings();
        }

        private void LoadSettings()
        {
            // General
            if (Settings.Default.InstanceBaseUrl == null || Settings.Default.InstanceBaseUrl == "")
                instanceUrlText.Text = "http://";
            else
                instanceUrlText.Text = Settings.Default.InstanceBaseUrl;
            instanceTypeLabel.Text = Settings.Default.InstanceType;
            showThumbsCheck.Checked = Settings.Default.ShowThumbs;
            downloadBeforePlayCheck.Checked = Settings.Default.DownloadBeforePlay;
            themeComboBox.SelectedItem = Settings.Default.Theme;

            // Video Player
            videoQualityBox.SelectedItem = Settings.Default.VideoQuality;
            playerWmpRadio.Checked = !Settings.Default.PlayerCustomEnabled;
            playerWmpFullScreenCheck.Checked = Settings.Default.PlayerWmpFullscreen;
            playerCustomRadio.Checked = Settings.Default.PlayerCustomEnabled;
            playerCustomPathText.Text = Settings.Default.PlayerCustomPath;
            PlayerTypeSelected(null, null);

            // Downloads
            downloadFolderText.Text = Settings.Default.DownloadFolder;
        }

        private void ValidateSettings()
        {
            if (instanceUrlText.Text == null || instanceUrlText.Text == "")
            {
                throw new Exception("Instance Base URL cannot be empty.");
            }
            else if (!Uri.IsWellFormedUriString(instanceUrlText.Text, UriKind.Absolute))
            {
                throw new Exception("Instance Base URL is not a valid URL.");
            }

            DetectInstance();

            if (playerCustomRadio.Checked)
            {
                if (playerCustomPathText.Text == null || playerCustomPathText.Text == "")
                {
                    throw new Exception("Custom Player Path cannot be empty.");
                }
                else if (!File.Exists(playerCustomPathText.Text))
                {
                    throw new Exception("Custom Player Path does not exist.");
                }
            }

            if (downloadFolderText.Text == null || downloadFolderText.Text == "")
            {
                MessageBox.Show("Download Folder cannot be empty.");
            }
            try
            {
                if (!Directory.Exists(downloadFolderText.Text))
                {
                    Directory.CreateDirectory(downloadFolderText.Text);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Download Folder is not a valid path. " + ex.Message);
            }
        }

        private void SaveSettings()
        {
            // General
            Settings.Default.InstanceBaseUrl = instanceUrlText.Text;
            Settings.Default.InstanceType = instanceTypeLabel.Text;
            Settings.Default.ShowThumbs = showThumbsCheck.Checked;
            Settings.Default.DownloadBeforePlay = downloadBeforePlayCheck.Checked;
            Settings.Default.Theme = (string)themeComboBox.SelectedItem;

            // Video Player
            Settings.Default.VideoQuality = (string)videoQualityBox.SelectedItem;
            Settings.Default.PlayerWmpFullscreen = playerWmpFullScreenCheck.Checked;
            Settings.Default.PlayerCustomEnabled = playerCustomRadio.Checked;
            Settings.Default.PlayerCustomPath = playerCustomPathText.Text;

            // Downloads
            Settings.Default.DownloadFolder = downloadFolderText.Text;

            Settings.Default.Save();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateSettings();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Settings Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SaveSettings();
            Close();
        }

        private void videoPlayerOpenBtn_Click(object sender, EventArgs e)
        {
            var fd = new OpenFileDialog();
            fd.Filter = "Executable Files (*.exe)|*.exe";

            if (fd.ShowDialog() == DialogResult.OK)
            {
                playerCustomPathText.Text = fd.FileName;
            }
        }

        private void downloadFolderOpenBtn_Click(object sender, EventArgs e)
        {
            var dd = new FolderBrowserDialog();

            if (dd.ShowDialog() == DialogResult.OK)
            {
                downloadFolderText.Text = "\"" + dd.SelectedPath + "\" %1";
            }
        }

        private void PlayerTypeSelected(object sender, EventArgs e)
        {
            playerWmpGroupBox.Enabled = playerWmpRadio.Checked;
            playerCustomGroup.Enabled = playerCustomRadio.Checked;
        }

        private void detectInstanceButton_Click(object sender, EventArgs e)
        {
            DetectInstance();
        }

        private void DetectInstance()
        { 
            instanceTypeLabel.Text = DataApi.GetInstanceType(instanceUrlText.Text);

            if (instanceTypeLabel.Text == "Unknown")
            {
                MessageBox.Show("Unknown instance type, videos may not play as expected.", "Detect Instance");
            }
            else if (instanceTypeLabel.Text == "Bad Instance")
            {
                MessageBox.Show("Could not detect instance type, check the URL and network connection.", "Detect Instance");
            }
        }
    }
}
