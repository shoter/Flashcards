using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tweaker
{
    class StudentMemory
    {
        public double tStart { get; set; }
        public double strength { get; set; }
        public double tNextReview { get; set; }
        public int lastInterval { get; set; }
        public int intervalCount { get; set; }
        public int lossCount { get; set; }

        public double algoStrength { get; set; }

        public StudentMemory(double str, double t, double tNextReview)
        {
            strength = str;
            tStart = t;
            lastInterval = 1;
            intervalCount = 1;
            this.tNextReview = tNextReview;
            this.lossCount = 0;
            algoStrength = 1;
        }
    }
}
