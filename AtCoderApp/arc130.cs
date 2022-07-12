using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoderApp
{

    //2021/11/28
    public class arc130_a
    {
        public arc130_a()
        {
            //input-------------
            var N = In.Read<int>();
            var S = In.Read<string>();

            //output------------
            //文字が連続している箇所を消したときしか条件を満たさないのは明らかで
            //連続箇所の個数nのとき条件を満たすΣ(i,j) = nC2
            long ret = 0; //回答

            int t_cnt = 1;
            char t_c = S[0];
            //foreach (var c in S)
            for (int i = 1; i < S.Length; i++)
            {
                char c = S[i];

                if (t_c == c)
                    t_cnt++;
                else
                {
                    ret += (long)(t_cnt * (t_cnt - 1)) / 2;
                    t_cnt = 1;
                    t_c = c;
                }
            }

            if (t_cnt > 1)
                ret += (long)(t_cnt * (t_cnt - 1)) / 2;

            Out.Write(ret);

            //-------------------------------
        }

    }


    //2021/11/28
    //この方針じゃダメ
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

    //2021/11/28
    //桁数同じだと思ってた・・・
    //作りかけのまま

    public class arc130_c
    {
        public arc130_c()
        {
            //input-------------
            var a = In.Read<string>();
            var b = In.Read<string>();

            //output------------



            var aCnt = new int[9];
            var bCnt = new int[9];
            for (int i = 1; i <= 9; i++)
            {
                aCnt[i - 1] = a.Count(p => p == Convert.ToChar(i.ToString()));
                bCnt[i - 1] = b.Count(p => p == Convert.ToChar(i.ToString()));
            }

            bool chk = false;//合計して10以上になる組み合わせが1つ以上あるかのフラグ
            for (int i = 1; i <= 9; i++)
            {
                if (aCnt[i - 1] > 0 && bCnt[9 - i] > 0)
                    chk = true;
            }
            if (!chk)
            {
                //合計して10以上になる組み合わせがない時はざっくり計算して終了
                var ret0 = 0;
                for (int i = 1; i <= 9; i++)
                    ret0 += aCnt[i - 1] * i + bCnt[i - 1] * i;

                Out.Write(ret0);
                return;
            }

            //合計して10以上になる組み合わせを一つ確保し、かつ、合計9になる組み合わせが最大になるように組む　
            //余りは合計なるべく10以上になるように組む
            int cnt = 0;
            var ret = Calc(aCnt, bCnt, ref cnt);
            Out.Write(ret);

            //メイン計算
            string[] Calc(int[] _aD, int[] _bD, ref int cnt)
            {
                var na = new int[9];
                var nb = new int[9];

                //合計9になる組み合わせが最大になるように組む
                for (int i = 1; i <= 8; i++)
                {
                    var r = Math.Min(_aD[i - 1], _bD[8 - i]);
                    na[i - 1] = _aD[i - 1] - r;
                    nb[i - 1] = _bD[8 - i] - r;
                }
                //もし357 + 642 みたいなときは.. 合計して10以上になる組み合わせが確保できないので、10,11とかになる桁を用意しなおして.. 357+264=621が最小?
                if (na.Count() == 0 && nb.Count() == 0)
                {
                    //合計すると"1t"という数になる数字の1の位t
                    for (int t = 0; t < 8; t++)
                    {
                        for (int i = 1; i <= 8; i++)
                        {
                            if ((9 - i + t) >= 0 && (9 - i + t) <= 8 && _aD[i - 1] > 0 && _bD[9 - i + t] > 0)
                            {
                                _aD[i - 1]--;
                                _bD[9 - i + t]--;
                                cnt += t;
                                var c = Calc(_aD, _bD, ref cnt);
                                return new string[2] { c[0] + i.ToString(), c[1] + (10 - i + t).ToString() };
                            }
                        }
                    }
                }

                //余りは合計なるべく10以上になるように組む
                string[] rx = new string[2];
                for (int t = 0; t <= 8; t++)
                {
                    for (int i = 1; i <= 9; i++)
                    {
                        if ((8 - i + t >= 0) && (8 - i + t <= 8))
                        {
                            var r = Math.Min(na[i - 1], nb[8 - i + t]);
                            na[i - 1] -= r;
                            nb[8 - i + t] -= r;

                            cnt += (t + 1) * r;
                            rx[0] += new string(Convert.ToChar(i), r);
                            rx[1] += new string(Convert.ToChar(9 - i + t), r);
                        }
                    }
                }

                //残りは合計しても10にならないものだけ
                for (int i = 1; i <= 4; i++)
                {
                    cnt += i * (na[i - 1] + nb[i - 1]);
                    rx[0] = new string(Convert.ToChar(i), na[i - 1]) + rx[0];
                    rx[1] = new string(Convert.ToChar(i), nb[i - 1]) + rx[1];
                }

                cnt++;//一番上の桁の1

                return rx;
            }

        }
    }
}