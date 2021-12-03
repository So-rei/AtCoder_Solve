using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoderApp
{
    public class abc230_a
    {
        public abc230_a()
        {
            //input-------------
            var N = In.Read<int>();

            //output------------
            if (N >= 42) N++;

            Out.Write("AGC" + N.ToString("000"));

            //-------------------------------
        }
    }


    public class abc230_b
    {
        public abc230_b()
        {
            //input-------------
            var S = In.Read<string>();

            //output------------
            var T = "oxxoxxoxxoxxoxxoxxoxxoxxoxx"; //S<=10なので27文字もあればもう問題ない

            if (T.Contains(S))
                Out.Write("Yes");
            else
                Out.Write("No");

            //-------------------------------
        }
    }


    public class abc230_c
    {
        public abc230_c()
        {
            //input-------------
            var NAB = In.ReadAry<long>().ToArray();
            var N = NAB[0];
            var A = NAB[1];
            var B = NAB[2];

            var PQRS = In.ReadAry<long>().ToArray();
            var P = PQRS[0];
            var Q = PQRS[1];
            var R = PQRS[2];
            var S = PQRS[3];

            //output------------
            System.Text.StringBuilder ret = new System.Text.StringBuilder();

            long r1 = Math.Max(1 - A, 1 - B);
            long r2 = Math.Min(N - A, N - B);
            long r3 = Math.Max(1 - A, B - N);
            long r4 = Math.Min(N - A, N - 1);

            for (long i = P; i <= Q; i++)
            {
                for (long j = R; j <= S; j++)
                {
                    if (Math.Abs(i - A) != Math.Abs(j - B)) //白
                    {
                        ret.Append('.');
                    }
                    else
                    {
                        var k = i - A; //マイナスの値になることもある
                        if ((r1 <= k && k <= r2 && A + k == i && B + k == j) ||
                            (r3 <= k && k <= r4 && A + k == i && B - k == j))
                            ret.Append('#');//黒
                        else
                            ret.Append('.');//それ以外は白
                    }
                }

                //出力
                Out.Write(ret.ToString());

                ret.Clear();
            }


            //-------------------------------
        }
    }

    public class abc230_d
    {
        public abc230_d()
        {
            //input-------------
            var ND = In.ReadAry<int>().ToArray();
            var N = ND[0];
            var D = ND[1];

            var LR = new int[N][];
            for (int i = 0; i < N; i++)
            {
                var _LR = In.ReadAry<int>().ToArray();
                LR[i] = (new int[] { _LR[0], _LR[1] });
            }

            //output------------
            //これが正解。配列に手を加えようとするから遅いのであって、
            //ソートしておけば1つのデータにアクセスする回数は１回で済む。
            //これぐらい思いつけ～～
            LR = LR.OrderBy(p => p[1]).ToArray();

            int cnt = 0;
            int right = 0;
            for (int i = 0; i < N; i++)
            {
                if (right < LR[i][0]) //まだ殴られていない壁だったら
                {
                    right = LR[i][1] + D - 1; //そこが左端にかかるように殴る
                    cnt++;
                }
            }

            //タプルをやめてもList<int[]>にしてもint[]にしても3TLE-----------------------------------
            //3TLE
            //var LR = new List<(int L, int R)>();
            //for (int i = 0; i < N; i++)
            //{
            //    var _LR = In.ReadAry<int>().ToArray();
            //    LR.Add((_LR[0], _LR[1]));
            //}

            ////output------------
            //int cnt = 0;
            //while (true)
            //{
            //    var point = LR.Min(p => p.R) + D; //右端が一番左にある壁を選び、そこが左端にかかるように殴る
            //    LR.RemoveAll(p => p.L < point); //壊れたやつは削除             
            //    cnt++;
            //    if (LR.Count() == 0) break;
            //}


            //5TLE
            //output------------
            //int cnt = 0;
            //int li = 0;
            //int r1 = LR.Max(p => p[0]); //左端が一番右にある壁
            //while (true)
            //{
            //    li = LR.Where(p => p[0] >= li).Min(p => p[1]) + D; //右端が一番左にある壁を選び、そこが左端にかかるように殴る
            //    cnt++;
            //    if (r1 < li) break;
            //}

            Out.Write(cnt);
            

            //-------------------------------
        }
    }
}