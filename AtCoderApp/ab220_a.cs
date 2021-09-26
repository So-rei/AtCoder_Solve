using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoderApp
{
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        new aB220_a();
    //    }
    //}

    public class aB220_a
    {
        public aB220_a()
        {
            //input-------------
            var ABC = In.ReadAry<int>().ToArray();
            decimal A = ABC[0];
            decimal B = ABC[1];
            decimal C = ABC[2];

            //out
            if (C > B)
            {
                Out.Write(-1);
                return;
            }

            var d = Math.Ceiling(A / C);
            var r = d * C;
            if (r > B)
            {
                Out.Write(-1);
                return;
            }

            Out.Write(r);

        }
    }
}