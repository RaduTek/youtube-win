using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ImageControls {
    public class ImageButton : Button {
        public ImageButton() {
            this.BackColor = Color.Transparent;
            base.BackColor = Color.Transparent;

            base.FlatStyle = FlatStyle.Flat;
            base.FlatAppearance.BorderSize = 0;
            base.FlatAppearance.CheckedBackColor = base.BackColor;
            base.FlatAppearance.MouseDownBackColor = base.BackColor;
            base.FlatAppearance.MouseOverBackColor = base.BackColor;

            base.BackgroundImageLayout = ImageLayout.None;

            this.BackImageSlice = new Padding(3, 3, 3, 3);

            this.Paint += new PaintEventHandler(ImageButton_Paint);

            this.Resize += new EventHandler(ImageButton_Resize);

            this.GotFocus += new EventHandler(ImageButton_GotFocus);
            this.LostFocus += new EventHandler(ImageButton_LostFocus);

            this.MouseEnter += new EventHandler(ImageButton_MouseEnter);
            this.MouseLeave += new EventHandler(ImageButton_MouseLeave);
            this.MouseDown += new MouseEventHandler(ImageButton_MouseDown);
            this.MouseUp += new MouseEventHandler(ImageButton_MouseUp);

            this.KeyDown += new KeyEventHandler(ImageButton_KeyDown);
            this.KeyUp += new KeyEventHandler(ImageButton_KeyUp);
        }

        #region Redraw Events

        private void ImageButton_Paint(object sender, PaintEventArgs e) {
            if (BackgroundImage == null) {
                ButtonRedraw();
            }
        }

        private bool buttonHover = false;
        private bool buttonPressed = false;

        private void ButtonRedraw() {
            if (BackImageDisabled != null && !Enabled)
                BackgroundImage = SliceImage(BackImageDisabled);
            else if (BackImagePressed != null && buttonPressed)
                BackgroundImage = SliceImage(BackImagePressed);
            else if (BackImageHover != null && buttonHover)
                BackgroundImage = SliceImage(BackImageHover);
            else if (BackImageFocus != null && Focused)
                BackgroundImage = SliceImage(BackImageFocus);
            else if (BackImageNormal != null)
                BackgroundImage = SliceImage(BackImageNormal);
            else BackgroundImage = null;
        }

        #endregion

        #region Events

        private void ImageButton_Resize(object sender, EventArgs e) {
            ButtonRedraw();
        }

        private void ImageButton_GotFocus(object sender, EventArgs e) {
            ButtonRedraw();
        }

        private void ImageButton_LostFocus(object sender, EventArgs e) {
            ButtonRedraw();
        }

        private void ImageButton_MouseEnter(object sender, EventArgs e) {
            buttonHover = true;
            ButtonRedraw();
        }

        private void ImageButton_MouseLeave(object sender, EventArgs e) {
            buttonHover = false;
            ButtonRedraw();
        }

        private void ImageButton_MouseDown(object sender, MouseEventArgs e) {
            buttonPressed = true;
            ButtonRedraw();
        }

        private void ImageButton_MouseUp(object sender, MouseEventArgs e) {
            buttonPressed = false;
            ButtonRedraw();
        }

        void ImageButton_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode != Keys.Space) return;
            buttonPressed = true;
            ButtonRedraw();
        }

        void ImageButton_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode != Keys.Space) return;
            buttonPressed = false;
            ButtonRedraw();
        }

        #endregion

        #region Hidden Properties

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new FlatStyle FlatStyle { 
            get {
                return base.FlatStyle;
            }
            set { }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new FlatButtonAppearance FlatAppearance {
            get {
                return base.FlatAppearance;
            }
            set { }
        }

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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override bool AutoSize {
            get {
                return false;
            }
            set {
                base.AutoSize = false;
            }
        }


        #endregion

        #region New Properties

        public Image BackImageNormal { get; set; }
        public Image BackImageHover { get; set; }
        public Image BackImagePressed { get; set; }
        public Image BackImageFocus { get; set; }
        public Image BackImageDisabled { get; set; }

        public Padding BackImageSlice { get; set; }

        #endregion

        #region Tweaks

        protected override bool ShowFocusCues {
            get {
                return false;
            }
        }

        #endregion

        #region Image Slicing

        private Image SliceImage(Image source) {
            return SliceImage(source, BackImageSlice, Size);
        }

        private Image SliceImage(Image source, Padding slice, Size size) {
            if (size.Width < 1 || size.Height < 1) return null;

            Rectangle outRect = new Rectangle(new Point(0, 0), size);
            Bitmap generated = new Bitmap(size.Width, size.Height);
            Graphics g = Graphics.FromImage(generated);

            Rectangle cropRect, destRect;

            // Top Left corner
            cropRect = new Rectangle(0, 0, slice.Left, slice.Top);
            destRect = new Rectangle(0, 0, slice.Left, slice.Top);
            g.DrawImage(source, destRect, cropRect, GraphicsUnit.Pixel);

            // Top border
            cropRect = new Rectangle(slice.Left, 0, source.Width - slice.Horizontal, slice.Top);
            destRect = new Rectangle(slice.Left, 0, size.Width - slice.Horizontal, slice.Top);
            g.DrawImage(source, destRect, cropRect, GraphicsUnit.Pixel);

            // Top Right corner
            cropRect = new Rectangle(source.Width - slice.Right, 0, slice.Right, slice.Top);
            destRect = new Rectangle(size.Width - slice.Right, 0, slice.Right, slice.Top);
            g.DrawImage(source, destRect, cropRect, GraphicsUnit.Pixel);

            // Left border
            cropRect = new Rectangle(0, slice.Top, slice.Left, source.Height - slice.Vertical);
            destRect = new Rectangle(0, slice.Top, slice.Left, size.Height - slice.Vertical);
            g.DrawImage(source, destRect, cropRect, GraphicsUnit.Pixel);

            // Middle fill
            cropRect = new Rectangle(slice.Left, slice.Top, source.Width - slice.Horizontal - 1, source.Height - slice.Vertical - 1);
            destRect = new Rectangle(slice.Left, slice.Top, size.Width - slice.Horizontal, size.Height - slice.Vertical);
            g.DrawImage(source, destRect, cropRect, GraphicsUnit.Pixel);

            // Right border
            cropRect = new Rectangle(source.Width - slice.Right, slice.Top, slice.Right, source.Height - slice.Vertical);
            destRect = new Rectangle(size.Width - slice.Right, slice.Top, slice.Right, size.Height - slice.Vertical);
            g.DrawImage(source, destRect, cropRect, GraphicsUnit.Pixel);

            // Bottom Left corner
            cropRect = new Rectangle(0, source.Height - slice.Bottom, slice.Left, slice.Bottom);
            destRect = new Rectangle(0, size.Height - slice.Bottom, slice.Left, slice.Bottom);
            g.DrawImage(source, destRect, cropRect, GraphicsUnit.Pixel);

            // Bottom border
            cropRect = new Rectangle(slice.Left, source.Height - slice.Bottom, source.Width - slice.Horizontal, slice.Bottom);
            destRect = new Rectangle(slice.Left, size.Height - slice.Bottom, size.Width - slice.Horizontal, slice.Bottom);
            g.DrawImage(source, destRect, cropRect, GraphicsUnit.Pixel);

            // Bottom Right corner
            cropRect = new Rectangle(source.Width - slice.Right, source.Height - slice.Bottom, slice.Right, slice.Bottom);
            destRect = new Rectangle(size.Width - slice.Right, size.Height - slice.Bottom, slice.Right, slice.Bottom);
            g.DrawImage(source, destRect, cropRect, GraphicsUnit.Pixel);

            return generated;
        }

        #endregion

    }
}
