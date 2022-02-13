using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtCoderApp
{
    /// <summary>
    /// 2022-02-13
    /// 0ten...
    /// </summary>
    public class arc135_a
    {
        //TLE.
        //Listが莫大になる?
        public arc135_a()
        {
            //input-------------
            var X = In.Read<long>();

            //calc--------------
            const long MOD = 998244353;

            var dL = div(X);

            long ret = 1;
            foreach (var d in dL)
                ret = ret * d % MOD;

            Out.Write(ret);

            List<long> div(long X)
            {
                var dAry = new List<long>();
                var r = (X - X % 2) / 2;
                if (X > 4)
                {
                    dAry.AddRange(div(r));
                    dAry.AddRange(div(X - r));
                }
                else
                    dAry.Add(X);

                return dAry;
            }
        }

        //これでもTLE
        public class arc135_a_2
        {
            public arc135_a_2()
            {
                //input-------------
                var X = In.Read<long>();

                //calc--------------
                const long MOD = 998244353;

                var ret = div(X);
                Out.Write(ret);

                long div(long X)
                {
                    if (X > 4)
                    {
                        if (X % 2 == 0)
                        {
                            var rm = div(X / 2) % MOD;
                            return rm * rm % MOD; //計算短縮用
                        }
                        else
                        {
                            var r = (X - 1) / 2;
                            return div(r) * div((X - r)) % MOD;
                        }
                    }
                    else
                        return X;
                }
            }
        }

        //時間切れ
        //AC27 WA14 TLE2
        public class arc135_b
        {
            public arc135_b()
            {
                //input-------------
                var N = In.Read<int>();
                var Sn = In.ReadAry<int>().ToArray();

                //calc--------------
                if (N == 1)
                {
                    Out.Write("Yes");
                    Out.Write("0 0 " + Sn[0].ToString());
                    return;
                }

                var r = new int[N + 2];
                //条件確認
                //s(n) < s(n+1) のとき、 A(n+3) >=  s(n+1) - s(n)
                //逆も同様 A(n-1) >= s(n+1) - s(n)
                for (int i = 0; i < N + 1; i++)
                {
                    var rR = 0;
                    var rL = 0;

                    if (i < N - 1 && Sn[i] > Sn[i + 1])
                    {
                        var sa = Sn[i] - Sn[i + 1];
                        if (Sn[i] < sa || Sn[i + 1] < sa)
                        {
                            Out.Write("No");
                            return;
                        }
                        rR = sa;
                    }

                    if (i >= 3 && Sn[i - 3] < Sn[i - 2])
                    {
                        var sa = Sn[i - 2] - Sn[i - 3];
                        if (Sn[i - 2] < sa || Sn[i - 3] < sa)
                        {
                            Out.Write("No");
                            return;
                        }
                        rL = sa;
                    }

                    //RL条件競合
                    if ((i >= 3 && rR > rL && rR > Math.Min(Sn[i - 3], Sn[i - 2])) ||
                        (i <= N - 1 && rR < rL && rL > Math.Min(Sn[i], Sn[i + 1])))
                    {
                        Out.Write("No");
                        return;
                    }
                    r[i] = Math.Max(rR, rL);
                }

                //success
                Out.Write("Yes");

                var rr = SetMain(0, r);
                Out.WriteMany(rr.ret);

                //条件によって計算量が減らせるループ
                (bool isOk, int[] ret) SetMain(int index, int[] joken)
                {
                    if (index >= N - 1)
                    {
                        joken[N] = Sn[index - 1] - joken[N - 1] - joken[N - 2];
                        joken[N + 1] = Sn[index] - joken[N] - joken[N - 1];
                        return (true, joken);//全部やったら終了
                    }

                    for (int i = joken[index]; i <= Sn[index] - joken[index] + joken[index + 1] + joken[index + 2]; i++)
                    {
                        if (index >= 2 && (joken[index - 2] + joken[index - 1] + i != Sn[index - 2]))//3つ目以降はサムチェック
                            continue;

                        var nj = new int[N + 2];
                        for (var k = 0; k < joken.Length; k++)
                        {
                            nj[k] = joken[k];
                        }

                        nj[index] = i;
                        var r = SetMain(index + 1, nj);
                        if (r.isOk) return r;
                    }

                    return (false, null);
                }


            }
        }
    }
}