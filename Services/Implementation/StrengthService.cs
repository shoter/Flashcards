using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class StrengthService : IStrengthService
    {
        public double CalculateStrengthAfterGoodAnswer(double previousStrength, double correctness)
        {
            double s = previousStrength;

            double r = 1 - Math.Pow(s, 3);
            r *= correctness;
            return (3 * s + r) / (3 + r);

        }

        public double CalculateStrengthAfterQuitingSession(double previousStrength, int lossCount)
        {
            return previousStrength / ((previousStrength + lossCount) * 1.2f);
        }

        public double CalculateStrengthAfterWinningBadAnswer(double previousStrength, int lossCount)
        {
            return previousStrength / (previousStrength + lossCount);
        }
    }
}
