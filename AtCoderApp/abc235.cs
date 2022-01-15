using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtCoderApp
{
    //AC
    public class abc235_a
    {
        public abc235_a()
        {
            //input-------------
            var abc = In.Read<string>();

            //calc--------------
            int r = 0;
            r += sum(Convert.ToInt32(abc[0].ToString()), Convert.ToInt32(abc[1].ToString()), Convert.ToInt32(abc[2].ToString()));
            r += sum(Convert.ToInt32(abc[1].ToString()), Convert.ToInt32(abc[2].ToString()), Convert.ToInt32(abc[0].ToString()));
            r += sum(Convert.ToInt32(abc[2].ToString()), Convert.ToInt32(abc[0].ToString()), Convert.ToInt32(abc[1].ToString()));

            Out.Write(r);

            int sum(int a, int b, int c)
            {
                return a * 100 + b * 10 + c;
            }
        }
    }



    //AC
    public class abc235_b
    {
        public abc235_b()
        {
            //input-------------
            var N = In.Read<int>();
            var Hn = In.ReadAry<int>().ToArray();

            //calc--------------
            int max = 0;
            foreach (var i in Hn)
            {
                if (max < i)
                    max = i;
                else
                    break;
            }

            Out.Write(max);

        }
    }

    public class abc235_c
    {
        public abc235_c()
        {
            //input-------------
            var NQ = In.ReadAry<int>().ToArray();
            var N = NQ[0];
            var Q = NQ[1];
            var a = In.ReadAry<int>().ToArray();
            var xk = new List<int[]>();
            for (int i = 0; i < Q; i++)
            {
                var _xk = In.ReadAry<int>().ToArray();
                xk.Add(new int[] { _xk[0], _xk[1] });
            }

            //calc--------------

            //TLE12 AC2
            for (int i = 0; i < Q; i++)
            {
                var col = a.Select((p, index) => (p, index)).Where(q => q.p == xk[i][0]);
                if (col.Count() < xk[i][1])
                    Out.Write(-1);
                else
                    Out.Write(col.ToList()[xk[i][1] - 1].index + 1);
            }

            //先にリストを作ってみても同じ...TLE12 AC2　うーん？
            ////aから先にリストを作成する
            //var ans = new Dictionary<(int x, int k), int>(); //x,k,cnt
            //for (int i = 0; i < N; i++)
            //{
            //    var cnt = ans.Count(p => p.Key.x == a[i]);
            //    ans.Add((a[i], cnt + 1), i + 1);
            //}

            ////結果計算
            //for (int i = 0; i < Q; i++)
            //{
            //    Out.Write(ans.ContainsKey((xk[i][0], xk[i][1])) ? ans[(xk[i][0], xk[i][1])] : -1);
            //}


        }
    }


    //AC
    public class abc235_d
    {
        public abc235_d()
        {
            //input-------------
            var aN = In.ReadAry<int>().ToArray();
            var a = aN[0];
            var N = aN[1];

            //calc--------------
            //無理なパターンを除外
            if (a.ToString().Count() > N.ToString().Count())
            {
                Out.Write(-1);
                return;
            }

            var rFrom = new Dictionary<int, int>();
            var rTo = new Dictionary<int, int>();
            //計算できるパターン(a→a^x乗)
            for (int i = 0; Math.Pow(a, i) < N; i++)
            {
                rFrom.Add((int)Math.Pow(a, i), i);
            }
            //計算できるパターンチェック(N→N/aまたは..)
            bool bEnd = false;
            maincalc(new List<int>() { N }, 0);

            void maincalc(List<int> Cn, int cnt)
            {
                var nextn = new List<int>();

                if (bEnd)
                    return;
                //件数が無理そう
                if (cnt > 1000)
                {
                    bEnd = true;
                    Out.Write(-1);
                    return;
                }
                foreach (var n in Cn)
                {
                    //結果がa^x乗で表すことが出来た
                    if (rFrom.ContainsKey(n))
                    {
                        bEnd = true;
                        Out.Write(rFrom[n] + cnt);
                        return;
                    }

                    //もう既に見た値はスキップ
                    if (rTo.ContainsKey(n))
                        continue;

                    rTo.Add(n, cnt);

                    if (n % a == 0)
                        nextn.Add(n / a);
                    if (n > 9 && n.ToString()[1] != '0')
                        nextn.Add((n % (int)Math.Pow(10, n.ToString().Count() - 1)) * 10 + Convert.ToInt32(n.ToString().First().ToString()));
                }

                nextn = nextn.Distinct().ToList();

                maincalc(nextn, cnt + 1);
            }
        }
    }
}
