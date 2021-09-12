using System;
using System.Linq;

namespace AtCoderApp
{
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        new abc081_b();
    //    }
    //}

    /// <summary>
    /// abc081_b
    /// </summary>
    public class abc081_b
    {        
        public abc081_b()
        {
            //input
            var N = Convert.ToInt32(Console.ReadLine());
            var An = Console.ReadLine().Split(' ').ToList().Select(p => Convert.ToUInt64(p)).ToList();

            //calc
            var Cnt = 0;

            while (true)
            {
                var POW2 = Math.Pow(2, Cnt + 1);
                if (An.Exists(p => p % POW2 > 0))
                {
                    Console.WriteLine(Cnt.ToString());
                    return;
                }

                Cnt++;
            }

            //return;
        }
    }
}
