using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms.DataVisualization.Charting;

namespace IKAA_171RDB117_3
{
    public class Histogram
    {
        public int[] hR;
        public int[] hG;
        public int[] hB;
        public int[] hI;

        public int[] hH;
        public int[] hS;
        public int[] hV;

        public int[] hC;
        public int[] hM;
        public int[] hY;
        public int[] hK;

        public int[] hY1;
        public int[] hU1;
        public int[] hV1;


        public Histogram()
        {
            hR = new int[257];
            hG = new int[257];
            hB = new int[257];
            hI = new int[257];

            hH = new int[257];
            hS = new int[257];
            hV = new int[257];

            hC = new int[257];
            hM = new int[257];
            hY = new int[257];
            hK = new int[257];

            hY1 = new int[257];
            hU1 = new int[257];
            hV1 = new int[257];
        }


        public int FindFirst(int[] H, int x)
        {
            x = (int)(x / 100.0 * H[256]);
            int i = 0;
            try
            {
                while (H[i] <= x)
                {
                    i++;
                }
                return i;
            }
            catch
            {
                return 0;
            }
        }

        public int FindLast(int[] H, int x)
        {
            x = (int)(x / 100.0 * H[256]);
            int i = 255;
            try
            {
                while (H[i] <= x)
                {
                    i--;
                }
                return i;
            }
            catch
            {
                return 0;
            }
        }
        public void clearGrah()
        {
            for (int i = 0; i < 256; i++)
            {
                hR[i] = 0;
                hG[i] = 0;
                hB[i] = 0;
                hI[i] = 0;

                hH[i] = 0;
                hS[i] = 0;
                hV[i] = 0;

                hC[i] = 0;
                hM[i] = 0;
                hY[i] = 0;
                hK[i] = 0;

                hY1[i] = 0;
                hU1[i] = 0;
                hV1[i] = 0;
            }
        }

        public void readGraph(PixelClassRGB[,] imgArray)
        {
            clearGrah();
            for (int i = 0; i < imgArray.GetLength(0); i++)
            {
                for (int j = 0; j < imgArray.GetLength(1); j++)
                {
                    hR[imgArray[i, j].R]++;
                    hG[imgArray[i, j].G]++;
                    hB[imgArray[i, j].B]++;
                    hI[imgArray[i, j].I]++;

                    hR[256] = Math.Max(hR[256], hR[imgArray[i, j].R]);
                    hG[256] = Math.Max(hG[256], hG[imgArray[i, j].G]);
                    hB[256] = Math.Max(hB[256], hB[imgArray[i, j].B]);
                    hI[256] = Math.Max(hI[256], hI[imgArray[i, j].I]);
                }
            }
        }
        public void readGraph(PixelClassHSV[,] imgArray)
        {
            clearGrah();
            for (int i = 0; i < imgArray.GetLength(0); i++)
            {
                for (int j = 0; j < imgArray.GetLength(1); j++)
                {
                    hH[(byte)(imgArray[i, j].H / 360.0 * 255.0)]++;
                    hS[imgArray[i, j].S]++;
                    hV[imgArray[i, j].V]++;

                    hH[256] = Math.Max(hR[256], hH[(byte)(imgArray[i, j].H / 360.0 * 255.0)]);
                    hS[256] = Math.Max(hG[256], hG[imgArray[i, j].S]);
                    hV[256] = Math.Max(hB[256], hB[imgArray[i, j].V]);
                }
            }
        }
        public void readGraph(PixelClassCMYK[,] imgArray)
        {
            clearGrah();
            for (int i = 0; i < imgArray.GetLength(0); i++)
            {
                for (int j = 0; j < imgArray.GetLength(1); j++)
                {
                    hC[(byte)(imgArray[i, j].C * 100)]++;
                    hM[(byte)(imgArray[i, j].M * 100)]++;
                    hY[(byte)(imgArray[i, j].Y * 100)]++;
                    hK[(byte)(imgArray[i, j].K * 100)]++;
                }
            }
        }
        public void readGraph(PixelClassYUV[,] imgArray)
        {
            clearGrah();
            for (int i = 0; i < imgArray.GetLength(0); i++)
            {
                for (int j = 0; j < imgArray.GetLength(1); j++)
                {
                    hY1[imgArray[i, j].Y]++;
                    hU1[imgArray[i, j].U]++;
                    hV1[imgArray[i, j].V]++;
                }
            }
        }

