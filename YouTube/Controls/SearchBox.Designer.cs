
namespace YouTube.Controls
{
    partial class SearchBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel = new YouTube.ExControls.ExPanel();
            this.textBox = new System.Windows.Forms.TextBox();
            this.button = new YouTube.ExControls.ExButton();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.Transparent;
            this.panel.BackKey = "SearchTextBox";
            this.panel.BackMargins = new System.Windows.Forms.Padding(4);
            this.panel.Controls.Add(this.textBox);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(243, 24);
            this.panel.TabIndex = 3;
            this.panel.Click += new System.EventHandler(this.backPanel_Click);
            // 
            // textBox
            // 
            this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox.Location = new System.Drawing.Point(5, 6);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(235, 13);
            this.textBox.TabIndex = 0;
            this.textBox.TextChanged += new System.EventHandler(this.searchTextBox_TextChanged);
            this.textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchTextBox_KeyDown);
            // 
            // button
            // 
            this.button.BackKey = "SearchButton";
            this.button.BackMargins = new System.Windows.Forms.Padding(2, 4, 5, 4);
            this.button.Dock = System.Windows.Forms.DockStyle.Right;
            this.button.EnableTransparency = true;
            this.button.Icon = null;
            this.button.IconKey = "";
            this.button.IconTransparencyKey = System.Drawing.Color.Magenta;
            this.button.Location = new System.Drawing.Point(243, 0);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(57, 24);
            this.button.TabIndex = 1;
            this.button.Text = "searchBtn";
            this.button.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.button.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // SearchBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel);
            this.Controls.Add(this.button);
            this.Name = "SearchBox";
            this.Size = new System.Drawing.Size(300, 24);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ExControls.ExPanel panel;
        private ExControls.ExButton button;
        private System.Windows.Forms.TextBox textBox;
    }
}
