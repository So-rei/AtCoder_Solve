using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtCoderApp
{
    //2022-07-17
    //Aはpython AC

    //時間切れ
    //方針は正解
    //解説を見てもあってるように見えるが,WAがある...?
    public class arc144_c
    {
        public arc144_c()
        {
            //input-------------
            (var N, var K) = In.ReadTuple2<int>();

            //output------------
            //いけないとき
            if (N < 2 * K)
            {
                Out.Write(-1);
                return;
            }

            //いけるときは例2みたいに大体ギリギリのライン(Ai-i==K)を攻めた値が最小になるやろ
            //4 5 6 7 8 1 2 3 = (4,5,6),(7,8),(1,2,3) の3種類の詰め方
            var ret = new int[N];
            //前N-K個
            for (int i = 0; i < N - K; i++)
                ret[i] = i + 1 + K;
            //後ろK個
            for (int i = N - K; i < N; i++)
                ret[i] = 1 + i - (N - K);

            //このままだと後ろK個に1,2,3...と入っている
            //K+1個目～N-K個目の真ん中部分で、この部分と交換できるものがあれば交換する
            if (K * 3 < N)
            {
                for (int i = 0; i < N; i++)
                {
                    if (i < K) { }
                    // ret[i] = i + 1 + K; //もとのまま
                    else if (i < N - 2 * K && i < 2 * K)
                        ret[i] = i + 1 - K;
                    else if (i < N - K) { }
                    // ret[i] = i + 1 + K; //もとのまま
                    else if (i < 3 * K)
                        ret[i] = i + 1 - 2 * K;
                    else
                        ret[i] = K * 4 < N ? i - K : i + 1 - K;
                }
                //if (K * 4 >= N)
                //{
                //    //ex) N=11 K=3 のとき、4,5,6,7,8,9,10,11,1,2,3　→ 4,5,6,1,2,9,10,11,3,7,8
                //    int delimit = N - K * 3;
                //    for (int i = 0; i < delimit; i++)
                //    {
                //        ret[K + i] = i + 1;
                //        ret[N - delimit + i] = 2 * K + i + 1;
                //    }
                //    for (int i = 0; i < K - delimit; i++)
                //        ret[N - K + i] = i + delimit + 1;
                //}
                //else
                //{
                //    //ex) N=100 K=10 のとき、11...20, 1...10, 31....100, 21...30
                //    for (int i = 0; i < K; i++)
                //    {
                //        int _r = ret[N - K + i];
                //        ret[N - K + i] = ret[K + i];
                //        ret[K + i] = _r;
                //    }
                //}
            }

            Out.WriteMany(ret);
        }
    }


    //方針は正解
    //解説を見てもあってるように見えるが,WAがある...?
    public class arc144_c2
    {
        public arc144_c2()
        {
            //input-------------
            (var N, var K) = In.ReadTuple2<int>();

            //output------------
            //いけないとき
            if (N < 2 * K)
            {
                Out.Write(-1);
                return;
            }

            //いけるときは例2みたいに大体ギリギリのライン(Ai-i==K)を攻めた値が最小になるやろ
            var ret = new int[N];
            if (N <= K * 3)
            {
                //4 5 6 7 8 1 2 3 => 4,5,6,7,8,1,2,3
                //前N-K個
                for (int i = 0; i < N - K; i++)
                    ret[i] = i + 1 + K;
                //後ろK個
                for (int i = N - K; i < N; i++)
                    ret[i] = 1 + i - (N - K);
            }
            else if (N > K * 3)
            {
                //N <= K * 3と同じ考え方では、後ろK個に1,2,3...と入っている
                //N > K * 3のとき、K+1個目～N-K個目の真ん中部分で、この部分と交換できるものが存在する
                if (N <= K * 4) //ジャッジ14/97
                {
                    //ex) N=11 K=3 のとき、4,5,6,7,8,9,10,11,1,2,3　→ 4,5,6,1,2,9,10,11,3,7,8
                    int delimit = N - K * 3;
                    //前N-K個
                    for (int i = 0; i < N - K; i++)
                        ret[i] = i + 1 + K;
                    //真ん中移動,後ろ追加
                    for (int i = 0; i < delimit; i++)
                    {
                        ret[K + i] = i + 1;
                        ret[N - delimit + i] = 2 * K + i + 1;
                    }
                    //後ろ修正
                    for (int i = 0; i < K - delimit; i++)
                        ret[N - K + i] = i + delimit + 1;
                }
                else if (N <= K * 5) //ジャッジ11/97
                {
                    //ex) N=50 K=10 のとき、11...20, 1...10, 31....50, 21...30
                    //前N-K個
                    for (int i = 0; i < N - K; i++)
                        ret[i] = i + 1 + K;
                    //後ろK個
                    for (int i = N - K; i < N; i++)
                        ret[i] = 1 + i - (N - K);
                    //1...10と21...30の部分をswap
                    for (int i = 0; i < K; i++)
                    {
                        int _r = ret[N - K + i];
                        ret[N - K + i] = ret[K + i];
                        ret[K + i] = _r;
                    }
                }
                else //ジャッジ30/97
                {
                    //■11/30がまだWA...

                    //N=13,K=2 のとき・・・　3,4,1,2,7,8,5,6,11,12,13,9,10
                    //(1,2)と(5,6),(9,10)が移動してる
                    int offset = 0;
                    //前N-K個
                    for (int i = 0; i < N - K; i++)
                    {
                        if ((i / K) % 2 == 0)
                            ret[i] = i + 1 + K; //そのままパターン(奇数回目)
                        else if ((N - K - 1) / K == i / K)
                        {
                            ret[i] = i + 1 + K; //そのままパターン(最終回)
                            offset++;
                        }
                        else
                            ret[i] = i + 1 - K; //移動するパターン(偶数回目)
                    }
                    //末尾K個
                    for (int i = N - K; i < N; i++)
                        ret[i] = i + 1 - K - offset;
                }
            }


            Out.WriteMany(ret);
        }
    }
}