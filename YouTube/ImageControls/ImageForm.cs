using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ImageControls
{
	public class ImageForm : Form
	{
		public Image BackImageNormal { get; set; }

		public Image BackImageGrayed { get; set; }

		public Padding BackImageSlice { get; set; }

		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
			set
			{
				base.BackgroundImage = value;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public override ImageLayout BackgroundImageLayout
		{
			get
			{
				return base.BackgroundImageLayout;
			}
		}

		public ImageForm()
		{
			base.TransparencyKey = Color.Fuchsia;
			base.BackgroundImageLayout = ImageLayout.None;
			base.Paint += ImagePanel_Paint;
			base.Activated += ImagePanel_Invalidate;
			base.Deactivate += ImagePanel_Invalidate;
			base.Resize += ImagePanel_Invalidate;
			base.FormBorderStyle = FormBorderStyle.None;
			DoubleBuffered = true;
			SetStyle(ControlStyles.ResizeRedraw, true);
		}

		private void ImagePanel_Paint(object sender, PaintEventArgs e)
		{
			if (BackImageNormal != null && (Form.ActiveForm == this || BackImageGrayed != null))
			{
				ImageSlice.DrawSlicedImage(e.Graphics, BackImageNormal, BackImageSlice, base.Size);
			}
			if (BackImageGrayed != null && Form.ActiveForm != this)
			{
				ImageSlice.DrawSlicedImage(e.Graphics, BackImageGrayed, BackImageSlice, base.Size);
			}
		}

		private void ImagePanel_Invalidate(object sender, EventArgs e)
		{
			Invalidate();
		}
	}
}
