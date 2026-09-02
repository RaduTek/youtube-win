using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ImageControls {
    public class ImageRadioButton : RadioButton {
        public ImageRadioButton() {

            this.BackColor = Color.Transparent;
            base.BackColor = Color.Transparent;

            // Enable button appearance for CheckBox/RadioButton
            base.AutoSize = false;
            base.Appearance = Appearance.Button;

            base.FlatStyle = FlatStyle.Flat;
            base.FlatAppearance.BorderSize = 0;
            base.FlatAppearance.CheckedBackColor = base.BackColor;
            base.FlatAppearance.MouseDownBackColor = base.BackColor;
            base.FlatAppearance.MouseOverBackColor = base.BackColor;

            base.TextAlign = ContentAlignment.MiddleCenter;

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

            this.CheckedChanged += new EventHandler(ImageRadioButton_CheckedChanged);
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
                BackgroundImage = ImageSlice.SliceImage(BackImageDisabled, BackImageSlice, Size);
            else if (Checked) {
                if (BackImageChecked != null)
                    BackgroundImage = ImageSlice.SliceImage(BackImageChecked, BackImageSlice, Size);
                else if (BackImagePressed != null)
                    BackgroundImage = ImageSlice.SliceImage(BackImagePressed, BackImageSlice, Size);
                else BackgroundImage = null;
            } else if (BackImagePressed != null && buttonPressed)
                BackgroundImage = ImageSlice.SliceImage(BackImagePressed, BackImageSlice, Size);
            else if (BackImageHover != null && buttonHover)
                BackgroundImage = ImageSlice.SliceImage(BackImageHover, BackImageSlice, Size);
            else if (BackImageFocus != null && Focused)
                BackgroundImage = ImageSlice.SliceImage(BackImageFocus, BackImageSlice, Size);
            else if (BackImageNormal != null)
                BackgroundImage = ImageSlice.SliceImage(BackImageNormal, BackImageSlice, Size);
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

        private void ImageRadioButton_CheckedChanged(object sender, EventArgs e) {
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

        public Image BackImageChecked { get; set; }
        public Image BackImageDisabled { get; set; }
        public Image BackImageFocus { get; set; }
        public Image BackImageHover { get; set; }
        public Image BackImageNormal { get; set; }
        public Image BackImagePressed { get; set; }

        public Padding BackImageSlice { get; set; }

        #endregion

        #region Tweaks

        protected override bool ShowFocusCues {
            get {
                return false;
            }
        }

        #endregion

    }
}
