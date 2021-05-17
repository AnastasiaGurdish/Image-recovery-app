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

namespace Noise_Template
{
    public partial class NoiseDemo : Form
    {
        public NoiseDemo()
        {
            InitializeComponent();
            ofd = new OpenFileDialog();
            sfd = new SaveFileDialog();
            ofd.Filter = filter;
            sfd.Filter = filter;
        }

        OpenFileDialog ofd;
        SaveFileDialog sfd;
        string image_path;
        string filter = "Image | *.png; *.jpg; *.gif";
        Bitmap image, processed, display;

        private void addNoiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = image.UniformNoise();
            display = processed.ScaleImage(image_frame.Width, image_frame.Height);
            image_frame.Image = display;
            foreach (var series in chart1.Series)
            {
                series.Points.Clear();
            }
            int[] vals = ReadPixels(processed);
            for (int i = 0; i < vals.Length; i++)
            {
                chart1.Series["Bytes"].Points.AddXY(i, vals[i]);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sfd.FileName = image_path;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                processed.Save(sfd.FileName);
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                image_path = ofd.FileName;
                image = (Bitmap)Bitmap.FromFile(image_path);
                display = image.ScaleImage(image_frame.Width, image_frame.Height);
                image_frame.Image = display;
                foreach (var series in chart1.Series)
                {
                    series.Points.Clear();
                }
                int[] vals = ReadPixels(image);
                for (int i = 0; i < vals.Length; i++)
                {
                    chart1.Series["Bytes"].Points.AddXY(i, vals[i]);
                }
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private int[] ReadPixels(Bitmap img)
        {
            int w = img.Width;
            int h = img.Height;

            BitmapData sd = img.LockBits(new Rectangle(0, 0, w, h),
                ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            int bytes = sd.Stride * sd.Height;
            byte[] buffer = new byte[bytes];
            Marshal.Copy(sd.Scan0, buffer, 0, bytes);
            img.UnlockBits(sd);
            int[] p = new int[256];
            for (int i = 0; i < bytes; i += 4)
            {
                p[buffer[i]]++;
            }
            return p;
        }

        public static double Factorial(int number)
        {
            double result = 1;
            int n = number;
            while (n != 1)
            {
                result *= n;
                n -= 1;
            }

            return result;
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

        public static Bitmap UniformNoise(this Bitmap image)
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
            
            Random rnd = new Random();
            int noise_chance = 10;

            for (int i = 0; i < bytes; i+=3)
            {
                int max = (int)(1000 / noise_chance);
                int tmp = rnd.Next(max + 1);
                for (int j = 0; j < 3; j++)
                {
                    if (tmp == 0 || tmp == max)
                    {
                        result[i + j] = 0;
                    }
                    else
                    {
                        result[i + j] = buffer[i + j];
                    }
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
