using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AtCoderApp
{
    //AC
    public class abc255_a
    {
        public abc255_a()
        {
            //input-------------
            (var R, var C) = In.ReadTuple2<int>();
            var A1 = In.ReadAry<int>().ToArray();
            var A2 = In.ReadAry<int>().ToArray();

            //output------------
            if (R == 1)
                Out.Write(A1[C - 1]);
            else
                Out.Write(A2[C - 1]);
        }
    }

    //ライトを持っている人を考え間違えていて、問題ごと飛ばしてしまった
    //時間後といた。これでAC
    public class abc255_b
    {
        public abc255_b()
        {
            //input-------------
            (var N, var K) = In.ReadTuple2<int>();
            var A = In.ReadAry<int>().ToArray();
            var XY = In.ReadManyAry<int>(N).ToArray();

            //output------------
            //ライトを持っている人だけのコレクションAK
            var AK = new List<int[]>();
            for (int i = 0; i < K; i++)
                AK.Add(XY[A[i] - 1]);

            long R = 0;
            for (int i = 0; i < N; i++)
            {
                long rmin = AK.Min(p => (long)(p[0] - XY[i][0]) * (p[0] - XY[i][0]) + (long)(p[1] - XY[i][1]) * (p[1] - XY[i][1]));
                R = Math.Max(R, rmin);
            }

            Out.Write(Math.Sqrt(R));
        }
    }
}