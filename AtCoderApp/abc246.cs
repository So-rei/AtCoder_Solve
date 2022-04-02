using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtCoderApp
{
    //2022/04/02 abc246 a+b+c 600点 38:04(1)

    public class abc246_a
    {
        public abc246_a()
        {
            //input-------------
            (var X, var Y) = In.ReadTuple2<int>();
            (var X2, var Y2) = In.ReadTuple2<int>();
            (var X3, var Y3) = In.ReadTuple2<int>();

            //outpu
            var ox = 0;
            var oy = 0;
            if (X == X2) ox = X3;
            else if (X2 == X3) ox = X;
            else ox = X2;
            if (Y == Y2) oy = Y3;
            else if (Y2 == Y3) oy = Y;
            else oy = Y2;

            Out.Write(ox.ToString() + " " + oy.ToString());

        }
    }
    public class abc246_b
    {
        public abc246_b()
        {
            //input-------------
            (var A, var B) = In.ReadTuple2<int>();

            //output------------
            double t = Math.Sqrt(A * A + B * B);
            double a = A / t;
            double b = B / t;
            Out.WriteMany(new[] { a, b });

        }
    }

    //TLE
    public class abc246_c
    {
        public abc246_c()
        {
            //input-------------
            (var N, var K, var X) = In.ReadTuple3<int>();
            var A = In.ReadAry<long>().ToArray();

            //output------------
 
            for (int i = 0; i < N; i++)
            {
                while (A[i] > X && K > 0)
                {
                    A[i] -= X;
                    K--;
                }
                if (K == 0) //無駄なく使い切れる時
                {
                    Out.Write(A.Sum());
                    return;
                }
            }

            var a = A.OrderByDescending(p => p).ToList();
            for (int t = 0; t < K; t++)
            {
                a.RemoveAt(0);
                if (a.Count() == 0)
                {
                    Out.Write(0);
                    return;
                }
            }
            Out.Write(a.Sum());

        }
    }

    //TLEで書き直し
    public class abc246_c2
    {
        public abc246_c2()
        {
            //input-------------
            (var N, var K, var X) = In.ReadTuple3<int>();
            var A = In.ReadAry<long>().ToArray();

            //output------------

            for (int i = 0; i < N; i++)
            {
                var mod = A[i] / X;
                if (K <= mod)//無駄なく使い切れる時
                {
                    Out.Write(A.Sum() - X * K);
                    return;
                }

                A[i] -= X * mod;
                K -= (int)mod;
            }

            var a = A.OrderByDescending(p => p).ToList();
            if (N <= K)
            {
                Out.Write(0);
                return;
            }
            a.RemoveRange(0, K);
            Out.Write(a.Sum());

        }
    }


    //遅い
    public class abc246_d
    {
        public abc246_d()
        {
            //input-------------
            var N = In.Read<long>();

            //output------------
            //(a^2+b^2)(a+b)
            long X = 1000000_000000_000000;
            for (int a = 0; a <= 1000000; a++) //条件よりmax=10^6なのは自明
            {
                if (a * a * a > X) break; //終了

                //パターン削減のためa>=bとする
                for (int b = 0; b <= a; b++)
                {
                    long tx = (a * a + b * b) * (a + b);
                    if (tx < N)
                        continue;

                    X = Math.Min(X, tx);
                    break;
                }
            }
            Out.Write(X);
        }
    }

    //時間切れのあと解いたやつ
    //ほとんどあっているが、
    public class abc246_e
    {
        public abc246_e()
        {
            //input-------------
            var N = In.Read<long>();
            (var Ax, var Ay) = In.ReadTuple2<int>();
            (var Bx, var By) = In.ReadTuple2<int>();
            Ax--;
            Ay--;
            Bx--;
            By--;
            var S = new string[N];
            for (int i = 0; i < N; i++)
                S[i] = In.Read<string>();

            //output------------

            //NGパターン
            if ((Math.Abs(Ax - Bx) - Math.Abs(Ay - By)) % 2 == 1) //ポーンの動き方的にいきようがない
            {
                Out.Write(-1);
                return;
            }
            //if (S[Bx][By] == '#') //行き先にポーンがある
            //{
            //    Out.Write(-1);
            //    return;
            //}    

            var dp = new int[N, N];
            var li = new List<(int x, int y)>(); //cnt回目の探索で見つかった点
            li.Add((Ax, Ay)); //初期位置

            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    dp[i, j] = (int)(N * N / 2) + 5;//最長手数は全マス数/2+1を超えることはない

            setdp(Ax, Ay, 0);
            for (int cnt = 1; cnt < N * N / 2 + 5; cnt++) //最長手数は全マス数/2+1を超えることはない
            {
                if (li.Count() == 0) break; //これ以上違う手がなければストップ

                var nli = new List<(int x, int y)>();
                foreach (var mem in li) nli.Add(mem);
                li.Clear();

                foreach (var mem in nli)
                {
                    if (setdp(mem.x, mem.y, cnt))
                    {
                        Out.Write(cnt + 1);
                        return;
                    }
                }
            }

            Out.Write(-1);

            bool setdp(int x, int y, int cnt)
            {
                for (int d = 1; d < Math.Min(N - x, N - y); d++)
                {
                    if (S[x + d][y + d] == '#' || dp[x + d, y + d] < cnt) break;
                    dp[x + d, y + d] = cnt + 1;
                    li.Add((x + d, y + d));
                }
                for (int d = 1; d < Math.Min(x + 1, y + 1); d++)
                {
                    if (S[x - d][y - d] == '#' || dp[x - d, y - d] < cnt) break;
                    dp[x - d, y - d] = cnt + 1;
                    li.Add((x - d, y - d));
                }
                for (int d = 1; d < Math.Min(N - x, y + 1); d++)
                {
                    if (S[x + d][y - d] == '#' || dp[x + d, y - d] < cnt) break;
                    dp[x + d, y - d] = cnt + 1;
                    li.Add((x + d, y - d));
                }
                for (int d = 1; d < Math.Min(x + 1, N - y); d++)
                {
                    if (S[x - d][y + d] == '#' || dp[x - d, y + d] < cnt) break;
                    dp[x - d, y + d] = cnt + 1;
                    li.Add((x - d, y + d));
                }

                return dp[Bx, By] == cnt + 1;//Bについたかどうかを返す
            }
        }
    }
}