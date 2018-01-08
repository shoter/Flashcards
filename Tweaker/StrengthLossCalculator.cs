using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tweaker
{
    public class StrengthLossCalculator
    {
        private readonly double numeratorMultiplier;
        private readonly double lossMultiplier;
        private readonly double denumeratorMultiplier;
        public StrengthLossCalculator(double numeratorMultiplier, double lossMultiplier, double denumeratorMultiplier)
        {
            this.numeratorMultiplier = numeratorMultiplier;
            this.lossMultiplier = lossMultiplier;
            this.denumeratorMultiplier = denumeratorMultiplier;
        }

        public double Calculate(double actualStrength, int lossCount)
        {
            double numerator = numeratorMultiplier * actualStrength;

            double denumerator = lossMultiplier * lossCount + denumeratorMultiplier * actualStrength;

            return numerator / denumerator;
        }
    }
}
