using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tweaker
{
    class StrengthWinCalculator
    {
        private readonly double numeratorStrengthMultiplier;
        private readonly double rNumeratorMultiplier;
        private readonly double denumeratorMultiplier;
        private readonly double rDenumeratorMultiplier;

        public StrengthWinCalculator(double numeratorStrengthMultiplier, double rNumeratorMultiplier, double denumeratorMultiplier, double rDenumeratorMultiplier)
        {
            this.numeratorStrengthMultiplier = numeratorStrengthMultiplier;
            this.rNumeratorMultiplier = rNumeratorMultiplier;
            this.denumeratorMultiplier = denumeratorMultiplier;
            this.rDenumeratorMultiplier = rDenumeratorMultiplier;
        }

        public double Calculate(double actualStrength)
        {
            double r = (1f - Math.Pow(actualStrength, 3));
            double numerator = numeratorStrengthMultiplier * actualStrength + rNumeratorMultiplier * r;
            double denumerator = denumeratorMultiplier + rDenumeratorMultiplier * r;

            return numerator / denumerator;
        }

    }
}
