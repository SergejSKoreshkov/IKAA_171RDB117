using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace IKAA_171RDB117_3
{
    public class imgData
    {
        public PixelClassRGB[,] img;
        public PixelClassHSV[,] imghsv;
        public PixelClassCMYK[,] imgcmyk;
        public PixelClassYUV[,] imgyuv;

        public PixelClassRGB[,] imgChartRGB;
        public PixelClassCMYK[,] imgChartCMYK;
        public PixelClassYUV[,] imgChartYUV;
        public PixelClassHSV[,] imgChartHSV;

        public Histogram histOriginal;
        public Histogram histEdited;

        public Filter filter;

        ~imgData()
        {
            img = null;
            imghsv = null;
            imgcmyk = null;
            imgyuv = null;

            imgChartRGB = null;
            imgChartCMYK = null;
            imgChartHSV = null;
            imgChartYUV = null;
            histEdited = null;
            histOriginal = null;
        }

        public void filterImage(Filter f)
        {
            if (img != null)
            {
                for (int x = 1; x < img.GetLength(0) - 1; x++)
                {
                    for (int y = 1; y < img.GetLength(1) - 1; y++)
                    {
                        int r = 0;
                        int g = 0;
                        int b = 0;
                        int i = 0;

                        for (int fi = 0; fi < 3; fi++)
                        {
                            for (int fj = 0; fj < 3; fj++)
                            {
                                r += img[x + fi - 1, y + fj - 1].R * f.F[fi, fj];
                                g += img[x + fi - 1, y + fj - 1].G * f.F[fi, fj];
                                b += img[x + fi - 1, y + fj - 1].B * f.F[fi, fj];
                                i += img[x + fi - 1, y + fj - 1].I * f.F[fi, fj];
                            }
                        }

                        Math.Max(0, Math.Min(255, r /= f.K));
                        Math.Max(0, Math.Min(255, g /= f.K));
                        Math.Max(0, Math.Min(255, b /= f.K));
                        Math.Max(0, Math.Min(255, i /= f.K));

                        imgChartRGB[x, y].R = (byte)r;
                        imgChartRGB[x, y].G = (byte)g;
                        imgChartRGB[x, y].B = (byte)b;
                        imgChartRGB[x, y].I = (byte)i;
                    }
                }
            }
        }

        public void edgeSegmentation(int type = 1, string mode = "I")
        {
            if (img != null)
            {
                int[,] Sx;
                int[,] Sy;
                switch (type)
                {
                    case 1:
                        Sx = new int[,] {
                                    { -1, 0, 1 },
                                    { -2, 0, 2 },
                                    { -1, 0, 1 } };

                        Sy = new int[,] {
                                     { -1, -2, -1 },
                                    {  0,  0,  0 },
                                    {  1,  2,  1 } };
                        break;
                    case 2:
                        Sx = new int[,] {
                                    { -1, 0 },
                                    { 0, 1 } };

                        Sy = new int[,] {
                                     { 1, 0 },
                                     { 0, -1 } };
                        break;
                    case 3:
                        Sx = new int[,] {
                                    { -1, 0, 1 },
                                    { -1, 0, 1 },
                                    { -1, 0, 1 } };

                        Sy = new int[,] {
                                     { 1, 1, 1 },
                                    {  0,  0,  0 },
                                    {  -1,  -1,  -1 } };
                        break;
                    default:
                        Sx = new int[,] {
                                    { -1, 0, 1 },
                                    { -2, 0, 2 },
                                    { -1, 0, 1 } };

                        Sy = new int[,] {
                                     { -1, -2, -1 },
                                    {  0,  0,  0 },
                                    {  1,  2,  1 } };
                        break;
                }
                //izskrienam cauri attēlam
                for (int x = 1; x < img.GetLength(0) - 1; x++)
                {
                    for (int y = 1; y < img.GetLength(1) - 1; y++)
                    {
                        int GxI = 0;
                        int GyI = 0;
                        int GI = 0;

                        int GxR = 0;
                        int GyR = 0;
                        int GR = 0;

                        int GxG = 0;
                        int GyG = 0;
                        int GG = 0;

                        int GxB = 0;
                        int GyB = 0;
                        int GB = 0;

                        //Apskatam visus tekoša pikseļa visus kaimiņus
                        for (int fi = 0; fi < (type == 2 ? 2 : 3); fi++)
                        {
                            for (int fj = 0; fj < (type == 2 ? 2 : 3); fj++)
                            {
                                GxI += img[x + fi - 1, y + fj - 1].I * Sx[fi, fj];
                                GyI += img[x + fi - 1, y + fj - 1].I * Sy[fi, fj];
                                GxR += img[x + fi - 1, y + fj - 1].R * Sx[fi, fj];
                                GyR += img[x + fi - 1, y + fj - 1].R * Sy[fi, fj];
                                GxG += img[x + fi - 1, y + fj - 1].G * Sx[fi, fj];
                                GyG += img[x + fi - 1, y + fj - 1].G * Sy[fi, fj];
                                GxB += img[x + fi - 1, y + fj - 1].B * Sx[fi, fj];
                                GyB += img[x + fi - 1, y + fj - 1].B * Sy[fi, fj];
                            }
                        }
                        //izrēķinam rezultējošu vērtību un nosakam kur ir robeža
                        GI = Convert.ToInt32(Math.Sqrt(GxI * GxI + GyI * GyI));
                        GR = Convert.ToInt32(Math.Sqrt(GxR * GxR + GyR * GyR));
                        GG = Convert.ToInt32(Math.Sqrt(GxG * GxG + GyG * GyG));
                        GB = Convert.ToInt32(Math.Sqrt(GxB * GxB + GyB * GyB));
                        //normalizējam attēlu atšķelot robežvērtības kas
                        //pēc intensitātes<128 un iekrāsojam pikseli melnā
                        //krāsa-fons, jeb baltā-robeža
                        if (GI < 128) { imgChartRGB[x, y].I = 0; }
                        else { imgChartRGB[x, y].I = 255; }

                        if (GR < 128) { imgChartRGB[x, y].R = 0; }
                        else { imgChartRGB[x, y].I = 255; }

                        if (GG < 128) { imgChartRGB[x, y].G = 0; }
                        else { imgChartRGB[x, y].I = 255; }

                        if (GB < 128) { imgChartRGB[x, y].B = 0; }
                        else { imgChartRGB[x, y].I = 255; }
                    }
                }
            }
        }
        public void filterMedianImage(int size = 3)
        {
            int borders = size / 2;
            if (img != null)
            {
                List<PixelClassRGB> sorted;
                for (int x = size - 2; x < img.GetLength(0) - borders; x++)
                {
                    for (int y = size - 2; y < img.GetLength(1) - borders; y++)
                    {
                        List<PixelClassRGB> pixels = new List<PixelClassRGB>();
                        for (int fi = 0; fi < size; fi++)
                        {
                            for (int fj = 0; fj < size; fj++)
                            {
                                pixels.Add(img[x - borders + fi, y - borders + fj]);
                            }
                        }
                        sorted = pixels.OrderBy(o => o.I).ToList();
                        imgChartRGB[x, y] = new PixelClassRGB(sorted[(size * size) / 2 + 1]);
                    }
                }
            }
        }

        public void contrastByHistogram(string mode, bool normalize = false, int normalizePercentage = 0)
        {
            int[] hRGBI = new int[257];
            if (mode == "R") { hRGBI = histOriginal.hR; }
            else if (mode == "G") { hRGBI = histOriginal.hG; }
            else if (mode == "B") { hRGBI = histOriginal.hB; }
            else if (mode == "I") { hRGBI = histOriginal.hI; }
            else if (mode == "S") { hRGBI = histEdited.hS; }
            else if (mode == "V") { hRGBI = histEdited.hV; }
            else if (mode == "H") { hRGBI = histEdited.hH; }

            Console.WriteLine(histEdited.hS);
            int DBegin = histEdited.FindFirst(hRGBI, normalize ? normalizePercentage : 0);
            int Dend = histEdited.FindLast(hRGBI, normalize ? normalizePercentage : 0);
            int Doriginal = Dend - DBegin;
            int Ddesired = 255;
            double k = Ddesired / (double)Doriginal;
            for (int i = 0; i < imgChartRGB.GetLength(0); i++)
            {
                for (int j = 0; j < imgChartRGB.GetLength(1); j++)
                {
                    if (mode == "I")
                    {
                        imgChartRGB[i, j].I = (byte)Math.Min(255, Math.Max(0, k * (img[i, j].I - DBegin)));
                    }
                    else if (mode == "R")
                    {
                        imgChartRGB[i, j].R = (byte)Math.Min(255, Math.Max(0, k * (img[i, j].R - DBegin)));
                    }
                    else if (mode == "G")
                    {
                        imgChartRGB[i, j].G = (byte)Math.Min(255, Math.Max(0, k * (img[i, j].G - DBegin)));
                    }
                    else if (mode == "B")
                    {
                        imgChartRGB[i, j].B = (byte)Math.Min(255, Math.Max(0, k * (img[i, j].B - DBegin)));
                    }
                }
            }
            for (int i = 0; i < imgChartHSV.GetLength(0); i++)
            {
                for (int j = 0; j < imgChartHSV.GetLength(1); j++)
                {
                    if (mode == "S")
                    {
                        imgChartHSV[i, j].S = (byte)Math.Min(255, Math.Max(0, k * (imghsv[i, j].S - DBegin)));
                    }
                    else if (mode == "V")
                    {
                        imgChartHSV[i, j].V = (byte)Math.Min(255, Math.Max(0, k * (imghsv[i, j].V - DBegin)));
                    }
                    else if (mode == "H")
                    {
                        imgChartHSV[i, j].H = imghsv[i, j].H;
                    }
                }
            }
        }

        public void histogramSegmentation(int type = 1, int threshold = 0, int threshold2 = 0)
        {
            if (img != null)
            {
                int T = 0;
                if (threshold == 0)
                {
                    switch (type)
                    {
                        case 1: T = histEdited.calculateAutomaticThreshold(histEdited.hI); break;
                        case 2: T = histEdited.calculateAutomaticThreshold(histEdited.hR); break;
                        case 3: T = histEdited.calculateAutomaticThreshold(histEdited.hG); break;
                        case 4: T = histEdited.calculateAutomaticThreshold(histEdited.hB); break;
                    }
                } 
                else
                {
                    T = threshold;
                }
                for (int x = 1; x < img.GetLength(0) - 1; x++)
                {
                    for (int y = 1; y < img.GetLength(1) - 1; y++)
                    {
                        switch (type)
                        {
                            case 1:
                                if (img[x, y].I <= T) { imgChartRGB[x, y].I = 0; }
                                else if (img[x, y].I > threshold2) { imgChartRGB[x, y].I = 255; }
                                else { imgChartRGB[x, y].I = 128; }
                                break;
                            case 2:
                                if (img[x, y].R <= T) { imgChartRGB[x, y].R = 0; }
                                else if (img[x, y].R > threshold2) { imgChartRGB[x, y].R = 255; }
                                else { imgChartRGB[x, y].R = 128; }
                                break;
                            case 3:
                                if (img[x, y].G <= T) { imgChartRGB[x, y].G = 0; }
                                else if (img[x, y].G > threshold2) { imgChartRGB[x, y].G = 255; }
                                else { imgChartRGB[x, y].G = 128; }
                                break;
                            case 4:
                                if (img[x, y].B <= T) { imgChartRGB[x, y].B = 0; }
                                else if (img[x, y].B > threshold2) { imgChartRGB[x, y].B = 255; }
                                else { imgChartRGB[x, y].B = 128; }
                                break;
                        }
                    }
                }
            }
        }

        public void histogramSegmentationHorizontal(int type = 1, int threshold = 0)
        {
            if (img != null)
            {
                for (int x = 1; x < img.GetLength(0) - 1; x++)
                {
                    for (int y = 1; y < img.GetLength(1) - 1; y++)
                    {
                        switch (type)
                        {
                            case 1:
                                if (histOriginal.hI[img[x, y].I] <= threshold) { imgChartRGB[x, y].I = 0; }
                                else { imgChartRGB[x, y].I = 255; }
                                break;
                            case 2:
                                if (histOriginal.hR[img[x, y].R] <= threshold) { imgChartRGB[x, y].R = 0; }
                                else { imgChartRGB[x, y].R = 255; }
                                break;
                            case 3:
                                if (histOriginal.hG[img[x, y].G] <= threshold) { imgChartRGB[x, y].G = 0; }
                                else { imgChartRGB[x, y].G = 255; }
                                break;
                            case 4:
                                if (histOriginal.hB[img[x, y].B] <= threshold) { imgChartRGB[x, y].B = 0; }
                                else { imgChartRGB[x, y].B = 255; }
                                break;
                        }
                    }
                }
            }
        }

        public void readImage(Bitmap bmp)
        {

            img = new PixelClassRGB[bmp.Width, bmp.Height];
            imghsv = new PixelClassHSV[bmp.Width, bmp.Height];
            imgcmyk = new PixelClassCMYK[bmp.Width, bmp.Height];
            imgyuv = new PixelClassYUV[bmp.Width, bmp.Height];

            imgChartRGB = new PixelClassRGB[bmp.Width, bmp.Height];
            imgChartCMYK = new PixelClassCMYK[bmp.Width, bmp.Height];
            imgChartHSV = new PixelClassHSV[bmp.Width, bmp.Height];
            imgChartYUV = new PixelClassYUV[bmp.Width, bmp.Height];
            histOriginal = new Histogram();
            histEdited = new Histogram();

            var bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, bmp.PixelFormat);

            IntPtr ptr = IntPtr.Zero;
            int pixelComponents;


            if (bmpData.PixelFormat == PixelFormat.Format24bppRgb)
                pixelComponents = 3;
            else if (bmpData.PixelFormat == PixelFormat.Format32bppArgb)
                pixelComponents = 4;
            else
                pixelComponents = 0;
            var line = new byte[bmp.Width * pixelComponents];

            for (int y = 0; y < bmpData.Height; y++)
            {
                ptr = bmpData.Scan0 + y * bmpData.Stride;
                Marshal.Copy(ptr, line, 0, line.Length);

                for (int x = 0; x < bmpData.Width; x++)
                {
                    img[x, y] = new PixelClassRGB(line[pixelComponents * x + 2], line[pixelComponents * x + 1], line[pixelComponents * x]);
                    imghsv[x, y] = new PixelClassHSV(img[x, y].R, img[x, y].G, img[x, y].B);
                    imgcmyk[x, y] = new PixelClassCMYK(img[x, y].R, img[x, y].G, img[x, y].B);
                    imgyuv[x, y] = new PixelClassYUV(img[x, y].R, img[x, y].G, img[x, y].B);

                    imgChartCMYK[x, y] = new PixelClassCMYK(img[x, y].R, img[x, y].G, img[x, y].B);
                    imgChartRGB[x, y] = new PixelClassRGB(line[pixelComponents * x + 2], line[pixelComponents * x + 1], line[pixelComponents * x]);
                    imgChartYUV[x, y] = new PixelClassYUV(img[x, y].R, img[x, y].G, img[x, y].B);
                    imgChartHSV[x, y] = new PixelClassHSV(img[x, y].R, img[x, y].G, img[x, y].B);
                }
            }
            bmp.UnlockBits(bmpData);
            histOriginal.readGraph(img);
        }

        public Bitmap drawImage(string mode)
        {
            if (img != null)
            {
                IntPtr ptr = IntPtr.Zero;
                int Height = img.GetLength(1);
                int Width = img.GetLength(0);
                var bmp = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
                var bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                        ImageLockMode.WriteOnly, bmp.PixelFormat);
                var line = new byte[bmp.Width * 3];

                for (int y = 0; y < bmpData.Height; y++)
                {
                    for (int x = 0; x < bmpData.Width; x++)
                    {
                        switch (mode)
                        {
                            case "RGB":
                                {
                                    line[3 * x] = img[x, y].B;
                                    line[3 * x + 1] = img[x, y].G;
                                    line[3 * x + 2] = img[x, y].R;
                                    imgChartRGB[x, y].B = line[3 * x];
                                    imgChartRGB[x, y].G = line[3 * x + 1];
                                    imgChartRGB[x, y].R = line[3 * x + 2];
                                    imgChartRGB[x, y].I = img[x, y].I;
                                    break;
                                }
                            case "R":
                                {
                                    line[3 * x] = 0;
                                    line[3 * x + 1] = 0;
                                    line[3 * x + 2] = img[x, y].R;
                                    imgChartRGB[x, y].B = 0;
                                    imgChartRGB[x, y].G = 0;
                                    imgChartRGB[x, y].R = img[x, y].R;
                                    imgChartRGB[x, y].I = 0;
                                    break;
                                }
                            case "G":
                                {
                                    line[3 * x] = 0;
                                    line[3 * x + 1] = img[x, y].G;
                                    line[3 * x + 2] = 0;
                                    imgChartRGB[x, y].B = 0;
                                    imgChartRGB[x, y].G = img[x, y].G;
                                    imgChartRGB[x, y].R = 0;
                                    imgChartRGB[x, y].I = 0;
                                    break;
                                }
                            case "B":
                                {
                                    line[3 * x] = img[x, y].B;
                                    line[3 * x + 1] = 0;
                                    line[3 * x + 2] = 0;
                                    imgChartRGB[x, y].B = img[x, y].B;
                                    imgChartRGB[x, y].G = 0;
                                    imgChartRGB[x, y].R = 0;
                                    imgChartRGB[x, y].I = 0;
                                    break;
                                }
                            case "I":
                                {
                                    line[3 * x] = img[x, y].I;
                                    line[3 * x + 1] = img[x, y].I;
                                    line[3 * x + 2] = img[x, y].I;
                                    imgChartRGB[x, y].B = 0;
                                    imgChartRGB[x, y].G = 0;
                                    imgChartRGB[x, y].R = 0;
                                    imgChartRGB[x, y].I = img[x, y].I;
                                    break;
                                }
                            case "Invert":
                                {
                                    line[3 * x] = Convert.ToByte(255 - img[x, y].B);
                                    line[3 * x + 1] = Convert.ToByte(255 - img[x, y].G);
                                    line[3 * x + 2] = Convert.ToByte(255 - img[x, y].R);
                                    imgChartRGB[x, y] = new PixelClassRGB(line[3 * x], line[3 * x + 1], line[3 * x + 2]);
                                    break;
                                }
                            case "Strech":
                                {
                                    line[3 * x] = imgChartRGB[x, y].B;
                                    line[3 * x + 1] = imgChartRGB[x, y].G;
                                    line[3 * x + 2] = imgChartRGB[x, y].R;
                                    break;
                                }
                            case "Strech R":
                                {
                                    line[3 * x] = 0;
                                    line[3 * x + 1] = 0;
                                    line[3 * x + 2] = imgChartRGB[x, y].R;
                                    break;
                                }
                            case "Strech G":
                                {
                                    line[3 * x] = 0;
                                    line[3 * x + 1] = imgChartRGB[x, y].G;
                                    line[3 * x + 2] = 0;
                                    break;
                                }
                            case "Strech B":
                                {
                                    line[3 * x] = imgChartRGB[x, y].B;
                                    line[3 * x + 1] = 0;
                                    line[3 * x + 2] = 0;
                                    break;
                                }
                            case "Strech I":
                                {
                                    line[3 * x] = imgChartRGB[x, y].I;
                                    line[3 * x + 1] = imgChartRGB[x, y].I;
                                    line[3 * x + 2] = imgChartRGB[x, y].I;
                                    break;
                                }
                            case "HSV":
                                {
                                    line[3 * x] = img[x, y].hsvToRGB(imghsv[x, y].H, imghsv[x, y].S, imghsv[x, y].V).B;
                                    line[3 * x + 1] = img[x, y].hsvToRGB(imghsv[x, y].H, imghsv[x, y].S, imghsv[x, y].V).G;
                                    line[3 * x + 2] = img[x, y].hsvToRGB(imghsv[x, y].H, imghsv[x, y].S, imghsv[x, y].V).R;
                                    imgChartHSV[x, y] = new PixelClassHSV(imghsv[x, y]);
                                    break;
                                }
                            case "Strech HSV":
                                {
                                    line[3 * x] = img[x, y].hsvToRGB(imgChartHSV[x, y].H, imgChartHSV[x, y].S, imgChartHSV[x, y].V).B;
                                    line[3 * x + 1] = img[x, y].hsvToRGB(imgChartHSV[x, y].H, imgChartHSV[x, y].S, imgChartHSV[x, y].V).G;
                                    line[3 * x + 2] = img[x, y].hsvToRGB(imgChartHSV[x, y].H, imgChartHSV[x, y].S, imgChartHSV[x, y].V).R;
                                    break;
                                }
                            case "Strech S":
                                {
                                    line[3 * x] = imgChartHSV[x, y].S;
                                    line[3 * x + 1] = imgChartHSV[x, y].S;
                                    line[3 * x + 2] = imgChartHSV[x, y].S;
                                    break;
                                }
                            case "H":
                                {
                                    line[3 * x] = img[x, y].hsvToRGB(imghsv[x, y].H, 255, 255).B;
                                    line[3 * x + 1] = img[x, y].hsvToRGB(imghsv[x, y].H, 255, 255).G;
                                    line[3 * x + 2] = img[x, y].hsvToRGB(imghsv[x, y].H, 255, 255).R;
                                    imgChartHSV[x, y] = new PixelClassHSV(imghsv[x, y].H, 0, 0, 0);
                                    break;
                                }
                            case "S":
                                {
                                    line[3 * x] = imghsv[x, y].S;
                                    line[3 * x + 1] = imghsv[x, y].S;
                                    line[3 * x + 2] = imghsv[x, y].S;
                                    imgChartHSV[x, y] = new PixelClassHSV(0, imghsv[x, y].S, 0, 0);
                                    break;
                                }
                            case "V":
                                {
                                    line[3 * x] = imghsv[x, y].V;
                                    line[3 * x + 1] = imghsv[x, y].V;
                                    line[3 * x + 2] = imghsv[x, y].V;
                                    imgChartHSV[x, y] = new PixelClassHSV(0, 0, imghsv[x, y].V, 0);
                                    break;
                                }
                            case "CMYK":
                                {
                                    line[3 * x] = img[x, y].cmykToRGB(imgcmyk[x, y].C, imgcmyk[x, y].M, imgcmyk[x, y].Y, imgcmyk[x, y].K).B;
                                    line[3 * x + 1] = img[x, y].cmykToRGB(imgcmyk[x, y].C, imgcmyk[x, y].M, imgcmyk[x, y].Y, imgcmyk[x, y].K).G;
                                    line[3 * x + 2] = img[x, y].cmykToRGB(imgcmyk[x, y].C, imgcmyk[x, y].M, imgcmyk[x, y].Y, imgcmyk[x, y].K).R;
                                    imgChartCMYK[x, y] = new PixelClassCMYK(imgcmyk[x, y]);
                                    break;
                                }
                            case "C":
                                {
                                    line[3 * x] = img[x, y].cmykToRGB(imgcmyk[x, y].C, 0, 0, 0).B;
                                    line[3 * x + 1] = img[x, y].cmykToRGB(imgcmyk[x, y].C, 0, 0, 0).G;
                                    line[3 * x + 2] = img[x, y].cmykToRGB(imgcmyk[x, y].C, 0, 0, 0).R;
                                    imgChartCMYK[x, y] = new PixelClassCMYK(imgcmyk[x, y].C, 0, 0, 0);

                                    break;
                                }
                            case "M":
                                {
                                    line[3 * x] = img[x, y].cmykToRGB(0, imgcmyk[x, y].M, 0, 0).B;
                                    line[3 * x + 1] = img[x, y].cmykToRGB(0, imgcmyk[x, y].M, 0, 0).G;
                                    line[3 * x + 2] = img[x, y].cmykToRGB(0, imgcmyk[x, y].M, 0, 0).R;
                                    imgChartCMYK[x, y] = new PixelClassCMYK(0, imgcmyk[x, y].M, 0, 0);
                                    break;
                                }
                            case "Y":
                                {
                                    line[3 * x] = img[x, y].cmykToRGB(0, 0, imgcmyk[x, y].Y, 0).B;
                                    line[3 * x + 1] = img[x, y].cmykToRGB(0, 0, imgcmyk[x, y].Y, 0).G;
                                    line[3 * x + 2] = img[x, y].cmykToRGB(0, 0, imgcmyk[x, y].Y, 0).R;
                                    imgChartCMYK[x, y] = new PixelClassCMYK(0, 0, imgcmyk[x, y].Y, 0);
                                    break;
                                }
                            case "K":
                                {
                                    line[3 * x] = img[x, y].cmykToRGB(0, 0, 0, imgcmyk[x, y].K).B;
                                    line[3 * x + 1] = img[x, y].cmykToRGB(0, 0, 0, imgcmyk[x, y].K).G;
                                    line[3 * x + 2] = img[x, y].cmykToRGB(0, 0, 0, imgcmyk[x, y].K).R;
                                    imgChartCMYK[x, y] = new PixelClassCMYK(0, 0, 0, imgcmyk[x, y].K);
                                    break;
                                }
                            case "YUV":
                                {
                                    line[3 * x] = img[x, y].yuvToRGB(imgyuv[x, y].Y, imgyuv[x, y].U, imgyuv[x, y].V).B;
                                    line[3 * x + 1] = img[x, y].yuvToRGB(imgyuv[x, y].Y, imgyuv[x, y].U, imgyuv[x, y].V).G;
                                    line[3 * x + 2] = img[x, y].yuvToRGB(imgyuv[x, y].Y, imgyuv[x, y].U, imgyuv[x, y].V).R;
                                    imgChartYUV[x, y] = new PixelClassYUV(imgyuv[x, y]);
                                    break;
                                }
                            case "Y1":
                                {
                                    line[3 * x] = imgyuv[x, y].Y;
                                    line[3 * x + 1] = imgyuv[x, y].Y;
                                    line[3 * x + 2] = imgyuv[x, y].Y;
                                    imgChartYUV[x, y] = new PixelClassYUV(imgyuv[x, y].Y, 0, 0, 0);
                                    break;
                                }
                            case "U1":
                                {
                                    line[3 * x] = img[x, y].yuvToRGB(0, imgyuv[x, y].U, 0).B;
                                    line[3 * x + 1] = img[x, y].yuvToRGB(0, imgyuv[x, y].U, 0).G;
                                    line[3 * x + 2] = img[x, y].yuvToRGB(0, imgyuv[x, y].U, 0).R;
                                    imgChartYUV[x, y] = new PixelClassYUV(0, imgyuv[x, y].U, 0, 0);
                                    break;
                                }
                            case "V1":
                                {
                                    line[3 * x] = img[x, y].yuvToRGB(0, 0, imgyuv[x, y].V).B;
                                    line[3 * x + 1] = img[x, y].yuvToRGB(0, 0, imgyuv[x, y].V).G;
                                    line[3 * x + 2] = img[x, y].yuvToRGB(0, 0, imgyuv[x, y].V).R;
                                    imgChartYUV[x, y] = new PixelClassYUV(0, 0, imgyuv[x, y].V, 0);
                                    break;
                                }
                        }
                    }
                    ptr = bmpData.Scan0 + y * bmpData.Stride;
                    Marshal.Copy(line, 0, ptr, line.Length);
                }
                bmp.UnlockBits(bmpData);
                switch (mode)
                {
                    case "RGB":
                    case "R":
                    case "G":
                    case "B":
                    case "I": histEdited.readGraph(imgChartRGB); break;

                    case "CMYK":
                    case "C":
                    case "M":
                    case "Y":
                    case "K": histEdited.readGraph(imgChartCMYK); break;

                    case "HSV":
                    case "H":
                    case "S":
                    case "V": histEdited.readGraph(imgChartHSV); break;

                    case "YUV":
                    case "Y1":
                    case "U1":
                    case "V1": histEdited.readGraph(imgChartYUV); break;
                }
                return bmp;
            }
            else { return null; }
        }

    }
}
