using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;


using System.Drawing.Drawing2D;


namespace ImageRecoveryApp
{
    static class DestroyImage
    {
        public static Bitmap CutPartOfImage(this Bitmap image)
        {
            int w = image.Width;
            int h = image.Height;

            BitmapData image_data = image.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            int bytes = image_data.Stride * image_data.Height;
            byte[] buffer = new byte[bytes];
            byte[] result = new byte[bytes];

            Marshal.Copy(image_data.Scan0, buffer, 0, bytes);
            image.UnlockBits(image_data);

            Random rndX = new Random();
            Random rndY = new Random();
            Point StartPoint, EndPoint;

            if (w < h)
            {
                StartPoint = new Point(rndX.Next(0, h), rndY.Next(0, h));
                EndPoint = new Point(rndX.Next(0, h), rndY.Next(0, h));
            }
            else
            {
                StartPoint = new Point(rndX.Next(0, w), rndY.Next(0, w));
                EndPoint = new Point(rndX.Next(0, w), rndY.Next(0, w));
            }

            Rectangle full_rectangle = new Rectangle(0, 0, w, h);
            Region region = new Region(full_rectangle);

            Rectangle ellipse_rect = SelectedRect(StartPoint, EndPoint);
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(ellipse_rect);
            // path.AddEllipse(ellipse_rect);
            region.Exclude(path);

            Bitmap bm = new Bitmap(w, h);
            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.Clear(Color.Transparent);
                gr.SetClip(region, CombineMode.Replace);
                gr.SmoothingMode = SmoothingMode.AntiAlias;
                using (TextureBrush brush = new TextureBrush(image, full_rectangle))
                {
                    gr.FillRectangle(brush, full_rectangle);
                }
            }
            //  return result_image;


            // result = ReadPixels(result_image);


            //for (int x = 0; x < w; x++)
            //{
            //    for (int y = 0; y < h; y++)
            //    {

            //    }
            //}


            Bitmap result_image = bm;
            BitmapData result_data = result_image.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            Marshal.Copy(result, 0, result_data.Scan0, bytes);
            result_image.UnlockBits(result_data);
            return result_image;

        }

        public static Rectangle SelectedRect(Point point0, Point point1)
        {
            int x = Math.Min(point0.X, point1.X);
            int y = Math.Min(point0.Y, point1.Y);
            int width = Math.Abs(point1.X - point0.X);
            int height = Math.Abs(point1.Y - point0.Y);
            return new Rectangle(x, y, width, height);
        }

        private static byte[] ReadPixels(Bitmap img)
        {
            int w = img.Width;
            int h = img.Height;

            BitmapData sd = img.LockBits(new Rectangle(0, 0, w, h),
                ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            int bytes = sd.Stride * sd.Height;
            byte[] buffer = new byte[bytes];
            Marshal.Copy(sd.Scan0, buffer, 0, bytes);
            img.UnlockBits(sd);
            byte[] p = new byte[256];
            for (int i = 0; i < bytes; i += 4)
            {
                p[buffer[i]]++;
            }
            return p;
        }
    }
}
