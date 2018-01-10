using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tweaker
{
    public class Simulator
    {

        public Simulator()
        {
            double ints = 9f;
            double aStart = 1f;
            double aEnd = 5f;
            double aInterval = (aEnd - aStart) / ints;

            double bStart = 0.25f;
            double bEnd = 12f;
            double bInterval = (bEnd - bStart) / ints;

            double cStart = 0.25f;
            double cEnd = 10f;
            double cInterval = (cEnd - cStart) / ints;

            double dStart = 0.25f;
            double dEnd = 5f;
            double dInterval = (dEnd - dStart) / ints;

            int max = 0;

            for (double A = aStart; A <= aEnd; A += aInterval)
                for (double B = bStart; B <= bEnd; B += bInterval)
                    for (double C = cStart; C <= cEnd; C += cInterval)
                        for (double D = dStart; D <= dEnd; D += dInterval)
                            for (double E = 20f; E <= 50f ; E += 1f)
                            {
                                var calc = new AllCulator(A, B, C, D, E);
                                Student stud = new Student(calc);
                                int cards = stud.MakeReviews(360);
                                if (cards >= max)
                                {
                                    max = cards;
                                    Console.WriteLine(
                                        $"[{DateTime.Now.ToShortTimeString()}]( {A}, {B}, {C}, {D}, {E}) - {cards}");
                                }

                            }
            Console.WriteLine("DONE");
            Console.ReadKey();
            Console.ReadKey();
            Console.ReadKey();
        }
    }
}
