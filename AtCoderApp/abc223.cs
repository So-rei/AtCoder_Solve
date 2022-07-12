using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoderApp
{

    public class abc223_a
    {
        public abc223_a()
        {
            //input-------------
            var X = In.Read<int>();

            //calc----

            Out.Write((X > 0 && X % 100 == 0) ? "Yes" : "No");
        }
    }
    public class abc223_b
    {
        public abc223_b()
        {
            //input-------------
            var S = In.Read<string>();

            //calc----
            var smin = S;
            var smax = S;
            var sminchar = S.ToArray().Min();
            var smaxchar = S.ToArray().Max();
            for (int x = 0; x < S.Length; x++)
            {
                if (S[x] == sminchar)
                    smin = new string[] { smin, (S.Substring(x) + S.Substring(0, x)) }.OrderBy(q => q).First();
                else if (S[x] == smaxchar)
                    smax = new string[] { smax, (S.Substring(x) + S.Substring(0, x)) }.OrderBy(q => q).Last();
            }

            Out.Write(smin);
            Out.Write(smax);
        }
    }

    public class abc223_c
    {
        public abc223_c()
        {
            //input-------------
            var N = In.Read<int>();
            int[][] AB = new int[N][];
            for (int i = 0; i < N; i++)
            {
                var _ab = In.ReadAry<int>().ToArray();
                AB[i] = new int[] { _ab[0], _ab[1] };
            }

            //calc-------------
            long[] sumLength = new long[N]; //右からi本目が始まる前の長さ
            decimal[] jTime = new decimal[N]; //右からi本目を始める時間

            jTime[0] = 0;
            sumLength[0] = 0;
            for (int i = 1; i < N; i++)
            {
                jTime[i] += (decimal)AB[i - 1][0] / (decimal)AB[i - 1][1] + jTime[i - 1];
                sumLength[i] += AB[i - 1][0] + sumLength[i - 1];
            }
            decimal jHalf = (jTime[N - 1] + ((decimal)AB[N - 1][0] / (decimal)AB[N - 1][1])) / 2;

            var target = jTime.Where(p => p <= jHalf).Select((p, index) => (p, index)).Max(); //ぶつかる場所i本目を求め
            decimal ret = (decimal)sumLength[target.index] + (decimal)(jHalf - target.p) * (decimal)(AB[target.index][1]);//i本目の何cm先かを求める

            //calc-------------
            double dTime = 0;
            //int[] sumLength = new int[N];
            //double[] jTime = new double[N];

            //dTime += (double)AB[0][0] / (double)AB[0][1];
            //jTime[0] = dTime;
            //sumLength[0] = AB[0][0];
            //for (int i = 1; i < N; i++)
            //{
            //    dTime += (double)AB[i][0] / (double)AB[i][1];
            //    jTime[i] = dTime;
            //    sumLength[i] += AB[i][0] + sumLength[i - 1];
            //}

            //var target = jTime.Where(p => p <= dTime / 2).Select((p, index) => (p, index)).Max();　//これだと１本めに境目が合った時取れない！！NG!
            //double ret = sumLength[target.index] + (double)(dTime / 2 - target.p) * (double)(AB[target.index + 1][1]);

            Out.Write(ret);
        }
    }


    //2022-07-13 練習中
    //TLEやWAが起こる
    public class abc223_d
    {
        public abc223_d()
        {
            //input-------------
            (var N, var M) = In.ReadTuple2<int>();
            var AB = In.ReadManyAry<int>(M).ToArray();

            //output------------
            //当然N!を全部求めるのは無理
            //1桁目T0=Biに出て来ない最も小さな値, 2桁目T1=T0以外のBiに出てこない値 or (T0,k)という(A,B)ペアが存在する時のk
            //...これをN桁回数行って間に合う？　→間に合いませんでした
            //有向グラフとして解くと間に合う？

            var Tx = new int[N]; //結果リスト
            var addList = new int[N + 1]; //Tiを探索する時、現在使用できる値リスト -1=まだ使用できない -200000000~=使用済 0以上:使用可
            var aList = new Dictionary<int, List<int>>();//Aを使用したあと解禁されるBリスト
            for (int i = 1; i <= N; i++)
                aList.Add(i, new List<int>());

            //初期状態
            for (int i = 0; i < M; i++)
            {
                addList[AB[i][1]] = -1;
                aList[AB[i][0]].Add(AB[i][1]);
            }

            if (!setNext(0, addList))
            {
                Out.Write("-1");
                return;
            }

            Out.WriteMany(Tx);

            //i行目をセット---
            bool setNext(int i, int[] useList)
            {
                if (i == N) return true;

                bool isOK = false;
                for (int j = 1; j <= N; j++)
                {
                    if (useList[j] < 0) continue;

                    //jと関係するaddListを更新
                    var cpy_useList = new int[N + 1];
                    Array.Copy(useList, cpy_useList, useList.Length);
                    cpy_useList[j] = -200000000; //使用済
                    foreach (var al in aList[j])
                        cpy_useList[al]++;//フラグをこのようにすることで、ここのフラグ設定の計算が少し短くできる

                    //このjを使ったとして、それ以降を確認する もし失敗した時は次の候補にする //■ここの失敗時は即失敗-1でよかった。このため、もっと短絡できる
                    if (!setNext(i + 1, cpy_useList)) continue;

                    //成功したので結果を格納
                    isOK = true;
                    Tx[i] = j; //使用可能な最も小さい値

                    break;
                }

                return isOK;
            }
        }
    }
}
