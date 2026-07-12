namespace YouTube
{
    partial class QueueForm
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
            this.rootPanel = new System.Windows.Forms.Panel();
            this.queueList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip = new NativeToolStrip.NativeToolStrip();
            this.titleLabel = new System.Windows.Forms.ToolStripLabel();
            this.playButton = new System.Windows.Forms.ToolStripButton();
            this.clearButton = new System.Windows.Forms.ToolStripButton();
            this.rootPanel.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // rootPanel
            // 
            this.rootPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rootPanel.Controls.Add(this.queueList);
            this.rootPanel.Controls.Add(this.toolStrip);
            this.rootPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rootPanel.Location = new System.Drawing.Point(0, 0);
            this.rootPanel.Name = "rootPanel";
            this.rootPanel.Padding = new System.Windows.Forms.Padding(2);
            this.rootPanel.Size = new System.Drawing.Size(240, 240);
            this.rootPanel.TabIndex = 1;
            // 
            // queueList
            // 
            this.queueList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.queueList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.queueList.HideSelection = false;
            this.queueList.Location = new System.Drawing.Point(2, 26);
            this.queueList.Name = "queueList";
            this.queueList.Size = new System.Drawing.Size(234, 210);
            this.queueList.TabIndex = 4;
            this.queueList.UseCompatibleStateImageBehavior = false;
            this.queueList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Video";
            this.columnHeader1.Width = 154;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Duration";
            this.columnHeader2.Width = 52;
            // 
            // toolStrip
            // 
            this.toolStrip.AutoSize = false;
            this.toolStrip.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.titleLabel,
            this.playButton,
            this.clearButton});
            this.toolStrip.Location = new System.Drawing.Point(2, 2);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.toolStrip.Size = new System.Drawing.Size(234, 24);
            this.toolStrip.TabIndex = 3;
            this.toolStrip.Text = "nativeToolStrip1";
            this.toolStrip.Theme = NativeToolStrip.NativeToolStripTheme.Transparent;
            // 
            // titleLabel
            // 
            this.titleLabel.Image = global::YouTube.Properties.Resources.List_empty;
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(58, 20);
            this.titleLabel.Text = "Queue";
            // 
            // playButton
            // 
            this.playButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.playButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.playButton.Image = global::YouTube.Properties.Resources.control_play_blue;
            this.playButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.playButton.Margin = new System.Windows.Forms.Padding(0);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(23, 23);
            this.playButton.Text = "Play Queue";
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.clearButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.clearButton.Image = global::YouTube.Properties.Resources.bin_closed;
            this.clearButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearButton.Margin = new System.Windows.Forms.Padding(0);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(23, 23);
            this.clearButton.Text = "Clear Queue";
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // QueueForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(240, 240);
            this.ControlBox = false;
            this.Controls.Add(this.rootPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "QueueForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Queue";
            this.Deactivate += new System.EventHandler(this.QueueForm_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.QueueForm_FormClosing);
            this.rootPanel.ResumeLayout(false);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel rootPanel;
        private NativeToolStrip.NativeToolStrip toolStrip;
        private System.Windows.Forms.ToolStripLabel titleLabel;
        private System.Windows.Forms.ToolStripButton clearButton;
        private System.Windows.Forms.ToolStripButton playButton;
        private System.Windows.Forms.ListView queueList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}