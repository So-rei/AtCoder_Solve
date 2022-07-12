using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoderApp
{

    public class arc129_a
    {
        public arc129_a()
        {
            //input-------------
            var NLR = In.ReadAry<long>().ToArray();
            long N = NLR[0];
            long L = NLR[1];
            long R = NLR[2];

            //output------------
            long cnt = 0;

            //当然これだとTLEなので..
            //for (long i = L; i <=R; i++)
            //{
            //    if ((i ^ N) < N)
            //        cnt++;
            //}

            //これでもTLE、WAも出る（？）
            //Nよりも多い桁数(2進)の値なら必ず条件を満たさないことを利用
            //Nと同じ桁数は必ず条件を満たすことを利用
            //int digit = Convert.ToString(N, 2).Count();
            //long th1 = Convert.ToInt64(Math.Pow(2, digit - 1));
            //long th2 = Convert.ToInt64(Math.Pow(2, digit));
            //for (long i = L; i < Math.Min(th1, R); i++)
            //{
            //    if ((i ^ N) < N)
            //        cnt++;
            //}
            //if (th1 <= R)
            //    cnt += Math.Min(th2 - 1, R) - th1 + 1;

            //桁数ごとにループで数える(AC)
            string Nstr = Convert.ToString(N, 2);
            int digit = Nstr.Count();

            for (int d = 0; d < digit; d++)
            {
                if (Nstr[digit - 1 - d] == '1') //下からd桁目が1なら,x=1000...1111(2^d個)は全部Nより小さくなる
                {
                    long rMax = Math.Min(Convert.ToInt64(Math.Pow(2, d + 1)) - 1, R);
                    long rMin = Math.Max(Convert.ToInt64(Math.Pow(2, d)), L);
                    if (rMax >= rMin)
                        cnt += rMax - rMin + 1;
                }
            }

            Out.Write(cnt);
        }

    }
    
    //2021/11/21
    //TLE7

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