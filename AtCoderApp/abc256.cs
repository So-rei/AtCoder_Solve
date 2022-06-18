using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AtCoderApp
{
    //AC
    public class abc256_a
    {
        public abc256_a()
        {
            //input-------------
            var N = In.Read<long>();

            //output------------
            Out.Write(Math.Pow(2, N));
        }
    }

    //AC
    public class abc256_b
    {
        public abc256_b()
        {
            //input-------------
            var N = In.Read<int>();
            var A = In.ReadAry<int>().ToArray();

            //output------------
            int P = 0;
            var B = new int[4];
            foreach (var a in A)
            {
                B[0] = 1;//バッター

                //3塁から順に
                for (int i = 3; i >= 0; i--)
                {
                    if (B[i] == 1)
                    {
                        if (i + a < 4)
                            B[i + a] = 1;
                        else
                            P++;

                        B[i] = 0;
                    }
                }
            }
            Out.Write(P);
        }
    }

    //AC
    public class abc256_c
    {
        public abc256_c()
        {
            //input-------------
            var hw = In.ReadAry<int>().ToArray();
            var h = new int[3] { hw[0], hw[1], hw[2] };
            var w = new int[3] { hw[3], hw[4], hw[5] };

            //output------------

            long ptn = 0;
            //renritu houteisiki
            //正の値なので有限組み合わせ
            //左上の4マスを固定できたら残りは確定する

            var x = new int[3, 3];
            //左上、中央左、中央上、中央の順
            for (int i = 1; i <= Math.Min(h[0] - 2, w[0] - 2); i++)
            {
                for (int j = 1; j <= Math.Min(w[1] - 2, h[0] - i - 1); j++)
                {
                    for (int k = 1; k <= Math.Min(h[1] - 2, w[0] - i - 1); k++)
                    {
                        for (int l = 1; l <= Math.Min(h[1] - k - 1, w[1] - j - 1); l++)
                        {
                            x[0, 2] = h[0] - i - k;
                            x[1, 2] = h[1] - j - l;
                            x[2, 0] = w[0] - i - j;
                            x[2, 1] = w[1] - k - l;

                            //最後右下のセルが辻褄あうか
                            if (w[2] - x[0, 2] - x[1, 2] < 1 || h[2] - x[2, 0] - x[2, 1] < 1)
                                continue;

                            if ((w[2] - x[0, 2] - x[1, 2]) + x[2, 0] + x[2, 1] != h[2])
                                continue;

                            ptn++;
                        }
                    }
                }
            }

            Out.Write(ptn);

        }
    }

    //AC
    public class abc256_d
    {
        public abc256_d()
        {
            //input-------------
            var N = In.Read<int>();
            var LR = In.ReadManyAry<int>(N);

            //output------------
            var sortedLR = LR.OrderBy(p => p[0]).ThenBy(p => p[1]);

            var cLR = new List<int[]>();
            var tmpSt = 0;
            var tmpMax = 0;

            bool isFirst = true;
            foreach (var lr in sortedLR)
            {
                //一旦途切れた時
                if (tmpMax < lr[0])
                {
                    if (!isFirst)
                        cLR.Add(new int[] { tmpSt, tmpMax });//前回分を追加
                    tmpSt = lr[0];
                    tmpMax = lr[1];

                    isFirst = false;
                }
                else //一つ前のマップと陸繋がりの時
                {
                    tmpMax = Math.Max(tmpMax, lr[1]);
                }
            }
            cLR.Add(new int[] { tmpSt, tmpMax });//最後の分を追加

            foreach (var lr in cLR)
                Out.WriteMany(lr);
        }
    }

    //残り時間ギリギリで出したが、
    //例題はACだが、一部WA、大半TLE
    //やはりInsertは重たい
    public class abc256_e
    {
        public abc256_e()
        {
            //input-------------
            var N = In.Read<int>();
            var X = In.ReadAry<int>().ToArray();
            var C = In.ReadAry<long>().ToArray();

            //output------------
            var iXC = new Dictionary<int, (int X, long C)>();
            for (int i = 0; i < N; i++)
            {
                iXC.Add(i + 1, (X[i], C[i]));
            }

            //まず誰も不満に覚えない人を取得して除去(それ以外全員の前にすれば良い)
            var xgrp = X.GroupBy(p => p);
            for (int i = 1; i <= N; i++)
            {
                if (!xgrp.Select(p => p.Key).Contains(i))
                    iXC.Remove(i);
            }

            //残りをコストが高い順にソート
            long cSum = 0;
            var oIXC = iXC.OrderByDescending(p => p.Value.C).ToList();

            var ret = new List<int>(); //残りの並び順
            foreach (var ixc in oIXC)
            {
                if (!ret.Contains(ixc.Key))
                {
                    if (!ret.Contains(ixc.Value.X))
                    {
                        //両方ない場合は単純にi→X
                        ret.Add(ixc.Key);
                        ret.Add(ixc.Value.X);
                    }
                    else
                    {
                        //iのみない
                        ret.Insert(0, ixc.Key);
                    }
                }
                else
                {
                    if (!ret.Contains(ixc.Value.X))
                    {
                        //xのみない
                        ret.Add(ixc.Value.X);
                    }
                    else
                    {
                        //両方ある(不満c発生)
                        //■両方あるかどうかしか見ていないのがダメ！
                        cSum += ixc.Value.C;
                    }
                }
            }

            Out.Write(cSum);
        }
    }

    //時間切れ後作成 AC
    //枝でのデータ処理にすると前後関係がしっかりする。
    //大量に作るListのコストがあるように見えたが、計算量的に間に合う。
    public class abc256_e2
    {
        public abc256_e2()
        {
            //input-------------
            var N = In.Read<int>();
            var X = In.ReadAry<int>().ToArray();
            var C = In.ReadAry<long>().ToArray();

            //output------------
            var iXC = new Dictionary<int, (int X, long C)>();
            for (int i = 0; i < N; i++)
            {
                iXC.Add(i + 1, (X[i], C[i]));
            }

            //まず誰も不満に覚えない人を取得して除去(それ以外全員の前にすれば良い)
            //コストの割にあまり効果ないので削除
            //var xgrp = X.GroupBy(p => p).Select(p => p.Key);
            //for (int i = 1; i <= N; i++)
            //{
            //    if (!xgrp.Contains(i))
            //        iXC.Remove(i);
            //}

            //残りをコストが高い順にソート
            long cSum = 0;
            var oIXC = iXC.OrderByDescending(p => p.Value.C).ToList();

            var ret = new Dictionary<int, List<int>>(N + 1); //残りの並び順(親子関係)
            for (int i = 1; i <= N; i++)
                ret.Add(i, new List<int>());

            foreach (var ixc in oIXC)
            {
                int i = ixc.Key;
                int x = ixc.Value.X;

                //i→Xの並び順であるかどうかチェック

                if (ret[x].Contains(i) || ContainsChild(x, i))
                {
                    //並び順がx->iで定義されている(逆なので不満c発生)
                    cSum += ixc.Value.C;
                }
                else
                {
                    //並び順が未定義の場合は、親子関係を追加する
                    if (!ContainsChild(i, x))
                        ret[i].Add(x);
                }
            }

            //子以降に存在するかどうか
            bool ContainsChild(int _x, int _i)
            {
                if (ret[_x].Contains(_i))
                    return true;

                foreach (var cx in ret[_x])
                    if (ContainsChild(cx, _i)) return true;

                return false;
            }

            Out.Write(cSum);
        }
    }
}