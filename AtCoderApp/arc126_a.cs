using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoderApp
{
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        new arc216_a();
    //    }
    //}

    //20210919
    //AC
    public class arc216_a
    {
        public arc216_a()
        {
            //input
            var T = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < T; i++)
            {
                var CaseN = Console.ReadLine().Split(' ').Select(p => Convert.ToInt64(p)).ToArray();
                CalcStart(CaseN);
            }
            //-> 3,4､2の順で使う


            return;
        }

        private void CalcStart(long[] c)
        {
            long max = 0;
            //calc
            //10 = 3 * 2 + 4
            //10 = 3 * 2 + 2 * 2
            //10 = 2 + 4 * 2
            //10 = 2 * 3 + 4
            //10 = 2 * 5

            var root1 = Math.Min(c[1] / 2, c[2]);
            max += root1;
            c[1] -= root1 * 2;
            c[2] -= root1;

            var root2 = Math.Min(c[0] / 2, c[1] / 2);
            max += root2;
            c[0] -= root2 * 2;
            c[1] -= root2 * 2;

            var root3 = Math.Min(c[0], c[2] / 2);
            max += root3;
            c[0] -= root3;
            c[2] -= root3 * 2;

            var root4 = Math.Min(c[0] / 3, c[2]);
            max += root4;
            c[0] -= root4 * 3;
            c[2] -= root4;

            var root5 = c[0] / 5;
            max += root5;

            Console.WriteLine(max);
        }
    }
}
