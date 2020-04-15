using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKAA_171RDB117_3
{
    public class Filter
    {
        public int[,] F;
        public int K;
        public Filter()
        {
            F = new int[3, 3];
        }
        public int calculateCoefficient(int[,] f)
        {
            int k = 0;
            for (int i = 0; i < f.GetLength(0); i++)
            {
                for (int j = 0; j < f.GetLength(1); j++)
                {
                    k += f[i, j];
                }
            }
            return k;
        }
        public void filter3x3Blur(int type = 16)
        {
            switch (type)
            {
                case 16: F = new int[,] { { 1, 2, 1 }, { 2, 4, 2 }, { 1, 2, 1 } }; break;
                case 10: F = new int[,] { { 1, 1, 1 }, { 1, 2, 1 }, { 1, 1, 1 } }; break;
                case 9: F = new int[,] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } }; break;
            }
            K = calculateCoefficient(F);
        }

        public void filterAntiBlur(int type = 1)
        {
            switch (type)
            {
                case 1: F = new int[,] { { 0, -1, 0 }, { -1, 5, -1 }, { 0, -1, 0 } }; break;
                case 2: F = new int[,] { { -1, -1, -1 }, { -1, 9, -1 }, { -1, -1, -1 } }; break;
                case 3: F = new int[,] { { 0, -2, 0 }, { -2, 9, -2 }, { 0, -2, 0 } }; break;
            }
            K = calculateCoefficient(F);
        }

    }
}
