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
    public partial class Form2 : Form
    {
        public imgData imgData1 = new imgData();
        public imgData imgData2 = new imgData();
        public imgData imgData3 = new imgData();

        public int transitionStep;
        public Form2()
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
                pictureBox1.Image = imgData1.drawImage("RGB");
                GC.Collect();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image = Bitmap.FromFile(openFileDialog2.FileName);
                Bitmap bmp = (Bitmap)pictureBox2.Image.Clone();
                imgData2.readImage(bmp);
                pictureBox2.Image = imgData2.drawImage("RGB");
                GC.Collect();
            }
        }

        private void applyTransition (int s)
        {
            for (int i = 0; i < imgData1.img.GetLength(0); i++)
            {
                for (int j = 0; j < imgData1.img.GetLength(1); j++)
                {
                    imgData3.img[i, j] = new PixelClassRGB();

                    if (radioButton1.Checked)
                    {
                        imgData3.img[i, j].R = Convert.ToByte(((double)s / 100) * imgData2.img[i, j].R + (1 - (double)s / 100) * imgData1.img[i, j].R);
                        imgData3.img[i, j].G = Convert.ToByte(((double)s / 100) * imgData2.img[i, j].G + (1 - (double)s / 100) * imgData1.img[i, j].G);
                        imgData3.img[i, j].B = Convert.ToByte(((double)s / 100) * imgData2.img[i, j].B + (1 - (double)s / 100) * imgData1.img[i, j].B );
                    }
                    else if (radioButton2.Checked) {
                        if (i < imgData1.img.GetLength(0) * s / 100)
                        {
                            imgData3.img[i, j].R = imgData2.img[i, j].R;
                            imgData3.img[i, j].G = imgData2.img[i, j].G;
                            imgData3.img[i, j].B = imgData2.img[i, j].B;
                        }
                        else
                        {
                            imgData3.img[i, j].R = imgData1.img[i, j].R;
                            imgData3.img[i, j].G = imgData1.img[i, j].G;
                            imgData3.img[i, j].B = imgData1.img[i, j].B;
                        }
                    }
                    else if (radioButton3.Checked)
                    {
                        if (j < imgData1.img.GetLength(1) * s / 100)
                        {
                            imgData3.img[i, j].R = imgData2.img[i, j].R;
                            imgData3.img[i, j].G = imgData2.img[i, j].G;
                            imgData3.img[i, j].B = imgData2.img[i, j].B;
                        }
                        else
                        {
                            imgData3.img[i, j].R = imgData1.img[i, j].R;
                            imgData3.img[i, j].G = imgData1.img[i, j].G;
                            imgData3.img[i, j].B = imgData1.img[i, j].B;
                        }
                    }
                    else if (radioButton4.Checked)
                    {
                        if (j < (imgData1.img.GetLength(1) / 2 - (imgData1.img.GetLength(1) * s / 200)) ||
                            j > (imgData1.img.GetLength(1) / 2 + (imgData1.img.GetLength(1) * s / 200)))
                        {
                            imgData3.img[i, j].R = imgData2.img[i, j].R;
                            imgData3.img[i, j].G = imgData2.img[i, j].G;
                            imgData3.img[i, j].B = imgData2.img[i, j].B;
                        }
                        else
                        {
                            imgData3.img[i, j].R = imgData1.img[i, j].R;
                            imgData3.img[i, j].G = imgData1.img[i, j].G;
                            imgData3.img[i, j].B = imgData1.img[i, j].B;
                        }
                    }
                    else if (radioButton5.Checked)
                    {
                        if (i < (imgData1.img.GetLength(0) / 2 - (imgData1.img.GetLength(0) * s / 200)) ||
                            i > (imgData1.img.GetLength(0) / 2 + (imgData1.img.GetLength(0) * s / 200)))
                        {
                            imgData3.img[i, j].R = imgData2.img[i, j].R;
                            imgData3.img[i, j].G = imgData2.img[i, j].G;
                            imgData3.img[i, j].B = imgData2.img[i, j].B;
                        }
                        else
                        {
                            imgData3.img[i, j].R = imgData1.img[i, j].R;
                            imgData3.img[i, j].G = imgData1.img[i, j].G;
                            imgData3.img[i, j].B = imgData1.img[i, j].B;
                        }
                    }
                    else if (radioButton6.Checked)
                    {
                        if (
                            (i < (imgData1.img.GetLength(0) / 2 - (imgData1.img.GetLength(0) * s / 200)) ||
                            i > (imgData1.img.GetLength(0) / 2 + (imgData1.img.GetLength(0) * s / 200))) &&
                           (j < (imgData1.img.GetLength(1) / 2 - (imgData1.img.GetLength(1) * s / 200)) ||
                            j > (imgData1.img.GetLength(1) / 2 + (imgData1.img.GetLength(1) * s / 200))))
                        {
                            imgData3.img[i, j].R = imgData2.img[i, j].R;
                            imgData3.img[i, j].G = imgData2.img[i, j].G;
                            imgData3.img[i, j].B = imgData2.img[i, j].B;
                        }
                        else
                        {
                            imgData3.img[i, j].R = imgData1.img[i, j].R;
                            imgData3.img[i, j].G = imgData1.img[i, j].G;
                            imgData3.img[i, j].B = imgData1.img[i, j].B;
                        }
                    }
                    else if (radioButton7.Checked)
                    {
                        if (Math.Pow(j, 2) + Math.Pow(i, 2) > Math.Pow(imgData1.img.GetLength(0) * s / 100 + imgData1.img.GetLength(1) * s / 100, 2))
                        {
                            imgData3.img[i, j].R = imgData2.img[i, j].R;
                            imgData3.img[i, j].G = imgData2.img[i, j].G;
                            imgData3.img[i, j].B = imgData2.img[i, j].B;
                        }
                        else
                        {
                            imgData3.img[i, j].R = imgData1.img[i, j].R;
                            imgData3.img[i, j].G = imgData1.img[i, j].G;
                            imgData3.img[i, j].B = imgData1.img[i, j].B;
                        }
                    }
                    else if (radioButton8.Checked)
                    {
                        if (j > Math.Pow(s / 100.0, 5) * imgData1.img.GetLength(1) && i > Math.Pow(s / 100.0, 5) * imgData1.img.GetLength(0))
                        {
                            imgData3.img[i, j].R = imgData2.img[i, j].R;
                            imgData3.img[i, j].G = imgData2.img[i, j].G;
                            imgData3.img[i, j].B = imgData2.img[i, j].B;
                        }
                        else
                        {
                            imgData3.img[i, j].R = imgData1.img[i, j].R;
                            imgData3.img[i, j].G = imgData1.img[i, j].G;
                            imgData3.img[i, j].B = imgData1.img[i, j].B;
                        }
                    }
                }
            }
            pictureBox3.Image = imgData3.drawImage("transition");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            transitionStep++;
            if (transitionStep > 100)
            {
                timer1.Enabled = false;
                progressBar1.Value = 100;
            }
            else
            {
                progressBar1.Value = transitionStep;
                progressBar1.Value = transitionStep - 1;
                applyTransition(transitionStep);
            }
        }

        public void applyEffect (String Method)
        {
            if (imgData1 != null && imgData2 != null && imgData1.img.GetLength(0) ==
                imgData2.img.GetLength(0) && imgData1.img.GetLength(1) == imgData2.img.GetLength(1))
            {
                imgData3.img = new PixelClassRGB[imgData1.img.GetLength(0), imgData2.img.GetLength(1)];

                if (imgData3.img != null)
                {
                    for (int i = 0; i < imgData1.img.GetLength(0); i++)
                    {
                        for (int j = 0; j < imgData1.img.GetLength(1); j++)
                        {
                            imgData3.img[i, j] = new PixelClassRGB();

                            switch (Method)
                            {
                                case "Opacity":
                                    imgData3.img[i, j].effectOpacity(imgData1.img[i, j], imgData2.img[i, j], (double)trackBar1.Value/100);
                                    break;

                                case "Screen":
                                    imgData3.img[i, j].effectScreen(imgData1.img[i, j], imgData2.img[i, j]);
                                    break;
                                case "Darken":
                                    imgData3.img[i, j].effectDarken(imgData1.img[i, j], imgData2.img[i, j]);
                                    break;
                                case "Lighten":
                                    imgData3.img[i, j].effectLighten(imgData1.img[i, j], imgData2.img[i, j]);
                                    break;
                                case "Multiply":
                                    imgData3.img[i, j].effectMultiply(imgData1.img[i, j], imgData2.img[i, j]);
                                    break;
                                case "Addition":
                                    imgData3.img[i, j].effectLinearDodge(imgData1.img[i, j], imgData2.img[i, j]);
                                    break;
                                case "Dodge":
                                    imgData3.img[i, j].effectDodge(imgData1.img[i, j], imgData2.img[i, j]);
                                    break;
                                case "Burn":
                                    imgData3.img[i, j].effectBurn(imgData1.img[i, j], imgData2.img[i, j]);
                                    break;
                                case "Overlay":
                                    imgData3.img[i, j].effectOverlay(imgData1.img[i, j], imgData2.img[i, j]);
                                    break;
                                case "HardLight":
                                    imgData3.img[i, j].effectHardLight(imgData1.img[i, j], imgData2.img[i, j]);
                                    break;
                                case "Diff":
                                    imgData3.img[i, j].effectDiff(imgData1.img[i, j], imgData2.img[i, j]);
                                    break;
                            }
                        }
                    }
                }
            }
            pictureBox3.Image = imgData3.drawImage("transition");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            applyEffect("Opacity");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            applyEffect("Screen");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            imgData3.img = new PixelClassRGB[imgData1.img.GetLength(0), imgData2.img.GetLength(1)];
            transitionStep = 0;
            timer1.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            applyEffect("Darken");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            applyEffect("Lighten");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            applyEffect("Multiply");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            applyEffect("Addition");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            applyEffect("Dodge");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            applyEffect("Burn");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            applyEffect("Overlay");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            applyEffect("HardLight");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            applyEffect("Diff");
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = "Opacity = " + trackBar1.Value.ToString() + " of 100"; 
        }
    }
}
