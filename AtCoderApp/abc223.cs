using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoderApp
{

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

    public class abc223_c
    {
        public abc223_c()
        {
            //input-------------
            var N = In.Read<int>();
            int[][] AB = new int[N][];
            for (int i = 0; i < N; i++)
            {
                var _ab = In.ReadAry<int>().ToArray();
                AB[i] = new int[] { _ab[0], _ab[1] };
            }

            //calc-------------
            long[] sumLength = new long[N]; //右からi本目が始まる前の長さ
            decimal[] jTime = new decimal[N]; //右からi本目を始める時間

            jTime[0] = 0;
            sumLength[0] = 0;
            for (int i = 1; i < N; i++)
            {
                jTime[i] += (decimal)AB[i - 1][0] / (decimal)AB[i - 1][1] + jTime[i - 1];
                sumLength[i] += AB[i - 1][0] + sumLength[i - 1];
            }
            decimal jHalf = (jTime[N - 1] + ((decimal)AB[N - 1][0] / (decimal)AB[N - 1][1])) / 2;

            var target = jTime.Where(p => p <= jHalf).Select((p, index) => (p, index)).Max(); //ぶつかる場所i本目を求め
            decimal ret = (decimal)sumLength[target.index] + (decimal)(jHalf - target.p) * (decimal)(AB[target.index][1]);//i本目の何cm先かを求める

            //calc-------------
            double dTime = 0;
            //int[] sumLength = new int[N];
            //double[] jTime = new double[N];

            //dTime += (double)AB[0][0] / (double)AB[0][1];
            //jTime[0] = dTime;
            //sumLength[0] = AB[0][0];
            //for (int i = 1; i < N; i++)
            //{
            //    dTime += (double)AB[i][0] / (double)AB[i][1];
            //    jTime[i] = dTime;
            //    sumLength[i] += AB[i][0] + sumLength[i - 1];
            //}

            //var target = jTime.Where(p => p <= dTime / 2).Select((p, index) => (p, index)).Max();　//これだと１本めに境目が合った時取れない！！NG!
            //double ret = sumLength[target.index] + (double)(dTime / 2 - target.p) * (double)(AB[target.index + 1][1]);

            Out.Write(ret);
        }
    }
}
