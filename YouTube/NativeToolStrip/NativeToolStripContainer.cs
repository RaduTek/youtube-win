using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NativeToolStrip {
    public class NativeToolStripContainer : ToolStripContainer {
        public NativeToolStripContainer() {
            // Set row margin for each panel
            this.TopToolStripPanel.RowMargin = new Padding(0);
            this.BottomToolStripPanel.RowMargin = new Padding(0);
            this.LeftToolStripPanel.RowMargin = new Padding(0);
            this.RightToolStripPanel.RowMargin = new Padding(0);
            // Set renderer for each panel
            this.TopToolStripPanel.Renderer = new NativeToolStripRenderer(_Theme);
            this.BottomToolStripPanel.Renderer = new NativeToolStripRenderer(_Theme);
            this.LeftToolStripPanel.Renderer = new NativeToolStripRenderer(_Theme);
            this.RightToolStripPanel.Renderer = new NativeToolStripRenderer(_Theme);
        }

        // Theme property
        private NativeToolStripTheme _Theme = NativeToolStripTheme.Toolbar;
        public NativeToolStripTheme Theme {
            get { return _Theme; }
            set {
                _Theme = value;
                this.TopToolStripPanel.Renderer = new NativeToolStripRenderer(_Theme);
                this.BottomToolStripPanel.Renderer = new NativeToolStripRenderer(_Theme);
                this.LeftToolStripPanel.Renderer = new NativeToolStripRenderer(_Theme);
                this.RightToolStripPanel.Renderer = new NativeToolStripRenderer(_Theme);
            }
        }
    }
}

