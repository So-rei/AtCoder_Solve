using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AtCoderApp
{
    //AC
    public class abc251_a
    {
        public abc251_a()
        {
            //input-------------
            var S = In.Read<string>();
            //output------------
            var R = S;
            while (R.Length < 6)
                R += S;

            Out.Write(R);
        }
    }

    //TLE4　Contains...を毎回やるのは遅い
    public class abc251_b
    {
        public abc251_b()
        {
            //input-------------
            (var N, var W) = In.ReadTuple2<int>();
            var A = In.ReadAry<int>().ToArray();

            //output------------
            var asort = A.OrderBy(p => p).ToArray();
            var plist = new List<int>();
            for (int i = 0; i < N; i++)
            {
                //おもり1個選択時
                var x1 = asort[i];
                if (x1 > W) break;
                if (!plist.Contains(x1)) plist.Add(x1);

                for (int j = i + 1; j < N; j++)
                {
                    //おもり2個選択時
                    var x2 = x1 + asort[j];
                    if (x2 > W) break;
                    if (!plist.Contains(x2)) plist.Add(x2);

                    for (int k = j + 1; k < N; k++)
                    {
                        //重り3個
                        var x3 = x2 + asort[k];
                        if (x3 > W) break;
                        if (!plist.Contains(x3)) plist.Add(x3);
                    }
                }
            }

            Out.Write(plist.Count());
        }
    }

    //AC
    public class abc251_b2
    {
        public abc251_b2()
        {
            //input-------------
            (var N, var W) = In.ReadTuple2<int>();
            var A = In.ReadAry<int>().ToArray();

            //output------------
            var asort = A.Where(p => p < W).OrderBy(p => p).ToArray(); //W以上は先に切っとく
            var Wx = new int[W + 1]; //listチェックすると重い
            for (int i = 0; i < asort.Count(); i++)
            {
                //おもり1個選択時
                var x1 = asort[i];
                //if (x1 > W) break;
                Wx[x1]++;

                for (int j = i + 1; j < asort.Count(); j++)
                {
                    //おもり2個選択時
                    var x2 = x1 + asort[j];
                    if (x2 > W) break;
                    Wx[x2]++;

                    for (int k = j + 1; k < asort.Count(); k++)
                    {
                        //重り3個
                        var x3 = x2 + asort[k];
                        if (x3 > W) break;
                        Wx[x3]++;
                    }
                }
            }

            Out.Write(Wx.Count(p => p > 0));
        }
    }


    //TLE1 ?!!
    public class abc251_c
    {
        public abc251_c()
        {
            //input-------------
            var N = In.Read<int>();
            var S = new List<string>();
            var T = new List<int>();
            for (int i = 0; i < N; i++)
            {
                var _st = In.ReadAry<string>().ToArray();
                S.Add(_st[0]);
                T.Add(Convert.ToInt32(_st[1]));
            }

            //output------------
            var SDi = S.Distinct().ToList();
            var score = 0; var Index = 0;
            for (int i = 0; i < N; i++)
            {
                if (!SDi.Contains(S[i])) continue;
                SDi.Remove(S[i]);
                if (score < T[i]) //更新するときだけ
                {
                    score = T[i];
                    Index = i + 1;
                }
            }

            Out.Write(Index);
        }
    }

    //TLE1　改善せず
    public class abc251_c2
    {
        public abc251_c2()
        {
            //input-------------
            var N = In.Read<int>();
            var S = new string[N];
            var T = new int[N];
            for (int i = 0; i < N; i++)
            {
                var _st = In.ReadAry<string>().ToArray();
                S[i] = _st[0];
                T[i] = Convert.ToInt32(_st[1]);
            }

            //output------------
            var SDi = S.Distinct().ToList();
            var score = 0;
            var Index = 0;
            for (int i = 0; i < N; i++)
            {
                if (!SDi.Contains(S[i])) continue;
                SDi.Remove(S[i]); //重複無視
                if (score < T[i]) //更新するときだけ
                {
                    score = T[i];
                    Index = i + 1;
                }
            }

            Out.Write(Index);
        }
    }

    //TLE4
    public class abc251_c3
    {
        public abc251_c3()
        {
            //input-------------
            var N = In.Read<int>();
            var S = new string[N];
            var T = new int[N];
            for (int i = 0; i < N; i++)
            {
                var _st = In.ReadAry<string>().ToArray();
                if (!S.Contains(_st[0]))
                {
                    S[i] = _st[0];
                    T[i] = Convert.ToInt32(_st[1]);
                }
            }

            //output------------
            var max = T.Max();
            var R = T.Select((p, index) => (p, index)).First(p => p.p == max).index + 1;
            Out.Write(R);
        }
    }

    //TLE4 だめだコレでも遅い
    public class abc251_c4
    {
        public abc251_c4()
        {
            //input-------------
            var N = In.Read<int>();
            var S = new string[N];
            var TI = new List<(int T, int I)>();
            for (int i = 0; i < N; i++)
            {
                var _st = In.ReadAry<string>().ToArray();
                if (!S.Contains(_st[0]))
                {
                    S[i] = _st[0];
                    TI.Add((Convert.ToInt32(_st[1]), i + 1));
                }
            }

            //output------------
            Out.Write(TI.OrderByDescending(p => p.T).ThenBy(p => p.I).First().I);
        }
    }

    //AC 回答見てabc251_c4をDictionaryに変えただけ。
    //Dictionaryだと検索が高速になることに留意！！
    public class abc251_c5
    {
        public abc251_c5()
        {
            //input-------------
            var N = In.Read<int>();
            var S = new Dictionary<string, int>();
            var TI = new List<(int T, int I)>();
            for (int i = 0; i < N; i++)
            {
                var _st = In.ReadAry<string>().ToArray();
                if (!S.ContainsKey(_st[0]))
                {
                    S.Add(_st[0], i);
                    TI.Add((Convert.ToInt32(_st[1]), i + 1));
                }
            }

            //output------------
            Out.Write(TI.OrderByDescending(p => p.T).ThenBy(p => p.I).First().I);
        }
    }

    //こんなゴリ押しではWA3
    //アルゴリズムをちゃんと考えないとだめ
    public class abc251_d
    {
        public abc251_d()
        {
            //input-------------
            var W = In.Read<int>();

            //output------------
            var r = new int[300];
            //1-10,残りは19以上..sum...
            for (int i = 0; i < 10; i++)
            {
                r[i] = i + 1;
            }
            for (int i = 10; i < 300; i++)
            {
                r[i] = (int)Math.Ceiling((r[i - 3] + r[i - 2] + r[i - 1]) / 2.7) + 1;
                if (r[i] > 1000000) r[i] = 15;
            }

            Out.Write(300);
            Out.WriteMany(r);
        }
    }
}