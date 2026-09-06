using System;
using System.Drawing;
using System.Windows.Forms;

namespace YouTube.ExControls
{
    public class ExUtils
    {
        public static void UpdateStringFormatFromAlignment(StringFormat format, ContentAlignment alignment)
        {
            // Horizontal alignment
            switch (alignment)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.BottomLeft:
                    format.Alignment = StringAlignment.Near;
                    break;

                case ContentAlignment.TopCenter:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.MiddleRight:
                    format.Alignment = StringAlignment.Center;
                    break;

                default:
                    format.Alignment = StringAlignment.Far;
                    break;
            }

            // Vertical alignment
            switch (alignment)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.TopCenter:
                case ContentAlignment.TopRight:
                    format.LineAlignment = StringAlignment.Near;
                    break;

                case ContentAlignment.MiddleLeft:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.MiddleRight:
                    format.LineAlignment = StringAlignment.Center;
                    break;

                default:
                    format.LineAlignment = StringAlignment.Far;
                    break;
            }
        }
    }
}
