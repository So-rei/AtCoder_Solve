using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtCoderApp
{
    //AC
    public class arc143_a
    {
        public arc143_a()
        {
            //input-------------
            var abc = In.ReadAry<long>().ToArray();

            //output------------
            var ABC = abc.OrderBy(p => p).ToArray();

            //大きいの2つを-1する
            long cnt = ABC[1] - ABC[0];
            ABC[1] -= cnt;
            ABC[2] -= cnt;

            //不可能なパターン
            if ((ABC[0] == 0 && ABC[2] > 0) || (ABC[2] > ABC[0] * 2))
            {
                Out.Write(-1);
                return;
            }
            //可能なパターンはここでk,k, k+n (n<k)　となるパターン
            cnt += ABC[2];

            Out.Write(cnt);

            return;
        }
    }

    //Bはpythonでとく
}