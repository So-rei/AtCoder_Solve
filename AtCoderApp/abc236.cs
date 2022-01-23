using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//2022-01-23
namespace AtCoderApp
{
    public class abc236_a
    {
        public abc236_a()
        {
            //input-------------
            var S = In.Read<string>();
            var AB = In.ReadAry<int>().ToArray();

            //calc--------------
            var r = S.Substring(0, AB[0] - 1) + S[AB[1] - 1].ToString() + S.Substring(AB[0], AB[1] - AB[0] - 1) + S[AB[0] - 1].ToString() + S.Substring(AB[1]);
            Out.Write(r);
        }
    }

    //これじゃ遅い
    //public class abc236_b
    //{
    //    public abc236_b()
    //    {
    //        //input-------------
    //        var N = In.Read<int>();
    //        var A = In.ReadAry<int>().ToArray();

    //        //calc--------------
    //        for (int i = 1; i <= N; i++)
    //        {
    //            if (A.Count(p => p == i) == 3)
    //            {
    //                Out.Write(i);
    //                return;
    //            }
    //        }
    //    }
    //}

    public class abc236_b
    {
        public abc236_b()
        {
            //input-------------
            var N = In.Read<int>();
            var A = In.ReadAry<long>().ToArray();

            //calc--------------
            Out.Write((long)((long)N * (long)(N + 1) * 2) - (long)(A.Sum()));
        }
    }

    //まだ遅い AC3/TLE9
    //public class abc236_c
    //{
    //    public abc236_c()
    //    {
    //        //input-------------
    //        var NM = In.ReadAry<int>().ToArray();
    //        (var N, var M) = (NM[0], NM[1]);
    //        var S = In.ReadAry<string>().ToArray();
    //        var T = In.ReadAry<string>().ToArray();

    //        //calc--------------
    //        for (int i = 0; i < N; i++)
    //        {
    //            if (i == 0 || i == N - 1)
    //            {
    //                Out.Write("Yes"); //始点・終点
    //                continue;
    //            }
    //            Out.Write(T.Contains(S[i]) ? "Yes" : "No");
    //        }
    //    }
    //}
    public class abc236_c
    {
        public abc236_c()
        {
            //input-------------
            var NM = In.ReadAry<int>().ToArray();
            (var N, var M) = (NM[0], NM[1]);
            var S = In.ReadAry<string>().ToArray();
            var T = In.ReadAry<string>().ToList();

            //calc--------------
            for (int i = 0; i < N; i++)
            {
                //始点・終点はどうせ書いてあるので無視
                if (S[i] == T[0])
                {
                    Out.Write("Yes");
                    T.RemoveAt(0);
                }
                else
                    Out.Write("No");
            }
        }
    }

    //TLE19 AC9
    public class abc236_d
    {
        public abc236_d()
        {
            //input-------------
            var N = In.Read<int>();
            var Ax = new List<long[]>();
            for (int i = 0; i < 2 * N - 1; i++)
            {
                var aa = In.ReadAry<long>().ToArray();
                Ax.Add(aa);
            }

            //calc--------------
            //とりあえずゴリ押し
            long XOR_Max = 0;

            var con = Algo.Combination(2 * N, N);
            con = con.Take(con.Count() / 2);//写像になるので全組み合わせの半分で良い
            foreach (var cc in con)
            {
                var F = Algo.Permutation<int>(cc.ToArray()); //ペアの片方の並べ方
                var mc = Enumerable.Range(0, 2 * N).ToList();
                foreach (var c in cc)
                {
                    mc.Remove(c);
                }
                var M = Algo.Permutation<int>(mc.ToArray()); //ペアのもう片方の並べ方

                foreach (var f in F)
                {
                    foreach (var m in M)
                    {
                        long x = 0;
                        var ff = f.ToList();
                        var mm = m.ToList();
                        //ff[0]とmm[0],ff[1]とmm[1],....の2N人のペア
                        for (int i = 0; i < N; i++)
                        {
                            if (ff[i] > mm[i])
                                x ^= Ax[mm[i]][ff[i] - 1 - mm[i]];
                            else
                                x ^= Ax[ff[i]][mm[i] - 1 - ff[i]];
                        }
                        XOR_Max = Math.Max(XOR_Max, x);
                    }
                }
            }

            Out.Write(XOR_Max);

        }
    }

    public class abc236_e
    {
        public abc236_e()
        {
            //input-------------
            var N = In.Read<int>();
            var Ax = In.ReadAry<int>().ToArray();
        }
    }
}