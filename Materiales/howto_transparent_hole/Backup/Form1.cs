using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Imaging;

namespace howto_transparent_hole
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Bitmap OriginalImage = null;

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ofdFile.ShowDialog() == DialogResult.OK)
            {
                OriginalImage = new Bitmap(ofdFile.FileName);
                picImage.Image = new Bitmap(ofdFile.FileName);
                saveAsToolStripMenuItem.Enabled = true;
                picImage.Visible = true;
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sfdFile.ShowDialog() == DialogResult.OK)
            {
                using (Bitmap bm = CutOutOval(StartPoint, EndPoint))
                {
                    SaveImage(bm, sfdFile.FileName);

                    // Display the result.
                    Bitmap sample_bm = new Bitmap(bm.Width, bm.Height);
                    using (Graphics gr = Graphics.FromImage(sample_bm))
                    {
                        for (int y = 0; y < bm.Height; y += 16)
                        {
                            int iy = y / 16;
                            for (int x = 0; x < bm.Width; x += 16)
                            {
                                int ix = x / 16;
                                if ((ix + iy) % 2 == 0)
                                    gr.FillRectangle(Brushes.Red, x, y, 16, 16);
                                else
                                    gr.FillRectangle(Brushes.Black, x, y, 16, 16);
                            }
                        }

                        Rectangle rect = new Rectangle(0, 0, bm.Width, bm.Height);
                        using (TextureBrush brush =
                            new TextureBrush(bm, rect))
                        {
                            gr.FillRectangle(brush, rect);
                        }
                        picImage.Image = sample_bm;
                    }
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Select an elliptical area.
        private bool Drawing = false;
        private Point StartPoint, EndPoint;
        private void picImage_MouseDown(object sender, MouseEventArgs e)
        {
            Drawing = true;
            StartPoint = e.Location;
            EndPoint = e.Location;
        }

        private void picImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (!Drawing) return;
            EndPoint = e.Location;
            DrawImage();
        }

        private void picImage_MouseUp(object sender, MouseEventArgs e)
        {
            Drawing = false;
        }

        private void DrawImage()
        {
            Bitmap bm = new Bitmap(OriginalImage);
            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.SmoothingMode = SmoothingMode.AntiAlias;
                using (Pen pen = new Pen(Color.Magenta, 3))
                {
                    gr.DrawEllipse(pen,
                        SelectedRect(StartPoint, EndPoint));
                }
            }
            picImage.Image = bm;
        }

        private Bitmap CutOutOval(Point start_point, Point end_point)
        {
            // Make a region covering the whole picture.
            Rectangle full_rect = new Rectangle(
                0, 0, OriginalImage.Width, OriginalImage.Height);
            Region region = new Region(full_rect);

            // Remove the ellipse from the region.
            Rectangle ellipse_rect =
                SelectedRect(start_point, end_point);
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(ellipse_rect);
            region.Exclude(path);

            // Draw.
            Bitmap bm = new Bitmap(OriginalImage);
            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.Clear(Color.Transparent);

                // Fill the region.
                gr.SetClip(region, CombineMode.Replace);
                gr.SmoothingMode = SmoothingMode.AntiAlias;
                using (TextureBrush brush =
                    new TextureBrush(OriginalImage, full_rect))
                {
                    gr.FillRectangle(brush, full_rect);
                }
            }
            return bm;
        }

        // Save the file with the appropriate format.
        public void SaveImage(Image image, string filename)
        {
            string extension = Path.GetExtension(filename);
            switch (extension.ToLower())
            {
                case ".bmp":
                    image.Save(filename, ImageFormat.Bmp);
                    break;
                case ".exif":
                    image.Save(filename, ImageFormat.Exif);
                    break;
                case ".gif":
                    image.Save(filename, ImageFormat.Gif);
                    break;
                case ".jpg":
                case ".jpeg":
                    image.Save(filename, ImageFormat.Jpeg);
                    break;
                case ".png":
                    image.Save(filename, ImageFormat.Png);
                    break;
                case ".tif":
                case ".tiff":
                    image.Save(filename, ImageFormat.Tiff);
                    break;
                default:
                    throw new NotSupportedException(
                        "Unknown file extension " + extension);
            }
        }

        private Rectangle SelectedRect(Point point0, Point point1)
        {
            int x = Math.Min(point0.X, point1.X);
            int y = Math.Min(point0.Y, point1.Y);
            int width = Math.Abs(point1.X - point0.X);
            int height = Math.Abs(point1.Y - point0.Y);
            return new Rectangle(x, y, width, height);
        }
    }
}
