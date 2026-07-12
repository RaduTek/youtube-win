using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NativeToolStrip {
    public class NativeMenuStrip : MenuStrip {
        public NativeMenuStrip() {
            InitializeComponent();

            this.Renderer = new NativeToolStripRenderer(_Theme);
        }

        private NativeToolStripTheme _Theme = NativeToolStripTheme.Toolbar;
        public NativeToolStripTheme Theme {
            get { return _Theme; }
            set {
                _Theme = value;
                this.Renderer = new NativeToolStripRenderer(_Theme);
            }
        }

        private void InitializeComponent() {
            this.SuspendLayout();
            // 
            // NativeMenuStrip
            // 
            this.AutoSize = false;
            this.Padding = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.Size = new System.Drawing.Size(200, 22);
            this.ResumeLayout(false);

        }
    }
}
