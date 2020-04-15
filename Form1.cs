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

namespace IKAA_171RDB117_3
{
    public partial class Form1 : Form
    {

        public imgData imgData = new imgData();
        public bool inverted = false;
        public int normalizePercentage = 0;

        public int lastVerticalSegmentType = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public Color[] getPixel(int x, int y, Bitmap imgNormal, Bitmap imgInverted)
        {
            try
            {
                Color[] colors = {
                    imgNormal.GetPixel(x, y),
                    imgInverted.GetPixel(x, y)
                };
                return colors;
            }
            catch
            {
                Color[] colors = { Color.White, Color.White };
                return colors;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                pictureBox1.Image = Bitmap.FromFile(openFileDialog1.FileName);

            Bitmap bmp = (Bitmap)pictureBox1.Image.Clone();
            imgData.readImage(bmp);
            pictureBox2.Image = imgData.drawImage("RGB");
            imgData.histOriginal.drawGraph(chart1, "RGB");
            imgData.histEdited.drawGraph(chart2, "RGB");

        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            inverted = false;
            label9.Visible = true;
            label12.Text = "R: ";
            label11.Text = "G: ";
            label10.Text = "B: ";
            label9.Text = "I: ";

            radioButton3.Checked = true;
            radioButton4.Text = "Red";
            radioButton5.Text = "Green";
            radioButton6.Text = "Blue";
            radioButton7.Text = "Intnsity";
            radioButton7.Visible = true;
            if (imgData.img != null)
            {
                pictureBox2.Image = imgData.drawImage("RGB");
                imgData.histEdited.drawGraph(chart2, "RGB");

            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            inverted = false;
            label9.Visible = false;
            label12.Text = "H: ";
            label11.Text = "S: ";
            label10.Text = "V: ";

            radioButton3.Checked = true;
            radioButton4.Text = "Hue";
            radioButton5.Text = "Saturation";
            radioButton6.Text = "Value";
            radioButton7.Visible = false;
            if (imgData.img != null)
            {
                pictureBox2.Image = imgData.drawImage("HSV");
                imgData.histEdited.drawGraph(chart2, "HSV");
            }
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            inverted = false;
            label9.Visible = true;
            label12.Text = "C: ";
            label11.Text = "M: ";
            label10.Text = "Y: ";
            label9.Text = "K: ";

            radioButton3.Checked = true;
            radioButton4.Text = "Cyan";
            radioButton5.Text = "Magenta";
            radioButton6.Text = "Yellow";
            radioButton7.Text = "Black";
            radioButton7.Visible = true;
            if (imgData.img != null)
            {
                pictureBox2.Image = imgData.drawImage("CMYK");
                imgData.histEdited.drawGraph(chart2, "CMYK");

            }
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            inverted = false;
            label9.Visible = false;

            label12.Text = "Y: ";
            label11.Text = "U: ";
            label10.Text = "V: ";
            label9.Visible = false;

            radioButton3.Checked = true;
            radioButton4.Text = "Intensity";
            radioButton5.Text = "Blue projection";
            radioButton6.Text = "Red projection";
            radioButton7.Visible = false;
            if (imgData.img != null)
            {
                pictureBox2.Image = imgData.drawImage("YUV");
                imgData.histEdited.drawGraph(chart2, "YUV");
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            inverted = false;
            label12.Visible = true;
            label11.Visible = true;
            label10.Visible = true;
            if (imgData.img != null)
            {
                if (radioButton1.Checked)
                {
                    pictureBox2.Image = imgData.drawImage("RGB");
                    imgData.histEdited.drawGraph(chart2, "RGB");
                    label9.Visible = true;
                }
                else if (radioButton2.Checked)
                {
                    pictureBox2.Image = imgData.drawImage("HSV");
                    imgData.histEdited.drawGraph(chart2, "HSV");
                    label9.Visible = false;
                }
                else if (radioButton8.Checked)
                {
                    pictureBox2.Image = imgData.drawImage("CMYK");
                    imgData.histEdited.drawGraph(chart2, "CMYK");
                    label9.Visible = true;
                }
                else if (radioButton9.Checked)
                {
                    pictureBox2.Image = imgData.drawImage("YUV");
                    imgData.histEdited.drawGraph(chart2, "YUV");
                    label9.Visible = false;
                }
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            inverted = false;
            label12.Visible = true;
            label11.Visible = false;
            label10.Visible = false;
            label9.Visible = false;

            if (imgData.img != null)
            {
                if (radioButton1.Checked)
                {
                    pictureBox2.Image = imgData.drawImage("R");
                    imgData.histEdited.drawGraph(chart2, "R");

                }
                else if (radioButton2.Checked)
                {
                    pictureBox2.Image = imgData.drawImage("H");
                    imgData.histEdited.drawGraph(chart2, "H");
                }
                else if (radioButton8.Checked)
                {
                    pictureBox2.Image = imgData.drawImage("C");
                    imgData.histEdited.drawGraph(chart2, "C");

                }
                else if (radioButton9.Checked)
                {
                    pictureBox2.Image = imgData.drawImage("Y1");
                    imgData.histEdited.drawGraph(chart2, "Y1");
                }
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            inverted = false;
            label12.Visible = false;
            label11.Visible = true;
            label10.Visible = false;
            label9.Visible = false;

            if (imgData.img != null)
            {
                if (radioButton1.Checked)
                {
                    pictureBox2.Image = imgData.drawImage("G");
                    imgData.histEdited.drawGraph(chart2, "G");

                }
                else if (radioButton2.Checked)
                {
                    pictureBox2.Image = imgData.drawImage("S");
                    imgData.histEdited.drawGraph(chart2, "S");
                }
                else if (radioButton8.Checked)
                {
                    pictureBox2.Image = imgData.drawImage("M");
                    imgData.histEdited.drawGraph(chart2, "M");


                }
                else if (radioButton9.Checked)
                {
                    pictureBox2.Image = imgData.drawImage("U1");
                    imgData.histEdited.drawGraph(chart2, "U1");
                }
            }
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            inverted = false;
            label12.Visible = false;
            label11.Visible = false;
            label10.Visible = true;
            label9.Visible = false;

            if (imgData.img != null)
            {
                if (radioButton1.Checked)
                {
                    pictureBox2.Image = imgData.drawImage("B");
                    imgData.histEdited.drawGraph(chart2, "B");

                }
                else if (radioButton2.Checked)
                {
                    pictureBox2.Image = imgData.drawImage("V");
                    imgData.histEdited.drawGraph(chart2, "V");
                }
                else if (radioButton8.Checked)
                {
                    pictureBox2.Image = imgData.drawImage("Y");
                    imgData.histEdited.drawGraph(chart2, "Y");

                }
                else if (radioButton9.Checked)
                {
                    pictureBox2.Image = imgData.drawImage("V1");
                    imgData.histEdited.drawGraph(chart2, "V1");
                }
            }
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            inverted = false;
            label12.Visible = false;
            label11.Visible = false;
            label10.Visible = false;
            label9.Visible = true;

            if (imgData.img != null)
            {
                if (radioButton1.Checked)
                {
                    pictureBox2.Image = imgData.drawImage("I");
                    imgData.histEdited.drawGraph(chart2, "I");

                }
                else if (radioButton2.Checked)
                {
                }
                else if (radioButton8.Checked)
                {
                    pictureBox2.Image = imgData.drawImage("K");
                    imgData.histEdited.drawGraph(chart2, "K");

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    pictureBox2.Image.Save(saveFileDialog1.FileName);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                Bitmap bmp = (Bitmap)pictureBox1.Image.Clone();
                imgData.readImage(bmp);
                pictureBox2.Image = imgData.drawImage("Invert");
                inverted = true;
                label12.Visible = true;
                label11.Visible = true;
                label10.Visible = true;
            }
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                double kX = (double)pictureBox1.Image.Width / pictureBox1.Width;
                double kY = (double)pictureBox1.Image.Height / pictureBox1.Height;
                double k = Math.Max(kX, kY);
                double nobideX = (pictureBox1.Width * k - pictureBox1.Image.Width) / 2;
                double nobideY = (pictureBox1.Height * k - pictureBox1.Image.Height) / 2;
                double kx = Math.Round(e.X * k - nobideX);
                double ky = Math.Round(e.Y * k - nobideY);

                label7.Text = "X: " + kx.ToString() + ", Y: " + ky.ToString();

                imgData imgDateObj = new imgData();
                Color[] pixels = getPixel((int)kx, (int)ky, new Bitmap(pictureBox1.Image), new Bitmap(pictureBox2.Image));
                label2.Text = "R: " + pixels[0].R.ToString();
                label3.Text = "G: " + pixels[0].G.ToString();
                label4.Text = "B: " + pixels[0].B.ToString();

                if (radioButton1.Checked)
                {
                    PixelClassRGB rgbi = new PixelClassRGB(pixels[1].R, pixels[1].G, pixels[1].B);
                    label12.Text = "R: " + rgbi.R.ToString();
                    label11.Text = "G: " + rgbi.G.ToString();
                    label10.Text = "B: " + rgbi.B.ToString();
                    label9.Text = "I: " + rgbi.I.ToString();
                }
                else if (radioButton2.Checked)
                {
                    if (inverted)
                    {
                        PixelClassHSV hsv = new PixelClassHSV(pixels[1].R, pixels[1].G, pixels[1].B);
                        label12.Text = "H: " + hsv.H.ToString();
                        label11.Text = "S: " + hsv.S.ToString();
                        label10.Text = "V: " + hsv.V.ToString();
                    }
                    else
                    {
                        PixelClassHSV hsv = new PixelClassHSV(pixels[0].R, pixels[0].G, pixels[0].B);
                        label12.Text = "H: " + hsv.H.ToString();
                        label11.Text = "S: " + hsv.S.ToString();
                        label10.Text = "V: " + hsv.V.ToString();
                    }
                }
                else if (radioButton8.Checked)
                {
                    if (inverted)
                    {
                        PixelClassCMYK cmyk = new PixelClassCMYK(pixels[1].R, pixels[1].G, pixels[1].B);
                        label12.Text = "C: " + cmyk.C.ToString();
                        label11.Text = "M: " + cmyk.M.ToString();
                        label10.Text = "Y: " + cmyk.Y.ToString();
                        label9.Text = "K: " + cmyk.K.ToString();
                    }
                    else
                    {
                        PixelClassCMYK cmyk = new PixelClassCMYK(pixels[0].R, pixels[0].G, pixels[0].B);
                        label12.Text = "C: " + cmyk.C.ToString();
                        label11.Text = "M: " + cmyk.M.ToString();
                        label10.Text = "Y: " + cmyk.Y.ToString();
                        label9.Text = "K: " + cmyk.K.ToString();
                    }
                }
                else if (radioButton9.Checked)
                {
                    if (inverted)
                    {
                        PixelClassYUV yuv = new PixelClassYUV(pixels[1].R, pixels[1].G, pixels[1].B);
                        label12.Text = "Y: " + yuv.Y.ToString();
                        label11.Text = "U: " + yuv.U.ToString();
                        label10.Text = "V: " + yuv.V.ToString();
                    }
                    else
                    {
                        PixelClassYUV yuv = new PixelClassYUV(pixels[0].R, pixels[0].G, pixels[0].B);
                        label12.Text = "Y: " + yuv.Y.ToString();
                        label11.Text = "U: " + yuv.U.ToString();
                        label10.Text = "V: " + yuv.V.ToString();
                    }
                }
            }
        }

        private void stretchingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                if (radioButton1.Checked)
                {
                    imgData.histEdited.readGraph(imgData.img);
                    if (radioButton4.Checked)
                    {
                        imgData.contrastByHistogram("R");
                        pictureBox2.Image = imgData.drawImage("Strech");
                    }
                    if (radioButton5.Checked)
                    {
                        imgData.contrastByHistogram("G");
                        pictureBox2.Image = imgData.drawImage("Strech");
                    }
                    if (radioButton6.Checked)
                    {
                        imgData.contrastByHistogram("B");
                        pictureBox2.Image = imgData.drawImage("Strech");
                    }
                    if (radioButton7.Checked)
                    {
                        imgData.contrastByHistogram("I");
                        pictureBox2.Image = imgData.drawImage("Strech I");
                    }
                    if (radioButton3.Checked)
                    {
                        imgData.contrastByHistogram("R");
                        imgData.contrastByHistogram("G");
                        imgData.contrastByHistogram("B");
                        imgData.contrastByHistogram("I");
                        pictureBox2.Image = imgData.drawImage("Strech");
                    }
                    imgData.histEdited.readGraph(imgData.imgChartRGB);
                    imgData.histEdited.drawGraph(chart2, "RGB");
                }
                else if (radioButton2.Checked)
                {
                    imgData.histEdited.readGraph(imgData.imghsv);
                    if (radioButton5.Checked)
                    {
                        imgData.contrastByHistogram("S");
                        pictureBox2.Image = imgData.drawImage("Strech S");
                    }
                    if (radioButton6.Checked)
                    {
                        imgData.contrastByHistogram("V");
                        pictureBox2.Image = imgData.drawImage("Strech HSV");
                    }
                    if (radioButton3.Checked)
                    {
                        imgData.contrastByHistogram("S");
                        imgData.contrastByHistogram("V");
                        imgData.contrastByHistogram("H");
                        pictureBox2.Image = imgData.drawImage("Strech HSV");
                    }
                    imgData.histEdited.readGraph(imgData.imgChartHSV);
                    imgData.histEdited.drawGraph(chart2, "HSV");
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                if (radioButton1.Checked)
                {
                    imgData.histEdited.readGraph(imgData.img);
                    if (radioButton4.Checked)
                    {
                        imgData.contrastByHistogram("R", true, normalizePercentage);
                        pictureBox2.Image = imgData.drawImage("Strech");
                    }
                    if (radioButton5.Checked)
                    {
                        imgData.contrastByHistogram("G", true, normalizePercentage);
                        pictureBox2.Image = imgData.drawImage("Strech");
                    }
                    if (radioButton6.Checked)
                    {
                        imgData.contrastByHistogram("B", true, normalizePercentage);
                        pictureBox2.Image = imgData.drawImage("Strech");
                    }
                    if (radioButton7.Checked)
                    {
                        imgData.contrastByHistogram("I", true, normalizePercentage);
                        pictureBox2.Image = imgData.drawImage("Strech I");
                    }
                    if (radioButton3.Checked)
                    {
                        imgData.contrastByHistogram("R", true, normalizePercentage);
                        imgData.contrastByHistogram("G", true, normalizePercentage);
                        imgData.contrastByHistogram("B", true, normalizePercentage);
                        imgData.contrastByHistogram("I", true, normalizePercentage);
                        pictureBox2.Image = imgData.drawImage("Strech");
                    }
                    imgData.histEdited.readGraph(imgData.imgChartRGB);
                    imgData.histEdited.drawGraph(chart2, "RGB");
                }
                else if (radioButton2.Checked)
                {
                    imgData.histEdited.readGraph(imgData.imghsv);
                    if (radioButton5.Checked)
                    {
                        imgData.contrastByHistogram("S", true, normalizePercentage);
                        pictureBox2.Image = imgData.drawImage("Strech S");
                    }
                    if (radioButton6.Checked)
                    {
                        imgData.contrastByHistogram("V", true, normalizePercentage);
                        pictureBox2.Image = imgData.drawImage("Strech HSV");
                    }
                    if (radioButton3.Checked)
                    {
                        imgData.contrastByHistogram("S", true, normalizePercentage);
                        imgData.contrastByHistogram("V", true, normalizePercentage);
                        imgData.contrastByHistogram("H", true, normalizePercentage);
                        pictureBox2.Image = imgData.drawImage("Strech HSV");
                    }
                    imgData.histEdited.readGraph(imgData.imgChartHSV);
                    imgData.histEdited.drawGraph(chart2, "HSV");
                }
                // radioButton1.Checked = true;
                // radioButton2.Checked = false;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label13.Text = trackBar1.Value.ToString() + " %";
            normalizePercentage = trackBar1.Value;
        }
        private void blur(int type)
        {
            if (imgData.img != null)
            {
                imgData.filter = new Filter(); //jauns filtrs
                imgData.filter.filter3x3Blur(type); //veidojam filtru

                imgData.filterImage(imgData.filter); //filtrējam attēlu

                imgData.histEdited.readGraph(imgData.img); //noalasam histogrammu
                imgData.histEdited.drawGraph(chart2, "RGB"); //zīmējam histogrammu

                radioButton1.Checked = true; //RGB
                radioButton3.Checked = true; //Composite

                pictureBox2.Image = imgData.drawImage("Strech"); //izvadam attēlu

                GC.Collect();
            }
        }
        private void antiBlur(int type)
        {
            if (imgData.img != null)
            {
                imgData.filter = new Filter(); //jauns filtrs
                imgData.filter.filterAntiBlur(type); //veidojam filtru

                imgData.filterImage(imgData.filter); //filtrējam attēlu

                imgData.histEdited.readGraph(imgData.img); //noalasam histogrammu
                imgData.histEdited.drawGraph(chart2, "RGB"); //zīmējam histogrammu

                radioButton1.Checked = true; //RGB
                radioButton3.Checked = true; //Composite

                pictureBox2.Image = imgData.drawImage("Strech"); //izvadam attēlu

                GC.Collect();
            }
        }
        private void medianFilter(int type)
        {
            if (imgData.img != null)
            {
                imgData.filterMedianImage(type); //filtrējam attēlu

                imgData.histEdited.readGraph(imgData.img); //noalasam histogrammu
                imgData.histEdited.drawGraph(chart2, "RGB"); //zīmējam histogrammu

                radioButton1.Checked = true; //RGB
                radioButton3.Checked = true; //Composite

                pictureBox2.Image = imgData.drawImage("Strech"); //izvadam attēlu

                GC.Collect();
            }
        }
        private void median3_Click(object sender, EventArgs e)
        {
            this.medianFilter(3);
        }
        private void median5_Click(object sender, EventArgs e)
        {
            this.medianFilter(5);
        }
        private void median7_Click(object sender, EventArgs e)
        {
            this.medianFilter(7);
        }
        private void blur1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.blur(16);
        }
        private void blur2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.blur(10);
        }
        private void blur3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.blur(9);
        }
        private void blur4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.antiBlur(1);
        }
        private void blur5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.antiBlur(2);
        }
        private void blur6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.antiBlur(3);
        }

