using System;
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

            BitmapData input_data = image.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            int bytes = input_data.Stride * input_data.Height;

            byte[] buffer = new byte[bytes];
            byte[] result = new byte[bytes];

            Marshal.Copy(input_data.Scan0, buffer, 0, bytes);
            image.UnlockBits(input_data);

            int pixel_position = 0;
            Random rndX = new Random();
            Random rndY = new Random();
            int x1 = 0;
            int x2 = 0;

            int y1 = 0;
            int y2 = 0;

            if (h >= w)
            {
                x1 = rndX.Next(0, w);
                x2 = rndX.Next(x1, w);

                y1 = rndY.Next(0, h);
                y2 = rndY.Next(y1, h);
            }
            else
            {
                x1 = rndX.Next(0, h);
                x2 = rndX.Next(x1, h);

                y1 = rndY.Next(0, w);
                y2 = rndY.Next(y1, w);
            }

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    if (x == x1 && y == y1)
                    {
                        for (int m = y1; m < y2; m++)
                        {
                            for (int k = x1; k < x2; k++)
                            {
                                pixel_position = k * input_data.Stride + m * 4;
                                for (int channel = 0; channel < 3; channel++)
                                {
                                    result[pixel_position + channel] = 255;
                                }
                                result[pixel_position + 3] = 255;
                            }
                        }
                    }
                    else
                    {
                        pixel_position = y * input_data.Stride + x * 4;
                        for (int channel = 0; channel < 3; channel++)
                        {
                            result[pixel_position + channel] = buffer[pixel_position + channel];
                        }
                        result[pixel_position + 3] = 255;
                    }
                }
            }

            Bitmap result_image = new Bitmap(w, h);
            BitmapData output_data = result_image.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(result, 0, output_data.Scan0, bytes);
            result_image.UnlockBits(output_data);

            return result_image;
        }
    }
}
