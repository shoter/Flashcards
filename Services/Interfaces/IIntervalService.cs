using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IIntervalService
    {
        int CalculateNewIntervalForLoss(int previousInterval, double strength, int intervalNumber);
        int CalculateNewIntervalForWin(int previousInterval, double strength, int intervalNumber);
    }
}
