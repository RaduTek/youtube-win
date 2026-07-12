using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NativeToolStrip {
    public class NativeToolStrip : ToolStrip {
        public NativeToolStrip() {
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
            // NativeToolStrip
            // 
            this.AutoSize = false;
            this.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Size = new System.Drawing.Size(100, 26);
            this.ResumeLayout(false);

        }
    }
}
