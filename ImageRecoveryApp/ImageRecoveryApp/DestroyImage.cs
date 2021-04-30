using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ImageRecoveryApp
{
    static class DestroyImage
    {
        public static Bitmap CutPartOfImage(this Bitmap image)
        {
            int w = image.Width;
            int h = image.Height;

            BitmapData image_data = image.LockBits(
                new Rectangle(0, 0, w, h),
                ImageLockMode.ReadOnly,
                PixelFormat.Format24bppRgb);

            int bytes = image_data.Stride * image_data.Height;
            byte[] buffer = new byte[bytes];
            byte[] result = new byte[bytes];

            Marshal.Copy(image_data.Scan0, buffer, 0, bytes);
            image.UnlockBits(image_data);

            Random rndX = new Random();
            Random rndY = new Random();

            Color pixelColor = image.GetPixel(50, 50);

            for (int x = 128; x < w; x++)
            {
                for (int y = 45; y < h; y++)
                {
                    Color next = Color.FromArgb(0, 60, 90);
                    image.SetPixel(x, y, next);
                }
            }


            for (int i = 0; i < bytes; i += 1)
            {

                for (int j = 0; j < 3; j++)
                {
                    //if ()
                    //{
                    //    Color next = Color.FromArgb(0, 60, 90);
                    //    result[i + j] = (byte)image.GetPixel(50, 50);
                    //}
                    //else
                    //{
                    //    result[i + j] = buffer[i + j];
                    //}
                }
            }

            Bitmap result_image = new Bitmap(w, h);
            BitmapData result_data = result_image.LockBits(
                new Rectangle(0, 0, w, h),
                ImageLockMode.WriteOnly,
                PixelFormat.Format24bppRgb);
            Marshal.Copy(result, 0, result_data.Scan0, bytes);
            result_image.UnlockBits(result_data);
            return result_image;

        }
    }
}
