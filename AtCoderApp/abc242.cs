using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 2022-03-05
/// </summary>
namespace AtCoderApp
{
    public class abc242_a
    {
        public abc242_a()
        {
            //input-------------
            var ABCX = In.ReadAry<int>().ToArray();
            (var A, var B, var C, var X) = (ABCX[0], ABCX[1], ABCX[2], ABCX[3]);

            //calc--------------
            if (A >= X)
            {
                Out.Write(1);
                return;
            }
            else if (B < X)
            {
                Out.Write(0);
                return;
            }

            Out.Write(((double)C / (double)(B - A)).ToString());
        }
    }
    public class abc242_b
    {
        public abc242_b()
        {
            //input-------------
            var S = In.Read<string>();

            //calc--------------
            var sd = new StringBuilder();
            foreach (var c in "abcdefghijklmnopqrstuvwxyz")
            {
                var cnt = S.ToList().Count(p => p == c);
                sd.Append(new string(c, cnt));
            }

            Out.Write(sd);
        }
    }
}