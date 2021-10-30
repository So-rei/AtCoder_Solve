using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoderApp
{
    //    class Program
    //    {
    //        static void Main(string[] args)
    //        {
    //            new abc225_a();
    //        }
    //    }

    public class abc225_a
    {
        public abc225_a()
        {
            //input-------------
            var S = In.Read<string>();

            //calc--------------
            if (S[0] == S[1] && S[0] == S[2])
                Out.Write(1);
            else if (S[0] == S[1] || S[0] == S[2] || S[1] == S[2])
                Out.Write(3);
            else
                Out.Write(6);
        }
    }
    public class abc225_b
    {
        public abc225_b()
        {
            //input-------------
            var N = In.Read<int>();
            var ab = new (int a, int b)[N - 1];
            for (int i = 0; i < N - 1; i++)
            {
                var _ab = In.ReadAry<int>().ToArray();
                ab[i].a = _ab[0];
                ab[i].b = _ab[1];
            }

            //calc--------------
            int StarP = 0;

            bool chk1 = ab[0].a == ab[1].a || ab[0].a == ab[1].b;
            bool chk2 = ab[0].b == ab[1].a || ab[0].b == ab[1].b;
            if (chk1 && chk2)
            {
                Out.Write("No");
                return;
            }

            if (chk1)
                StarP = ab[0].a;
            else if (chk2)
                StarP = ab[0].b;
            else
            {
                Out.Write("No");
                return;
            }

            for (int x = 2; x < N - 1; x++)
            {
                if (ab[x].a != StarP && ab[x].b != StarP)
                {
                    Out.Write("No");
                    return;
                }
            }
            Out.Write("Yes");
        }
    }

    public class abc225_c
    {
        public abc225_c()
        {
            //input-------------
            var NM = In.ReadAry<int>().ToArray();
            int N = NM[0];
            int M = NM[1];

            var B = new int[N][];
            for (int i = 0; i < N; i++)
            {
                var _b = In.ReadAry<int>().ToArray();
                B[i] = _b;
            }

            //calc--------------

            //開始位置チェック(7列)
            if ((B[0][0] % 7 == 0 && M > 1) ||
                (B[0][0] % 7 + M > 8))
            {
                Out.Write("No");
                return;
            }

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    if (B[0][0] + i * 7 + j != B[i][j])
                    {
                        Out.Write("No");
                        return;
                    }
                }
            }

            Out.Write("Yes");
        }
    }

    //TLE *12 OK*13
    public class abc225_d
    {
        public abc225_d()
        {
            //input-------------
            var NQ = In.ReadAry<int>().ToArray();
            int N = NQ[0];
            int Q = NQ[1];

            var query = new (int q, int x, int y)[Q];
            for (int i = 0; i < Q; i++)
            {
                var _q = In.ReadAry<int>().ToArray();
                query[i].q = _q[0];
                query[i].x = _q[1];
                if (_q[0] < 3)
                    query[i].y = _q[2];
            }

            //calc--------------
            var grp = new List<List<int>>();
            for (int i = 0; i < Q; i++)
            {
                switch (query[i].q)
                {
                    case 1: //連結
                        int xindex = -1, yindex = -1;
                        for (int c = 0; c < grp.Count(); c++)
                        {
                            if (grp[c].Last() == query[i].x)
                                xindex = c;
                            if (grp[c].First() == query[i].y)
                                yindex = c;

                            if (xindex > 0 && yindex > 0) break;
                        }

                        //両方連結データにある
                        if (xindex > -1 && yindex > -1)
                        {
                            grp[xindex].AddRange(grp[yindex]);
                            grp.RemoveAt(yindex);
                        }
                        else if (xindex > -1) //xがある
                            grp[xindex].Add(query[i].y);
                        else if (yindex > -1) //yがある
                            grp[yindex].Insert(0, query[i].x);
                        else
                            //無い時は新規
                            grp.Add(new List<int> { query[i].x, query[i].y });

                        break;

                    case 2: //分割
                        for (int x = 0; x < grp.Count(); x++)
                        {
                            var index = grp[x].IndexOf(query[i].x);
                            if (index < 0) continue;

                            //データ分割
                            var bottom = grp[x].Skip(index + 1).ToList();

                            if (index == 0)
                                grp.RemoveAt(x);//計算量削減用(長さ1を削除する)
                            else
                                grp[x] = grp[x].Take(index + 1).ToList();

                            if (bottom.Count > 1)//計算量削減用(長さ1は不要)
                                grp.Add(bottom);

                            break;
                        }
                        break;

                    case 3:
                        bool bNotOut = true;
                        foreach (var g in grp)
                        {
                            //連結データにある時は先頭から出力
                            if (g.Contains(query[i].x))
                            {
                                bNotOut = false;
                                Out.Write(g.Count().ToString() + " " + String.Join(' ', g));
                                break;
                            }
                        }

                        //無い時は単独
                        if (bNotOut)
                            Out.Write("1 " + query[i].x);
                        break;
                }
            }
        }
    }
}
