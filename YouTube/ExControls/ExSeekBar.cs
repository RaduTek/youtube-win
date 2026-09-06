using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace YouTube.ExControls
{
    public class SeekEventArgs : EventArgs
    {
        public int Position { get; set; }
    }

    public class ExSeekBar : ExControl
    {
        public ExSeekBar() : base()
        {
            EnableTransparency = true;
            BackColor = Color.Transparent;
            ForeColor = Color.FromArgb(255, 200, 200, 200);

            Value = 0; // calc initial rectangles
        }

        private int maxValue = 100;
        private int value = 0;
        private int bufferValue = 50;
        private int seekPosition = 0;
        private int leftMargin = 0, rightMargin = 0;

        private bool seeking = false, showLabels = true;

        private string currentTime = "0:00", totalTime = "0:00";

        private readonly int seekBarMargin = 16, labelMargin = 8;
        private readonly Image trackImg = Properties.Resources.SeekBar_Track;
        private readonly Image bufferImg = Properties.Resources.SeekBar_Buffer;
        private readonly Image fillImg = Properties.Resources.SeekBar_Fill;
        private Image thumbImg = Properties.Resources.SeekBarThumb_Normal;

        private Rectangle trackRect, bufferRect, seekRect, thumbRect, srcRect;


        public bool ShowLabels
        {
            get { return showLabels; }
            set
            {
                if (showLabels != value)
                {
                    showLabels = value;

                    UpdateRectangles();
                }
            }
        }


        public int MaxValue
        {
            get { return maxValue; }
            set
            {
                if (maxValue != value)
                {
                    maxValue = value;

                    totalTime = Utils.FormatDurationSeconds(maxValue);

                    UpdateRectangles();
                }
            }
        }

        public int Value
        {
            get { return value; }
            set
            {
                if (this.value != value)
                {
                    this.value = value;

                    currentTime = Utils.FormatDurationSeconds(this.value);

                    UpdateRectangles();
                }
            }
        }

        public int BufferValue
        {
            get { return bufferValue; }
            set
            {
                if (bufferValue != value)
                {
                    bufferValue = value;

                    UpdateRectangles();
                }
            }
        }

        
        private int RealSeekValue
        {
            get { return (int)((seekPosition / (double)trackRect.Width) * maxValue); }
        }

        public int SeekValue
        {
            get { return seeking ? RealSeekValue : value; }
        }

        public bool Seeking
        {
            get { return seeking; }
        }


        public event EventHandler<SeekEventArgs> SeekFinished;

        private void UpdateRectangles()
        {
            leftMargin = seekBarMargin;
            rightMargin = seekBarMargin;

            if (showLabels)
            {
                leftMargin += TextRenderer.MeasureText(currentTime, Font).Width;
                rightMargin += TextRenderer.MeasureText(totalTime, Font).Width;
            }

            var totalMargins = leftMargin + rightMargin;

            // Fix rounding error on image scale
            srcRect = new Rectangle(0, 0, trackImg.Width - 1, trackImg.Height);

            trackRect = new Rectangle(leftMargin, (Height - trackImg.Height) / 2, Width - totalMargins, trackImg.Height);

            var bufferPosX = (int)Math.Floor((bufferValue / (double)maxValue) * trackRect.Width);
            bufferRect = new Rectangle(trackRect.X, trackRect.Y, bufferPosX, trackRect.Height);

            var seekPosX = (int)Math.Floor((value / (double)maxValue) * trackRect.Width);
            seekPosX = seeking ? seekPosition : seekPosX;
            seekRect = new Rectangle(trackRect.X, trackRect.Y, seekPosX, trackRect.Height);

            thumbRect = new Rectangle(leftMargin + seekPosX - (thumbImg.Width / 2), (Height - thumbImg.Height) / 2, thumbImg.Width, thumbImg.Height);

            Invalidate();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

            e.Graphics.DrawImage(trackImg, trackRect, srcRect, GraphicsUnit.Pixel);

            e.Graphics.DrawImage(bufferImg, bufferRect, srcRect, GraphicsUnit.Pixel);

            e.Graphics.DrawImage(fillImg, seekRect, srcRect, GraphicsUnit.Pixel);

            e.Graphics.DrawImage(thumbImg, thumbRect);

            if (showLabels)
            {
                var textHeight = TextRenderer.MeasureText(currentTime, Font).Height;
                var textY = (Height - textHeight) / 2;

                var brush = new SolidBrush(ForeColor);

                e.Graphics.DrawString(currentTime, Font, brush, new Point(labelMargin, textY));
                e.Graphics.DrawString(totalTime, Font, brush, new Point(Width - rightMargin + labelMargin, textY));
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            UpdateRectangles();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (seeking)
            {
                seekPosition = Math.Min(Math.Max(e.X - leftMargin, 0), trackRect.Width);

                UpdateRectangles();
                return;
            }

            var newImg = Hovering && thumbRect.Contains(e.Location) ? Properties.Resources.SeekBarThumb_Hover : Properties.Resources.SeekBarThumb_Normal;
            
            if (thumbImg != newImg)
            {
                thumbImg = newImg;
                Invalidate();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left && (thumbRect.Contains(e.Location) || trackRect.Contains(e.Location)))
            {
                seeking = true;
                thumbImg = Properties.Resources.SeekBarThumb_Hover;

                seekPosition = Math.Min(Math.Max(e.X - leftMargin, 0), trackRect.Width);
                
                UpdateRectangles();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (seeking)
            {
                if (SeekFinished != null)
                    SeekFinished.Invoke(this, new SeekEventArgs() { Position = RealSeekValue });

                seeking = false;
                thumbImg = Hovering && thumbRect.Contains(e.Location) ? Properties.Resources.SeekBarThumb_Hover : Properties.Resources.SeekBarThumb_Normal;
                seekPosition = 0;

                UpdateRectangles();
            }
        }
    }
}
