using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ImageControls {
    class ImagePanel : Panel {
        public ImagePanel() {
            this.BackColor = Color.Transparent;

            base.BackgroundImageLayout = ImageLayout.None;

            this.Paint += new PaintEventHandler(ImagePanel_Paint);

            this.Resize += new EventHandler(ImagePanel_Resize);

        }

        #region Events

        void Redraw() {
            if (BackImageNormal != null)
                BackgroundImage = ImageSlice.SliceImage(BackImageNormal, BackImageSlice, Size);
        }

        void ImagePanel_Paint(object sender, PaintEventArgs e) {
            if (BackgroundImage == null) {
                Redraw();
            }
        }

        void ImagePanel_Resize(object sender, EventArgs e) {
            Redraw();
        }

        #endregion

        #region New Properties

        public Image BackImageNormal { get; set; }

        public Padding BackImageSlice { get; set; }

        #endregion

        #region Hidden Properties

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override Image BackgroundImage {
            get {
                return base.BackgroundImage;
            }
            set {
                base.BackgroundImage = value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override ImageLayout BackgroundImageLayout {
            get {
                return base.BackgroundImageLayout;
            }
        }

        #endregion


    }
}
