using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tweaker
{
    public class IntervalCalculator
    {
        private readonly int secondInterval;
        private readonly double maxDay;
        private readonly double lossDenominator;
        private readonly double factorMultiplier;

        public IntervalCalculator(int secondInterval, double maxDay, double lossDenominator, double factorMultiplier)
        {
            this.secondInterval = secondInterval;
            this.maxDay = maxDay;
            this.lossDenominator = lossDenominator;
            this.factorMultiplier = factorMultiplier;
        }

        public double CalculateWin(double str, int lastInterval)
        {
            double factor = 1f - (double)lastInterval / maxDay;
            double bracket = 1 + str + factor * factorMultiplier;

            return Math.Ceiling((double)lastInterval + bracket);
        }

        public double CalculateLoss(double str, int lastInterval)
        {
            double second = (1f - str) / (lossDenominator);
            return (double)lastInterval * second;
        }
    }
}
