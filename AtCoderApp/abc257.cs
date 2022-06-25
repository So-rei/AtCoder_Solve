using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AtCoderApp
{
    //AC
    //■Ceilingでうっかりミス　1回WA
    public class abc257_a
    {
        public abc257_a()
        {
            //input-------------
            (var N, var X) = In.ReadTuple2<double>();

            //output------------
            //int div = X / N;
            //Out.Write("ABCDEFGHIJKLMNOPQRSTUVWXYZ"[div]);


            int div = (int)Math.Ceiling(X / N);
            Out.Write("ABCDEFGHIJKLMNOPQRSTUVWXYZ"[div - 1]);

            return;
        }
    }

    //AC
    public class abc257_b
    {
        public abc257_b()
        {
            //input-------------
            (var N, var K, var Q) = In.ReadTuple3<int>();
            var A = In.ReadAry<int>().ToArray();
            var L = In.ReadAry<int>().ToArray();

            //output------------
            for (int i = 0; i < Q; i++)
            {
                if (A[L[i] - 1] == N) continue; //一番右とばす
                                                //一番右のコマまたは、右隣の駒が隣接していない駒
                if (L[i] == K || A[L[i]] != A[L[i] - 1] + 1)
                    A[L[i] - 1]++;
            }
            Out.WriteMany(A);

            return;
        }
    }

    //AC
    public class abc257_c
    {
        public abc257_c()
        {
            //input-------------
            var N = In.Read<int>();
            var S = In.Read<string>();
            var W = In.ReadAry<int>().ToArray();

            //output------------
            //並べる
            var sw = new List<(int w, char s)>();
            for (int i = 0; i < N; i++)
                sw.Add((W[i], S[i]));

            sw = sw.OrderBy(p => p.w).ToList();

            //左から判定していく
            int adult_cnt = S.Count(p => p == '1');
            int fx = adult_cnt; //f(x)
            int fxMax = adult_cnt;

            for (int k = 0; k < N; k++)
            {
                int weight = sw[k].w + 1;

                //同じ重量の人が並んでることを考慮してループ
                int roop = 0;
                int ac = 0;
                while (k + roop < N && sw[k + roop].w == sw[k].w)
                {
                    ac += sw[k + roop].s == '0' ? 1 : -1;
                    roop++;
                }

                fxMax = Math.Max(fxMax, fx + ac);
                fx += ac;

                //同じ重量でループした分だけindexずらす
                k += roop - 1;
            }

            Out.Write(fxMax);

            return;
        }
    }

    //AC10 / TLE30
    //経由処理が雑、、
    public class abc257_d
    {
        public abc257_d()
        {
            //input-------------
            var N = In.Read<int>();
            var xyp = new List<(long x, long y, long p)>();
            for (int i = 0; i < N; i++)
            {
                var _xyp = In.ReadAry<int>().ToArray();
                xyp.Add((_xyp[0], _xyp[1], _xyp[2]));
            }

            //output------------
            //たかだかN200なので、ややこしいコストを明示的な値にする
            //t1 -> t2に移動する時のコストR

            var Sn = new long[N, N];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (i == j) continue;

                    double range = (double)(Math.Abs(xyp[i].x - xyp[j].x) + Math.Abs(xyp[i].y - xyp[j].y));
                    Sn[i, j] = Math.Max(1, (long)Math.Ceiling(range / (double)xyp[i].p)); //1÷(4*10^9)ということもありうるなるので、安全のため最小=1としとく
                }
            }

            //Snの大きい値をなるべく回避するようなスタート位置を探す
            //i->j への最短移動Snを求めることでわかる
            //N*N*(N-1)
            //var minSn = new long[N, N];
            for (int dim = 0; dim < N - 1; dim++) //経由回数=dim
                for (int i = 0; i < N; i++)
                    for (int j = 0; j < N; j++)
                    {
                        if (i == j) continue;
                        SearchMinTree(i, j);
                    }

            var SMin = long.MaxValue;
            for (int i = 0; i < N; i++)
            {
                long Si = 0;
                for (int j = 0; j < N; j++)
                    Si = Math.Max(Si, Sn[i, j]);

                SMin = Math.Min(SMin, Si);
            }

            Out.Write(SMin);

            //-----------------------------------
            //i->jの最短経路を求める(経由箇所1ヶ)
            void SearchMinTree(int i, int j)
            {
                for (int t = 0; t < N; t++)
                {
                    if (t == i || t == j) continue;

                    Sn[i, j] = Math.Min(Sn[i, j], Math.Max(Sn[i, t], Sn[t, j]));
                }
                return;
            }


            return;
        }
    }

    //AC10 / TLE30
    //処理の安全性加味したが、まだTLE・・・
    public class abc257_d2
    {
        public abc257_d2()
        {
            //input-------------
            var N = In.Read<int>();
            var xyp = new List<(long x, long y, long p)>();
            for (int i = 0; i < N; i++)
            {
                var _xyp = In.ReadAry<int>().ToArray();
                xyp.Add((_xyp[0], _xyp[1], _xyp[2]));
            }

            //output------------
            //たかだかN200なので、ややこしいコストを明示的な値にする
            //t1 -> t2に移動する時のコストR

            var Sn = new long[N, N];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (i == j) continue;

                    double range = (double)(Math.Abs(xyp[i].x - xyp[j].x) + Math.Abs(xyp[i].y - xyp[j].y));
                    Sn[i, j] = Math.Max(1, (long)Math.Ceiling(range / (double)xyp[i].p)); //1÷(4*10^9)ということもありうるなるので、安全のため最小=1としとく
                }
            }

            //Snの大きい値をなるべく回避するようなスタート位置を探す
            //i->j への最短移動Snを求めることでわかる
            //i,jの組み合わせ:N^2-N
            //経由数:最大N-2
            var SnCalced = new long[N, N, N - 1];
            for (int dim = 0; dim < N - 1; dim++) //経由回数=dim
                for (int i = 0; i < N; i++)
                    for (int j = 0; j < N; j++)
                    {
                        if (i == j) continue;
                        SearchMinTree(i, j, dim);
                    }

            //最小スタート位置を探す
            long SMin = long.MaxValue;
            for (int i = 0; i < N; i++)
            {
                long ti_max = 0;
                for (int j = 0; j < N; j++)
                {
                    if (i == j) continue;

                    long tij_min = long.MaxValue;
                    for (int dim = 0; dim < N - 1; dim++)
                    {
                        tij_min = Math.Min(tij_min, SnCalced[i, j, dim]);
                    }
                    ti_max = Math.Max(ti_max, tij_min);
                }
                SMin = Math.Min(SMin, ti_max);
            }

            Out.Write(SMin);

            //-----------------------------------
            //i->jの最短経路を求める
            long SearchMinTree(int i, int j, int dimension)
            {
                if (SnCalced[i, j, dimension] != 0) return SnCalced[i, j, dimension]; //既に計算したことがあったらその値を返す
                if (dimension == 0)
                {
                    SnCalced[i, j, dimension] = Sn[i, j];
                    return Sn[i, j]; //経由しない
                }

                //経由数をだいたい半分にして探す
                //ex)経由7の最小 = すべてのtについて【{i→tの経由3 , t→jの経由4} の大きい方】　の最小
                long tmp_min = long.MaxValue;
                int tmp_dimension = dimension / 2;
                for (int t = 0; t < N; t++)
                {
                    if (t == i || t == j) continue;

                    tmp_min = Math.Min(tmp_min, Math.Max(SearchMinTree(i, t, tmp_dimension), SearchMinTree(t, j, dimension - tmp_dimension - 1)));
                }

                SnCalced[i, j, dimension] = tmp_min;
                return tmp_min;
            }


            return;
        }
    }
}