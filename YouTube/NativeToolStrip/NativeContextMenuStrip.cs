using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NativeToolStrip {
    public class NativeContextMenuStrip : ContextMenuStrip {
        public NativeContextMenuStrip() {
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
    }
}
