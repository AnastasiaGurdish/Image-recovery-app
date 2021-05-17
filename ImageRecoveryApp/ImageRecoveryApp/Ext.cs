using System;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ImageRecoveryApp
{
    static class Ext
    {
        public static Bitmap Grayscale(this Bitmap image)
        {
            int w = image.Width;
            int h = image.Height;

            BitmapData input_data = image.LockBits(
                new Rectangle(0, 0, w, h),
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);

            int bytes = input_data.Stride * input_data.Height;

            byte[] buffer = new byte[bytes];
            byte[] result = new byte[bytes];

            Marshal.Copy(input_data.Scan0, buffer, 0, bytes);
            image.UnlockBits(input_data);

            int pixel_position = 0;

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    pixel_position = y * input_data.Stride + x * 4;
                    int grayscale = (int)(buffer[pixel_position] * 0.0722 + buffer[pixel_position + 1] * 0.7152 + buffer[pixel_position + 2] * 0.2126);
                    for (int channel = 0; channel < 3; channel++)
                    {
                        result[pixel_position + channel] = (byte)grayscale;
                    }
                    result[pixel_position + 3] = 255;
                }
            }

            Bitmap result_image = new Bitmap(w, h);
            BitmapData output_data = result_image.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(result, 0, output_data.Scan0, bytes);
            result_image.UnlockBits(output_data);

            return result_image;
        }

        public static Bitmap Padding(this Bitmap image)
        {
            int w = image.Width;
            int h = image.Height;
            int n = 0;
            while (ImageRecoveryImage.Size <= Math.Max(w, h))
            {
                ImageRecoveryImage.Size = (int)Math.Pow(2, n);
                if (ImageRecoveryImage.Size == Math.Max(w, h))
                {
                    break;
                }
                n++;
            }
            double horizontal_padding = ImageRecoveryImage.Size - w;
            double vertical_padding = ImageRecoveryImage.Size - h;
            int left_padding = (int)Math.Floor(horizontal_padding / 2);
            int top_padding = (int)Math.Floor(vertical_padding / 2);

            BitmapData image_data = image.LockBits(
                new Rectangle(0, 0, w, h),
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);

            int bytes = image_data.Stride * image_data.Height;
            byte[] buffer = new byte[bytes];
            Marshal.Copy(image_data.Scan0, buffer, 0, bytes);
            image.UnlockBits(image_data);

            Bitmap padded_image = new Bitmap(ImageRecoveryImage.Size, ImageRecoveryImage.Size);

            BitmapData padded_data = padded_image.LockBits(
                new Rectangle(0, 0, ImageRecoveryImage.Size, ImageRecoveryImage.Size),
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppArgb);

            int padded_bytes = padded_data.Stride * padded_data.Height;
            byte[] result = new byte[padded_bytes];

            for (int i = 3; i < padded_bytes; i += 4)
            {
                result[i] = 255;
            }

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    int image_position = y * image_data.Stride + x * 4;
                    int padding_position = y * padded_data.Stride + x * 4;
                    for (int i = 0; i < 3; i++)
                    {
                        result[padded_data.Stride * top_padding + 4 * left_padding + padding_position + i] = buffer[image_position + i];
                    }
                }
            }

            Marshal.Copy(result, 0, padded_data.Scan0, padded_bytes);
            padded_image.UnlockBits(padded_data);

            return padded_image;
        }
        public static Bitmap VisualizeFourier(this Complex[][] f)
        {
            float[] frequencies = new float[ImageRecoveryImage.Size * ImageRecoveryImage.Size];
            for (int i = 0; i < f.Length; i++)
            {
                for (int j = 0; j < f[0].Length; j++)
                {
                    frequencies[j * f.Length + i] = Complex.Modulus(f[j][i]);
                }
            }

            float min = frequencies.Min();
            float max = frequencies.Max();
            double log_constant = 255 / Math.Log10(1 + 255);
            for (int i = 0; i < frequencies.Length; i++)
            {
                frequencies[i] = 255 * (frequencies[i] - min) / (max - min);
                frequencies[i] = (int)(Math.Log10(1 + frequencies[i]) * log_constant);
            }

            Bitmap image = new Bitmap(ImageRecoveryImage.Size, ImageRecoveryImage.Size);
            BitmapData image_data = image.LockBits(new Rectangle(0, 0, ImageRecoveryImage.Size, ImageRecoveryImage.Size),ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            int bytes = image_data.Stride * image_data.Height;
            byte[] result = new byte[bytes];
            for (int y = 0; y < ImageRecoveryImage.Size; y++)
            {
                for (int x = 0; x < ImageRecoveryImage.Size; x++)
                {
                    int pixel_position = y * image_data.Stride + x * 4;
                    for (int i = 0; i < 3; i++)
                    {
                        result[pixel_position + i] = (byte)frequencies[y * ImageRecoveryImage.Size + x];
                    }
                    result[pixel_position + 3] = 255;
                }
            }
            Marshal.Copy(result, 0, image_data.Scan0, bytes);
            image.UnlockBits(image_data);
            return image;
        }
    }
}
