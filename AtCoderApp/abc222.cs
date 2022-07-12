using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoderApp
{
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        new abc222_a();
    //    }
    //}

    //20211009 ac

    public class abc222_a
    {
        public abc222_a()
        {
            //input-------------
            var N = In.Read<int>();

            //calc----

            Out.Write(N.ToString("0000"));
        }
    }

    //20211009 ac

    public class abc222_b
    {
        public abc222_b()
        {
            //input-------------
            var NP = In.ReadAry<int>().ToArray();
            var N = NP[0];
            var P = NP[1];

            var an = In.ReadAry<int>().ToArray();

            //calc----
            var ret = an.Count(s => s < P);

            Out.Write(ret);
        }
    }

    //20211009 ac

    public class abc222_c
    {
        public abc222_c()
        {
            //input-------------
            var NM = In.ReadAry<int>().ToArray();
            var N = NM[0];
            var M = NM[1];

            var GCPList = new List<char[]>();
            for (int i = 0; i < 2 * N; i++)
            {
                GCPList.Add(In.Read<string>().ToArray());
            }

            //calc----
            var nW = new List<int>();//i番目のn人目の勝ち星

            for (int i = 0; i < 2 * N; i++)
                nW.Add(0);

            for (int i = 0; i < M; i++)
            {
                var SortedAry = nW.Select((i, index) => (i, index)).OrderByDescending(p => p.i).ThenBy(q => q.index).ToArray();

                //i戦目の2x人目と2x+1人目の戦い
                for (int x = 0; x < N; x++)
                {
                    var c1 = SortedAry[2 * x];
                    var c2 = SortedAry[2 * x + 1];
                    switch (GCPList[c1.index][i], GCPList[c2.index][i])
                    {
                        case ('G', 'C'):
                        case ('C', 'P'):
                        case ('P', 'G'):
                            nW[c1.index]++;
                            break;
                        case ('G', 'P'):
                        case ('C', 'G'):
                        case ('P', 'C'):
                            nW[c2.index]++;
                            break;
                        default:
                            break;
                    }
                }
            }

            //出力のためにindex 0から　→　1からに
            var ret = nW.Select((i, index) => (i, index)).OrderByDescending(p => p.i).ThenBy(q => q.index).Select(p => p.index + 1).ToArray();

            foreach (var r in ret)
            {
                Out.Write(r);
            }
        }
    }


    //20211009
    //例は正解だがほとんどWRやTLEになる...なにか抜けてる

    public class abc222_d
    {
        public abc222_d()
        {
            //input-------------
            var N = In.Read<int>();
            var a = In.ReadAry<int>().ToArray();
            var b = In.ReadAry<int>().ToArray();

            //calc----
            long lRet = 0;
            var Ccalced = new Dictionary<(int next, int index), long>(); //途中計算分を記憶
            //初回計算のみループで2回目以降は再帰関数
            for (int i = a[0]; i < b[0] + 1; i++)
            {
                lRet += LoopCnt(i, 1);
            }

            //output----
            Out.Write((long)(lRet % 998244353));
            return;

            long LoopCnt(int bx, int index)
            {
                //計算履歴がある場合はその値
                if (Ccalced.ContainsKey((bx, index)))
                {
                    return Ccalced[(bx, index)];
                }

                long iRet = 0;

                //最終回
                if (index == N - 1)
                {
                    var iMin = Math.Max(a[index], bx);
                    iRet = b[index] - iMin < 0 ? 0 : b[index] - iMin + 1;
                    Ccalced.Add((bx, index), iRet);
                    return iRet;
                }

                //index回目
                for (int bNext = bx; bNext < b[index] + 1; bNext++)
                {
                    iRet += LoopCnt(bNext, index + 1);
                }
                Ccalced.Add((bx, index), iRet);
                return iRet;
            }

        }
    }
}