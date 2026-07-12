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
            this.playerCustomPathText = new System.Windows.Forms.TextBox();
            this.playerCustomPathButton = new System.Windows.Forms.Button();
            this.downloadFolderLabel = new System.Windows.Forms.Label();
            this.downloadFolderText = new System.Windows.Forms.TextBox();
            this.downloadFolderOpenBtn = new System.Windows.Forms.Button();
            this.showThumbsCheck = new System.Windows.Forms.CheckBox();
            this.enableHdCheck = new System.Windows.Forms.CheckBox();
            this.playerCustomRadio = new System.Windows.Forms.RadioButton();
            this.playerWmpRadio = new System.Windows.Forms.RadioButton();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.generalTab = new System.Windows.Forms.TabPage();
            this.videoPlayerTab = new System.Windows.Forms.TabPage();
            this.playerWmpGroupBox = new System.Windows.Forms.GroupBox();
            this.playerWmpFullScreenCheck = new System.Windows.Forms.CheckBox();
            this.playerCustomGroup = new System.Windows.Forms.GroupBox();
            this.playerCustomLabel = new System.Windows.Forms.Label();
            this.downloadsTab = new System.Windows.Forms.TabPage();
            this.downloadBeforePlayCheck = new System.Windows.Forms.CheckBox();
            this.tabControl.SuspendLayout();
            this.generalTab.SuspendLayout();
            this.videoPlayerTab.SuspendLayout();
            this.playerWmpGroupBox.SuspendLayout();
            this.playerCustomGroup.SuspendLayout();
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
            // playerCustomPathText
            // 
            this.playerCustomPathText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.playerCustomPathText.Location = new System.Drawing.Point(9, 32);
            this.playerCustomPathText.Name = "playerCustomPathText";
            this.playerCustomPathText.Size = new System.Drawing.Size(250, 20);
            this.playerCustomPathText.TabIndex = 5;
            // 
            // playerCustomPathButton
            // 
            this.playerCustomPathButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.playerCustomPathButton.Location = new System.Drawing.Point(265, 31);
            this.playerCustomPathButton.Name = "playerCustomPathButton";
            this.playerCustomPathButton.Size = new System.Drawing.Size(32, 22);
            this.playerCustomPathButton.TabIndex = 6;
            this.playerCustomPathButton.Text = "...";
            this.playerCustomPathButton.UseVisualStyleBackColor = true;
            this.playerCustomPathButton.Click += new System.EventHandler(this.videoPlayerOpenBtn_Click);
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
            // showThumbsCheck
            // 
            this.showThumbsCheck.AutoSize = true;
            this.showThumbsCheck.Location = new System.Drawing.Point(10, 65);
            this.showThumbsCheck.Name = "showThumbsCheck";
            this.showThumbsCheck.Size = new System.Drawing.Size(110, 17);
            this.showThumbsCheck.TabIndex = 10;
            this.showThumbsCheck.Text = "Show Thumbnails";
            this.showThumbsCheck.UseVisualStyleBackColor = true;
            // 
            // enableHdCheck
            // 
            this.enableHdCheck.AutoSize = true;
            this.enableHdCheck.Location = new System.Drawing.Point(10, 88);
            this.enableHdCheck.Name = "enableHdCheck";
            this.enableHdCheck.Size = new System.Drawing.Size(108, 17);
            this.enableHdCheck.TabIndex = 11;
            this.enableHdCheck.Text = "Enable HD Video";
            this.enableHdCheck.UseVisualStyleBackColor = true;
            // 
            // playerCustomRadio
            // 
            this.playerCustomRadio.AutoSize = true;
            this.playerCustomRadio.Location = new System.Drawing.Point(16, 112);
            this.playerCustomRadio.Name = "playerCustomRadio";
            this.playerCustomRadio.Size = new System.Drawing.Size(60, 17);
            this.playerCustomRadio.TabIndex = 13;
            this.playerCustomRadio.Text = "Custom";
            this.playerCustomRadio.UseVisualStyleBackColor = true;
            this.playerCustomRadio.CheckedChanged += new System.EventHandler(this.PlayerTypeSelected);
            // 
            // playerWmpRadio
            // 
            this.playerWmpRadio.AutoSize = true;
            this.playerWmpRadio.Checked = true;
            this.playerWmpRadio.Location = new System.Drawing.Point(13, 8);
            this.playerWmpRadio.Name = "playerWmpRadio";
            this.playerWmpRadio.Size = new System.Drawing.Size(133, 17);
            this.playerWmpRadio.TabIndex = 12;
            this.playerWmpRadio.TabStop = true;
            this.playerWmpRadio.Text = "Windows Media Player";
            this.playerWmpRadio.UseVisualStyleBackColor = true;
            this.playerWmpRadio.CheckedChanged += new System.EventHandler(this.PlayerTypeSelected);
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
            this.generalTab.Controls.Add(this.downloadBeforePlayCheck);
            this.generalTab.Controls.Add(this.instanceUrlLabel);
            this.generalTab.Controls.Add(this.instanceUrlText);
            this.generalTab.Controls.Add(this.enableHdCheck);
            this.generalTab.Controls.Add(this.showThumbsCheck);
            this.generalTab.Location = new System.Drawing.Point(4, 22);
            this.generalTab.Name = "generalTab";
            this.generalTab.Padding = new System.Windows.Forms.Padding(3);
            this.generalTab.Size = new System.Drawing.Size(318, 186);
            this.generalTab.TabIndex = 0;
            this.generalTab.Text = "General";
            this.generalTab.UseVisualStyleBackColor = true;
            // 
            // videoPlayerTab
            // 
            this.videoPlayerTab.Controls.Add(this.playerWmpRadio);
            this.videoPlayerTab.Controls.Add(this.playerCustomRadio);
            this.videoPlayerTab.Controls.Add(this.playerWmpGroupBox);
            this.videoPlayerTab.Controls.Add(this.playerCustomGroup);
            this.videoPlayerTab.Location = new System.Drawing.Point(4, 22);
            this.videoPlayerTab.Name = "videoPlayerTab";
            this.videoPlayerTab.Padding = new System.Windows.Forms.Padding(3);
            this.videoPlayerTab.Size = new System.Drawing.Size(318, 186);
            this.videoPlayerTab.TabIndex = 1;
            this.videoPlayerTab.Text = "Video Player";
            this.videoPlayerTab.UseVisualStyleBackColor = true;
            // 
            // playerWmpGroupBox
            // 
            this.playerWmpGroupBox.Controls.Add(this.playerWmpFullScreenCheck);
            this.playerWmpGroupBox.Location = new System.Drawing.Point(6, 10);
            this.playerWmpGroupBox.Name = "playerWmpGroupBox";
            this.playerWmpGroupBox.Size = new System.Drawing.Size(304, 96);
            this.playerWmpGroupBox.TabIndex = 14;
            this.playerWmpGroupBox.TabStop = false;
            // 
            // playerWmpFullScreenCheck
            // 
            this.playerWmpFullScreenCheck.AutoSize = true;
            this.playerWmpFullScreenCheck.Location = new System.Drawing.Point(10, 21);
            this.playerWmpFullScreenCheck.Name = "playerWmpFullScreenCheck";
            this.playerWmpFullScreenCheck.Size = new System.Drawing.Size(108, 17);
            this.playerWmpFullScreenCheck.TabIndex = 0;
            this.playerWmpFullScreenCheck.Text = "Play in full screen";
            this.playerWmpFullScreenCheck.UseVisualStyleBackColor = true;
            // 
            // playerCustomGroup
            // 
            this.playerCustomGroup.Controls.Add(this.playerCustomLabel);
            this.playerCustomGroup.Controls.Add(this.playerCustomPathText);
            this.playerCustomGroup.Controls.Add(this.playerCustomPathButton);
            this.playerCustomGroup.Location = new System.Drawing.Point(6, 115);
            this.playerCustomGroup.Name = "playerCustomGroup";
            this.playerCustomGroup.Size = new System.Drawing.Size(303, 64);
            this.playerCustomGroup.TabIndex = 15;
            this.playerCustomGroup.TabStop = false;
            // 
            // playerCustomLabel
            // 
            this.playerCustomLabel.AutoSize = true;
            this.playerCustomLabel.Location = new System.Drawing.Point(6, 16);
            this.playerCustomLabel.Name = "playerCustomLabel";
            this.playerCustomLabel.Size = new System.Drawing.Size(104, 13);
            this.playerCustomLabel.TabIndex = 0;
            this.playerCustomLabel.Text = "Path to video player:";
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
            // downloadBeforePlayCheck
            // 
            this.downloadBeforePlayCheck.AutoSize = true;
            this.downloadBeforePlayCheck.Location = new System.Drawing.Point(10, 111);
            this.downloadBeforePlayCheck.Name = "downloadBeforePlayCheck";
            this.downloadBeforePlayCheck.Size = new System.Drawing.Size(172, 17);
            this.downloadBeforePlayCheck.TabIndex = 14;
            this.downloadBeforePlayCheck.Text = "Download video before playing";
            this.downloadBeforePlayCheck.UseVisualStyleBackColor = true;
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
            this.playerWmpGroupBox.ResumeLayout(false);
            this.playerWmpGroupBox.PerformLayout();
            this.playerCustomGroup.ResumeLayout(false);
            this.playerCustomGroup.PerformLayout();
            this.downloadsTab.ResumeLayout(false);
            this.downloadsTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label instanceUrlLabel;
        private System.Windows.Forms.TextBox instanceUrlText;
        private System.Windows.Forms.TextBox playerCustomPathText;
        private System.Windows.Forms.Button playerCustomPathButton;
        private System.Windows.Forms.Label downloadFolderLabel;
        private System.Windows.Forms.TextBox downloadFolderText;
        private System.Windows.Forms.Button downloadFolderOpenBtn;
        private System.Windows.Forms.CheckBox showThumbsCheck;
        private System.Windows.Forms.CheckBox enableHdCheck;
        private System.Windows.Forms.RadioButton playerWmpRadio;
        private System.Windows.Forms.RadioButton playerCustomRadio;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage generalTab;
        private System.Windows.Forms.TabPage videoPlayerTab;
        private System.Windows.Forms.TabPage downloadsTab;
        private System.Windows.Forms.GroupBox playerWmpGroupBox;
        private System.Windows.Forms.GroupBox playerCustomGroup;
        private System.Windows.Forms.Label playerCustomLabel;
        private System.Windows.Forms.CheckBox playerWmpFullScreenCheck;
        private System.Windows.Forms.CheckBox downloadBeforePlayCheck;
    }
}