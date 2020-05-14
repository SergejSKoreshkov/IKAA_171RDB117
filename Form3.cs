using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IKAA_171RDB117_3
{
    public partial class Form3 : Form
    {
        public imgData imgData1 = new imgData();
        public imgData imgData2 = new imgData();

        public Form3()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Bitmap.FromFile(openFileDialog1.FileName);

                Bitmap bmp = (Bitmap)pictureBox1.Image.Clone();
                imgData1.readImage(bmp);
                imgData2.readImage(bmp);
                pictureBox1.Image = imgData1.drawImage("RGB");
                imgData1.x0 = imgData1.img.GetLength(0) / 2;
                imgData1.y0 = imgData1.img.GetLength(1) / 2;

                GC.Collect();
            }
        }

        private void translation(int tx, int ty)
        {
            for (int i = 0; i < imgData1.img.GetLength(0); i++)
            {
                for (int j = 0; j < imgData1.img.GetLength(1); j++)
                {
                    if (imgData1.img != null)
                    {
                        imgData1.img[i, j].X = imgData1.img[i, j].X + tx;
                        imgData1.img[i, j].Y = imgData1.img[i, j].Y + ty;
                    }
                }
            }
        }

        private void skew(int tx, int ty)
        {
            for (int i = 0; i < imgData1.img.GetLength(0); i++)
            {
                for (int j = 0; j < imgData1.img.GetLength(1); j++)
                {
                    if (imgData1.img != null)
                    {
                        imgData1.img[i, j].X = imgData1.img[i, j].X + tx * imgData1.img[i, j].Y;
                        imgData1.img[i, j].Y = imgData1.img[i, j].Y + ty * imgData1.img[i, j].X;
                    }
                }
            }
        }

        private void rotation(double rads)
        {
            for (int i = 0; i < imgData1.img.GetLength(0); i++)
            {
                for (int j = 0; j < imgData1.img.GetLength(1); j++)
                {
                    if (imgData1.img != null)
                    {
                        imgData1.img[i, j].X = Convert.ToInt32(Math.Round(imgData1.img[i, j].X * Math.Cos(rads) - imgData1.img[i, j].Y * Math.Sin(rads)));
                        imgData1.img[i, j].Y = Convert.ToInt32(Math.Round(imgData1.img[i, j].X * Math.Sin(rads) + imgData1.img[i, j].Y * Math.Cos(rads)));
                    }
                }
            }
        }

        private void wave(int tx, int ty, double radsA, double radsB)
        {
            for (int i = 0; i < imgData1.img.GetLength(0); i++)
            {
                for (int j = 0; j < imgData1.img.GetLength(1); j++)
                {
                    if (imgData1.img != null)
                    {
                        try
                        {
                            imgData1.img[i, j].X = Convert.ToInt32(Math.Round(imgData1.img[i, j].X + tx * Math.Sin(2 * Math.PI * imgData1.img[i, j].Y / radsA)));
                            imgData1.img[i, j].Y = Convert.ToInt32(Math.Round(imgData1.img[i, j].Y + ty * Math.Sin(2 * Math.PI * imgData1.img[i, j].X / radsB)));
                        } catch { return; }
                    }
                }
            }
        }

        private void warp()
        {
            for (int i = 0; i < imgData1.img.GetLength(0); i++)
            {
                for (int j = 0; j < imgData1.img.GetLength(1); j++)
                {
                    if (imgData1.img != null)
                    {
                        imgData1.img[i, j].X = Convert.ToInt32((Math.Sign(imgData1.img[i, j].X)*Math.Pow((imgData1.img[i, j].X), 2)) / imgData1.x0);
                        imgData1.img[i, j].Y = imgData1.img[i, j].Y;
                    }
                }
            }
        }

        private void swirl()
        {
            int x0 = 0;
            int y0 = 0;
            for (int i = 0; i < imgData1.img.GetLength(0); i++)
            {
                for (int j = 0; j < imgData1.img.GetLength(1); j++)
                {
                    if (imgData1.img != null)
                    {
                        
                        double r = Math.Sqrt(Math.Pow(imgData1.img[i, j].X - x0, 2) + Math.Pow(imgData1.img[i, j].Y - y0, 2));
                        double rads = Math.PI * r / (pictureBox1.Image.Width);
                        imgData1.img[i, j].X = Convert.ToInt32(Math.Round((imgData1.img[i, j].X - x0) * Math.Cos(rads) + (imgData1.img[i, j].Y - y0) * Math.Sin(rads) + x0));
                        imgData1.img[i, j].Y = Convert.ToInt32(Math.Round(-(imgData1.img[i, j].X - x0) * Math.Sin(rads) + (imgData1.img[i, j].Y - y0) * Math.Cos(rads) + y0));
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            translation(
                Convert.ToInt32(textBox1.Text),
                Convert.ToInt32(textBox2.Text)
            );
            pictureBox1.Image = imgData1.drawImage("transformation");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            rotation(Convert.ToInt32(textBox3.Text) * Math.PI / 180.0);
            pictureBox1.Image = imgData1.drawImage("transformation");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < imgData1.img.GetLength(0); i++)
            {
                for (int j = 0; j < imgData1.img.GetLength(1); j++)
                {
                    if (imgData1.img != null)
                    {
                        imgData1.img[i, j].R = imgData2.img[i, j].R;
                        imgData1.img[i, j].G = imgData2.img[i, j].G;
                        imgData1.img[i, j].B = imgData2.img[i, j].B;

                        imgData1.img[i, j].X = i - imgData1.img.GetLength(0) / 2;
                        imgData1.img[i, j].Y = j - imgData1.img.GetLength(1) / 2;
                    }
                }
            }
            pictureBox1.Image = imgData1.drawImage("RGB");
            imgData1.x0 = pictureBox1.Image.Width / 2;
            imgData1.y0 = pictureBox1.Image.Height / 2;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image.Save(saveFileDialog1.FileName);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            skew(
                Convert.ToInt32(textBox4.Text),
                Convert.ToInt32(textBox5.Text)
            );
            pictureBox1.Image = imgData1.drawImage("transformation");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            wave(
                Convert.ToInt32(textBox6.Text),
                Convert.ToInt32(textBox7.Text),
                Convert.ToInt32(textBox8.Text),
                Convert.ToInt32(textBox9.Text)
            );
            pictureBox1.Image = imgData1.drawImage("transformation");
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            warp();
            pictureBox1.Image = imgData1.drawImage("transformation");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            swirl();
            pictureBox1.Image = imgData1.drawImage("transformation");
        }
    }
}