        private void edgeSobelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                imgData.edgeSegmentation(1);
                pictureBox2.Image = imgData.drawImage("Strech");
            }
        }
        private void Sobel_R(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                imgData.edgeSegmentation(1);
                pictureBox2.Image = imgData.drawImage("Strech R");
            }
        }
        private void Sobel_G(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                imgData.edgeSegmentation(1);
                pictureBox2.Image = imgData.drawImage("Strech G");
            }
        }
        private void Sobel_B(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                imgData.edgeSegmentation(1);
                pictureBox2.Image = imgData.drawImage("Strech B");
            }
        }
        private void Sobel_I(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                imgData.edgeSegmentation(1);
                pictureBox2.Image = imgData.drawImage("Strech I");
            }
        }
        private void edgeRobertsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                imgData.edgeSegmentation(2);
                pictureBox2.Image = imgData.drawImage("Strech");
            }
        }
        private void Roberts_R(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                imgData.edgeSegmentation(2);
                pictureBox2.Image = imgData.drawImage("Strech R");
            }
        }
        private void Roberts_G(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                imgData.edgeSegmentation(2);
                pictureBox2.Image = imgData.drawImage("Strech G");
            }
        }
        private void Roberts_B(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                imgData.edgeSegmentation(2);
                pictureBox2.Image = imgData.drawImage("Strech B");
            }
        }
        private void Roberts_I(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                imgData.edgeSegmentation(2);
                pictureBox2.Image = imgData.drawImage("Strech I");
            }
        }
        private void edgePrewittToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                imgData.edgeSegmentation(3);
                pictureBox2.Image = imgData.drawImage("Strech");
            }
        }
        private void Prewitt_R(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                imgData.edgeSegmentation(3);
                pictureBox2.Image = imgData.drawImage("Strech R");
            }
        }
        private void Prewitt_G(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                imgData.edgeSegmentation(3);
                pictureBox2.Image = imgData.drawImage("Strech G");
            }
        }
        private void Prewitt_B(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                imgData.edgeSegmentation(3);
                pictureBox2.Image = imgData.drawImage("Strech B");
            }
        }
        private void Prewitt_I(object sender, EventArgs e)
        {
            if (imgData.img != null)
            {
                imgData.edgeSegmentation(3);
                pictureBox2.Image = imgData.drawImage("Strech I");
            }
        }

        private void verticalSegment(String color, Color chartColor, int type, int threshold = 0, int threshold2 = 0)
        {
            trackBar2.Maximum = 255;
            if (imgData.img != null)
            {
                imgData.histogramSegmentation(type, threshold, threshold2);

                pictureBox2.Image = imgData.drawImage("Strech " + color);

                if (threshold == 0)
                {
                    label14.Text = "Threshold: " + Convert.ToString(imgData.histEdited.calculateAutomaticThreshold(imgData.histEdited.hB));
                }
                chart2.Series.Clear();
                chart2.Series.Add(color);
                chart2.Series[color].Color = chartColor;
                chart2.Series[color].Points.AddXY(imgData.histEdited.calculateAutomaticThreshold(imgData.histEdited.hB), 0);
                chart2.Series[color].Points.AddXY(imgData.histEdited.calculateAutomaticThreshold(imgData.histEdited.hB), imgData.histEdited.hB[256]);

            }
        }

        private void horizontalSegment(String color, Color chartColor, int type, int threshold = 0)
        {
            trackBar2.Maximum = 22000;
            if (imgData.img != null)
            {
                imgData.histogramSegmentationHorizontal(type, threshold);

                pictureBox2.Image = imgData.drawImage("Strech " + color);

                chart2.Series.Clear();
                chart2.Series.Add(color);
                chart2.Series[color].Color = chartColor;
                chart2.Series[color].Points.AddXY(imgData.histEdited.calculateAutomaticThreshold(imgData.histEdited.hB), 0);
                chart2.Series[color].Points.AddXY(imgData.histEdited.calculateAutomaticThreshold(imgData.histEdited.hB), imgData.histEdited.hB[256]);

            }
        }
        private void verticalAuto_I(object sender, EventArgs e)
        {
            this.verticalSegment("I", Color.Gray, 1);
        }
        private void verticalAuto_R(object sender, EventArgs e)
        {
            this.verticalSegment("R", Color.Red, 2);
        }
        private void verticalAuto_G(object sender, EventArgs e)
        {
            this.verticalSegment("G", Color.Green, 3);
        }
        private void verticalAuto_B(object sender, EventArgs e)
        {
            this.verticalSegment("B", Color.Blue, 4);
        }
 
        private void varticalManual_I(object sender, EventArgs e) { this.lastVerticalSegmentType = 1; this.trackBar2_Scroll(null, null); }
        private void varticalManual_R(object sender, EventArgs e) { this.lastVerticalSegmentType = 2; this.trackBar2_Scroll(null, null); }
        private void varticalManual_G(object sender, EventArgs e) { this.lastVerticalSegmentType = 3; this.trackBar2_Scroll(null, null); }
        private void varticalManual_B(object sender, EventArgs e) { this.lastVerticalSegmentType = 4; this.trackBar2_Scroll(null, null); }

        private void varticalManual2_I(object sender, EventArgs e) { this.lastVerticalSegmentType = 5; this.trackBar2_Scroll(null, null); }
        private void varticalManual2_R(object sender, EventArgs e) { this.lastVerticalSegmentType = 6; this.trackBar2_Scroll(null, null); }
        private void varticalManual2_G(object sender, EventArgs e) { this.lastVerticalSegmentType = 7; this.trackBar2_Scroll(null, null); }
        private void varticalManual2_B(object sender, EventArgs e) { this.lastVerticalSegmentType = 8; this.trackBar2_Scroll(null, null); }

        private void horizontalManual_I(object sender, EventArgs e) { this.lastVerticalSegmentType = 9; this.trackBar2_Scroll(null, null); }
        private void horizontalManual_R(object sender, EventArgs e) { this.lastVerticalSegmentType = 10; this.trackBar2_Scroll(null, null); }
        private void horizontalManual_G(object sender, EventArgs e) { this.lastVerticalSegmentType = 11; this.trackBar2_Scroll(null, null); }
        private void horizontalManual_B(object sender, EventArgs e) { this.lastVerticalSegmentType = 12; this.trackBar2_Scroll(null, null); }


        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            int scrollValue = trackBar2.Value;
            int scrollValue2 = trackBar3.Value;
            label14.Text = "Threshold: " + Convert.ToString(scrollValue);

            if (scrollValue > trackBar3.Value && scrollValue <= 254)
            {
                trackBar3.Value = scrollValue2 = scrollValue + 1;
                label15.Text = "Threshold: " + Convert.ToString(scrollValue2);
            }

            switch (this.lastVerticalSegmentType)
            {
                case 1: this.verticalSegment("I", Color.Gray, 1, scrollValue); break;
                case 2: this.verticalSegment("R", Color.Red, 2, scrollValue); break;
                case 3: this.verticalSegment("G", Color.Green, 3, scrollValue); break;
                case 4: this.verticalSegment("B", Color.Blue, 4, scrollValue); break;

                case 5: this.verticalSegment("I", Color.Gray, 1, scrollValue, scrollValue2); break;
                case 6: this.verticalSegment("R", Color.Red, 2, scrollValue, scrollValue2); break;
                case 7: this.verticalSegment("G", Color.Green, 3, scrollValue, scrollValue2); break;
                case 8: this.verticalSegment("B", Color.Blue, 4, scrollValue, scrollValue2); break;

                case 9: this.horizontalSegment("I", Color.Gray, 1, scrollValue); break;
                case 10: this.horizontalSegment("R", Color.Red, 2, scrollValue); break;
                case 11: this.horizontalSegment("G", Color.Green, 3, scrollValue); break;
                case 12: this.horizontalSegment("B", Color.Blue, 4, scrollValue); break;

            }
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            int scrollValue = trackBar3.Value;
            label15.Text = "Threshold: " + Convert.ToString(scrollValue);

            if (scrollValue < trackBar2.Value && scrollValue >= 1)
            {
                trackBar2.Value = scrollValue - 1;
            }
            this.trackBar2_Scroll(null, null);
        }
    }
}
