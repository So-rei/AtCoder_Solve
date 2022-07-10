using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AtCoderApp
{
    //2022-07-09

    //AC
    public class abc259_a
    {
        public abc259_a()
        {
            //input-------------
            (var N, var M, var X, var T, var D) = In.ReadTuple5<int>();

            //output------------
            var r = T;
            if (M < X)
                r -= (X - M) * D;

            Out.Write(r);
        }
    }

    //精度ミスって2回WAのあとAC
    //10^-6ぐらい大丈夫と思ったのだが…
    public class abc259_b
    {
        public abc259_b()
        {
            //input-------------
            (var a, var b, var d) = In.ReadTuple3<double>();

            //output------------

            double theta = a == 0 ? 0 : Math.Atan2(b, a); //a,bのθ(rad)
            double rad = theta + (d / 180 * Math.PI); //変化後の角度(rad)

            double x = Math.Sqrt(a * a + b * b) * Math.Cos(rad);
            double y = Math.Sqrt(a * a + b * b) * Math.Sin(rad);

            //Out.Write(x.ToString() + " " + y.ToString());
            //Out.Write(x.ToString("#0.00000000") + " " + y.ToString("#0.00000000"));
            Out.Write(x.ToString("#0.000000000000") + " " + y.ToString("#0.000000000000"));
        }
    }

    //AC31,WA1
    //なんか処理ミスがあるっぽい...どこかわからん...
    public class abc259_c
    {
        public abc259_c()
        {
            //input-------------
            var S = In.Read<string>().ToArray();
            var T = In.Read<string>().ToArray();

            //output------------
            if (S.Count() > T.Count() || S[0] != T[0])
            {
                Out.Write("No");
                return;
            }

            var sLen = S.Count();

            //重複セットを作製
            var aryCnt = new int[sLen];
            var aryChar = new char[sLen];
            int p = 0;

            //1文字目
            aryCnt[0] = 1;
            aryChar[0] = S[0];
            for (int i = 1; i < sLen; i++)
            {
                if (aryChar[p] == S[i])
                    aryCnt[p]++;
                else
                {
                    aryCnt[p + 1] = 1;
                    aryChar[p + 1] = S[i];
                    p++;
                }
            }

            //T判定
            int q = 0;
            int tmp_cnt = 1;//1文字目のぶん

            //2文字目以降
            for (int i = 1; i < T.Count(); i++)
            {
                if (aryChar[q] == T[i])
                    tmp_cnt++;
                else
                {
                    //i文字目がセットと違う中身
                    //if ((q == 0 || q == p || aryCnt[q] > 1) && aryCnt[q] <= tmp_cnt) //■これじゃ不十分
                    if ((aryCnt[q] == 1 && tmp_cnt == 1) || (aryCnt[q] > 1 && aryCnt[q] <= tmp_cnt))
                    {
                        q++;
                        tmp_cnt = 1;
                    }
                    else
                    {
                        Out.Write("No");
                        return;
                    }
                }
            }
            //最後1回分
            //if ((aryCnt[q] == 1 && tmp_cnt > 1) || (aryCnt[q] > 1 && aryCnt[q] > tmp_cnt)) //■これじゃ不十分
            if (!((aryCnt[q] == 1 && tmp_cnt == 1) || (aryCnt[q] > 1 && aryCnt[q] <= tmp_cnt)))
            {
                Out.Write("No");
                return;
            }

            Out.Write("Yes");
        }
    }

    //AC　配列サイズは取るが、わかりやすい処理になるようにした
    //この一部作り直しで時間ロス～～！
    public class abc259_c2
    {
        public abc259_c2()
        {
            //input-------------
            var S = In.Read<string>();
            var T = In.Read<string>();

            //output------------
            if (S.Count() > T.Count() || S[0] != T[0])
            {
                Out.Write("No");
                return;
            }

            //重複セットを作製
            (int[] aryCnt, char[] aryChar) = GetDupeSet(S);
            (int[] aryCntT, char[] aryCharT) = GetDupeSet(T);

            //比較関数
            for (int i = 0; i < aryCnt.Length; i++)
            {
                if (aryCnt[i] == 0 && aryCntT[i] == 0) break;

                //片方だけ終わったNG
                if (aryCnt[i] == 0 || aryCntT[i] == 0)
                {
                    Out.Write("No");
                    return;
                }

                //不一致NG
                if (aryChar[i] != aryCharT[i])
                {
                    Out.Write("No");
                    return;
                }

                //aaとaaaのような関係でないNG
                if ((aryCnt[i] == 1 && aryCntT[i] != 1) || (aryCntT[i] < aryCnt[i]))
                {
                    Out.Write("No");
                    return;
                }
            }

            (int[] aryCnt, char[] aryChar) GetDupeSet(string ts)
            {
                var sLen = ts.Count();
                var aryCnt = new int[sLen];
                var aryChar = new char[sLen];
                int p = 0;

                //1文字目
                aryCnt[0] = 1;
                aryChar[0] = ts[0];
                for (int i = 1; i < sLen; i++)
                {
                    if (aryChar[p] == ts[i])
                        aryCnt[p]++;
                    else
                    {
                        aryCnt[p + 1] = 1;
                        aryChar[p + 1] = ts[i];
                        p++;
                    }
                }

                return (aryCnt, aryChar);
            }

            Out.Write("Yes");
        }
    }

    //時間切れ後に作成
    //AC32 / TLE16
    //DFSが遅い、、、？
    public class abc259_d
    {
        public abc259_d()
        {
            //input-------------
            var N = In.Read<int>();
            (var sx, var sy, var tx, var ty) = In.ReadTuple4<int>();
            var XYR = new List<(long x, long y, long r)>();
            for (int i = 0; i < N; i++)
                XYR.Add(In.ReadTuple3<long>());

            //output------------
            var clossed = new int[N, N];
            var bStart = new int[N];
            var bEnd = new int[N];
            //スタートゴールがどの円と接するか
            bStart = GetHit(sx, sy);
            bEnd = GetHit(tx, ty);

            //円tと円sが接するかどうか調べてその結果を記憶
            for (int i = 0; i < N; i++)
            {
                for (int j = i + 1; j < N; j++)
                {
                    //SQRTにすると計算誤差でうまくいかない可能性があるので、二乗のまま比較計算する

                    long len = (XYR[i].x - XYR[j].x) * (XYR[i].x - XYR[j].x) + (XYR[i].y - XYR[j].y) * (XYR[i].y - XYR[j].y);
                    long r = XYR[i].r + XYR[j].r;
                    long rmax = Math.Max(XYR[i].r, XYR[j].r);
                    long rmin = Math.Min(XYR[i].r, XYR[j].r);
                    //最大：外接　～　最小：内接
                    if (len <= r * r && (rmax - rmin) * (rmax - rmin) <= len)
                        clossed[i, j] = 1;
                }
            }

            for (int i = 0; i < N; i++)
                if (bStart[i] == 1 && IsConnect(i)) //全てのスタートについて、ゴールとつながっているかどうか
                {
                    Out.Write("Yes");
                    return;
                }

            Out.Write("No");

            //---------------------------------------------
            int[] GetHit(int x, int y)
            {
                int[] ret = new int[N];
                for (int i = 0; i < N; i++)
                {
                    long len = (XYR[i].x - x) * (XYR[i].x - x) + (XYR[i].y - y) * (XYR[i].y - y);
                    if (len == XYR[i].r * XYR[i].r)
                        ret[i] = 1;
                }
                return ret;
            }

            bool IsConnect(int st, int deep = 0)
            {
                if (bEnd[st] == 1) return true;//自分が接している

                if (deep >= N) return false; //深さ探索N-1を超えた

                for (int i = 0; i < N; i++)
                {
                    if (i == st) continue;//自分は飛ばす

                    int left = st > i ? i : st;
                    int right = st > i ? st : i;

                    if (clossed[left, right] == 1)
                    {
                        if (bEnd[i] == 1) return true;//ゴールと接していることを発見した
                        if (IsConnect(i, deep + 1)) return true;//子が接していることを発見した
                    }
                }
                return false;
            }
        }
    }


    //DFSを解説をもとに書き換えたらAC
    //isCheckedは偉大
    public class abc259_d2
    {
        public abc259_d2()
        {
            //input-------------
            var N = In.Read<int>();
            (var sx, var sy, var tx, var ty) = In.ReadTuple4<int>();
            var XYR = new List<(long x, long y, long r)>();
            for (int i = 0; i < N; i++)
                XYR.Add(In.ReadTuple3<long>());

            //output------------
            //スタートおよびゴールがどの円と接するか記憶
            var bStart = GetHit(sx, sy);
            var bEnd = GetHit(tx, ty);

            //円tと円sが接するかどうか調べてその結果を記憶
            var clossed = new int[N, N];
            for (int i = 0; i < N; i++)
            {
                for (int j = i + 1; j < N; j++)
                {
                    //SQRTにすると計算誤差でうまくいかない可能性があるので、二乗のまま比較計算する

                    long len = (XYR[i].x - XYR[j].x) * (XYR[i].x - XYR[j].x) + (XYR[i].y - XYR[j].y) * (XYR[i].y - XYR[j].y);
                    long r = XYR[i].r + XYR[j].r;
                    //最大：外接　～　最小：内接
                    if (len <= r * r && (XYR[i].r - XYR[j].r) * (XYR[i].r - XYR[j].r) <= len)
                    {
                        clossed[i, j] = 1;
                        clossed[j, i] = 1; //i→jもj→iも意味は同じなので、両方にセットしておく
                    }
                }
            }

            //DFS判別用関数
            var isChecked = new bool[N];

            for (int i = 0; i < N; i++)
                if (bStart[i] == 1 && DFS(i)) //全てのスタートについて、ゴールとつながっているかどうか
                {
                    Out.Write("Yes");
                    return;
                }
            //※isCheckedはループごとにリセットする必要がない

            Out.Write("No");

            //local-calc-----------------------------------------------
            int[] GetHit(int x, int y)
            {
                int[] ret = new int[N];
                for (int i = 0; i < N; i++)
                {
                    long len = (XYR[i].x - x) * (XYR[i].x - x) + (XYR[i].y - y) * (XYR[i].y - y);
                    if (len == XYR[i].r * XYR[i].r)
                        ret[i] = 1;
                }
                return ret;
            }

            bool DFS(int st)
            {
                isChecked[st] = true; //調査済
                if (bEnd[st] == 1) return true;//自分が接している

                for (int i = 0; i < N; i++)
                {
                    if (isChecked[i]) continue; //既に調査済みのときは飛ばす
                    if (clossed[i, st] == 1)
                    {
                        if (DFS(i)) return true;//子が接していることを発見した
                    }
                }
                return false;
            }
        }
    }
}