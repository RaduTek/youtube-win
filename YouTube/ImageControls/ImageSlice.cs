using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace ImageControls {
    public static class ImageSlice {

        public static Image SliceImage(Image source, Padding slice, Size size) {
            if (size.Width < 1 || size.Height < 1) return null;

            slice.Bottom += 1;
            slice.Right += 1;

            Rectangle outRect = new Rectangle(new Point(0, 0), size);
            Bitmap generated = new Bitmap(size.Width, size.Height);
            Graphics g = Graphics.FromImage(generated);

            Rectangle cropRect, destRect;

            // Top Left corner
            cropRect = new Rectangle(0, 0, slice.Left, slice.Top);
            destRect = new Rectangle(0, 0, slice.Left, slice.Top);
            g.DrawImage(source, destRect, cropRect, GraphicsUnit.Pixel);

            // Top border
            cropRect = new Rectangle(slice.Left, 0, source.Width - slice.Left - slice.Right, slice.Top);
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
            cropRect = new Rectangle(slice.Left, slice.Top, source.Width - slice.Horizontal, source.Height - slice.Vertical);
            destRect = new Rectangle(slice.Left, slice.Top, size.Width - slice.Horizontal, size.Height - slice.Vertical);
            g.DrawImage(source, destRect, cropRect, GraphicsUnit.Pixel);

            // Right border
            cropRect = new Rectangle(source.Width - slice.Right, slice.Top, slice.Right, source.Height - slice.Vertical);
            destRect = new Rectangle(size.Width - slice.Right, slice.Top, slice.Right, size.Height - slice.Vertical);
            g.DrawImage(source, destRect, cropRect, GraphicsUnit.Pixel);

            // Bottom border
            cropRect = new Rectangle(slice.Left, source.Height - slice.Bottom, source.Width - slice.Horizontal, slice.Bottom);
            destRect = new Rectangle(slice.Left, size.Height - slice.Bottom, size.Width - slice.Horizontal, slice.Bottom);
            g.DrawImage(source, destRect, cropRect, GraphicsUnit.Pixel);

            // Bottom Left corner
            cropRect = new Rectangle(0, source.Height - slice.Bottom, slice.Left, slice.Bottom);
            destRect = new Rectangle(0, size.Height - slice.Bottom, slice.Left, slice.Bottom);
            g.DrawImage(source, destRect, cropRect, GraphicsUnit.Pixel);

            // Bottom Right corner
            cropRect = new Rectangle(source.Width - slice.Right, source.Height - slice.Bottom, slice.Right, slice.Bottom);
            destRect = new Rectangle(size.Width - slice.Right, size.Height - slice.Bottom, slice.Right, slice.Bottom);
            g.DrawImage(source, destRect, cropRect, GraphicsUnit.Pixel);


            return generated;
        }


        public static void DrawSlicedImage(Graphics g, Image source, Padding slice, Size size) {
            if (size.Width >= 1 && size.Height >= 1) {

                slice.Bottom += 1;
                slice.Right += 1;

                Rectangle cropRect = new Rectangle(0, 0, slice.Left, slice.Top);
                Rectangle destRect = new Rectangle(0, 0, slice.Left, slice.Top);

                // Top Left corner
                cropRect = new Rectangle(0, 0, slice.Left, slice.Top);
                destRect = new Rectangle(0, 0, slice.Left, slice.Top);
                g.DrawImage(source, destRect, cropRect, GraphicsUnit.Pixel);

                // Top border
                cropRect = new Rectangle(slice.Left, 0, source.Width - slice.Left - slice.Right, slice.Top);
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
                cropRect = new Rectangle(slice.Left, slice.Top, source.Width - slice.Horizontal, source.Height - slice.Vertical);
                destRect = new Rectangle(slice.Left, slice.Top, size.Width - slice.Horizontal, size.Height - slice.Vertical);
                g.DrawImage(source, destRect, cropRect, GraphicsUnit.Pixel);

                // Right border
                cropRect = new Rectangle(source.Width - slice.Right, slice.Top, slice.Right, source.Height - slice.Vertical);
                destRect = new Rectangle(size.Width - slice.Right, slice.Top, slice.Right, size.Height - slice.Vertical);
                g.DrawImage(source, destRect, cropRect, GraphicsUnit.Pixel);

                // Bottom border
                cropRect = new Rectangle(slice.Left, source.Height - slice.Bottom, source.Width - slice.Horizontal, slice.Bottom);
                destRect = new Rectangle(slice.Left, size.Height - slice.Bottom, size.Width - slice.Horizontal, slice.Bottom);
                g.DrawImage(source, destRect, cropRect, GraphicsUnit.Pixel);

                // Bottom Left corner
                cropRect = new Rectangle(0, source.Height - slice.Bottom, slice.Left, slice.Bottom);
                destRect = new Rectangle(0, size.Height - slice.Bottom, slice.Left, slice.Bottom);
                g.DrawImage(source, destRect, cropRect, GraphicsUnit.Pixel);

                // Bottom Right corner
                cropRect = new Rectangle(source.Width - slice.Right, source.Height - slice.Bottom, slice.Right, slice.Bottom);
                destRect = new Rectangle(size.Width - slice.Right, size.Height - slice.Bottom, slice.Right, slice.Bottom);
                g.DrawImage(source, destRect, cropRect, GraphicsUnit.Pixel);


            }
        }

    }
}
