using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtCoderApp
{
    //ARC140
    //2022-05-15

    //AC (WA１回、修正後)
    public class ARC140_a2
    {
        public ARC140_a2()
        {
            //input-------------
            (var N, var K) = In.ReadTuple2<int>();
            var S = In.Read<string>();

            //output------------

            //aaaa..とできるなら解は1
            if (K >= N - 1)
            {
                Out.Write(1);
                return;
            }

            //解がN未満になるケースを考える
            //abab...とできるなら解は2
            //abcabc...とできるなら3                
            //解=N/2になるまで見る
            for (int i = 1; i <= (int)(N / 2) + 1; i++)
            {
                if (CanSplit(S, i))
                {
                    Out.Write(i);
                    return;
                }
            }

            //短絡が無理な場合は解N。これ書くの忘れてた！***
            Out.Write(N);


            bool CanSplit(string s, int split)
            {
                if (N % split != 0) return false;

                var sAry = new List<string>();
                for (int i = 0; i < N / split; i++)
                {
                    sAry.Add(s.Substring(i * split, split));
                }

                //(N/split)個の同じ文字の繰り返し、にするために最短修正回数cnt
                int cnt = 0;
                for (int i = 0; i < split; i++)
                {
                    cnt += (N / split) - sAry.Select(p => p[i]).GroupBy(p => p).Select(p => p.Count()).Max();
                    if (cnt > K) return false;
                }

                return true;
            }
        }
    }

    //TLE
    public class ARC140_b
    {
        public ARC140_b()
        {
            //input-------------
            var N = In.Read<int>();
            var S = In.Read<string>();

            //output------------
            var ret = Next(S, 1);
            Out.Write(ret);
            return;

            int Next(string s, int t)
            {
                var rr = 0;

                for (int i = 0; i < s.Length - 2; i++)
                {
                    if (s[i].ToString() + s[i + 1].ToString() + s[i + 2].ToString() == "ARC")
                    {
                        var stmp = "";
                        if (t % 2 == 0)
                            stmp = (s.Substring(0, i) + "AC" + s.Substring(i + 3));
                        else
                            stmp = (s.Substring(0, i) + "R" + s.Substring(i + 3));

                        rr = Math.Max(rr, 1 + Next(stmp, t + 1));
                    }
                }

                return rr;
            }
        }
    }

    //ACだああああああああああああああああああ
    public class ARC140_c
    {
        public ARC140_c()
        {
            //input-------------
            (var N, var X) = In.ReadTuple2<int>();

            //output------------

            var ret = new int[N];
            ret[0] = X;
            if ((N % 2 == 1 && X == (int)(N / 2) + 1) ||
                X == (int)(N / 2) + 1)
            {
                //X=真ん中を選んだときのみ?max = N - 1
                //ex) (5,3) のとき、3 2 4 1 5と選ぶ。
                for (int i = 1; i < N; i++)
                {
                    if (i % 2 == 1)
                        ret[i] = ret[i - 1] - i;
                    else
                        ret[i] = ret[i - 1] + i;
                }
            }
            else if (N % 2 == 0 && X == (int)(N / 2))
            {
                //X=真ん中を選んだときのみ?max = N - 1
                //ex) (4,2)のとき 2 3 1 4と選ぶ。-,+,-...上のとは順番逆(2の次が1ではダメ)のケース。
                for (int i = 1; i < N; i++)
                {
                    if (i % 2 == 1)
                        ret[i] = ret[i - 1] + i;
                    else
                        ret[i] = ret[i - 1] - i;
                }
            }
            else
            {
                //それ以外のときはmax=N-2
                //先頭無視して次からきれいに並べる

                var tmp = new int[N - 1];//並べるためのXを無視した配列
                var p = 0;
                for (int i = 0; i < N - 1; i++)
                {
                    if (i + 1 == X)
                        p++;

                    tmp[i] = p + 1;
                    p++;
                }
                ret[1] = tmp[(N - 1) / 2];

                //ex(6,2) 2を無視して、残り{1,3,4,5,6}を 4 3 5 1 6と並べる(残り部分のP={1,2,4,5})
                for (int i = 2; i < N; i++)
                {
                    if (i % 2 == 0)
                        ret[i] = tmp[(N - 1) / 2 - i / 2];
                    else
                        ret[i] = tmp[(N - 1) / 2 + i / 2];
                }
            }

            Out.WriteMany(ret);
        }
    }
}