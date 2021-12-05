using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoderApp
{

    //20211205 正解！
    public class arc131_a
    {
        public arc131_a()
        {
            //input-------------
            var A = In.Read<int>();
            var B = In.Read<int>();

            //output
            //解答は10^18未満でいい。　つまりAとBで絡み合う必要はなく、上8桁A+下9桁がB/2の数字を作ればいい
            long R = (long)(A * Math.Pow(10, 9));
            if (B % 2 == 0) //偶数なら
                R += B / 2;
            else
                R += 10 * (B / 2) + 5; //一番下の桁を5にする ex)123→615。　つまり615*2=1230

            Out.Write(R);

            //-------------------------------
        }
    }

    //20211205 正解！
    public class arc131_b
    {
        public arc131_b()
        {
            //input-------------
            var HW = In.ReadAry<int>().ToArray();
            var H = HW[0];
            var W = HW[1];

            var c = new string[H];
            for (int i = 0; i < H; i++)
            {
                c[i] = In.Read<string>();
            }

            //output------------
            var mc = new string[H]; //シャローコピーじゃだめ
            for (int i = 0; i < H; i++)
                mc[i] = c[i];

            for (int x = 0; x < W; x++)
            {
                for (int y = 0; y < H; y++)
                {
                    PaintStart(x, y, 0);
                }
            }

            for (int i = 0; i < H; i++)
                Out.Write(mc[i]);

            //-------------------------------

            //1マス塗る関数
            //ng:塗り直し時用ng設定
            void PaintStart(int x, int y, int ng)
            {
                if (c[y][x] != '.') return; //塗らない

                while (true)
                {
                    var ptn = new List<int>();
                    for (int i = 1; i <= 5; i++)
                    {
                        if ((x - 1 < 0 || mc[y][x - 1] != Convert.ToChar(i.ToString())) &&
                            (x + 1 == W || mc[y][x + 1] != Convert.ToChar(i.ToString())) &&
                            (y - 1 < 0 || mc[y - 1][x] != Convert.ToChar(i.ToString())) &&
                            (y + 1 == H || mc[y + 1][x] != Convert.ToChar(i.ToString()))
                            )
                        {
                            ptn.Add(i);
                        }
                    }

                    if (ptn.Count() > 0)//塗れた
                    {
                        var ptn0 = 0;
                        if (ptn.Count() == 1 && ptn.Contains(ng))
                            ptn0 = ng; //別の色で塗り直せなかったので同じのまま
                        else
                            ptn0 = ptn.First(p => p != ng);

                        mc[y] = mc[y].Substring(0, x) + Convert.ToString(ptn0) + mc[y].Substring(x + 1);
                        return;
                    }
                    else //どれでも塗れなかった
                    {
                        //その隣接する.マスを出来る限り別の色で塗り直す
                        if (x - 1 < 0 || c[y][x - 1] == '.')
                            PaintStart(x - 1, y, mc[y][x - 1]);
                        if (y - 1 < 0 || c[y - 1][x] == '.')
                            PaintStart(x - 1, y, mc[y - 1][x]);

                        //右と下はまだ塗ってないので塗り直し不要
                    }
                }
            }
        }
    }

    //20211205 TLE11/40 ac29 やっぱ配列のRemoveが重い
    public class arc131_c
    {
        public arc131_c()
        {
            //input-------------
            var N = In.Read<int>();
            var A = In.ReadAry<int>().ToList();

            //output------------
            var X = 0;
            foreach (var ca in A)
                X = X ^ ca;

            //最適行動1)1ターン目に勝てるなら即勝利する
            if (A.Contains(X))
            {
                Out.Write("Win");
                return;
            }

            for (int i = 0; i < N; i++)
            {
                //i番目の最適行動を開始する
                bool bTry = false;
                for (int j = 0; j < A.Count(); j++)
                {
                    //最適行動2)A[j]を引いた後の残りN-i-1枚の中から、1枚引いても0にならないこと
                    if (!A.Where((p, index) => index != j).Contains(X ^ A[j]))
                    {
                        bTry = true;
                        X ^= A[j]; //XOR再計算
                        A.RemoveAt(j); //使ったカード除去
                        break;
                    }
                }

                if (!bTry)//最適行動1,2が取れなかった=次で相手が0にする=負け
                {
                    Out.Write(i % 2 == 0 ? "Lose" : "Win");
                    return;
                }
            }
        }
    }
}