using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace ImageRecoveryApp
{
    public partial class ImageRecoveryImage : Form
    {
        Bitmap OriginalImage, processedImage, DestroyedImage, processedDestroyedImage, reconstructedImage, reconstructedImageFinal, imagePadding;
        string image_path;
        string filter = "Image | *.png; *.jpg; *.gif";
        public static int Size;
        OpenFileDialog ofd;
        SaveFileDialog sfd;
        Complex[][] OriginalImageComplex, DestroyedImageComplex, reconstructedImageComplex;

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
                OriginalImage = (Bitmap)Bitmap.FromFile(image_path);
                Size = OriginalImage.Width;
                UploadedImage.Image = OriginalImage;
                imagePadding = OriginalImage.Padding();
                processedImage = imagePadding.Grayscale();
                OriginalImageComplex = Fourier.Forward(processedImage);
                processedImage = OriginalImageComplex.VisualizeFourier();
                pictureBox1.Image = processedImage;
            }
            else
            {
                MessageBox.Show("Невозможно открыть изображение!");
            }
        }

        private void SpoilImage_Click(object sender, EventArgs e)
        {
            if (UploadedImage.Image != null)
            {
                DestroyedImage = OriginalImage.CutPartOfImage();
                Size = DestroyedImage.Width;
                SpoiledImage.Image = DestroyedImage;
                imagePadding = DestroyedImage.Padding();
                processedDestroyedImage = imagePadding.Grayscale();
                DestroyedImageComplex = Fourier.Forward(processedDestroyedImage);
                processedDestroyedImage = DestroyedImageComplex.VisualizeFourier();
                pictureBox2.Image = processedDestroyedImage;
            }
            else
            {
                MessageBox.Show("Загрузите сперва изображение!");
            }
        }

        private void RecoverImage_Click(object sender, EventArgs e)
        {
            if(SpoiledImage.Image != null)
            {


                //  reconstructedImage = OriginalImageComplex.RecoverDestroidImage(DestroyedImageComplex);


                // reconstructedImage = reconstructedImageComplex.VisualizeFourier();

                // reconstructedImageComplex = Fourier.Forward(reconstructedImage);
                // reconstructedImage = reconstructedImageComplex.VisualizeFourier();
                // reconstructedImage = Fourier.Inverse(DestroyedImageComplex);

                //SpoiledImage.Image = reconstructedImage;


                // DestroyedImageComplex = Fourier.Forward(processedDestroyedImage);
                // processedDestroyedImage = DestroyedImageComplex.VisualizeFourier();

                reconstructedImage = OriginalImageComplex.RecoverDestroidImage(DestroyedImageComplex);
                reconstructedImageComplex = Fourier.Forward(reconstructedImage);
                reconstructedImageFinal = Fourier.Inverse(reconstructedImageComplex);
                pictureBox2.Image = reconstructedImageFinal;


                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    image_path = ofd.FileName;
                    OriginalImage = (Bitmap)Bitmap.FromFile(image_path);
                    Size = OriginalImage.Width;
                    SpoiledImage.Image = OriginalImage;
                }
                else
                {
                    MessageBox.Show("Невозможно открыть изображение!");
                }



            }
            else
            {
                MessageBox.Show("Нет испорченного изображения!");
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            sfd.FileName = image_path;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                UploadedImage.Image.Save(sfd.FileName);
                SpoiledImage.Image.Save(sfd.FileName);
            }
            else
            {
                MessageBox.Show("Ошибка сохранения!");
            }
        }

    }

}
