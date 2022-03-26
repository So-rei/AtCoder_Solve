using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtCoderApp
{
    /// <summary>
    /// 2022-03-26 abc245
    /// abcd,1000点 63:38
    /// </summary>

    //AC
    public class abc245_a
    {
        public abc245_a()
        {
            //input-------------
            var abcd = In.ReadAry<int>().ToArray();

            //calc--------------
            if (abcd[0] < abcd[2] || (abcd[0] == abcd[2] && abcd[1] <= abcd[3]))
                Out.Write("Takahashi");
            else
                Out.Write("Aoki");
        }
    }

    //AC
    public class abc245_b
    {
        public abc245_b()
        {
            //input-------------
            var N = In.Read<int>();
            var a = In.ReadAry<int>().ToArray();

            //calc--------------
            for (int i = 0; i < 2001; i++)
            {
                if (!a.Contains(i))
                {
                    Out.Write(i);
                    return;
                }
            }
        }
    }
    //AC
    public class abc245_c
    {
        public abc245_c()
        {
            //input-------------
            var nk = In.ReadAry<int>().ToArray();
            (var N, var K) = (nk[0], nk[1]);
            var A = In.ReadAry<int>().ToArray();
            var B = In.ReadAry<int>().ToArray();

            //calc--------------
            var R = A[0];
            var rA = new int[N]; //0,1 1=Aとつながってる
            var rB = new int[N]; //0,1 1=Bとつながってる

            //初回
            rA[0] = 1;
            rB[0] = 1;
            //2回目以降はi-1回目の情報を元に
            for (var i = 1; i < N; i++)
            {
                if (rA[i - 1] == 1)
                {
                    if (Math.Abs(A[i - 1] - A[i]) <= K)
                        rA[i] = 1;
                    if (Math.Abs(A[i - 1] - B[i]) <= K)
                        rB[i] = 1;
                }
                if (rB[i - 1] == 1)
                {
                    if (Math.Abs(B[i - 1] - A[i]) <= K)
                        rA[i] = 1;
                    if (Math.Abs(B[i - 1] - B[i]) <= K)
                        rB[i] = 1;
                }
                if (rA[i] + rB[i] == 0)
                {
                    Out.Write("No");
                    return;
                }
            }
            Out.Write("Yes");
        }
    }

    //AC
    public class abc245_d
    {
        public abc245_d()
        {
            //input-------------
            var nm = In.ReadAry<int>().ToArray();
            (var N, var M) = (nm[0], nm[1]);
            var A = In.ReadAry<int>().Reverse().ToArray();//順序逆
            var C = In.ReadAry<int>().Reverse().ToArray();//＊

            //calc--------------
            var B = new int[M + 1];

            for (int i = 0; i < M + 1; i++)
            //for (int i = 0; i < N + M; i++) //間違い！ 1WA
            {
                B[i] = (int)(C[i] / A[0]); //上からi個目(=M-1次)項の数字
                for (int a = 0; a < N + 1; a++)
                    C[i + a] -= A[a] * B[i]; //Aの残りの項を引いていく
            }
            Out.WriteMany(B.Reverse()); //順序戻して出力
        }
    }

    //TLE。解決する方法わからないまま時間切れ
    public class abc245_e
    {
        public abc245_e()
        {
            //input-------------
            var nm = In.ReadAry<int>().ToArray();
            (var N, var M) = (nm[0], nm[1]);
            var A = In.ReadAry<int>().ToArray();
            var B = In.ReadAry<int>().ToArray();
            var C = In.ReadAry<int>().ToArray();
            var D = In.ReadAry<int>().ToArray();

            //calc--------------

            //無駄な面積最小=最善手　である
            //大きいものから順になるべく小さい箱に入れていく
            var AB = new List<(int A, int B)>();
            var CD = new List<(int C, int D)>();

            for (int i = 0; i < N; i++)
                AB.Add((A[i], B[i]));
            for (int i = 0; i < M; i++)
                CD.Add((C[i], D[i]));

            AB = AB.OrderByDescending(p => p.A).ThenByDescending(q => q.B).ToList();
            CD = CD.OrderBy(p => p.C).ThenBy(q => q.D).ToList();

            var nAB = new List<(int A, int B)>(AB);
            var nCD = new List<(int C, int D)>(CD);

            for (int i = 0; i < AB.Count(); i++)
            {
                //ぴったりが最優先
                var rSame = CD.Where(p => AB[i].A == p.C && AB[i].B == p.D);
                if (rSame.Count() > 0)
                {
                    nAB.Remove(AB[i]);
                    nCD.Remove(rSame.First());
                    continue;
                }
                //ただ1つしかない場合も優先
                var rOne = CD.Where(p => AB[i].A <= p.C && AB[i].B <= p.D);
                if (rOne.Count() == 1)
                {
                    nAB.Remove(AB[i]);
                    nCD.Remove(rOne.First());
                    continue;
                }
            }

            //残りパタン
            var b = MainCalc(nCD, 0);
            if (b) Out.Write("Yes");
            else Out.Write("No");
            return;


            bool MainCalc(List<(int C, int D)> cd, int r)
            {
                for (int i = r; i < nAB.Count(); i++)
                {
                    var bone = true;
                    var box = cd.Where(p => nAB[i].A <= p.C && nAB[i].B <= p.D);
                    if (box.Count() == 0) return false;

                    foreach (var bx in box)
                    {
                        var ccd = cd.Where(p => p.C != bx.C && p.D != bx.D).ToList();
                        bone = MainCalc(ccd, r + 1);
                        if (!bone) return false;
                    }
                }
                return true;
            }
        }
    }
}