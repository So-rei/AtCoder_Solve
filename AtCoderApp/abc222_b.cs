using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoderApp
{
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        new abc222_b();
    //    }
    //}

    //20211009 ac

    public class abc222_b
    {
        public abc222_b()
        {
            //input-------------
            var NP = In.ReadAry<int>().ToArray();
            var N = NP[0];
            var P = NP[1];

            var an = In.ReadAry<int>().ToArray();

            //calc----
            var ret = an.Count(s => s < P);

            Out.Write(ret);
        }
    }
}