using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AtCoderApp
{
    //abc252 2022-05-22
    //a,b,c 600点

    //AC
    public class abc252_a
    {
        public abc252_a()
        {
            //input-------------
            var N = In.Read<int>();

            //output------------
            var c = Convert.ToChar(N);
            Out.Write(c.ToString());
        }
    }

    //AC
    public class abc252_b
    {
        public abc252_b()
        {
            //input-------------
            (var N, var K) = In.ReadTuple2<int>();
            var A = In.ReadAry<int>().ToArray();
            var B = In.ReadAry<int>().ToArray();

            //output------------
            var amax = A.Max();
            for (int i = 0; i < N; i++)
            {
                if (A[i] == amax && B.Contains(i + 1))
                {
                    Out.Write("Yes");
                    return;
                }
            }

            Out.Write("No");
        }
    }

    //AC
    public class abc252_c
    {
        public abc252_c()
        {
            //input-------------
            var N = In.Read<int>();
            var S = In.ReadMany<string>(N).ToArray();

            //output------------
            var rx = new int[10, 10];//{0が１文字目、２文字目・・},{1が..}
            foreach (var s in S)
            {
                for (int i = 0; i < 10; i++)
                {
                    rx[Convert.ToInt32(s[i].ToString()), i]++;
                }
            }

            //最小計算
            //ex)0が２個、３が１個　-> min = 0+3+7 = 10
            var minR = int.MaxValue;
            for (int i = 0; i < 10; i++)
            {
                var cnt = 0;
                var tmpmin = 0;
                var before = 0;
                while (cnt < N)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (rx[i, j] > 0)
                        {
                            tmpmin += j - before;
                            before = j;
                            rx[i, j]--;
                            cnt++;
                        }
                    }
                    before = before - 10;//次回繰り上がり
                }
                minR = Math.Min(minR, tmpmin);
            }

            Out.Write(minR);
        }
    }

    //TLE　やっぱゴリ押しだとダメ..
    public class abc252_d
    {
        public abc252_d()
        {
            //input-------------
            var N = In.Read<int>();
            var A = In.ReadAry<int>().ToArray();

            //output------------
            var adic = new Dictionary<int, int>();
            for (int i = 0; i < N; i++)
            {
                adic.Add(i, A[i]);
            }

            //i,j,*
            var ret = 0;
            for (int x = 0; x < N - 2; x++)
            {
                var adx = adic.Where(p => p.Key > x && p.Value != A[x]).ToDictionary(x => x.Key, x => x.Value);
                //j,k
                while (adx.Count() > 1)
                {
                    var yv = adx.First().Value;
                    adx.Remove(adx.First().Key);
                    ret += adx.Count(p => p.Value != yv);
                }
            }
            Out.Write(ret);
        }
    }

    //解説見て作成 AC
    //longに気をつけること
    public class abc252_d2
    {
        public abc252_d2()
        {
            //input-------------
            var N = In.Read<long>();
            var A = In.ReadAry<int>().ToList();

            //output------------
            //i,j,k
            long r = (long)(N * (N - 1) * (N - 2) / 6); //溢れるのでlong!
            var dup = A.GroupBy(p => p).Where(q => q.Count() > 1).Select(q => q.Count()); //この時点ではint

            foreach (long d in dup) //dをlongにしておかないと途中で溢れる可能性あり!
            {
                if (d >= 2)
                    r -= d * (d - 1) * (N - d) / 2;//2つ同じ * 残りの数字 dC2 * (N-d)
                if (d > 2)
                    r -= d * (d - 1) * (d - 2) / 6; //3つ同じ dC3
            }

            Out.Write(r);
        }
    }

    //E 考え方はわかったのだが、最短経路を出す方法が..
    public class abc252_e
    {
        public abc252_e()
        {
            //input-------------
            (var N, var M) = In.ReadTuple2<int>();
            var ABC = new List<(int A, int B, int C)>();
            for (int i = 0; i < M; i++)
                ABC.Add(In.ReadTuple3<int>());

            //output------------
            //ABC = ABC.OrderBy(p => p.A).ThenBy(q => q.B).ToList();

            var tr = new Dictionary<int, int[]>();//i,{最短距離,１からiへのその最短ルートを通った時、最後に通ったルートNo}
            for (int i = 2; i <= N; i++)
                tr.Add(i, new int[] { int.MaxValue, -1 });

            var abd = new int[N, N, 2];//a,b,{aとbの最短距離d,最後に通ったルートNo}
            for (int i = 0; i < M; i++)
            {
                (var a, var b) = (ABC[i].A, ABC[i].B);
                //Refresh(a, b);
            }

            for (int i = 0; i < M; i++)
            {
                if (ABC[i].A == 1)
                {
                    //1-B√について
                    if (tr[ABC[i].B][0] > ABC[i].C)
                    {
                        tr[ABC[i].B][0] = ABC[i].C;//最短距離d
                        tr[ABC[i].B][1] = i + 1; //ルートNo
                                                 //さらに1-B-C、1-B-C-D...などを確認しないといけないが、、
                    }
                }
                else
                {
                    //A-B√について

                    //1->Bの値と今回1->A->Bを比較
                    if (tr[ABC[i].B][0] > tr[ABC[i].A][0] + ABC[i].C)
                    {
                        tr[ABC[i].B][0] = tr[ABC[i].A][0] + ABC[i].C;
                        tr[ABC[i].B][1] = i + 1;
                        //さらに1-A-B-C、1-A-B-C-D...などを確認しないといけないが、、
                    }

                    //1->Aの値と今回1->B->Aを比較
                    if (tr[ABC[i].A][0] > tr[ABC[i].B][0] + ABC[i].C)
                    {
                        tr[ABC[i].A][0] = tr[ABC[i].B][0] + ABC[i].C;
                        tr[ABC[i].A][1] = i + 1;
                    }
                }
            }

            var ret = tr.Select(p => p.Value[1]);
            Out.WriteMany(ret);
        }
    }
}