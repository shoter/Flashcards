using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IStrengthService
    {
        double CalculateStrengthAfterWinningBadAnswer(double previousStrength, int lossCount);

        double CalculateStrengthAfterQuitingSession(double previousStrength, int lossCount);

        double CalculateStrengthAfterGoodAnswer(double previousStrength, double correctness);

    }
}
