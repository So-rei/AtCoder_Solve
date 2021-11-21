using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoderApp
{

    //2021/11/21
    //TLE7

    using System;

    public class ac129_b
    {

        //これだとTLEが少し出る Listとかタプル、Max計算毎回が遅い
        //public ac129_b()
        //{
        //    //input-------------
        //    var N = In.Read<int>();
        //    var LR = new List<(int L, int R)>();
        //    for (int i = 0; i < N; i++)
        //    {
        //        var _lr = In.ReadAry<int>().ToArray();
        //        LR.Add((_lr[0], _lr[1]));
        //    }

        //    //output------------

        //    //１回目は必ず0

        //    for (int k = 1; k <= N; k++)
        //    {
        //        //k個目までのLMax,RMinを取得
        //        //その中間が最短になる
        //        var target = LR.GetRange(0, k);
        //        var LMax = target.Max(p => p.L);
        //        var RMin = target.Min(p => p.R);

        //        if (RMin < LMax)
        //            Out.Write(Math.Ceiling(((double)(LMax - RMin) / 2)));
        //        else
        //            Out.Write(0);
        //    }

        //    //int dist(int l, int r, int x)
        //    //{
        //    //    if (x < l)
        //    //        return l - x;
        //    //    else if (r < x)
        //    //        return x - r;
        //    //    else
        //    //        return 0;
        //    //}
        //}

        public ac129_b()
        {
            //input------------ -
            var N = In.Read<int>();
            var L = new int[N];
            var R = new int[N];
            for (int i = 0; i < N; i++)
            {
                var _lr = In.ReadAry<int>().ToArray();
                L[i] = _lr[0];
                R[i] = _lr[1];
            }

            //output------------
            var LMax = L[0];
            var RMin = R[0];

            for (int k = 0; k < N; k++)
            {
                //k個目までのLMax,RMinを取得
                //その中間が最短になる
                LMax = Math.Max(LMax, L[k]);
                RMin = Math.Min(RMin, R[k]);

                if (RMin < LMax)
                    Out.Write(Math.Ceiling(((double)(LMax - RMin) / 2)));
                else
                    Out.Write(0);
            }
        }
    }
}

