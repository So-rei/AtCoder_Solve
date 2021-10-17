using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoderApp
{
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        new abc223_b();
    //    }
    //}

    public class abc223_b
    {
        public abc223_b()
        {
            //input-------------
            var S = In.Read<string>();

            //calc----
            var smin = S;
            var smax = S;
            var sminchar = S.ToArray().Min();
            var smaxchar = S.ToArray().Max();
            for (int x = 0; x < S.Length; x++)
            {
                if (S[x] == sminchar)
                    smin = new string[] { smin, (S.Substring(x) + S.Substring(0, x)) }.OrderBy(q => q).First();
                else if (S[x] == smaxchar)
                    smax = new string[] { smax, (S.Substring(x) + S.Substring(0, x)) }.OrderBy(q => q).Last();
            }

            Out.Write(smin);
            Out.Write(smax);
        }
    }
}
