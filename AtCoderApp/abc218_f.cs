using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoderApp
{
    //20210912 半分ぐらいタイムオーバー

    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        new abc218_f();
    //    }
    //}

    public class abc218_f
    {
        public abc218_f()
        {
            //Set---
            var NM = Console.ReadLine().Split(' ');
            int N = Convert.ToInt32(NM[0]);
            int M = Convert.ToInt32(NM[1]);

            var st = new int[M][];
            for (var i = 0; i < M; i++)
            {
                var _st = Console.ReadLine().Split(' ');
                st[i] = new int[2];
                st[i][0] = Convert.ToInt32(_st[0]);
                st[i][1] = Convert.ToInt32(_st[1]);
            }

            //Calc---
            var ToN = CalcToN(st);

            //結果出力---

            //そもそもたどり着けない場合全部-1
            if (ToN == null)
            {
                for (var i = 0; i < M; i++)
                    Console.WriteLine(-1);
                return;
            }

            for (var i = 0; i < M; i++)
            {
                //最短ルート
                if (!ToN.Contains(i))
                {
                    Console.WriteLine(ToN.Count());
                    continue;
                }

                //最短ルートが使えない場合
                var eraseST = st.Where((p,index) => index != i).ToArray();
                var eraseToN = CalcToN(eraseST);

                Console.WriteLine(eraseToN == null ? -1 : eraseToN.Count());
            }

            //ローカル関数 1 to N minを計算
            List<int> CalcToN(int[][] st)
            {
                var cToN = new Dictionary<int, List<int>>();

                //1回目
                foreach (var q in st.Select((p, index) => (p, index)).Where(q => q.p[0] == 1).ToList())
                {
                    if (q.p[1] == N) return new List<int>() { q.index };//でたら即終了

                    cToN.Add(q.p[1], new List<int> { q.index });
                };

                //2～N回目
                for (var i = 1; i < N; i++)
                {
                    var nextcToN = cToN.Where(r => r.Value.Count() == i).ToList();

                    foreach (var p in nextcToN)
                    {
                        foreach (var s in st.Select((q, index) => (q, index)).Where(r => r.q[0] == p.Key).ToList())
                        {
                            if (!cToN.ContainsKey(s.q[1]))
                            {
                                var indexAry = new List<int>();
                                indexAry.AddRange(p.Value);
                                indexAry.Add(s.index);
                                cToN.Add(s.q[1], indexAry);
                                if (s.q[1] == N) break;//でたら即終了
                            }
                        };
                    };
                }

                var Ret = cToN.Where(p => p.Key == N);
                return Ret.Count() == 0 ? null : Ret.First().Value;
            }

            return;
        }
    }
}
