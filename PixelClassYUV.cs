using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKAA_171RDB117_3
{
    public class PixelClassYUV
    {
        public byte Y;
        public byte U;
        public byte V;

        public PixelClassYUV()
        {
            Y = 0;
            U = 0;
            V = 0;
        }
        public PixelClassYUV(PixelClassYUV img)
        {
            Y = img.Y;
            U = img.U;
            V = img.V;
        }
        public PixelClassYUV(byte y, byte u, byte v, byte zero)
        {
            Y = y;
            U = u;
            V = v;
        }
        public PixelClassYUV(byte r, byte g, byte b)
        {
            Y = (byte)(0.299*r + 0.587*g + 0.114*b);
            U = (byte)(-0.14713*r - 0.28886*g + 0.436*b + 128.0);
            V = (byte)(0.615*r - 0.51499*g - 0.10001*b + 128.0);
        }
    }
}
