using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoderApp
{

    //2021/11/28
    public class arc130_b
    {
        public arc130_b()
        {
            //input-------------
            var HWCQ = In.ReadAry<int>().ToArray();
            int H = HWCQ[0];
            int W = HWCQ[1];
            int C = HWCQ[2];
            int Q = HWCQ[3];

            int[] t = new int[Q];
            int[] n = new int[Q];
            int[] c = new int[Q];
            for (int i = 0; i < Q; i++)
            {
                var tnc = In.ReadAry<int>().ToArray();
                t[i] = tnc[0];
                n[i] = tnc[1];
                c[i] = tnc[2];
            }

            //output------------
            //タテヨコ逆のW,Hサイズの行列
            var ary = new int[W, H, 2]; //■■1億x1億の配列はメモリエラーで作れねぇ

            //タテだけ塗る
            for (int i = 0; i < Q; i++)
            {
                if (t[i] == 2)
                    for (int j = 0; j < H; j++)
                    {
                        ary[n[i] - 1, j, 0] = i; //Zorder調査のためにindex保存
                        ary[n[i] - 1, j, 1] = c[i];
                    }
            }
            //rotateする
            var RAry = new int[H, W, 2];
            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < W; j++)
                {
                    RAry[i, j, 0] = ary[j, i, 0];
                    RAry[i, j, 1] = ary[j, i, 1];
                }
            }

            //ヨコだけ塗る
            for (int i = 0; i < Q; i++)
            {
                if (t[i] == 1)
                    for (int j = 0; j < W; j++)
                    {
                        //Zorderは保存する必要なし
                        if (RAry[n[i] - 1, j, 0] < i) //Zorder比較
                            RAry[n[i] - 1, j, 1] = c[i];
                    }
            }

            //回答
            var cnt = new int[C];
            for (int i = 0; i < H; i++)
                for (int j = 0; j < W; j++)
                    if (RAry[i, j, 1] > 0)
                        cnt[RAry[i, j, 1] - 1]++;

            for (int i = 0; i < C; i++)
            {
                Out.Write(cnt[i]);
            }

            //-------------------------------
        }

    }

}