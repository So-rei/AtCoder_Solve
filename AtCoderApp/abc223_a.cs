using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoderApp
{
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        new abc223_a();
    //    }
    //}

    public class abc223_a
    {
        public abc223_a()
        {
            //input-------------
            var X = In.Read<int>();

            //calc----

            Out.Write((X > 0 && X % 100 == 0) ? "Yes" : "No");
        }
    }
}
