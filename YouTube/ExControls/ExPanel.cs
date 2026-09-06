using System;
using System.Windows.Forms;

namespace YouTube.ExControls
{
    public class ExPanel : Panel
    {
        public ExPanel() : base()
        {
            npi = new NinePatchImage();
        }

        protected NinePatchImage npi;
        protected string resourceKey = "Button";


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

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            if (npi.Margins == null)
            {
                return;
            }

            if (npi.Image == null)
            {
                npi.Image = ExResourceManager.GetImage(resourceKey, ExControlState.None, (int)e.Graphics.DpiX);
            }

            npi.Draw(e.Graphics);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            npi.Size = this.Size;
            Invalidate();
        }
    }
}
