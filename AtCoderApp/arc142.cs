using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtCoderApp
{
    //WA
    public class arc142_a
    {
        public arc142_a()
        {
            //input-------------
            (var N, var K) = In.ReadTuple2<long>();

            //output------------
            //ex) f(x) = 142 ならば
            //反転して 241 -> 2410,2410,...
            //または 1420,14200,...
            //2回以上反転することはない！
            int rCnt = 0;

            string Kt = K.ToString();
            string Ks = string.Join("", K.ToString().Reverse()).TrimStart('0');

            rCnt += GetAns(Kt);
            rCnt += GetAns(Ks);
            Out.Write(rCnt);

            int GetAns(string k)
            {
                int cnt = 0;
                for (int i = 0; i < N.ToString().Length + 1; i++)
                {
                    if (Convert.ToInt64(k + new string('0', i)) <= N)
                        cnt++;
                }
                return cnt;
            }

        }
    }

    //WA
    public class arc142_a2
    {
        public arc142_a2()
        {
            //input-------------
            (var N, var K) = In.ReadTuple2<long>();

            //output------------
            //ex) f(x) = 142 ならば
            //反転して 241 -> 2410,2410,...
            //または 1420,14200,...
            //2回以上反転することはない！
            int rCnt = 0;

            long Ks = Convert.ToInt64(string.Join("", K.ToString().Reverse()));

            //題意を満たさない値,
            //K反転した結果の方が小さい
            //Kの下1桁が0
            //N範囲未満
            if (K > Ks ||
                K % 10 == 0 ||
                (K > N && Ks > N))
            {
                Out.Write("0");
                return;
            }

            rCnt += GetAns(K);

            if (K != Ks) //同じだと二重に計算しちゃう！
                rCnt += GetAns(Ks);

            Out.Write(rCnt);

            int GetAns(long k)
            {
                int cnt = 0;
                for (int i = 0; i < N.ToString().Length + 1; i++) //■ 多分この取り方が悪い！
                {
                    if ((long)(k * Math.Pow(10, i)) <= N)
                        cnt++;
                }
                return cnt;
            }

        }
    }

    //AC
    public class arc142_a3
    {
        public arc142_a3()
        {
            //input-------------
            (var N, var K) = In.ReadTuple2<long>();

            //output------------
            //ex) f(x) = 142 ならば
            //反転して 241 -> 2410,2410,...
            //または 1420,14200,...
            //Kを2回反転したら必ずもとに戻る(戻らない=Kの末尾に0がある=Kが最小ではない)
            int rCnt = 0;

            long Ks = Convert.ToInt64(string.Join("", K.ToString().Reverse()));

            //題意を満たさない値,
            //K反転した結果の方が小さい
            //Kの下1桁が0
            //N範囲未満
            if (K > Ks ||
                K % 10 == 0 ||
                (K > N && Ks > N))
            {
                Out.Write("0");
                return;
            }

            rCnt += GetAns(K);

            if (K != Ks) //同じだと二重に計算しちゃう！
                rCnt += GetAns(Ks);

            Out.Write(rCnt);

            int GetAns(long k)
            {
                int cnt = 0;
                int ii = 0;
                while ((long)(k * Math.Pow(10, ii)) <= N)
                {
                    cnt++;
                    ii++;
                }
                return cnt;
            }

        }
    }

    //AC
    public class arc142_b
    {
        public arc142_b()
        {
            //input-------------
            var N = In.Read<int>();

            //output------------
            //例を見た感じ,以下で行けそう
            //1,2,.. N
            //N+1～2N　のくみあわせ
            //2N+1～3N
            //...
            //N*(N-1)+1,,,,N*N

            //一番上と下の段以外を
            //5 7 6 8　のように小大小大…にすればいける

            Out.WriteMany(Enumerable.Range(1, N).ToArray());

            for (int i = 1; i < N - 1; i++)
            {
                var ary = new int[N];
                for (int j = 0; j < N; j++)
                {
                    if ((j + (N % 2)) % 2 == 0)
                        ary[j] = i * N + 1 + j / 2;
                    else
                        ary[j] = i * N + 1 + j / 2 + N / 2;
                }

                Out.WriteMany(ary);
            }
            Out.WriteMany(Enumerable.Range(N * (N - 1) + 1, N).ToArray());
        }
    }

    //AC25 WA14
    public class arc142_c
    {
        public arc142_c()
        {
            //input-------------
            var N = In.Read<int>();

            //output------------

            int u = 1;
            int v = 3;

            var t1d = new int[N + 1];
            var t2d = new int[N + 1];

            //1からvまで
            while (v <= N)
            {
                Out.WriteMany(new string[] { "?", u.ToString(), v.ToString() });
                var ret = In.Read<int>();
                if (ret == -1) return;//error

                t1d[v] = ret;
                v++;
            }

            //2からvまで
            u = 2;
            v = 3;
            while (v <= N)
            {
                Out.WriteMany(new string[] { "?", u.ToString(), v.ToString() });
                var ret = In.Read<int>();
                if (ret == -1) return;//error

                t2d[v] = ret;
                v++;
            }

            //処理
            int minlen = int.MaxValue;
            int same_count = 0;
            for (int i = 3; i <= N; i++)
            {
                if (minlen == t1d[i] + t2d[i]) same_count++;
                minlen = Math.Min(minlen, t1d[i] + t2d[i]);
            }

            //1-2が直接繋がってないかどうかのチェックができる?
            //1-2-a でmin=3にあってるか、1-a-b-2でmin=3になってるかで判断
            //b-1-2-aがあると処理できねえ..ムリやん
            if (same_count == 0 && minlen == 3)
                Out.Write("! 1");
            else
                Out.Write("! " + minlen.ToString());

            return;
        }
    }

    //解説見て作成
    //方針は間違ってなかった、、、最後の1-2結合チェックがガバだった
    public class arc142_c2
    {
        public arc142_c2()
        {
            //input-------------
            var N = In.Read<int>();

            //output------------

            int u = 1;
            int v = 3;

            var t1d = new int[N + 1];
            var t2d = new int[N + 1];

            //1からvまで
            while (v <= N)
            {
                Out.WriteMany(new string[] { "?", u.ToString(), v.ToString() });
                var ret = In.Read<int>();
                if (ret == -1) return;//error

                t1d[v] = ret;
                v++;
            }

            //2からvまで
            u = 2;
            v = 3;
            while (v <= N)
            {
                Out.WriteMany(new string[] { "?", u.ToString(), v.ToString() });
                var ret = In.Read<int>();
                if (ret == -1) return;//error

                t2d[v] = ret;
                v++;
            }

            //処理
            int minlen = int.MaxValue;
            var root_memo = new List<int>();
            for (int i = 3; i <= N; i++)
            {
                if (t1d[i] + t2d[i] == 3) root_memo.Add(i);

                minlen = Math.Min(minlen, t1d[i] + t2d[i]);
            }

            //1-2が直接繋がってないかどうかのチェック
            if (minlen == 3)
                if (root_memo.Count() != 2) //3個以上min=3があるときは、すなわちa,b-1-2-cのような形しかありえない
                    Out.Write("! 1");
                else
                {
                    //(b-1-2-a) or (1-a-b-2またはa-b-1,2)
                    Out.WriteMany(new string[] { "?", root_memo[0].ToString(), root_memo[1].ToString() });
                    var ret = In.Read<int>();
                    if (ret == -1) return;//error

                    if (ret == 2 || ret == 3)
                        Out.Write("! 1");
                    else
                        Out.Write("! 3");
                }
            else
                Out.Write("! " + minlen.ToString());

            return;
        }
    }

    //D考えたけどこれで良いのか全然わかんない
    public class arc142_d
    {
        public arc142_d()
        {
            //input-------------
            var N = In.Read<int>();
            var ab = In.ReadManyAry<int>(N);

            //output------------

            //ex)
            //1-2-3-4-5,6　なら
            //1回だけなら1,5,6,45,46,... 2345,2346がいけるが、
            //K回全部いけるのはない
            //ex2)
            //1-2-4-5,6
            //   ＼3
            //1回だけなら1,3,5,6,45,46がいけるが、2回目以降がNG

            //ex) 1-2-3-4-5 なら、1234や2345と置けばOK
            //(2と4に枝分かれがあってはならない)
            //3-6-7のような枝があってもOK。
            //-> 一番長い辺を探して、その辺の直前で枝分かれしてないものを探す

            var map = new int[N + 1, N + 1];//iからjの最短距離(i<j)
            var mapbfone = new int[N + 1, N + 1, 2];//最短を通った時の両端より1つ手前の位置n
            foreach (var _ab in ab)
                map[Math.Min(_ab[0], _ab[1]), Math.Max(_ab[0], _ab[1])] = 1;

            //距離探索
            for (int i = 1; i <= N; i++)
            {
                for (int j = i + 1; j <= N; j++)
                {
                    //まだ探索結果がない
                    if (map[i, j] == 0)
                    {
                        DFS(i, j);
                    }
                }
            }

            int maxlen = -1;
            for (int ii = 1; ii <= N; ii++)
            {
                for (int jj = 1; jj <= N; jj++)
                {
                    maxlen = Math.Max(maxlen, map[ii, jj]);
                }
            }

            //maxlenと同じ長さのx-yのルートをすべて観測する
            //そのうち、mapbfone両方ともが重複するものは除去
            //その数x2が答え??
            //以下の例なら,maxlen(1～7の距離6)より短い1-2-3-4-8とかがいけそう
            //1-2-3-4-5-6-7
            //      8
            //      9
            //だめぽ・・・

            //-----------------------------

            int DFS(int x, int y)
            {
                if (x == y) return 0;

                (x, y) = swap(x, y);
                if (map[x, y] != 0) return map[x, y];

                int minlen = N + 1;
                var bfone = new int[2]; //両端より1つ手前の位置n
                for (int ii = 1; ii <= N; ii++)
                {
                    for (int jj = 1; jj <= N; jj++)
                    {
                        (int s, int t) = swap(ii, x);
                        (int u, int v) = swap(jj, y);

                        if (map[s, t] > 0 && map[u, v] > 0)
                            if (minlen >= 2 + DFS(ii, jj))
                            {
                                minlen = 2 + DFS(ii, jj);
                                bfone[0] = ii;
                                bfone[1] = jj;
                            }
                    }
                }

                map[x, y] = minlen;
                mapbfone[x, y, 0] = bfone[0];
                mapbfone[x, y, 1] = bfone[1];
                return minlen;
            }

            (int x, int y) swap(int _x, int _y)
            {
                //swap
                if (_x >= _y)
                {
                    int w = _x;
                    _x = _y;
                    _y = w;
                }
                return (_x, _y);
            }
        }
    }
}