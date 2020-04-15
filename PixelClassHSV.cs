using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKAA_171RDB117_3
{
    public class PixelClassHSV
    {
        public int H;
        public byte S;
        public byte V;

        public PixelClassHSV()
        {
            H = 0;
            S = 0;
            V = 0;
        }
        public PixelClassHSV(PixelClassHSV img)
        {
            H = img.H;
            S = img.S;
            V = img.V;
        }
        public PixelClassHSV(int h, byte s, byte v, byte zero)
        {
            H = h;
            S = s;
            V = v;
        }
        public PixelClassHSV(byte r, byte g, byte b)
        {
            int MAX = Math.Max(r, Math.Max(g, b));
            int MIN = Math.Min(r, Math.Min(g, b));
            if (MAX == MIN) { H = 0; }
            else if ((MAX == r) && (g >= b)) { H = 60 * (g - b) / (MAX - MIN); }
            else if ((MAX == r) && (g < b)) { H = 60 * (g - b) / (MAX - MIN) + 360; }
            else if (MAX == g) { H = 60 * (b - r) / (MAX - MIN) + 120; }
            else { H = 60 * (r - g) / (MAX - MIN) + 240; }
            if (H == 360) { H = 0; }
            if (MAX == 0) { S = 0; }
            else { S = Convert.ToByte(255 * (1 - ((float)MIN / MAX))); }
            V = (byte)(MAX);
        }
    }
}