        public void drawGraph(Chart chart1, string type)
        {
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();

            chart1.ChartAreas.Add("ChartArea");
            chart1.ChartAreas["ChartArea"].BackColor = Color.Transparent;
            chart1.ChartAreas["ChartArea"].AxisX.Maximum = 255;
            chart1.ChartAreas["ChartArea"].AxisX.Minimum = 0;

            switch (type)
            {
                case "RGB":
                    chart1.Series.Add("R");
                    chart1.Series.Add("G");
                    chart1.Series.Add("B");
                    chart1.Series.Add("I");

                    chart1.Series["R"].Color = Color.Red;
                    chart1.Series["G"].Color = Color.Green;
                    chart1.Series["B"].Color = Color.Blue;
                    chart1.Series["I"].Color = Color.Gray;

                    for (int i = 0; i < 256; i++)
                    {
                        chart1.Series["R"].Points.AddXY(i, hR[i]);
                        chart1.Series["G"].Points.AddXY(i, hG[i]);
                        chart1.Series["B"].Points.AddXY(i, hB[i]);
                        chart1.Series["I"].Points.AddXY(i, hI[i]);
                    }
                    break;
                case "R":
                    chart1.Series.Add("R");

                    chart1.Series["R"].Color = Color.Red;

                    for (int i = 0; i < 256; i++)
                    {
                        chart1.Series["R"].Points.AddXY(i, hR[i]);
                    }
                    break;
                case "G":
                    chart1.Series.Add("G");

                    chart1.Series["G"].Color = Color.Green;

                    for (int i = 0; i < 256; i++)
                    {
                        chart1.Series["G"].Points.AddXY(i, hG[i]);
                    }
                    break;
                case "B":
                    chart1.Series.Add("B");

                    chart1.Series["B"].Color = Color.Blue;

                    for (int i = 0; i < 256; i++)
                    {
                        chart1.Series["B"].Points.AddXY(i, hB[i]);
                    }
                    break;
                case "I":
                    chart1.Series.Add("I");

                    chart1.Series["I"].Color = Color.Gray;

                    for (int i = 0; i < 256; i++)
                    {
                        chart1.Series["I"].Points.AddXY(i, hI[i]);
                    }
                    break;
                case "CMYK":
                    chart1.ChartAreas["ChartArea"].AxisX.Maximum = 100;

                    chart1.Series.Add("C");
                    chart1.Series.Add("M");
                    chart1.Series.Add("Y");
                    chart1.Series.Add("K");

                    chart1.Series["C"].Color = Color.Cyan;
                    chart1.Series["M"].Color = Color.Magenta;
                    chart1.Series["Y"].Color = Color.Yellow;
                    chart1.Series["K"].Color = Color.Black;

                    for (int i = 0; i < 256; i++)
                    {
                        chart1.Series["C"].Points.AddXY(i, hC[i]);
                        chart1.Series["M"].Points.AddXY(i, hM[i]);
                        chart1.Series["Y"].Points.AddXY(i, hY[i]);
                        chart1.Series["K"].Points.AddXY(i, hK[i]);
                    }
                    break;
                case "C":
                    chart1.ChartAreas["ChartArea"].AxisX.Maximum = 100;
                    chart1.Series.Add("C");

                    chart1.Series["C"].Color = Color.Cyan;

                    for (int i = 0; i < 256; i++)
                    {
                        chart1.Series["C"].Points.AddXY(i, hC[i]);
                    }
                    break;
                case "M":
                    chart1.ChartAreas["ChartArea"].AxisX.Maximum = 100;
                    chart1.Series.Add("M");

                    chart1.Series["M"].Color = Color.Magenta;

                    for (int i = 0; i < 256; i++)
                    {
                        chart1.Series["M"].Points.AddXY(i, hM[i]);
                    }
                    break;
                case "Y":
                    chart1.ChartAreas["ChartArea"].AxisX.Maximum = 100;
                    chart1.Series.Add("Y");

                    chart1.Series["Y"].Color = Color.Yellow;

                    for (int i = 0; i < 256; i++)
                    {
                        chart1.Series["Y"].Points.AddXY(i, hY[i]);
                    }
                    break;
                case "K":
                    chart1.ChartAreas["ChartArea"].AxisX.Maximum = 100;
                    chart1.Series.Add("K");

                    chart1.Series["K"].Color = Color.Black;

                    for (int i = 0; i < 256; i++)
                    {
                        chart1.Series["K"].Points.AddXY(i, hK[i]);
                    }
                    break;
                case "HSV":
                    chart1.Series.Add("H");
                    chart1.Series.Add("S");
                    chart1.Series.Add("V");

                    chart1.Series["H"].Color = Color.Orange;
                    chart1.Series["S"].Color = Color.LightBlue;
                    chart1.Series["V"].Color = Color.Gray;

                    for (int i = 0; i < 256; i++)
                    {
                        chart1.Series["H"].Points.AddXY(i, hH[i]);
                        chart1.Series["S"].Points.AddXY(i, hS[i]);
                        chart1.Series["V"].Points.AddXY(i, hV[i]);
                    }
                    break;
                case "H":
                    chart1.Series.Add("H");

                    chart1.Series["H"].Color = Color.Orange;

                    for (int i = 0; i < 256; i++)
                    {
                        chart1.Series["H"].Points.AddXY(i, hH[i]);
                    }
                    break;
                case "S":
                    chart1.Series.Add("S");

                    chart1.Series["S"].Color = Color.LightBlue;

                    for (int i = 0; i < 256; i++)
                    {
                        chart1.Series["S"].Points.AddXY(i, hS[i]);
                    }
                    break;
                case "V":
                    chart1.Series.Add("V");

                    chart1.Series["V"].Color = Color.Gray;

                    for (int i = 0; i < 256; i++)
                    {
                        chart1.Series["V"].Points.AddXY(i, hV[i]);
                    }
                    break;
                case "YUV":
                    chart1.Series.Add("Y");
                    chart1.Series.Add("U");
                    chart1.Series.Add("V");

                    chart1.Series["Y"].Color = Color.Magenta;
                    chart1.Series["U"].Color = Color.Blue;
                    chart1.Series["V"].Color = Color.Red;

                    for (int i = 0; i < 256; i++)
                    {
                        chart1.Series["Y"].Points.AddXY(i, hY1[i]);
                        chart1.Series["U"].Points.AddXY(i, hU1[i]);
                        chart1.Series["V"].Points.AddXY(i, hV1[i]);
                    }
                    break;
                case "Y1":
                    chart1.Series.Add("Y");

                    chart1.Series["Y"].Color = Color.Magenta;

                    for (int i = 0; i < 256; i++)
                    {
                        chart1.Series["Y"].Points.AddXY(i, hY1[i]);
                    }
                    break;
                case "U1":
                    chart1.Series.Add("U");

                    chart1.Series["U"].Color = Color.Blue;

                    for (int i = 0; i < 256; i++)
                    {
                        chart1.Series["U"].Points.AddXY(i, hU1[i]);
                    }
                    break;
                case "V1":
                    chart1.Series.Add("V");

                    chart1.Series["V"].Color = Color.Red;

                    for (int i = 0; i < 256; i++)
                    {
                        chart1.Series["V"].Points.AddXY(i, hV1[i]);
                    }
                    break;
            }
        }

        public int calculateAutomaticThreshold(int[] H)
        {
            int Dbegin = FindFirst(H, 0);
            int Dend = FindLast(H, 0);
            int T = (Dend - Dbegin) / 2;
            int Tprevious = 0;
            while (T != Tprevious)
            {
                Tprevious = T;
                int m1 = 0;
                int m2 = 0;
                int p1 = 0;
                int p2 = 0;

                for (int i = Dbegin; i < T; i++)
                {
                    p1 += H[i];
                    m1 += (i * H[i]);
                }

                if (p1 == 0) { p1 = 1; }
                m1 /= p1;

                for (int i = T; i <= Dend; i++)
                {
                    p2 += H[i];
                    m2 += (i * H[i]);
                }


                if (p2 == 0) { p2 = 1; }
                m2 /= p2;

                T = (m1 + m2) / 2;
            }
            return T;
        }
    }
}
