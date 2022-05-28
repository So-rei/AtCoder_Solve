using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AtCoderApp
{
    //AC
    public class abc253_a
    {
        public abc253_a()
        {
            //input-------------
            var abc = In.ReadAry<int>().ToArray();

            //output------------
            if (abc.OrderBy(p => p).ToArray()[1] == abc[1])
                Out.Write("Yes");
            else
                Out.Write("No");
        }
    }

    //AC
    public class abc253_b
    {
        public abc253_b()
        {
            //input-------------
            (var H, var W) = In.ReadTuple2<int>();
            var S = In.ReadMany<string>(H).ToArray();

            //output------------
            int tx = -1;
            int ty = -1;
            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < W; j++)
                {
                    if (S[i][j] == 'o')
                    {
                        if (tx == -1) //1個目のoを記憶
                        {
                            tx = i;
                            ty = j;
                        }
                        else //2個目を発見
                        {
                            Out.Write(Math.Abs(i - tx) + Math.Abs(j - ty));
                            return;
                        }
                    }
                }
            }
        }
    }

    //全部配列用意してるとOut Of Memory?
    public class abc253_c
    {
        public abc253_c()
        {
            //input-------------
            var Q = In.Read<int>();
            var query = In.ReadManyAry<int>(Q).ToArray();

            //output------------
            int[] S = new int[1_000_000_001];
            var SList = new List<int>();
            int max = 0;
            int min = int.MaxValue;

            for (int i = 0; i < Q; i++)
            {
                switch (query[i][0])
                {
                    case 1:
                        if (S[query[i][1]] == 0)
                            SList.Add(query[i][1]);

                        S[query[i][1]]++;
                        max = Math.Max(max, query[i][1]);
                        min = Math.Min(min, query[i][1]);
                        break;

                    case 2:
                        if (query[i][2] >= S[query[i][1]])
                        {
                            S[query[i][1]] = 0;
                            SList.Remove(query[i][1]);
                            if (max == query[i][1]) //max更新
                                max = SList.Max();
                            //max = S.Select((p, index) => (p, index)).Where(q => q.p > 0).Max(r => r.index);
                            if (min == query[i][1]) //max更新
                                min = SList.Min();
                            //min = S.Select((p, index) => (p, index)).Where(q => q.p > 0).Min(r => r.index);
                        }
                        else
                            S[query[i][1]] -= query[i][2];
                        break;

                    case 3:
                        Out.Write(max - min);
                        break;
                }
            }

        }
    }

    //REがいくつか..
    public class abc253_c2
    {
        public abc253_c2()
        {
            //input-------------
            var Q = In.Read<int>();


            //output------------
            var S = new Dictionary<int, int>();
            int max = 0;
            int min = int.MaxValue;

            for (int q = 0; q < Q; q++)
            {
                var query = In.ReadAry<int>().ToArray();

                switch (query[0])
                {
                    case 1:
                        var x = query[1];

                        if (S.ContainsKey(x))
                            S[x]++;
                        else
                        {
                            S.Add(x, 1);
                            max = Math.Max(max, query[1]);
                            min = Math.Min(min, query[1]);
                        }
                        break;

                    case 2:
                        var xx = query[1];
                        var c = query[2];
                        if (c >= S[xx])
                        {
                            S.Remove(xx);

                            if (max == xx) //max更新
                                max = S.Max(p => p.Key);
                            if (min == xx) //max更新
                                min = S.Min(p => p.Key);
                        }
                        else
                            S[xx] -= c;
                        break;

                    case 3:
                        Out.Write(max - min);
                        break;
                }
            }
        }
    }

    //AC
    public class abc253_c3
    {
        public abc253_c3()
        {
            //input-------------
            var Q = In.Read<int>();
            var qu = In.ReadManyAry<long>(Q).ToArray();

            //output------------
            var S = new Dictionary<long, long>();
            long max = 0;
            long min = int.MaxValue;

            for (int q = 0; q < Q; q++)
            {
                var query = qu[q];

                switch (query[0])
                {
                    case 1:
                        var x = (long)query[1];

                        if (S.ContainsKey(x))
                            S[x]++;
                        else
                        {
                            S.Add(x, 1);
                            max = Math.Max(max, x);
                            min = Math.Min(min, x);
                        }
                        break;

                    case 2:
                        var xx = query[1];
                        var c = query[2];

                        //2なのにないこともある！！
                        if (!S.ContainsKey(xx)) continue;

                        if (c >= S[xx])
                        {
                            S.Remove(xx);

                            //Sの中身が空になることもある！！
                            if (S.Count() == 0)
                            {
                                max = 0;
                                min = int.MaxValue;
                                break;
                            }

                            if (max == xx) //max更新
                                max = S.Max(p => (long)p.Key);
                            if (min == xx) //max更新
                                min = S.Min(p => (long)p.Key);
                        }
                        else
                            S[xx] -= c;
                        break;

                    case 3:
                        Out.Write(max - min);
                        break;
                }
            }
        }
    }

    //Dはpythonで解いた AC
}