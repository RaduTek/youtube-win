using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NativeToolStrip {
    public class NativeStatusStrip : StatusStrip {
        public NativeStatusStrip() {
            InitializeComponent();
        }

        private void InitializeComponent() {
            this.SuspendLayout();
            this.ResumeLayout(false);
        }
    }
}

