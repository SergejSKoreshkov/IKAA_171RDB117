using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKAA_171RDB117_3
{
    public class PixelClassRGB
    {

        public byte R;
        public byte G;
        public byte B;
        public byte I;

        public PixelClassRGB()
        {
            R = 0;
            G = 0;
            B = 0;
            I = 0;
        }
        public PixelClassRGB(PixelClassRGB toCopy)
        {
            R = toCopy.R;
            G = toCopy.G;
            B = toCopy.B;
            I = toCopy.I;
        }
        public PixelClassRGB(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
            I = (byte)Math.Round(0.0722f * b + 0.7152f * g + 0.2126f * r);
        }

        public PixelClassRGB hsvToRGB(int h, byte s, byte v)
        {
            byte r = 0;
            byte g = 0;
            byte b = 0;

            int Hi = Convert.ToInt32(h / 60);
            byte Vmin = Convert.ToByte((255 - s) * v / 255);
            int a = Convert.ToInt32((v - Vmin) * (h % 60) / 60);
            byte Vinc = Convert.ToByte(Vmin + a);
            byte Vdec = Convert.ToByte(v - a);

            switch (Hi)
            {
                case 0: { r = v; g = Vinc; b = Vmin; break; }
                case 1: { r = Vdec; g = v; b = Vmin; break; }
                case 2: { r = Vmin; g = v; b = Vinc; break; }
                case 3: { r = Vmin; g = Vdec; b = v; break; }
                case 4: { r = Vinc; g = Vmin; b = v; break; }
                case 5: { r = v; g = Vmin; b = Vdec; break; }
            }
            PixelClassRGB rgbPix = new PixelClassRGB(r, g, b);
            return rgbPix;

        }

        public PixelClassRGB cmykToRGB(double c, double m, double y, double k)
        {
            int r = (int)(255.0 * (1.0 - c) * (1.0 - k));
            int g = (int)(255.0 * (1.0 - m) * (1.0 - k));
            int b = (int)(255.0 * (1.0 - y) * (1.0 - k));
            PixelClassRGB rgbPix = new PixelClassRGB((byte)r, (byte)g, (byte)b);
            return rgbPix;

        }

        public PixelClassRGB yuvToRGB(byte y, byte u, byte v)
        {
            double r = (y + 1.13983 * (v - 128.0));
            double g = (y - 0.39465 * (u - 128.0) - 0.58060 * (v - 128.0));
            double b = (y + 2.03211 * (u - 128.0));

            if (r > 255.0)
            {
                r = 255.0;
            }
            if (r < 0.0)
            {
                r = 0.0;
            }
            if (g > 255.0)
            {
                g = 255.0;
            }
            if (g < 0.0)
            {
                g = 0.0;
            }
            if (b > 255.0)
            {
                b = 255.0;
            }
            if (b < 0.0)
            {
                b = 0.0;
            }

            PixelClassRGB rgbPix = new PixelClassRGB((byte)r, (byte)g, (byte)b);
            return rgbPix;

        }

        public void effectOpacity(PixelClassRGB a, PixelClassRGB b, double d)
        {
            R = Convert.ToByte(d * a.R + (1 - d) * b.R);
            G = Convert.ToByte(d * a.G + (1 - d) * b.G);
            B = Convert.ToByte(d * a.B + (1 - d) * b.B);
        }

        public void effectScreen(PixelClassRGB a, PixelClassRGB b)
        {
            double theFirst = (double)a.R / 255;
            double theSecond = (double)b.R / 255;

            R = Convert.ToByte((1 - (1 - theFirst) * (1 - theSecond)) * 255);

            theFirst = (double)a.G / 255;
            theSecond = (double)b.G / 255;

            G = Convert.ToByte((1 - (1 - theFirst) * (1 - theSecond)) * 255);

            theFirst = (double)a.B / 255;
            theSecond = (double)b.B / 255;

            B = Convert.ToByte((1 - (1 - theFirst) * (1 - theSecond)) * 255);
        }

        public void effectDarken(PixelClassRGB a, PixelClassRGB b)
        {
            R = b.R <= a.R ? b.R : a.R;
            G = b.G <= a.G ? b.G : a.G;
            B = b.B <= a.B ? b.B : a.B;
        }

        public void effectLighten(PixelClassRGB a, PixelClassRGB b)
        {
            R = b.R <= a.R ? a.R : b.R;
            G = b.G <= a.G ? a.G : b.G;
            B = b.B <= a.B ? a.B : b.B;
        }

        public void effectMultiply (PixelClassRGB a, PixelClassRGB b)
        {
            R = Convert.ToByte((double)(a.R / 255.0 * b.R / 255.0 * 255.0));
            G = Convert.ToByte((double)(a.G / 255.0 * b.G / 255.0 * 255.0));
            B = Convert.ToByte((double)(a.B / 255.0 * b.B / 255.0 * 255.0));
        }

        public void effectBurn(PixelClassRGB a, PixelClassRGB b)
        {
            double rA = (double)(a.R / 255.0) <= 0 ? 1 : (double)(a.R / 255.0);
            double gA = (double)(a.G / 255.0) <= 0 ? 1 : (double)(a.G / 255.0);
            double bA = (double)(a.B / 255.0) <= 0 ? 1 : (double)(a.B / 255.0);

            double rB = (double)(b.R / 255.0);
            double gB = (double)(b.R / 255.0);
            double bB = (double)(b.R / 255.0);

            R = Convert.ToByte(Math.Max(Math.Min((1 - (1 - rB) / rA) * 255.0, 255), 0));
            G = Convert.ToByte(Math.Max(Math.Min((1 - (1 - gB) / gA) * 255.0, 255), 0));
            B = Convert.ToByte(Math.Max(Math.Min((1 - (1 - bB) / bA) * 255.0, 255), 0));
        }

        public void effectDodge(PixelClassRGB a, PixelClassRGB b)
        {
            double rA = a.R / 255.0 == 1 ? 0 : a.R / 255.0;
            double gA = a.G / 255.0 == 1 ? 0 : a.G / 255.0;
            double bA = a.B / 255.0 == 1 ? 0 : a.B / 255.0;

            R = Convert.ToByte(Math.Max(Math.Min(((b.R / 255.0) / (1 - rA)) * 255.0, 255), 0));
            G = Convert.ToByte(Math.Max(Math.Min(((b.G / 255.0) / (1 - gA)) * 255.0, 255), 0));
            B = Convert.ToByte(Math.Max(Math.Min(((b.B / 255.0) / (1 - bA)) * 255.0, 255), 0));
        }

        public void effectOverlay(PixelClassRGB a, PixelClassRGB b)
        {
            R = Convert.ToByte((double)(b.R / 255.0) <= 0.5 ? (2 * (a.R / 255.0) * (b.R / 255.0) * 255.0) : ((1 - 2 * (1 - a.R/255.0)*(1 - b.R/255.0)) * 255.0));
            G = Convert.ToByte((double)(b.G / 255.0) <= 0.5 ? (2 * (a.G / 255.0) * (b.G / 255.0) * 255.0) : ((1 - 2 * (1 - a.G/255.0)*(1 - b.G/255.0)) * 255.0));
            B = Convert.ToByte((double)(b.B / 255.0) <= 0.5 ? (2 * (a.B / 255.0) * (b.B / 255.0) * 255.0) : ((1 - 2 * (1 - a.B/255.0)*(1 - b.B/255.0)) * 255.0));
        }

        public void effectHardLight(PixelClassRGB a, PixelClassRGB b)
        {
            R = Convert.ToByte((double)(a.R / 255.0) <= 0.5 ? (2 * (a.R / 255.0) * (b.R / 255.0) * 255.0) : ((1 - 2 * (1 - a.R / 255.0) * (1 - b.R / 255.0)) * 255.0));
            G = Convert.ToByte((double)(a.G / 255.0) <= 0.5 ? (2 * (a.G / 255.0) * (b.G / 255.0) * 255.0) : ((1 - 2 * (1 - a.G / 255.0) * (1 - b.G / 255.0)) * 255.0));
            B = Convert.ToByte((double)(a.B / 255.0) <= 0.5 ? (2 * (a.B / 255.0) * (b.B / 255.0) * 255.0) : ((1 - 2 * (1 - a.B / 255.0) * (1 - b.B / 255.0)) * 255.0));
        }

        public void effectDiff(PixelClassRGB a, PixelClassRGB b)
        {
            R = Convert.ToByte(Math.Abs(a.R - b.R));
            G = Convert.ToByte(Math.Abs(a.G - b.G));
            B = Convert.ToByte(Math.Abs(a.B - b.B));
        }

        public void effectLinearDodge(PixelClassRGB a, PixelClassRGB b)
        {
            int r = a.R + b.R;
            if (r > 255) { r = 255; }
            if (r < 0) { r = 0; }

            int g = a.G + b.G;
            if (g > 255) { g = 255; }
            if (g < 0) { g = 0; }

            int bl = a.B + b.B;
            if (bl > 255) { bl = 255; }
            if (bl < 0) { bl = 0; }

            R = (byte)r;
            G = (byte)g;
            B = (byte)bl;
        }

    }
}
