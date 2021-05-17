using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace FourierTransformFFT
{
    public partial class FFT : Form
    {

        public FFT()
        {
            InitializeComponent();
            ofd = new OpenFileDialog();
            sfd = new SaveFileDialog();
            ofd.Filter = filter;
            sfd.Filter = filter;
        }

        Bitmap image, processed, display;
        string image_path;
        string filter = "Image | *.png; *.jpg; *.gif";
        OpenFileDialog ofd;
        SaveFileDialog sfd;
        public static int Size;
        Complex[][] converted;

        private void transformToSpatialDomainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = FFTMethods.Inverse(converted);
            display = processed.ScaleImage(image_frame.Width, image_frame.Height);
            image_frame.Image = display;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sfd.FileName = image_path;
            if (sfd.ShowDialog()==DialogResult.OK)
            {
                processed.Save(sfd.FileName);
            }
        }

        private void transformToFreqencyDomainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            converted = FFTMethods.Forward(processed);
            processed = converted.VisualizeFourier();
            display = processed.ScaleImage(image_frame.Width, image_frame.Height);
            image_frame.Image = display;
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                image_path = ofd.FileName;
                image = (Bitmap)Bitmap.FromFile(image_path);
                Size = image.Width;
                image = image.Padding();
                display = image.ScaleImage(image_frame.Width, image_frame.Height);
                image_frame.Image = display;
                processed = image.Grayscale();
            }
        }
    }

    static class Ext
    {
        public static Bitmap ScaleImage(this Bitmap image, int width, int height)
        {
            float xratio = (float)width / image.Width;
            float yratio = (float)height / image.Height;
            float ratio = Math.Min(xratio, yratio);

            int w = (int)(image.Width * ratio);
            int h = (int)(image.Height * ratio);

            Bitmap result = new Bitmap(image, new Size(w, h));
            return result;
        }

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
            BitmapData output_data = result_image.LockBits(
                new Rectangle(0, 0, w, h),
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppArgb);

            Marshal.Copy(result, 0, output_data.Scan0, bytes);
            result_image.UnlockBits(output_data);

            return result_image;
        }

        public static Bitmap Padding(this Bitmap image)
        {
            int w = image.Width;
            int h = image.Height;
            int n = 0;
            while (FFT.Size <= Math.Max(w,h))
            {
                FFT.Size = (int)Math.Pow(2, n);
                if (FFT.Size == Math.Max(w,h))
                {
                    break;
                }
                n++;
            }
            double horizontal_padding = FFT.Size - w;
            double vertical_padding = FFT.Size - h;
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

            Bitmap padded_image = new Bitmap(FFT.Size, FFT.Size);

            BitmapData padded_data = padded_image.LockBits(
                new Rectangle(0, 0, FFT.Size, FFT.Size),
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppArgb);

            int padded_bytes = padded_data.Stride * padded_data.Height;
            byte[] result = new byte[padded_bytes];

            for (int i = 3; i < padded_bytes; i+=4)
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
            float[] frequencies = new float[FFT.Size * FFT.Size];
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

            Bitmap image = new Bitmap(FFT.Size, FFT.Size);
            BitmapData image_data = image.LockBits(
                new Rectangle(0, 0, FFT.Size, FFT.Size),
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppArgb);
            int bytes = image_data.Stride * image_data.Height;
            byte[] result = new byte[bytes];
            for (int y = 0; y < FFT.Size; y++)
            {
                for (int x = 0; x < FFT.Size; x++)
                {
                    int pixel_position = y * image_data.Stride + x * 4;
                    for (int i = 0; i < 3; i++)
                    {
                        result[pixel_position + i] = (byte)frequencies[y * FFT.Size + x];
                    }
                    result[pixel_position + 3] = 255;
                }
            }
            Marshal.Copy(result, 0, image_data.Scan0, bytes);
            image.UnlockBits(image_data);
            return image;
        }
    }

    public class FFTMethods : FFT
    {

        public static Complex[][] ToComplex(Bitmap image)
        {
            int w = image.Width;
            int h = image.Height;

            BitmapData input_data = image.LockBits(
                new Rectangle(0, 0, w, h),
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);

            int bytes = input_data.Stride * input_data.Height;

            byte[] buffer = new byte[bytes];
            Complex[][] result = new Complex[w][];

            Marshal.Copy(input_data.Scan0, buffer, 0, bytes);
            image.UnlockBits(input_data);

            int pixel_position;

            for (int x = 0; x < w; x++)
            {
                result[x] = new Complex[h];
                for (int y = 0; y < h; y++)
                {
                    pixel_position = y * input_data.Stride + x * 4;
                    result[x][y] = new Complex(buffer[pixel_position], 0);
                }
            }

            return result;
        }

        public static Complex[] Forward(Complex[] input, bool phaseShift = true)
        {
            var result = new Complex[input.Length];
            var omega = (float)(-2.0 * Math.PI / input.Length);

            if (input.Length == 1)
            {
                result[0] = input[0];

                if (Complex.IsNaN(result[0]))
                {
                    return new [] {new Complex(0, 0)};
                }
                return result;
            }

            var evenInput = new Complex[input.Length / 2];
            var oddInput = new Complex[input.Length / 2];

            for (int i = 0; i < input.Length / 2; i++)
            {
                evenInput[i] = input[2 * i];
                oddInput[i] = input[2 * i + 1];
            }

            var even = Forward(evenInput, phaseShift);
            var odd = Forward(oddInput, phaseShift);

            for (int k = 0; k < input.Length / 2; k++)
            {
                int phase;

                if (phaseShift)
                {
                    phase = k - Size / 2;
                }
                else
                {
                    phase = k;
                }
                odd[k] *= Complex.Polar(1, omega * phase);
            }

            for (int k = 0; k < input.Length / 2; k++)
            {
                result[k] = even[k] + odd[k];
                result[k + input.Length / 2] = even[k] - odd[k];
            }

            return result;
        }
        public static Complex[][] Forward(Bitmap image)
        {
            var p = new Complex[Size][];
            var f = new Complex[Size][];
            var t = new Complex[Size][];

            var complexImage = ToComplex(image);

            for (int l = 0; l < Size; l++)
            {
                p[l] = Forward(complexImage[l]);
            }

            for (int l = 0; l < Size; l++)
            {
                t[l] = new Complex[Size];
                for (int k = 0; k < Size; k++)
                {
                    t[l][k] = p[k][l];
                }
                f[l] = Forward(t[l]);
            }
            
            return f;
        }
        public static Complex[] Inverse(Complex[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                input[i] = Complex.Conjugate(input[i]);
            }

            var transform = Forward(input, false);

            for (int i = 0; i < input.Length; i++)
            {
                transform[i] = Complex.Conjugate(transform[i]);
            }

            return transform;
        }

        public static Bitmap Inverse(Complex[][] frequencies)
        {
            var p = new Complex[Size][];
            var f = new Complex[Size][];
            var t = new Complex[Size][];

            Bitmap image = new Bitmap(Size, Size);
            BitmapData image_data = image.LockBits(
                new Rectangle(0, 0, Size, Size),
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppArgb);
            int bytes = image_data.Stride * image_data.Height;
            byte[] result = new byte[bytes];

            for (int i = 0; i < Size; i++)
            {
                p[i] = Inverse(frequencies[i]);
            }

            for (int i = 0; i < Size; i++)
            {
                t[i] = new Complex[Size];
                for (int j = 0; j < Size; j++)
                {
                    t[i][j] = p[j][i] / (Size * Size);
                }
                f[i] = Inverse(t[i]);
            }

            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    int pixel_position = y * image_data.Stride + x * 4;
                    for (int i = 0; i < 3; i++)
                    {
                        result[pixel_position + i] = (byte)Complex.Modulus(f[x][y]);
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
