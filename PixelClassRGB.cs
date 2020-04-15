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

    }
}
