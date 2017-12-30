using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class IntervalService : IIntervalService
    {
        public int CalculateNewIntervalForLoss(int previousInterval, double strength, int intervalNumber)
        {
            if (intervalNumber <= 2)
                return calcualteConstInterval(intervalNumber);
            int newInterval = (int)((double)previousInterval * (1f - strength) / 2f);
            newInterval = Math.Min(newInterval, previousInterval - 1);

            return newInterval;

        }

        public int CalculateNewIntervalForWin(int previousInterval, double strength, int intervalNumber)
        {
            if (intervalNumber <= 2)
                return calcualteConstInterval(intervalNumber);

            double factor = 1 - (double)previousInterval / 30f;
            int newInterval = (int)((double)previousInterval * (1f + strength * factor));
            newInterval = Math.Max(newInterval, previousInterval + 1);

            return newInterval;
        }

        private int calcualteConstInterval(int intervalNumber)
        {
            switch (intervalNumber)
            {
                case 1:
                    return 1;
                case 2:
                    return 6;
                default:
                    throw new ArgumentException();
            }
        }
    }
}
