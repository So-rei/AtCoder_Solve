using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoderApp
{
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        new abc226_a();
    //    }
    //}

    public class abc226_a
    {
        public abc226_a()
        {
            //input-------------
            var X = In.Read<double>();

            //calc--------------
            double xx = Math.Round(X, MidpointRounding.AwayFromZero);

            Out.Write(xx.ToString());
        }
    }

    public class abc226_b
    {
        public abc226_b()
        {
            //input-------------
            var N = In.Read<double>();
            var ax = new List<int[]>();
            for (int i = 0; i < N; i++)
            {
                var c = In.ReadAry<int>().ToArray();
                ax.Add(c);//配列サイズごと覚えても問題ない
            }

            //これだとTLE
            //calc--------------
            //var cnt = 0;
            //var cGrp = ax.OrderBy(p => p[0]).ToList();
            //for (int x = 0; x < N; x++)
            //{
            //    for (int y = x + 1; y < N; y++)
            //    {
            //        if (cGrp[x][0] == cGrp[y][0])
            //        {
            //            //重複チェック
            //            bool bSame = true;
            //            for (int i = 1; i <= cGrp[x][0]; i++) //indexは除く
            //            {
            //                if (cGrp[x][i] != cGrp[y][i])
            //                {
            //                    bSame = false;
            //                    break;
            //                }
            //            }
            //            if (bSame) cnt++;
            //        }
            //        else
            //        {
            //            x = y - 1;//xはサイズが変わるごとに1回だけループするようにする
            //            break;
            //        }
            //    }
            //}

            //これでもまだTLE
            //calc--------------
            var cnt = 0;
            var cGrp = ax.OrderBy(p => p[0]).ToList();
            for (int x = 0; x < cGrp.Count(); x++)
            {
                cnt += cGrp.Count(p => p.SequenceEqual(cGrp[x])) - 1;
                cGrp.Remove(cGrp[x]);
            }

            Out.Write(N - cnt);
        }
    }
    public class abc226_c
    {
        public abc226_c()
        {
            //input-------------
            var N = In.Read<int>();
            var tka = new List<int[]>();
            for (int i = 0; i < N; i++)
            {
                var c = In.ReadAry<int>().ToArray();
                tka.Add(c);
            }

            //TLE 3/15
            //calc--------------
            long ret = childs(N, new List<int>());
            Out.Write(ret);

            long childs(int Tx, List<int> iUsed)
            {
                long cnt = tka[Tx - 1][0];
                for (int i = 2; i < 2 + tka[Tx - 1][1]; i++) //対応tkaを見て子供dp処理
                {
                    if (!iUsed.Contains(tka[Tx - 1][i]))
                    {
                        iUsed.Add(tka[Tx - 1][i]);
                        cnt += childs(tka[Tx - 1][i], iUsed);
                    }
                }
                return cnt;
            }

            //こっちでもTLE
            ////TLE 3/15
            ////calc--------------
            //var Chi = childs(N).Distinct();
            //long ret = tka[N - 1][0];
            //foreach (int i in Chi)
            //    ret += tka[i - 1][0];
            //Out.Write(ret);

            //IEnumerable<int> childs(int Tx)
            //{
            //    for (int i = 2; i < 2 + tka[Tx - 1][1]; i++) //対応tkaを見て子供dp処理
            //    {
            //        yield return tka[Tx - 1][i];
            //        foreach (var c in childs(tka[Tx - 1][i]))
            //            yield return c;
            //    }
            //    yield break;
            //}
        }
    }
}