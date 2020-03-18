using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Filter
{
    public partial class Window : Form
    {
        Bitmap originalImage;
        List<Bitmap> images = new List<Bitmap>();
        int action;
        public Window()
        {
            InitializeComponent();
        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(picBox.Image);
            Color[] allPixels = new Color[image.Height * image.Width];
            Color[] newPixels = new Color[image.Height * image.Width];
            Color red = Color.Red;
            int currentPixel = 0;
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++, currentPixel++)
                {
                    //
                    allPixels[currentPixel] = image.GetPixel(x, y);

                    image.SetPixel(x, y, Color.FromArgb(Math.Min(Math.Max(image.GetPixel(x, y).R + 100, 0), 255), Math.Min(Math.Max(image.GetPixel(x, y).G - 100, 0), 255), Math.Min(Math.Max(image.GetPixel(x, y).B - 100, 0), 255)));
                }
            }
            //debugLabel.Text = Convert.ToString(allPixels[0]);
            currentPixel = 0;
            //Color color = image.GetPixel(x, y);
            picBox.Image = image;
            ImageAdder(action, image);
            action++;
        }

        private void openImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FileDialog.ShowDialog() == DialogResult.OK)
            {
                Bitmap image = new Bitmap(FileDialog.FileName);
                originalImage = new Bitmap(FileDialog.FileName);
                picBox.Image = image;
                picBox.Height = image.Height;
                picBox.Width = image.Width;
                ImageAdder(action, image);
                action = 0;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(picBox.Image);
            Color[] allPixels = new Color[image.Height * image.Width];
            Color[] newPixels = new Color[image.Height * image.Width];
            Color red = Color.Red;
            int currentPixel = 0;
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++, currentPixel++)
                {
                    //allPixels[currentPixel] = image.GetPixel(x, y);

                    image.SetPixel(x, y, Color.FromArgb(Math.Min(Math.Max(image.GetPixel(x, y).R - 100, 0), 255), Math.Min(Math.Max(image.GetPixel(x, y).G - 100, 0), 255), Math.Min(Math.Max(image.GetPixel(x, y).B + 100, 0), 255)));
                }
            }
            //debugLabel.Text = Convert.ToString(allPixels[0]);
            currentPixel = 0;
            //Color color = image.GetPixel(x, y);
            picBox.Image = image;
            ImageAdder(action, image);
            action++;
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(picBox.Image);
            Color[] allPixels = new Color[image.Height * image.Width];
            Color[] newPixels = new Color[image.Height * image.Width];
            Color red = Color.Red;
            int currentPixel = -1;
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++, currentPixel++)
                {
                    //allPixels[currentPixel] = image.GetPixel(x, y);

                    image.SetPixel(x, y, Color.FromArgb(Math.Min(Math.Max(image.GetPixel(x, y).R - 100, 0), 255), Math.Min(Math.Max(image.GetPixel(x, y).G + 50, 0), 255), Math.Min(Math.Max(image.GetPixel(x, y).B - 100, 0), 255)));
                }
            }
            //debugLabel.Text = Convert.ToString(allPixels[0]);
            currentPixel = 0;
            //Color color = image.GetPixel(x, y);
            picBox.Image = image;
            ImageAdder(action, image);
            action++;
        }

        private void grayscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(picBox.Image);
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color bright = Color.FromArgb((image.GetPixel(x, y).R + image.GetPixel(x, y).G + image.GetPixel(x, y).B) / 3, (image.GetPixel(x, y).R + image.GetPixel(x, y).G + image.GetPixel(x, y).B) / 3, (image.GetPixel(x, y).R + image.GetPixel(x, y).G + image.GetPixel(x, y).B) / 3);
                    image.SetPixel(x, y, bright);
                }
            }
            picBox.Image = image;
            ImageAdder(action, image);
            action++;
        }

        private void resetImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            picBox.Image = originalImage;
            ImageAdder(action, originalImage);
            action++;
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                picBox.Image = images[Math.Max(action - 1, 0)];
                action--;
            }
            catch (Exception)
            {
            }
        }

        private void ImageAdder(int action, Bitmap image)
        {
            try
            {
                if (images[action] != null)
                {
                    images[action + 1] = image;
                }
            }
            catch (Exception)
            {
                images.Add(image);
            }
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                picBox.Image = images[action + 1];
                action++;
            }
            catch (Exception)
            {
            }
        }

        //private void invertedToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    Bitmap image = new Bitmap(picBox.Image);
        //    Color[] allPixels = new Color[image.Height * image.Width];
        //    Color[] newPixels = new Color[image.Height * image.Width];
        //    for (int y = 0; y < image.Height; y++)
        //    {
        //        for (int x = 0; x < image.Width; x++)
        //        {
        //            image.SetPixel(x, y, Color.FromArgb(255 - image.GetPixel(x, y).R, 255 - image.GetPixel(x, y).G, 255 - image.GetPixel(x, y).B));
        //        }
        //        picBox.Image = image;
        //        ImageAdder(action, image);
        //        action++;
        //    }
        //}

        private void invertedToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(picBox.Image);
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    image.SetPixel(x, y, Color.FromArgb(255 - image.GetPixel(x, y).R, 255 - image.GetPixel(x, y).G, 255 - image.GetPixel(x, y).B));
                }
            }
            picBox.Image = image;
            ImageAdder(action, image);
            action++;
        }

        private void brightenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(picBox.Image);
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    image.SetPixel(x, y, Color.FromArgb(Math.Min(image.GetPixel(x, y).R + 50, 255), Math.Min(image.GetPixel(x, y).G + 50, 255), Math.Min(image.GetPixel(x, y).B + 50, 255)));
                }
            }
            picBox.Image = image;
            ImageAdder(action, image);
            action++;
        }
    }
}
