using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace YouTube.ExControls
{
    public class NinePatchImage
    {
        private Image image = null;

        private Size size;
        private Padding margins;

        private readonly Rectangle[] src = new Rectangle[9];
        private readonly Rectangle[] dst = new Rectangle[9];

        private int lastImageWidth = -1;
        private int lastImageHeight = -1;

        private bool dirty = true;
        private bool drawCenter = true;

        private InterpolationMode interpolationMode = InterpolationMode.NearestNeighbor;

        public Image Image
        {
            get { return image; }
            set
            {
                image = value;

                if (image != null)
                {
                    if (image.Width != lastImageWidth ||
                        image.Height != lastImageHeight)
                    {
                        lastImageWidth = image.Width;
                        lastImageHeight = image.Height;
                        dirty = true;
                    }
                }
            }
        }

        private ImageAttributes imageAttrs = new ImageAttributes();
        private Color transparencyKey = Color.Magenta;

        public Color TransparencyKey
        {
            get { return transparencyKey; }
            set
            {
                transparencyKey = value;

                imageAttrs.SetColorKey(transparencyKey, transparencyKey);
            }
        }

        public Size Size
        {
            get { return size; }
            set
            {
                if (size != value)
                {
                    size = value;
                    dirty = true;
                }
            }
        }

        public Padding Margins
        {
            get { return margins; }
            set
            {
                if (margins != value)
                {
                    margins = value;
                    dirty = true;
                }
            }
        }

        public InterpolationMode InterpolationMode
        {
            get => interpolationMode;
            set
            {
                if (interpolationMode != value)
                {
                    interpolationMode = value;
                    dirty = true;
                }
            }
        }

        public bool DrawCenter
        {
            get { return drawCenter; }
            set
            {
                if (drawCenter != value)
                {
                    drawCenter = value;
                    dirty = true;
                }
            }
        }

        public void Invalidate()
        {
            dirty = true;
        }

        private void CalculateSlices()
        {
            if (image == null)
                return;

            int w = size.Width;
            int h = size.Height;

            int imgW = image.Width;
            int imgH = image.Height;

            int left = margins.Left;
            int top = margins.Top;
            int right = margins.Right;
            int bottom = margins.Bottom;

            int centerW = imgW - left - right;
            int centerH = imgH - top - bottom;

            int dstCenterW = w - left - right;
            int dstCenterH = h - top - bottom;

            // Drawing order:
            //
            // 5 | 1 | 6
            // --+---+---
            // 2 | 0 | 4 
            // --+---+---
            // 8 + 3 + 7
            //
            // This is done to reduce flicker, as areas 0, 1, 2, 3, 4 take longer to draw since scaling is required

            // --- Source rectangles ---

            // Top left
            src[5] = new Rectangle(0, 0, left, top);
            // Top center
            src[1] = new Rectangle(left, 0, centerW, top);
            // Top right
            src[6] = new Rectangle(imgW - right, 0, right, top);

            // Middle left
            src[2] = new Rectangle(0, top, left, centerH);
            // Middle center
            src[0] = new Rectangle(left, top, centerW, centerH);
            // Middle right
            src[4] = new Rectangle(imgW - right, top, right, centerH);

            // Bottom left
            src[8] = new Rectangle(0, imgH - bottom, left, bottom);
            // Bottom center
            src[3] = new Rectangle(left, imgH - bottom, centerW, bottom);
            // Bottom right
            src[7] = new Rectangle(imgW - right, imgH - bottom, right, bottom);

            // --- Destination rectangles ---

            // Top left
            dst[5] = new Rectangle(0, 0, left, top);
            // Top center
            dst[1] = new Rectangle(left, 0, dstCenterW, top);
            // Top right
            dst[6] = new Rectangle(left + dstCenterW, 0, right, top);

            // Middle left
            dst[2] = new Rectangle(0, top, left, dstCenterH);
            // Middle cener
            dst[0] = new Rectangle(left, top, dstCenterW, dstCenterH);
            // Middle right
            dst[4] = new Rectangle(left + dstCenterW, top, right, dstCenterH);

            // Bottom left
            dst[8] = new Rectangle(0, top + dstCenterH, left, bottom);
            // Bottom center
            dst[3] = new Rectangle(left, top + dstCenterH, dstCenterW, bottom);
            // Bottom right
            dst[7] = new Rectangle(left + dstCenterW, top + dstCenterH, right, bottom);

            dirty = false;
        }

        public void Draw(Graphics g)
        {
            if (image == null)
                return;

            if (dirty)
                CalculateSlices();

            var prevInterpolationMode = g.InterpolationMode;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;

            int i = drawCenter ? 0 : 1;

            for (; i < 9; i++)
            {
                g.DrawImage(image, dst[i], src[i].X, src[i].Y, src[i].Width, src[i].Height, GraphicsUnit.Pixel, imageAttrs);
            }

            g.InterpolationMode = prevInterpolationMode;
        }
    }
}