using System;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;


namespace ImageRecoveryApp
{
    static class RecoverImage
    {
        public static Bitmap RecoverDestroidImage(this Complex[][] Originalimage, Complex[][] DestroidImage)
        {
            float[] frequenciesOriginal = new float[ImageRecoveryImage.Size * ImageRecoveryImage.Size];
            float[] frequenciesDestroid = new float[ImageRecoveryImage.Size * ImageRecoveryImage.Size];

            for (int i = 0; i < Originalimage.Length; i++)
            {
                for (int j = 0; j < Originalimage[0].Length; j++)
                {
                    frequenciesOriginal[j * Originalimage.Length + i] = Complex.Modulus(Originalimage[j][i]);
                    frequenciesDestroid[j * DestroidImage.Length + i] = Complex.Modulus(DestroidImage[j][i]);

                }
            }

            float minO = frequenciesOriginal.Min();
            float maxO = frequenciesOriginal.Max();
            double log_constant = 255 / Math.Log10(1 + 255);

            float minD = frequenciesDestroid.Min();
            float maxD = frequenciesDestroid.Max();
            double log_constantD = 255 / Math.Log10(1 + 255);
            for (int i = 0; i < frequenciesDestroid.Length; i++)
            {
                if (frequenciesDestroid[i] != frequenciesOriginal[i])
                {
                    frequenciesOriginal[i] = 255 * (frequenciesOriginal[i] - minO) / (maxO - minO);
                    frequenciesDestroid[i] = (int)(Math.Log10(1 + frequenciesOriginal[i]) * log_constant);

                }
                else
                {
                    frequenciesDestroid[i] = 255 * (frequenciesDestroid[i] - minD) / (maxD - minD);
                    frequenciesDestroid[i] = (int)(Math.Log10(1 + frequenciesDestroid[i]) * log_constant);
                }
            }

            Bitmap image = new Bitmap(ImageRecoveryImage.Size, ImageRecoveryImage.Size);
            BitmapData image_data = image.LockBits(new Rectangle(0, 0, ImageRecoveryImage.Size, ImageRecoveryImage.Size), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            int bytes = image_data.Stride * image_data.Height;
            byte[] result = new byte[bytes];


            for (int y = 0; y < ImageRecoveryImage.Size; y++)
            {
                for (int x = 0; x < ImageRecoveryImage.Size; x++)
                {
                    int pixel_position = y * image_data.Stride + x * 4;
                    for (int i = 0; i < 3; i++)
                    {
                        result[pixel_position + i] = (byte)frequenciesDestroid[y * ImageRecoveryImage.Size + x];
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
