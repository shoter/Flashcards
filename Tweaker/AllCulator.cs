using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tweaker
{
    class AllCulator
    {
        private readonly double a, b, c, d, e;

        public AllCulator(double a, double b, double c, double d, double e)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
            this.e = e;
        }

        public double CalcWinStr(double str)
        {
            double r = (1f - Math.Pow(str, 3));
            return (a * str + r) / (a + r);
        }

        public double CalcLossStr(double str, int lossCount)
        {
            return b  *(str / (c * lossCount +str));
        }

        public int CalcLostInterval(double str, int prev, int count)
        {
            if (count == 1)
                return 1;
            if (count == 2)
                return 6;

            return (int)Math.Floor(prev * (1 - str) / d);
        }

        public int CalcWinInterval(double str, int prev, int count)
        {
            if (count == 1)
                return 1;
            if (count == 2)
                return 6;
            double f = 1f - (double)prev / e;
            return (int)Math.Ceiling(prev * (1 + str + f));
        }
    }
}
