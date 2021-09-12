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
            var NM = Console.ReadLine().Split(' ');
            int N = Convert.ToInt32(NM[0]);
            int M = Convert.ToInt32(NM[1]);

            var st = new List<int[]>();
            for (var i = 0; i < M; i++)
            {
                var _st = Console.ReadLine().Split(' ');
                st.Add(new int[] { Convert.ToInt32(_st[0]), Convert.ToInt32(_st[1]) });
            }

            //---
            var XX = new Dictionary<int, List<int>>();// 1 to K min
            var YY = new Dictionary<int, List<int>>();// K to N min

            st.Select((p, index) => (p, index)).Where(q => q.p[0] == 1).ToList().ForEach(q =>
            {
                XX.Add(q.p[1], new List<int> { q.index });
            });

            for (var i = 1; i < M; i++)
            {
                var nextXX = XX.Where(r => r.Value.Count() == i).ToList();

                nextXX.ForEach(p =>
                {
                    st.Select((q, index) => (q, index)).Where(r => r.q[0] == p.Key).ToList().ForEach(s =>
                    {
                        if (!XX.ContainsKey(s.q[1]))
                        {
                            var indexAry = new List<int>();
                            indexAry.AddRange(p.Value);
                            indexAry.Add(s.index);
                            XX.Add(s.q[1], indexAry);
                        }
                    });
                });

                if (XX.Count() == M - 1)
                    break;
            }


            st.Select((p, index) => (p, index)).Where(q => q.p[1] == N).ToList().ForEach(q =>
            {
                YY.Add(q.p[0], new List<int> { q.index });
            });

            for (var i = 1; i < M; i++)
            {
                var nextYY = YY.Where(r => r.Value.Count() == i).ToList();

                nextYY.ForEach(p =>
                {
                    st.Select((q, index) => (q, index)).Where(r => r.q[1] == p.Key).ToList().ForEach(s =>
                    {
                        if (!YY.ContainsKey(s.q[0]))
                        {
                            var indexAry = new List<int>();
                            indexAry.AddRange(p.Value);
                            indexAry.Add(s.index);
                            YY.Add(s.q[0], indexAry);
                        }
                    });
                });

                if (YY.Count() == M - 1)
                    break;
            }


            for (var i = 0; i < M; i++)
            {
                //最短ルート
                var t = XX.Where(p => p.Key == N && !p.Value.Contains(i));
                if (t.Count() > 0)
                {
                    Console.WriteLine(t.First().Value.Count());
                    continue;
                }

                //そのルートが使えない場合
                //{1 to K} + {K to N}
                int Ret = N + 1;
                for (var j = 1; j < M; j++)
                {
                    XX.TryGetValue(j, out var cXX);
                    YY.TryGetValue(j, out var cYY);

                    if (cXX == null || cYY == null || cXX.Contains(i) || cYY.Contains(i))
                        continue;
                    Ret = Math.Min(Ret, cXX.Count() + cYY.Count());
                }

                Console.WriteLine(Ret == N + 1 ? -1 : Ret);
            }

            return;
        }
    }
}
