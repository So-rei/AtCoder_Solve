using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoderApp
{
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        new abc222_c();
    //    }
    //}

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
}