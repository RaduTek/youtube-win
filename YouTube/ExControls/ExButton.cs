using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace YouTube.ExControls
{
    public class ExButton : ExControl
    {
        public ExButton()
            : base()
        {
            format = new StringFormat();
            npi = new NinePatchImage();
            foreTheme = new ExControlTheme(Color.Black);

            EnableTransparency = true;
            Size = new Size(70, 30);
            TextAlign = ContentAlignment.MiddleCenter;
        }

        protected NinePatchImage npi;

        protected ContentAlignment textAlign;
        protected StringFormat format;
        protected string resourceKey = "Button";

        protected string iconKey = "";
        protected bool iconNeedsUpdate = false;
        protected Image icon;

        protected ExControlTheme foreTheme;

        /// <summary>
        /// Icon drawn in the center of the button
        /// </summary>
        public Image Icon
        {
            get { return icon; }
            set
            {
                if (icon != value)
                {
                    icon = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Resource key for icon
        /// </summary>
        public string IconKey
        {
            get { return iconKey; }
            set
            {
                if (iconKey != value)
                {
                    iconKey = value;
                    iconNeedsUpdate = true;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Resource key for background image
        /// </summary>
        public string BackKey
        {
            get { return resourceKey; }
            set
            {
                if (resourceKey != value)
                {
                    resourceKey = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Slice margins for background
        /// </summary>
        public Padding BackMargins
        {
            get { return npi.Margins; }
            set
            {
                npi.Margins = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Foreground (text) color theme
        /// </summary>
        public ExControlTheme ForeTheme
        {
            get { return foreTheme; }
        }

        private ImageAttributes imageAttr = new ImageAttributes();
        private Color iconTransparencyKey = Color.Magenta;
        public Color IconTransparencyKey
        {
            get { return iconTransparencyKey; }
            set
            {
                if (iconTransparencyKey != value)
                {
                    iconTransparencyKey = value;
                    imageAttr.SetColorKey(value, value);
                    Invalidate();
                }
            }
        }

        public ContentAlignment TextAlign
        {
            get { return textAlign; }
            set
            {
                if (textAlign != value)
                {
                    textAlign = value;
                    ExUtils.UpdateStringFormatFromAlignment(format, textAlign);
                    Invalidate();
                }
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            if (npi.Margins == null)
            {
                return;
            }

            if (npi.Image == null)
            {
                npi.Image = ExResourceManager.GetImage(resourceKey, State, (int)e.Graphics.DpiX);
            }

            npi.Draw(e.Graphics);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Rectangle rect = ClientRectangle;

            // ---- Draw Icon ----
            if (iconNeedsUpdate)
            {
                icon = ExResourceManager.GetImage(iconKey, State, (int)e.Graphics.DpiX);
                iconNeedsUpdate = false;
            }

            if (icon != null)
            {
                int x = (rect.Width - icon.Width) / 2;
                int y = (rect.Height - icon.Height) / 2;

                var destRect = new Rectangle(x, y, icon.Width, icon.Height);

                e.Graphics.DrawImage(icon, destRect, 0, 0, icon.Width, icon.Height, GraphicsUnit.Pixel, imageAttr);
            }

            // ---- Draw Text ----
            if (!string.IsNullOrEmpty(Text))
            {
                Brush b = new SolidBrush(foreTheme.GetStateColor(State));

                e.Graphics.DrawString(
                    Text,
                    Font,
                    b,
                    rect,
                    format
                );
            }
        }

        protected override void OnStateChange(ExControlState newState)
        {
            base.OnStateChange(newState);

            if (iconKey != null && iconKey != "")
            {
                iconNeedsUpdate = true;
            }

            npi.Image = null;
            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            npi.Size = this.Size;
            Invalidate();
        }
    }
}