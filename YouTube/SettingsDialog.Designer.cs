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
            this.instanceUrlTextBox = new System.Windows.Forms.TextBox();
            this.videoPlayerLabel = new System.Windows.Forms.Label();
            this.videoPlayerTextBox = new System.Windows.Forms.TextBox();
            this.videoPlayerOpenBtn = new System.Windows.Forms.Button();
            this.downloadFolderLabel = new System.Windows.Forms.Label();
            this.downloadFolderTextBox = new System.Windows.Forms.TextBox();
            this.downloadFolderOpenBtn = new System.Windows.Forms.Button();
            this.showThumbsCheck = new System.Windows.Forms.CheckBox();
            this.enableHdCheck = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(256, 196);
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
            this.cancelButton.Location = new System.Drawing.Point(337, 196);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // instanceUrlLabel
            // 
            this.instanceUrlLabel.AutoSize = true;
            this.instanceUrlLabel.Location = new System.Drawing.Point(12, 21);
            this.instanceUrlLabel.Name = "instanceUrlLabel";
            this.instanceUrlLabel.Size = new System.Drawing.Size(73, 13);
            this.instanceUrlLabel.TabIndex = 2;
            this.instanceUrlLabel.Text = "Instance URL";
            // 
            // instanceUrlTextBox
            // 
            this.instanceUrlTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.instanceUrlTextBox.Location = new System.Drawing.Point(115, 18);
            this.instanceUrlTextBox.Name = "instanceUrlTextBox";
            this.instanceUrlTextBox.Size = new System.Drawing.Size(259, 20);
            this.instanceUrlTextBox.TabIndex = 3;
            // 
            // videoPlayerLabel
            // 
            this.videoPlayerLabel.AutoSize = true;
            this.videoPlayerLabel.Location = new System.Drawing.Point(12, 59);
            this.videoPlayerLabel.Name = "videoPlayerLabel";
            this.videoPlayerLabel.Size = new System.Drawing.Size(66, 13);
            this.videoPlayerLabel.TabIndex = 4;
            this.videoPlayerLabel.Text = "Video Player";
            // 
            // videoPlayerTextBox
            // 
            this.videoPlayerTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.videoPlayerTextBox.Location = new System.Drawing.Point(115, 56);
            this.videoPlayerTextBox.Name = "videoPlayerTextBox";
            this.videoPlayerTextBox.Size = new System.Drawing.Size(259, 20);
            this.videoPlayerTextBox.TabIndex = 5;
            // 
            // videoPlayerOpenBtn
            // 
            this.videoPlayerOpenBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.videoPlayerOpenBtn.Location = new System.Drawing.Point(380, 56);
            this.videoPlayerOpenBtn.Name = "videoPlayerOpenBtn";
            this.videoPlayerOpenBtn.Size = new System.Drawing.Size(32, 21);
            this.videoPlayerOpenBtn.TabIndex = 6;
            this.videoPlayerOpenBtn.Text = "...";
            this.videoPlayerOpenBtn.UseVisualStyleBackColor = true;
            this.videoPlayerOpenBtn.Click += new System.EventHandler(this.videoPlayerOpenBtn_Click);
            // 
            // downloadFolderLabel
            // 
            this.downloadFolderLabel.AutoSize = true;
            this.downloadFolderLabel.Location = new System.Drawing.Point(12, 100);
            this.downloadFolderLabel.Name = "downloadFolderLabel";
            this.downloadFolderLabel.Size = new System.Drawing.Size(87, 13);
            this.downloadFolderLabel.TabIndex = 7;
            this.downloadFolderLabel.Text = "Download Folder";
            // 
            // downloadFolderTextBox
            // 
            this.downloadFolderTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadFolderTextBox.Location = new System.Drawing.Point(115, 97);
            this.downloadFolderTextBox.Name = "downloadFolderTextBox";
            this.downloadFolderTextBox.Size = new System.Drawing.Size(259, 20);
            this.downloadFolderTextBox.TabIndex = 8;
            // 
            // downloadFolderOpenBtn
            // 
            this.downloadFolderOpenBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadFolderOpenBtn.Location = new System.Drawing.Point(380, 96);
            this.downloadFolderOpenBtn.Name = "downloadFolderOpenBtn";
            this.downloadFolderOpenBtn.Size = new System.Drawing.Size(32, 21);
            this.downloadFolderOpenBtn.TabIndex = 9;
            this.downloadFolderOpenBtn.Text = "...";
            this.downloadFolderOpenBtn.UseVisualStyleBackColor = true;
            this.downloadFolderOpenBtn.Click += new System.EventHandler(this.downloadFolderOpenBtn_Click);
            // 
            // showThumbsCheck
            // 
            this.showThumbsCheck.AutoSize = true;
            this.showThumbsCheck.Location = new System.Drawing.Point(15, 143);
            this.showThumbsCheck.Name = "showThumbsCheck";
            this.showThumbsCheck.Size = new System.Drawing.Size(110, 17);
            this.showThumbsCheck.TabIndex = 10;
            this.showThumbsCheck.Text = "Show Thumbnails";
            this.showThumbsCheck.UseVisualStyleBackColor = true;
            // 
            // enableHdCheck
            // 
            this.enableHdCheck.AutoSize = true;
            this.enableHdCheck.Location = new System.Drawing.Point(15, 170);
            this.enableHdCheck.Name = "enableHdCheck";
            this.enableHdCheck.Size = new System.Drawing.Size(108, 17);
            this.enableHdCheck.TabIndex = 11;
            this.enableHdCheck.Text = "Enable HD Video";
            this.enableHdCheck.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(424, 231);
            this.Controls.Add(this.enableHdCheck);
            this.Controls.Add(this.showThumbsCheck);
            this.Controls.Add(this.downloadFolderOpenBtn);
            this.Controls.Add(this.downloadFolderTextBox);
            this.Controls.Add(this.downloadFolderLabel);
            this.Controls.Add(this.videoPlayerOpenBtn);
            this.Controls.Add(this.videoPlayerTextBox);
            this.Controls.Add(this.videoPlayerLabel);
            this.Controls.Add(this.instanceUrlTextBox);
            this.Controls.Add(this.instanceUrlLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "YouTube Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label instanceUrlLabel;
        private System.Windows.Forms.TextBox instanceUrlTextBox;
        private System.Windows.Forms.Label videoPlayerLabel;
        private System.Windows.Forms.TextBox videoPlayerTextBox;
        private System.Windows.Forms.Button videoPlayerOpenBtn;
        private System.Windows.Forms.Label downloadFolderLabel;
        private System.Windows.Forms.TextBox downloadFolderTextBox;
        private System.Windows.Forms.Button downloadFolderOpenBtn;
        private System.Windows.Forms.CheckBox showThumbsCheck;
        private System.Windows.Forms.CheckBox enableHdCheck;
    }
}