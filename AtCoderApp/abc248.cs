using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtCoderApp
{
    //AC
    public class abc248_a
    {
        public abc248_a()
        {
            //input-------------
            var S = In.Read<string>();

            //output------------
            for (int i = 0; i <= 9; i++)
            {
                if (!S.Contains(i.ToString()))
                {
                    Out.Write(i.ToString());
                    return;
                }
            }
        }
    }

    //AC
    public class abc248_b
    {
        public abc248_b()
        {
            //input-------------
            (var A, var B, var K) = In.ReadTuple3<int>();

            //output------------
            int cnt = 0;
            try //オーバーフロー対策
            {
                var sum = A;
                while (true)
                {
                    if (sum >= B)
                    {
                        Out.Write(cnt);
                        return;
                    }
                    cnt++;
                    sum *= K;
                }
            }
            catch
            {
                Out.Write(cnt);
            }
        }
    }
    
    //c :python

    public class abc248_d
    {
        public abc248_d()
        {
            //input-------------
            var N = In.Read<int>();
            var A = In.ReadAry<int>().ToArray();
            var Q = In.Read<int>();

            var query = new List<int[]>();
            for (int i = 0; i < Q; i++)
                query.Add(In.ReadAry<int>().ToArray());

            //output------------
            int L, R, RL, X;
            for (int i = 0; i < Q; i++)
            {
                //TLE17 AC1
                //配列を毎回カットするのは遅い
                //L = query[i][0] - 1;
                //RL = query[i][1] - L;
                //X = query[i][2];

                //Out.Write(A.Skip(L).Take(RL).Count(p => p == X));

                //TLE 11AC7
                //単純な実装
                L = query[i][0] - 1;
                R = query[i][1];
                X = query[i][2];

                var cnt = 0;
                for (int j = L; j < R; j++)
                    if (A[j] == X) cnt++;
                Out.Write(cnt);
            }
        }
    }
    /// <summary>
    /// 解説を見て作成だがまだTLE
    /// </summary>
    public class abc248_d_2
    {
        public abc248_d_2()
        {
            //input-------------
            var N = In.Read<int>();
            var A = In.ReadAry<int>().ToArray();
            var Q = In.Read<int>();

            var query = new List<int[]>();
            for (int i = 0; i < Q; i++)
                query.Add(In.ReadAry<int>().ToArray());

            //output------------
            //どでかい入れ物に全部覚えさせてしまう(昇順なのでのちの探索が容易)
            var vek = new List<int>[N];
            for (int i = 0; i < N; i++)
                vek[i] = new List<int>();
            for (int i = 0; i < N; i++)
                vek[A[i] - 1].Add(i);

            int L, R, X;
            for (int i = 0; i < Q; i++)
            {
                int cnt = 0;

                L = query[i][0];
                R = query[i][1];
                X = query[i][2];

                //ここを二分探索にしたらACできるはず？
                for (int j = 0; j < vek[X - 1].Count(); j++)
                {
                    if (vek[X - 1][j] < L - 1) continue;
                    if (vek[X - 1][j] < R) cnt++;
                }
                Out.Write(cnt);
            }
        }
    }

    //TLE5 AC31
    public class abc248_e
    {
        public abc248_e()
        {
            //input-------------
            (var N, var K) = In.ReadTuple2<int>();
            var xy = new List<(int X, int Y)>();
            for (int i = 0; i < N; i++)
                xy.Add(In.ReadTuple2<int>());

            //output------------
            if (K == 1)
            {
                Out.Write("Infinity");
                return;
            }

            int cnt = 0;//結果
            var used = new List<List<int>>();

            //N*(N-1)/2の組み合わせ
            for (int i = 0; i < N; i++)
            {
                for (int j = i+1; j < N; j++) 
                {
                    if (i == j) continue;
                    if (used.Exists(p => p.Contains(i) && p.Contains(j))) continue; //チェック済の直線

                    //i,jを通る直線を作成
                    //ax+by+c = 0
                    int a, b, c;
                    b = xy[j].X - xy[i].X;
                    a = -(xy[j].Y - xy[i].Y);
                    c = -(a * xy[i].X + b * xy[i].Y);

                    //何本を同時に通るか
                    int k = 2;
                    var tmp_list = new List<int>() { i, j };
                    for (int r = 0; r < N; r++)
                    {
                        if (r == i || r == j) //2点を除く
                            continue;

                        if (N - r < K - k) break; //もう見込みが無いので途中終了

                        if (xy[r].X * a + xy[r].Y * b + c == 0)
                        {
                            k++;
                            tmp_list.Add(r);
                        }
                    }

                    if (k >= K)
                    {
                        //条件を満たす
                        cnt++;
                        used.Add(tmp_list); //通ったリストを保存
                    }
                }
            }
            Out.Write(cnt);
        }
    }

    ////TLE
    //public class abc248_e
    //{
    //    public abc248_e()
    //    {
    //        //input-------------
    //        (var N, var K) = In.ReadTuple2<int>();
    //        var xy = new List<(int X, int Y)>();
    //        for (int i = 0; i < N; i++)
    //            xy.Add(In.ReadTuple2<int>());

    //        //output------------
    //        if (K == 1)
    //        {
    //            Out.Write("Infinity");
    //            return;
    //        }

    //        int cnt = 0;//結果
    //        var used = new List<int[]>();

    //        for (int i = 0; i < N; i++)
    //        {
    //            for (int j = 0; j < N; j++)
    //            {
    //                if (i == j) continue;

    //                //ax+by+c = 0
    //                int a, b, c;
    //                b = xy[j].X - xy[i].X;
    //                a = -(xy[j].Y - xy[i].Y);
    //                c = -(a * xy[i].X + b * xy[i].Y);

    //                if (used.Exists(p => (p[0] == a && p[1] == b && p[2] == c) || (p[0] == -a && p[1] == -b && p[2] == -c))) continue; //チェック済の直線

    //                //何本を同時に通るか
    //                int k = 2;
    //                for (int r = 0; r < N; r++)
    //                {
    //                    if (r == i || r == j)
    //                        continue;

    //                    if (N - r < K - k) break; //もう見込みが無いので途中終了

    //                    if (xy[r].X * a + xy[r].Y * b + c == 0)
    //                    {
    //                        k++;
    //                    }
    //                }

    //                if (k >= K)
    //                {
    //                    //条件を満たす
    //                    cnt++;
    //                    used.Add(new int[] { a, b, c }); //通った直線リストを保存
    //                }
    //            }
    //        }
    //        Out.Write(cnt);
    //    }
    //}

    //解説見て作成
    //WAもでるしTLEもでるしこの手法はあんまよくないっぽい！
    public class abc248_e_2
    {
        public abc248_e_2()
        {
            //input-------------
            (var N, var K) = In.ReadTuple2<int>();
            var xy = new List<(int X, int Y)>();
            for (int i = 0; i < N; i++)
                xy.Add(In.ReadTuple2<int>());

            //output------------
            if (K == 1)
            {
                Out.Write("Infinity");
                return;
            }

            int cnt = 0;//結果
            var used = new List<double[]>();

            for (int i = 0; i < N; i++)
            {
                for (int j = i + 1; j < N; j++)
                {
                    if (i == j) continue;
                    //if (used.Exists(p => p.Contains(i) && p.Contains(j))) continue; //チェック済の直線

                    //i,jを通る直線を作成
                    //ax+by+c = 0
                    int a, b, c;
                    b = xy[j].X - xy[i].X;
                    a = -(xy[j].Y - xy[i].Y);
                    c = -(a * xy[i].X + b * xy[i].Y);

                    //傾きとy切片とx=nのときのnを保存する
                    double r = b == 0 ? 0 : -a / b;
                    double ys = a == 0 ? 0 : -c / a;
                    double n = b == 0 && a != 0 ? -c / a : int.MaxValue;//x=nのときのn
                    used.Add(new double[] { r, ys, n }); //通ったリストを保存

                    //こういう小細工でabcをユニークにしてabc保存するのは難しい
                    //if (a < 0 || (a == 0 && b < 0))
                    //{
                    //    a = -a;
                    //    b = -b;
                    //    c = -c;
                    //}
                    //var max = (a == 0 || Math.Abs(a) < Math.Abs(b) ? b : a);
                    //max = c == 0 || max < Math.Abs(c) ? max : c;
                    //int t = 2;
                    //while (t <= max)
                    //{
                    //    if (a % t == 0 && b % t == 0 && c % t == 0)
                    //    {
                    //        a /= t;
                    //        b /= t;
                    //        c /= t;
                    //        max /= t;
                    //        t = 2;
                    //        continue;
                    //    }
                    //    t++;
                    //}
                    //used.Add(new int[] { a, b, c }); //通ったリストを保存
                }
            }

            var ecomp = new eqali();
            foreach (var u in used.Distinct(ecomp))
            {
                var ptn = used.Count(p => p[0] == u[0] && p[1] == u[1] && p[2] == u[2]);
                if (ptn >= K * (K - 1) / 2)
                    cnt++;
            }
            Out.Write(cnt);
        }

        public class eqali : IEqualityComparer<double[]>
        {
            public bool Equals(double[] x, double[] y)
            {
                for (int i = 0; i < x.Length; i++)
                {
                    if (x[i] != y[i]) return false;
                }
                return true;
            }
            public int GetHashCode(double[] obj)
            {
                return string.Join("-", obj).GetHashCode();
            }
        }
    }
}