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
                        clossed[j, i] = 1; //今回は無向グラフであるので、i→jもj→iも両方にセットしておく
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

    //時間切れ後に作成
    //inputめんどくさ!!
    //特に解説見てないけどACになった。やったぜ。
    public class abc259_e
    {
        public abc259_e()
        {
            //input-------------
            var N = In.Read<int>();
            var m = new int[N];
            var pe = new List<(int p, int e)[]>();
            for (int i = 0; i < N; i++)
            {
                m[i] = In.Read<int>();
                var pp = new (int p, int e)[m[i]];
                for (int j = 0; j < m[i]; j++)
                    pp[j] = In.ReadTuple2<int>();
                pe.Add(pp);
            }

            //output------------
            //全てバラバラに重複しないpが入っている場合、答えは明らかにN
            //a(i)のすべてのpj,ejについて、他のすべてのaのp,eの組み合わせ中にpj = pk かつ ej <= ekのものが存在する
            //となるようなa(i)が複数存在する（以下、ai∈a他 と表記）　→　その重複の分だけ、最終結果が減る。(1個だけなら減らない)
            //ex)2,3,5 →　全部バラバラ。解=3-0=3
            //ex)6,9,4,20 → {6∈9と4}{4∈20} なので,6を消したときと4を消した時はどちらも同じ結果(=9と20の最小公倍数)となる。∴解は4-1=3
            //これでいけるはず

            //この探索を真面目に１個ずつ二重ループするとO(N*N/2)回判定やらないといけない....?これは多すぎ
            //→aに関係なく全てのp,eを予めリスト化しておく。同じpが出たら最も大きい乗数eMaxをリストっとく。
            //リストを作るのにO(N)
            //a(i)がリスト条件の中で全て満たすか調べる。ただし、自分自身がそのリストのeMaxであるときは除外しないといけない。。
            //除外のためには、あるpについて、ei=3,ej=4,ek=5... の場合、(i,3),(j,4),(k,5)...と全部リストに追加しておく？でもこれだと最大N件はいるので、除外処理が最悪O(N*N)になる？？
            //→不要。eMaxとその次に大きいeを誰が作ったかだけリストしとけば良い。
            //リスト確認O(2*N)ぐらいでできるはず

            var memo = new Dictionary<int, (int index, int e)[]>(); //リスト Key=p, 中身はeMaxとその一つ小さいeの(index,e)
                                                                    //リストを作る
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < m[i]; j++)
                {
                    if (memo.ContainsKey(pe[i][j].p))
                    {
                        if (memo[pe[i][j].p][0].e < pe[i][j].e)
                        {
                            //eMax>e次 と入るように順番swap
                            memo[pe[i][j].p][1] = memo[pe[i][j].p][0];
                            memo[pe[i][j].p][0] = (i, pe[i][j].e);
                        }
                        else if (memo[pe[i][j].p][1].e < pe[i][j].e)
                        {
                            memo[pe[i][j].p][1] = (i, pe[i][j].e);
                        }
                    }
                    else
                    {
                        //まだないので追加
                        var newary = new (int index, int e)[2];
                        newary[0] = (i, pe[i][j].e);
                        memo.Add(pe[i][j].p, newary);
                    }
                }
            }

            //リスト確認
            int eraseCnt = 0;
            for (int i = 0; i < N; i++)
            {
                bool canErase = true;

                for (int j = 0; j < m[i]; j++)
                {
                    //自分が最高乗数eMaxの持ち主であり、除外するとeMaxを満たせなくなってしまう場合は重複する可能性がない
                    if (memo[pe[i][j].p][0].index == i && memo[pe[i][j].p][1].e < memo[pe[i][j].p][0].e)
                    {
                        canErase = false;
                        break;
                    }
                }

                if (canErase) eraseCnt++;
            }

            Out.Write(eraseCnt < 2 ? N : N - (eraseCnt - 1));
        }
    }
}