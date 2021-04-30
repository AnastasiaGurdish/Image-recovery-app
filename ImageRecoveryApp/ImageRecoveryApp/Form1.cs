using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace ImageRecoveryApp
{
    public partial class ImageRecoveryImage : Form
    {
        Bitmap image, destroyed_image, processed, display;
        string image_path;
        string filter = "Image | *.png; *.jpg; *.gif";
        public static int Size;
        OpenFileDialog ofd;
        SaveFileDialog sfd;
        Complex[][] converted;

        public ImageRecoveryImage()
        {
            InitializeComponent();
            chart1.ChartAreas[0].AxisX.Maximum = 280; //Задаешь максимальные значения координат
            chart1.ChartAreas[0].AxisY.Maximum = 6000;
            ofd = new OpenFileDialog();
            sfd = new SaveFileDialog();
            ofd.Filter = filter;
            sfd.Filter = filter;
        }

        private void UploadPhotoButton_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                image_path = ofd.FileName;
                image = (Bitmap)Bitmap.FromFile(image_path);
                Size = image.Width;
                image = image.Padding();
                display = image.ScaleImage(UploadedImage.Width, UploadedImage.Height);
                UploadedImage.Image = display;
                // processed = image.Grayscale();
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
            else
            {
                MessageBox.Show("Невозможно открыть изображение!");
            }
        }

        private void SpoilImage_Click(object sender, EventArgs e)
        {
            // processed = image.CutPartOfImage();
            // display = processed.ScaleImage(UploadedImage.Width, UploadedImage.Height);
            // UploadedImage.Image = display;
            // Вырезаем выбранный кусок картинки

            //  Rectangle rec = new Rectangle(50, 50, UploadedImage.Width - 50, UploadedImage.Height - 50);
            //Bitmap bmp = new Bitmap(image.Width,image.Height);
            //using (Graphics g = Graphics.FromImage(bmp))
            //{
            //    Rectangle crop = new Rectangle(222, 222, 55, 55);
            //    g.SetClip(crop);
            //    g.Clear(Color.Transparent);
            //}
            // UploadedImage.Image = bmp;

            using (Bitmap bmp = new Bitmap(100, 100, PixelFormat.Format32bppArgb))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                using (Brush opaqueRedBrush = new SolidBrush(Color.FromArgb(255, 255, 0, 0)))
                using (Brush semiRedBrush = new SolidBrush(Color.FromArgb(128, 255, 0, 0)))
                {
                    g.Clear(Color.Transparent);
                    Rectangle bigRect = new Rectangle(0, 0, 100, 100);
                    Rectangle smallRect = new Rectangle(25, 25, 50, 50);
                    g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                    g.FillRectangle(opaqueRedBrush, bigRect);
                    g.FillRectangle(semiRedBrush, smallRect);
                }
                bmp.Save(@"C:\FilePath\TestDrawTransparent.png", ImageFormat.Png); //Рабочий стол
            }
        }


        private void SaveButton_Click(object sender, EventArgs e)
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

}
