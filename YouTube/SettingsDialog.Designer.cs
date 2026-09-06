namespace YouTube
{
    partial class SettingsDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.instanceUrlLabel = new System.Windows.Forms.Label();
            this.instanceUrlText = new System.Windows.Forms.TextBox();
            this.downloadFolderLabel = new System.Windows.Forms.Label();
            this.downloadFolderText = new System.Windows.Forms.TextBox();
            this.downloadFolderOpenBtn = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.generalTab = new System.Windows.Forms.TabPage();
            this.detectInstanceButton = new System.Windows.Forms.Button();
            this.instanceTypeLabel = new System.Windows.Forms.Label();
            this.instanceTypeHintLabel = new System.Windows.Forms.Label();
            this.videoPlayerTab = new System.Windows.Forms.TabPage();
            this.videoQualityLabel = new System.Windows.Forms.Label();
            this.videoQualityBox = new System.Windows.Forms.ComboBox();
            this.downloadsTab = new System.Windows.Forms.TabPage();
            this.largeControlsCheck = new System.Windows.Forms.CheckBox();
            this.tabControl.SuspendLayout();
            this.generalTab.SuspendLayout();
            this.videoPlayerTab.SuspendLayout();
            this.downloadsTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(173, 223);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "&OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(254, 223);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // instanceUrlLabel
            // 
            this.instanceUrlLabel.AutoSize = true;
            this.instanceUrlLabel.Location = new System.Drawing.Point(7, 11);
            this.instanceUrlLabel.Name = "instanceUrlLabel";
            this.instanceUrlLabel.Size = new System.Drawing.Size(76, 13);
            this.instanceUrlLabel.TabIndex = 2;
            this.instanceUrlLabel.Text = "Instance URL:";
            // 
            // instanceUrlText
            // 
            this.instanceUrlText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.instanceUrlText.Location = new System.Drawing.Point(10, 27);
            this.instanceUrlText.Name = "instanceUrlText";
            this.instanceUrlText.Size = new System.Drawing.Size(300, 20);
            this.instanceUrlText.TabIndex = 3;
            // 
            // downloadFolderLabel
            // 
            this.downloadFolderLabel.AutoSize = true;
            this.downloadFolderLabel.Location = new System.Drawing.Point(7, 11);
            this.downloadFolderLabel.Name = "downloadFolderLabel";
            this.downloadFolderLabel.Size = new System.Drawing.Size(92, 13);
            this.downloadFolderLabel.TabIndex = 7;
            this.downloadFolderLabel.Text = "Downloads folder:";
            // 
            // downloadFolderText
            // 
            this.downloadFolderText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadFolderText.Location = new System.Drawing.Point(10, 27);
            this.downloadFolderText.Name = "downloadFolderText";
            this.downloadFolderText.Size = new System.Drawing.Size(264, 20);
            this.downloadFolderText.TabIndex = 8;
            // 
            // downloadFolderOpenBtn
            // 
            this.downloadFolderOpenBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadFolderOpenBtn.Location = new System.Drawing.Point(280, 26);
            this.downloadFolderOpenBtn.Name = "downloadFolderOpenBtn";
            this.downloadFolderOpenBtn.Size = new System.Drawing.Size(32, 22);
            this.downloadFolderOpenBtn.TabIndex = 9;
            this.downloadFolderOpenBtn.Text = "...";
            this.downloadFolderOpenBtn.UseVisualStyleBackColor = true;
            this.downloadFolderOpenBtn.Click += new System.EventHandler(this.downloadFolderOpenBtn_Click);
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.generalTab);
            this.tabControl.Controls.Add(this.videoPlayerTab);
            this.tabControl.Controls.Add(this.downloadsTab);
            this.tabControl.Location = new System.Drawing.Point(6, 7);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(326, 212);
            this.tabControl.TabIndex = 15;
            // 
            // generalTab
            // 
            this.generalTab.Controls.Add(this.detectInstanceButton);
            this.generalTab.Controls.Add(this.instanceTypeLabel);
            this.generalTab.Controls.Add(this.instanceTypeHintLabel);
            this.generalTab.Controls.Add(this.instanceUrlLabel);
            this.generalTab.Controls.Add(this.instanceUrlText);
            this.generalTab.Location = new System.Drawing.Point(4, 22);
            this.generalTab.Name = "generalTab";
            this.generalTab.Padding = new System.Windows.Forms.Padding(3);
            this.generalTab.Size = new System.Drawing.Size(318, 186);
            this.generalTab.TabIndex = 0;
            this.generalTab.Text = "General";
            this.generalTab.UseVisualStyleBackColor = true;
            // 
            // detectInstanceButton
            // 
            this.detectInstanceButton.Location = new System.Drawing.Point(235, 53);
            this.detectInstanceButton.Name = "detectInstanceButton";
            this.detectInstanceButton.Size = new System.Drawing.Size(75, 23);
            this.detectInstanceButton.TabIndex = 19;
            this.detectInstanceButton.Text = "Detect";
            this.detectInstanceButton.UseVisualStyleBackColor = true;
            this.detectInstanceButton.Click += new System.EventHandler(this.detectInstanceButton_Click);
            // 
            // instanceTypeLabel
            // 
            this.instanceTypeLabel.AutoSize = true;
            this.instanceTypeLabel.Location = new System.Drawing.Point(110, 58);
            this.instanceTypeLabel.Name = "instanceTypeLabel";
            this.instanceTypeLabel.Size = new System.Drawing.Size(53, 13);
            this.instanceTypeLabel.TabIndex = 18;
            this.instanceTypeLabel.Text = "Unknown";
            // 
            // instanceTypeHintLabel
            // 
            this.instanceTypeHintLabel.AutoSize = true;
            this.instanceTypeHintLabel.Location = new System.Drawing.Point(7, 58);
            this.instanceTypeHintLabel.Name = "instanceTypeHintLabel";
            this.instanceTypeHintLabel.Size = new System.Drawing.Size(97, 13);
            this.instanceTypeHintLabel.TabIndex = 17;
            this.instanceTypeHintLabel.Text = "Instance Backend:";
            // 
            // videoPlayerTab
            // 
            this.videoPlayerTab.Controls.Add(this.largeControlsCheck);
            this.videoPlayerTab.Controls.Add(this.videoQualityLabel);
            this.videoPlayerTab.Controls.Add(this.videoQualityBox);
            this.videoPlayerTab.Location = new System.Drawing.Point(4, 22);
            this.videoPlayerTab.Name = "videoPlayerTab";
            this.videoPlayerTab.Padding = new System.Windows.Forms.Padding(3);
            this.videoPlayerTab.Size = new System.Drawing.Size(318, 186);
            this.videoPlayerTab.TabIndex = 1;
            this.videoPlayerTab.Text = "Video Player";
            this.videoPlayerTab.UseVisualStyleBackColor = true;
            // 
            // videoQualityLabel
            // 
            this.videoQualityLabel.AutoSize = true;
            this.videoQualityLabel.Location = new System.Drawing.Point(8, 20);
            this.videoQualityLabel.Name = "videoQualityLabel";
            this.videoQualityLabel.Size = new System.Drawing.Size(72, 13);
            this.videoQualityLabel.TabIndex = 22;
            this.videoQualityLabel.Text = "Video Quality:";
            // 
            // videoQualityBox
            // 
            this.videoQualityBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.videoQualityBox.FormattingEnabled = true;
            this.videoQualityBox.Items.AddRange(new object[] {
            "360p",
            "480p",
            "720p",
            "1080p"});
            this.videoQualityBox.Location = new System.Drawing.Point(92, 16);
            this.videoQualityBox.Name = "videoQualityBox";
            this.videoQualityBox.Size = new System.Drawing.Size(82, 21);
            this.videoQualityBox.TabIndex = 21;
            // 
            // downloadsTab
            // 
            this.downloadsTab.Controls.Add(this.downloadFolderLabel);
            this.downloadsTab.Controls.Add(this.downloadFolderText);
            this.downloadsTab.Controls.Add(this.downloadFolderOpenBtn);
            this.downloadsTab.Location = new System.Drawing.Point(4, 22);
            this.downloadsTab.Name = "downloadsTab";
            this.downloadsTab.Padding = new System.Windows.Forms.Padding(3);
            this.downloadsTab.Size = new System.Drawing.Size(318, 186);
            this.downloadsTab.TabIndex = 2;
            this.downloadsTab.Text = "Downloads";
            this.downloadsTab.UseVisualStyleBackColor = true;
            // 
            // largeControlsCheck
            // 
            this.largeControlsCheck.AutoSize = true;
            this.largeControlsCheck.Location = new System.Drawing.Point(11, 55);
            this.largeControlsCheck.Name = "largeControlsCheck";
            this.largeControlsCheck.Size = new System.Drawing.Size(228, 17);
            this.largeControlsCheck.TabIndex = 23;
            this.largeControlsCheck.Text = "Use large controls in full screen (for tablets)";
            this.largeControlsCheck.UseVisualStyleBackColor = true;
            // 
            // SettingsDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(339, 254);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "YouTube Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.tabControl.ResumeLayout(false);
            this.generalTab.ResumeLayout(false);
            this.generalTab.PerformLayout();
            this.videoPlayerTab.ResumeLayout(false);
            this.videoPlayerTab.PerformLayout();
            this.downloadsTab.ResumeLayout(false);
            this.downloadsTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label instanceUrlLabel;
        private System.Windows.Forms.TextBox instanceUrlText;
        private System.Windows.Forms.Label downloadFolderLabel;
        private System.Windows.Forms.TextBox downloadFolderText;
        private System.Windows.Forms.Button downloadFolderOpenBtn;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage generalTab;
        private System.Windows.Forms.TabPage videoPlayerTab;
        private System.Windows.Forms.TabPage downloadsTab;
        private System.Windows.Forms.Button detectInstanceButton;
        private System.Windows.Forms.Label instanceTypeLabel;
        private System.Windows.Forms.Label instanceTypeHintLabel;
        private System.Windows.Forms.Label videoQualityLabel;
        private System.Windows.Forms.ComboBox videoQualityBox;
        private System.Windows.Forms.CheckBox largeControlsCheck;
    }
}