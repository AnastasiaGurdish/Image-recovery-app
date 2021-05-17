using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace ImageRecoveryApp
{
    public partial class ImageRecoveryImage : Form
    {
        Bitmap image, processed, processedDest, imagePadding, destroyed_image;
        string image_path;
        string filter = "Image | *.png; *.jpg; *.gif";
        public static int Size;
        OpenFileDialog ofd;
        SaveFileDialog sfd;
        Complex[][] converted;

        public ImageRecoveryImage()
        {
            InitializeComponent();
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
                UploadedImage.Image = image;
            }
            else
            {
                MessageBox.Show("Невозможно открыть изображение!");
            }
            imagePadding = image.Padding();
            processed = imagePadding.Grayscale();
            converted = FFTMethods.Forward(processed);
            processed = converted.VisualizeFourier();
            pictureBox1.Image = processed;
        }

        private void SpoilImage_Click(object sender, EventArgs e)
        {
            if (UploadedImage.Image != null)
            {
                destroyed_image = image.CutPartOfImage();
                Size = destroyed_image.Width;
                UploadedImage.Image = destroyed_image;
            }
            else
            {
                MessageBox.Show("Загрузите сперва изображение!");
            }
            imagePadding = destroyed_image.Padding();
            processedDest = imagePadding.Grayscale();
            converted = FFTMethods.Forward(processedDest);
            processedDest = converted.VisualizeFourier();
            pictureBox2.Image = processedDest;

        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            sfd.FileName = image_path;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                UploadedImage.Image.Save(sfd.FileName);
            }
        }

    }

}
