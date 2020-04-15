using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKAA_171RDB117_3
{
    public class PixelClassCMYK
    {
        public double C;
        public double M;
        public double Y;
        public double K;

        public PixelClassCMYK()
        {
            C = 0;
            M = 0;
            Y = 0;
            K = 0;
        }
        public PixelClassCMYK(PixelClassCMYK img)
        {
            C = img.C;
            M = img.M;
            Y = img.Y;
            K = img.K;
        }
        public PixelClassCMYK(double c, double m, double y, double k)
        {
            C = c;
            M = m;
            Y = y;
            K = k;
        }
        public PixelClassCMYK(byte r, byte g, byte b)
        {
            double rI = r / 255.0;
            double gI = g / 255.0;
            double bI = b / 255.0;

            K = (1.0 - Math.Max(Math.Max(rI, gI), bI));
            C = (1.0 - rI - K) / (1.0 - K);
            M = (1.0 - gI - K) / (1.0 - K);
            Y = (1.0 - bI - K) / (1.0 - K);
        }
    }
}
