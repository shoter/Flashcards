using Common.Extensions;
using Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tweaker
{
    class Student
    {
        public List<StudentMemory> Memories { get; set; }
        private AllCulator calc;
        private IntervalService intervalService = new IntervalService();
        Random rand = new Random();
        double tMin = 1f / 1440f;

        public Student(AllCulator calc)
        {
            this.calc = calc;

        }

        public int MakeReviews(int days)
        {
            double t = 0f;
            Memories = new List<StudentMemory>();
            for (int day = 1; day <= days; ++day)
            {
                for (int i = 0; i < 5; ++i)
                    Memories.Add(new StudentMemory(0.5f, t, t + 1f));

                MakeReview(ref t);
                t += 1f;
            }

            return Memories.Count(x => calcR(x, t) >= 0.75 && x.intervalCount >= 5);

        }


        private void MakeReview(ref double t)
        {
            double timeLimit = t + tMin * 60;
            double actT = t;
            List<StudentMemory> queue = new List<StudentMemory>();
            var able = Memories.Where(m => actT >= m.tNextReview).OrderBy(m => m.tStart);
            if (able.Count() == 0)
                return;
            queue.AddRange(able);

            while (actT <= timeLimit && queue.Count > 0)
            {
                var card = queue.TakeFirst();
                if (StudyFlashcard(card, actT) == false)
                {
                    int place = GetPlacementForCardInQueue(queue.Count + 1, card.lossCount);
                    queue.Insert(place, card);
                }
                actT += tMin;
            }

            t = actT;
        }

        public void MoveDeclinedFlashcardInQueue(int flashcardID)
        {

            /*var card = revInfo.ReviewCards.TakeFirst(c => c.FlashcardID == flashcardID);
            //put it in other place in queue
            int cardCount = revInfo.ReviewCards.Count;
            int place = GetPlacementForCardInQueue(cardCount, card.InternalLossCount);
            revInfo.ReviewCards.Insert(place, card);*/
        }


        public int GetPlacementForCardInQueue(int cardCount, int lossCount)
        {
            int factor = Math.Min(lossCount, 3);
            int min = cardCount / (factor + 2);
            int max = cardCount / (factor + 1);

            return rand.Next(min, max);
        }


        private bool StudyFlashcard(StudentMemory mem, double t)
        {
            double r = calcR(mem, t);

            if (rand.NextDouble() < r)
            {
                //win
                int interval = 0;
                if (mem.lossCount == 0)
                {
                    mem.strength = calc.CalcWinStr(mem.strength);
                    interval = calc.CalcWinInterval(mem.strength, mem.lastInterval, mem.intervalCount++);
                }
                else
                {
                    mem.strength = calc.CalcLossStr(mem.strength, mem.lossCount);
                    interval = calc.CalcLostInterval(mem.strength, mem.lastInterval, mem.intervalCount++);
                }
                mem.tNextReview = t + interval;
                mem.tStart = t;
                mem.lastInterval = interval;
                mem.lossCount = 1;
                mem.algoStrength++;

                return true;
            }
            else
            {
                mem.tStart = t;
                mem.lossCount++;
                return false;
            }

        }

        private static double calcR(StudentMemory mem, double t)
        {
            double deltaT = t - mem.tStart;
            double r = NewMethod(mem, deltaT);
            return r;
        }

        private static double NewMethod(StudentMemory mem, double deltaT)
        {
            return Math.Exp(-deltaT / (mem.algoStrength * 10));
        }
    }
}
