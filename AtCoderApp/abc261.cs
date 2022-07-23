using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AtCoderApp
{
    //雑に作って1回WAしてしまった…(コメントの部分)
    //これでAC
    public class abc261_a
    {
        public abc261_a()
        {
            //input-------------
            (var L, var R, var L2, var R2) = In.ReadTuple4<int>();

            //output------------
            //if (L <= L2)
            //    Out.Write(R - L2 < 0 ? 0 : R - L2);
            //else
            //    Out.Write(R2 - L < 0 ? 0 : R2 - L);

            int LL = Math.Max(L, L2);
            int RR = Math.Min(R, R2);
            Out.Write(RR - LL < 0 ? 0 : RR - LL);
        }
    }

    //AC
    public class abc261_b
    {
        public abc261_b()
        {
            //input-------------
            int N = In.Read<int>();
            var A = In.ReadMany<string>(N).ToArray();

            //output------------
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if ((A[i][j] == 'W' && A[j][i] != 'L') ||
                        (A[i][j] == 'L' && A[j][i] != 'W') ||
                        (A[i][j] == 'D' && A[j][i] != 'D') ||
                        (A[i][j] != 'D' && A[j][i] == 'D'))
                    {
                        Out.Write("incorrect");
                        return;
                    }
                }
            }

            Out.Write("correct");
        }
    }

    //AC
    public class abc261_c
    {
        public abc261_c()
        {
            //input-------------
            int N = In.Read<int>();
            var S = In.ReadMany<string>(N).ToArray();

            //output------------
            var Sdic = new Dictionary<string, int>();
            for (int i = 0; i < N; i++)
            {
                if (!Sdic.ContainsKey(S[i]))
                    Sdic.Add(S[i], 0);
                else
                    Sdic[S[i]]++;

                if (Sdic[S[i]] == 0)
                    Out.Write(S[i]);
                else
                    Out.Write(S[i] + "(" + Sdic[S[i]].ToString() + ")");
            }
            return;
        }
    }

    //1発AC！
    public class abc261_d
    {
        public abc261_d()
        {
            //input-------------
            (int N, int M) = In.ReadTuple2<int>();
            var X = In.ReadAry<int>().ToArray();
            var CY = new Dictionary<int, int>(); //条件より重複しないことがわかっている
            for (int i = 0; i < M; i++)
            {
                var _cy = In.ReadAry<int>().ToArray();
                CY.Add(_cy[0], _cy[1]);
            }

            //output------------
            //DP i回目、カウンタcのとき、もらえる最大金額dp
            var DP = new long[N, N + 1];
            DP[0, 1] = X[0] + (CY.ContainsKey(1) ? CY[1] : 0); //1回目
                                                               //二回目以降
            for (int i = 1; i < N; i++)
                MainDP(i);

            long rMax = 0; //N回終わったあとの最大値が答え
            for (int i = 0; i <= N; i++)
                rMax = Math.Max(DP[N - 1, i], rMax);
            Out.Write(rMax);

            //-----
            void MainDP(int i)
            {
                for (int c = 1; c <= i + 1; c++)
                    DP[i, c] = DP[i - 1, c - 1] + X[i] + (CY.ContainsKey(c) ? CY[c] : 0); //表

                //裏は全部消えるので1回だけでいい
                long rMax = 0;
                for (int cc = 0; cc <= i; cc++)
                    rMax = Math.Max(DP[i - 1, cc], rMax);
                DP[i, 0] = rMax;
            }
        }
    }

    //AC4 TLE19
    //これだとN*N/2回(*30bit)のor/xor/and計算が必要になってしまう
    public class abc261_e
    {
        public abc261_e()
        {
            //input-------------
            (int N, int C) = In.ReadTuple2<int>();
            var TA = new List<(int T, int A)>();
            for (int i = 0; i < N; i++)
                TA.Add(In.ReadTuple2<int>());

            //output------------
            int x = C;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    switch (TA[j].T)
                    {
                        case 1:
                            x &= TA[j].A;
                            break;
                        case 2:
                            x |= TA[j].A;
                            break;
                        case 3:
                            x ^= TA[j].A;
                            break;
                    }
                }
                Out.Write(x);
            }
        }
    }

    //解説のC++の回答をそのまま写経
    public class abc261_e2
    {
        public abc261_e2()
        {
            //input-------------
            (int N, int C) = In.ReadTuple2<int>();
            var TA = new List<(int T, int A)>();
            for (int i = 0; i < N; i++)
                TA.Add(In.ReadTuple2<int>());

            //output------------
            var ans = new int[N];//i回目の計算結果
            for (int k = 0; k < 30; k++) //問題より2^30まで
            {
                var func = new int[2] { 0, 1 }; //初期関数func 0→0 1→1
                int crr = (C >> k) & 1; //初期値=Cのk桁目
                for (int i = 0; i < N; i++)
                {
                    var f = new int[2];
                    int x = (TA[i].A >> k) & 1; //k桁目の値が1か0か
                    if (TA[i].T == 1) f = new int[] { 0 & x, 1 & x };
                    if (TA[i].T == 2) f = new int[] { 0 | x, 1 | x };
                    if (TA[i].T == 3) f = new int[] { 0 ^ x, 1 ^ x };
                    func = new int[] { f[func[0]], f[func[1]] }; //関数を合成　合成することで、N回目にN回の計算の演算しないといけないところが1回になる
                    crr = func[crr];
                    ans[i] |= crr << k; //１つ前の値(bit)*合成関数()=現在値(bit) →数字に変換して結果に足していく
                }
            }

            Out.WriteMany(ans);
        }
    }
}