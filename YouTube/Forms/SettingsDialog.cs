using System;
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

            // Video Player
            videoQualityBox.SelectedItem = Settings.Default.VideoQuality;
            autoPlayCheck.Checked = Settings.Default.AutoPlayVideo;
            largeControlsCheck.Checked = Settings.Default.LargePlayerControls;

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

            // Video Player
            Settings.Default.VideoQuality = (string)videoQualityBox.SelectedItem;
            Settings.Default.AutoPlayVideo = autoPlayCheck.Checked;
            Settings.Default.LargePlayerControls = largeControlsCheck.Checked;

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

        private void downloadFolderOpenBtn_Click(object sender, EventArgs e)
        {
            var dd = new FolderBrowserDialog();

            if (dd.ShowDialog() == DialogResult.OK)
            {
                downloadFolderText.Text = "\"" + dd.SelectedPath + "\" %1";
            }
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
