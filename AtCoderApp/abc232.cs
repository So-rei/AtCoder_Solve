//2021-12-19
//abc 232

//a,b,d 700点
//c途中
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoderApp
{

    //ac
    public class abc232_a
    {
        public abc232_a()
        {
            //input-------------
            var S = In.Read<string>();
            //output------------

            var r = S.Split('x');
            Out.Write((int)(Convert.ToInt32(r[0]) * Convert.ToInt32(r[1])));
        }
    }

    //ac
    public class abc232_b
    {
        public abc232_b()
        {
            //input-------------
            var S = In.Read<string>();
            var T = In.Read<string>();

            //output------------
            int r0 = 0;

            for (int i = 0; i < S.Length; i++)
            {
                int rx = Convert.ToByte(S[i]) - Convert.ToByte(T[i]);
                if (rx < 0) rx += 26;//to alphabet roop
                if (i == 0)
                    r0 = rx;
                else
                {
                    if (r0 != rx)
                    {
                        Out.Write("No");
                        return;
                    }
                }
            }

            Out.Write("Yes");

            //----------------------------------------

        }
    }


    //2021-12-19
    //時間切れで解けず　練習でとく
    public class abc232_c
    {
        public abc232_c()
        {
            //input-------------
            var NM = In.ReadAry<int>().ToArray();
            var (N, M) = (NM[0], NM[1]);
            var AB = new (int A, int B)[M];
            var CD = new (int C, int D)[M];
            for (int i = 0; i < M; i++)
            {
                var r = In.ReadAry<int>().ToArray();
                AB[i] = (r[0], r[1]);
            }
            for (int i = 0; i < M; i++)
            {
                var r = In.ReadAry<int>().ToArray();
                CD[i] = (r[0], r[1]);
            }

            //output------------
            //8!しないための計算節約
            //ソートしといたほうが後が早い?

            var XconList = new Dictionary<int, List<int>>();//ボールNo,結合先List
            var YconList = new Dictionary<int, List<int>>();//ボールNo,結合先List
            for (var i = 1; i <= N; i++)
            {
                var Xi = AB.Where(p => p.A == i).Select(p => p.B).ToList();
                Xi.AddRange(AB.Where(p => p.B == i).Select(p => p.A));
                XconList.Add(i, Xi);

                var Yi = CD.Where(p => p.C == i).Select(p => p.D).ToList();
                Yi.AddRange(CD.Where(p => p.D == i).Select(p => p.C));
                YconList.Add(i, Yi);
            }

            var xcon = XconList.OrderBy(p => p.Value.Count()).ToList();
            var ycon = YconList.OrderBy(p => p.Value.Count()).ToList();
            for (int i = 0; i < N; i++)
            {
                if (xcon[i].Value.Count() != ycon[i].Value.Count())
                {
                    Out.Write("No");//結合数が不一致ならNo
                    return;
                }
            }

            //8!しないようにマッピング探索
            //var mapp = new List<int[]>();
            //mapp = PatternMake();
            //for (int i = 0; i < N; i++)
            //{
            //    mapp[i] = 
            //}



            Out.Write("Yes");

            //----------------------------------------
            (int[] X, int[] Y) ChgAry(int[] X0, int[] Y0, int a, int b)
            {
                var X = new int[M];
                var Y = new int[M];
                for (int i = 0; i < M; i++)
                {
                    if (X0[i] == a)
                    {
                        X[i] = Math.Min(Y0[i], b);
                        Y[i] = Math.Max(Y0[i], b);
                    }
                    else if (Y0[i] == a)
                    {
                        X[i] = Math.Min(X0[i], b);
                        Y[i] = Math.Max(X0[i], b);
                    }
                    else
                    {
                        X[i] = X0[i];
                        Y[i] = Y0[i];
                    }
                }
                return (X, Y);
            }
        }
    }


    //2021-12-19
    //mappedなしだと3TLE/全20
    public class abc232_dx
    {
        public abc232_dx()
        {
            //input-------------
            var HW = In.ReadAry<int>().ToArray();
            var (H, W) = (HW[0], HW[1]);
            var C = new string[H];
            for (int i = 0; i < H; i++)
            {
                var r = In.Read<string>();
                C[i] = r;
            }

            //output------------
            var mapped = new Dictionary<int[], int>();
            int cnt = DP_grid(0, 0, 0);//移動できるマス
            Out.Write(cnt);

            //----------------------------------------
            int DP_grid(int tmp, int i, int j)
            {
                if (i < 0 || j < 0 || i >= H || j >= W) return tmp;
                if (C[i][j] == '#') return tmp;


                int x = 0, y = 0;
                if (!mapped.ContainsKey(new int[] { i + 1, j }))
                    x = DP_grid(tmp + 1, i + 1, j);
                else
                    x = mapped[new int[] { i + 1, j }];

                if (!mapped.ContainsKey(new int[] { i, j + 1 }))
                    y = DP_grid(tmp + 1, i, j + 1);
                else
                    y = mapped[new int[] { i, j + 1 }];
                //他の方向はない

                return Math.Max(x, y);
            }

        }
    }

    //AC
    public class abc232_d
    {
        public abc232_d()
        {
            //input-------------
            var HW = In.ReadAry<int>().ToArray();
            var (H, W) = (HW[0], HW[1]);
            var C = new string[H];
            for (int i = 0; i < H; i++)
            {
                var r = In.Read<string>();
                C[i] = r;
            }

            //output------------
            var mapped = new List<int[]>();
            int cnt = DP_grid(0, 0, 0);//移動できるマス
            Out.Write(cnt);

            //----------------------------------------
            int DP_grid(int tmp, int i, int j)
            {
                if (i < 0 || j < 0 || i >= H || j >= W) return tmp;
                if (C[i][j] == '#') return tmp;


                int x = 0, y = 0;
                var r = mapped.Where(p => p[0] == i + 1 && p[1] == j);
                if (r.Count() == 0)
                {
                    x = DP_grid(tmp + 1, i + 1, j);
                    mapped.Add(new int[] { i + 1, j, x });
                }
                else
                    x = r.First()[2];

                var r2 = mapped.Where(p => p[0] == i && p[1] == j + 1);
                if (r2.Count() == 0)
                {
                    y = DP_grid(tmp + 1, i, j + 1);
                    mapped.Add(new int[] { i, j + 1, y });
                }
                else
                    y = r2.First()[2];
                //他の方向はない

                return Math.Max(x, y);
            }

        }
    }
}