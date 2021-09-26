using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoderApp
{
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        new aB220_b();
    //    }
    //}

    public class aB220_b
    {
        public aB220_b()
        {
            //input-------------
            var K = In.Read<int>();
            var AB = In.ReadAry<string>().ToArray();
            string A = AB[0];
            string B = AB[1];

            //out----
            long iA = Calc(A);
            long iB = Calc(B);

            Out.Write((long)(iA * iB));

            //local c
            int Calc(string _AB)
            {
                int ret = 0;
                int digit = 0;
                for (var i = _AB.Length - 1; i >= 0; i--)
                {
                    ret += (int)Math.Pow(K, digit) * Convert.ToInt32(_AB[i].ToString());

                    digit++;
                }
                return ret;
            }
        }
    }
}