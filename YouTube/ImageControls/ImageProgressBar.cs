using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ImageControls {
    public class ImageProgressBar : Control {
        public ImageProgressBar() {
            
            SetStyle(ControlStyles.Selectable, false);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint, true);

            this.BackColor = Color.Transparent;

            this.Resize += new EventHandler(ImageProgressBar_Resize);
        }

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
        
        #region New Properties

        public enum FillColors {
            Green = 0,
            Yellow = 1,
            Red = 2,
            Blue = 3
        }

        public Image BackImageNormal { get; set; }
        public Image FillImageGreen { get; set; }
        public Image FillImageYellow { get; set; }
        public Image FillImageRed { get; set; }
        public Image FillImageBlue { get; set; }

        private FillColors _FillColor = FillColors.Green;
        public FillColors FillColor {
            get { return _FillColor; }
            set {
                _FillColor = value;

                this.Invalidate();
            }
        }

        public Padding BackImageSlice { get; set; }
        public Padding FillSlice { get; set; }

        public bool ProgressLabel { get; set; }

        private int _MinValue = 0;
        public int MinValue {
            get { return _MinValue; }
            set {
                _MinValue = value;

                this.Invalidate();
            }
        }
        private int _MaxValue = 100;
        public int MaxValue {
            get { return _MaxValue; }
            set {
                _MaxValue = value;

                this.Invalidate();
            }
        }
        private int _Value = 50;
        public int Value {
            get { return _Value; }
            set {
                _Value = value;

                this.Invalidate();
            }
        }

        #endregion

        public int Remap(int value, int from1, int to1, int from2, int to2) {
            return from2 + (to2 - from2) * (value - from1) / (to1 - from1);
        }

        void ImageProgressBar_Resize(object sender, EventArgs e) {
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e) {
            Graphics g = e.Graphics;

            if (BackImageNormal != null) {
                g.DrawImage(ImageSlice.SliceImage(BackImageNormal, BackImageSlice, this.Size), new Point(0, 0));
            }

            Image newFillImage = null;
            if (FillColor == FillColors.Green && FillImageGreen != null) {
                newFillImage = FillImageGreen;
            } else if (FillColor == FillColors.Yellow && FillImageYellow != null) {
                newFillImage = FillImageYellow;
            } else if (FillColor == FillColors.Red && FillImageRed != null) {
                newFillImage = FillImageRed;
            } else if (FillColor == FillColors.Blue && FillImageBlue != null) {
                newFillImage = FillImageBlue;
            }
            if (newFillImage != null) {
                int barWidth = Remap(Value, MinValue, MaxValue, 0, this.Width - this.Padding.Horizontal);
                int barHeight = this.Height - this.Padding.Vertical;

                if (barWidth > 0 && barHeight > 0)
                    g.DrawImage(ImageSlice.SliceImage(newFillImage, FillSlice, new Size(barWidth, barHeight)), new Point(this.Padding.Left, this.Padding.Top));
            }

            if (ProgressLabel) {
                Rectangle destRect = this.ClientRectangle;

                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;

                g.DrawString(Value + "%", this.Font, new SolidBrush(this.ForeColor), destRect, stringFormat);
            }
        }

    }
}
