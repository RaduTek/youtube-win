using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace YouTube.ExControls
{
    public class NinePatchImage
    {
        private Image _Image = null;

        private Size _size;
        private Padding _margins;

        private readonly Rectangle[] _src = new Rectangle[9];
        private readonly Rectangle[] _dst = new Rectangle[9];

        private int _lastImageWidth = -1;
        private int _lastImageHeight = -1;

        private bool _dirty = true;
        private bool _drawCenter = true;

        public Image Image
        {
            get { return _Image; }
            set
            {
                _Image = value;

                if (_Image != null)
                {
                    if (_Image.Width != _lastImageWidth ||
                        _Image.Height != _lastImageHeight)
                    {
                        _lastImageWidth = _Image.Width;
                        _lastImageHeight = _Image.Height;
                        _dirty = true;
                    }
                }
            }
        }

        private ImageAttributes _imageAttr = new ImageAttributes();
        private Color _TransparencyKey = Color.Magenta;
        public Color TransparencyKey
        {
            get { return _TransparencyKey; }
            set
            {
                _TransparencyKey = value;

                _imageAttr.SetColorKey(_TransparencyKey, _TransparencyKey);
            }
        }

        public Size Size
        {
            get { return _size; }
            set
            {
                if (_size != value)
                {
                    _size = value;
                    _dirty = true;
                }
            }
        }

        public Padding Margins
        {
            get { return _margins; }
            set
            {
                if (_margins != value)
                {
                    _margins = value;
                    _dirty = true;
                }
            }
        }

        public bool DrawCenter
        {
            get { return _drawCenter; }
            set
            {
                if (_drawCenter != value)
                {
                    _drawCenter = value;
                    _dirty = true;
                }
            }
        }

        public void Invalidate()
        {
            _dirty = true;
        }

        private void CalculateSlices()
        {
            if (_Image == null)
                return;

            int w = _size.Width;
            int h = _size.Height;

            int imgW = _Image.Width;
            int imgH = _Image.Height;

            int left = _margins.Left;
            int top = _margins.Top;
            int right = _margins.Right;
            int bottom = _margins.Bottom;

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
            _src[5] = new Rectangle(0, 0, left, top);
            // Top center
            _src[1] = new Rectangle(left, 0, centerW, top);
            // Top right
            _src[6] = new Rectangle(imgW - right, 0, right, top);

            // Middle left
            _src[2] = new Rectangle(0, top, left, centerH);
            // Middle center
            _src[0] = new Rectangle(left, top, centerW, centerH);
            // Middle right
            _src[4] = new Rectangle(imgW - right, top, right, centerH);

            // Bottom left
            _src[8] = new Rectangle(0, imgH - bottom, left, bottom);
            // Bottom center
            _src[3] = new Rectangle(left, imgH - bottom, centerW, bottom);
            // Bottom right
            _src[7] = new Rectangle(imgW - right, imgH - bottom, right, bottom);

            // --- Destination rectangles ---

            // Top left
            _dst[5] = new Rectangle(0, 0, left, top);
            // Top center
            _dst[1] = new Rectangle(left, 0, dstCenterW, top);
            // Top right
            _dst[6] = new Rectangle(left + dstCenterW, 0, right, top);

            // Middle left
            _dst[2] = new Rectangle(0, top, left, dstCenterH);
            // Middle cener
            _dst[0] = new Rectangle(left, top, dstCenterW, dstCenterH);
            // Middle right
            _dst[4] = new Rectangle(left + dstCenterW, top, right, dstCenterH);

            // Bottom left
            _dst[8] = new Rectangle(0, top + dstCenterH, left, bottom);
            // Bottom center
            _dst[3] = new Rectangle(left, top + dstCenterH, dstCenterW, bottom);
            // Bottom right
            _dst[7] = new Rectangle(left + dstCenterW, top + dstCenterH, right, bottom);

            _dirty = false;
        }

        public void Draw(Graphics g)
        {
            if (_Image == null)
                return;

            if (_dirty)
                CalculateSlices();

            int i = _drawCenter ? 0 : 1;

            for (; i < 9; i++)
            {
                g.DrawImage(_Image, _dst[i], _src[i].X, _src[i].Y, _src[i].Width, _src[i].Height, GraphicsUnit.Pixel, _imageAttr);
            }
        }
    }
}