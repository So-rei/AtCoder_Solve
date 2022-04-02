using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtCoderApp
{
    class Program
    {
        static void Main(string[] args)
        {
            new abc246_e();
        }

        public class abc246_e
        {
            public abc246_e()
            {
                //input-------------
                var N = In.Read<long>();
                (var Ax , var Ay) = In.ReadTuple2<int>();
                (var Bx, var By) = In.ReadTuple2<int>();
                Ax--;
                Ay--;
                Bx--;
                By--;
                var S = new string[N];
                for (int i = 0; i < N; i++)
                    S[i] = In.Read<string>();

                //output------------

                //NGパターン
                if ((Math.Abs(Ax - Bx) - Math.Abs(Ay - By)) % 2 == 1) //ポーンの動き方的にいきようがない
                {
                    Out.Write(-1);
                    return;
                }
                //if (S[Bx][By] == '#') //行き先にポーンがある
                //{
                //    Out.Write(-1);
                //    return;
                //}    

                var dp = new int[N, N];
                for (int i = 0; i < N; i++)
                    for (int j = 0; j < N; j++)
                        dp[i, j] = (int)(N * N / 2) + 5;//最長手数は全マス数/2+1を超えることはない

                var li = new List<(int x, int y)>(); //cnt回目の探索で見つかった点
                li.Add((Ax, Ay)); //初期位置

                for (int cnt = 0; cnt < N * N / 2 + 5; cnt++) //最長手数は全マス数/2+1を超えることはない
                {
                    if (li.Count() == 0) break; //これ以上違う手がなければストップ

                    var nli = new List<(int x, int y)>();
                    foreach (var mem in li) nli.Add(mem);
                    li.Clear();

                    foreach (var mem in nli)
                    {
                        if (setdp(mem.x, mem.y, cnt))
                        {
                            Out.Write(cnt + 1);
                            return;
                        }
                    }
                }

                Out.Write(-1);

                bool setdp(int x, int y, int cnt)
                {
                    for (int d = 1; d < Math.Min(N - x, N - y); d++)
                    {
                        if (S[x + d][y + d] == '#' || dp[x + d, y + d] < cnt) break;
                        dp[x + d, y + d] = cnt + 1;
                        li.Add((x + d, y + d));
                    }
                    for (int d = 1; d < Math.Min(x + 1, y + 1); d++)
                    {
                        if (S[x - d][y - d] == '#' || dp[x - d, y - d] < cnt) break;
                        dp[x - d, y - d] = cnt + 1;
                        li.Add((x - d, y - d));
                    }
                    for (int d = 1; d < Math.Min(N - x, y + 1); d++)
                    {
                        if (S[x + d][y - d] == '#' || dp[x + d, y - d] < cnt) break;
                        dp[x + d, y - d] = cnt + 1;
                        li.Add((x + d, y - d));
                    }
                    for (int d = 1; d < Math.Min(x + 1, N - y); d++)
                    {
                        if (S[x - d][y + d] == '#' || dp[x - d, y + d] < cnt) break;
                        dp[x - d, y + d] = cnt + 1;
                        li.Add((x - d, y + d));
                    }

                    return dp[Bx, By] == cnt + 1;//Bについたかどうかを返す
                }
            }
        }
    }



    //Common Class------------------------------------------------------------------------------------------------------------------------------------------

    static class In
    {
        //1行=>1個の値取得
        public static T Read<T>() { var s = Console.ReadLine(); return (T)Convert.ChangeType(s, typeof(T)); }
        //1行=>n個の配列値取得
        public static IEnumerable<T> ReadAry<T>() { return Array.ConvertAll(Console.ReadLine().Split(' '), e => (T)Convert.ChangeType(e, typeof(T))); }
        //n行=>1個の値取得
        public static IEnumerable<T> ReadMany<T>(long n) { var TT = new List<T>(); for (long i = 0; i < n; i++) TT.Add(Read<T>()); return TT; }
        //n行=>*個の配列値取得
        public static IEnumerable<T[]> ReadManyAry<T>(long n) { var TT = new List<T[]>(); for (long i = 0; i < n; i++) TT.Add(ReadAry<T>().ToArray()); return TT; }


        //1行=>n個のタプル取得
        public static (T, T) ReadTuple2<T>() { var c = ReadAry<T>().ToArray(); return (c[0], c[1]); }
        public static (T, T, T) ReadTuple3<T>() { var c = ReadAry<T>().ToArray(); return (c[0], c[1], c[2]); }
        public static (T, T, T, T) ReadTuple4<T>() { var c = ReadAry<T>().ToArray(); return (c[0], c[1], c[2], c[3]); }
        public static (T, T, T, T, T) ReadTuple5<T>() { var c = ReadAry<T>().ToArray(); return (c[0], c[1], c[2], c[3], c[4]); }
    }

    static class Out
    {
        //1行出力
        public static void Write<T>(T item) => Console.WriteLine(item);
        //n行出力
        public static void WriteMany<T>(IEnumerable<T> items, string separetor = " ") => Write(string.Join(separetor, items));
    }

    public static class Algo
    {
        //Permutation(順列)計算 解はn!通りある

        //n個の数値オブジェクトの順列を返す(n!個)
        public static IEnumerable<IEnumerable<int>> Permutation(int max, int min = 1) { return Permutation<int>(Enumerable.Range(min, max).ToArray()); }

        //n個のオブジェクトの順列を返す(n!個)
        public static IEnumerable<IEnumerable<T>> Permutation<T>(params T[] items) where T : IComparable
        {
            int cnt = items.Count();

            if (cnt < 10)
            {
                foreach (var p in Permutation_Single(items))
                    yield return p;
            }
            else
            {
                //nが10以上の場合、前半と後半に分割して処理を減らす
                var combs = Combination(cnt, cnt / 2);
                foreach (var comb in combs)
                {
                    var Litems = new List<T>();
                    var Ritems = items.ToList();
                    foreach (var cc in comb)
                    {
                        Ritems.Remove(items[cc]);
                        Litems.Add(items[cc]);
                    }
                    var vL = Permutation(Litems.ToArray());
                    var vR = Permutation(Ritems.ToArray());

                    foreach (var l in vL)
                    {
                        foreach (var r in vR)
                        {
                            var plr = new List<T>();
                            plr.AddRange(l.ToList());
                            plr.AddRange(r.ToList());

                            yield return plr;
                        }
                    }
                }

            }
        }


        private static IEnumerable<T[]> Permutation_Single<T>(params T[] array) where T : IComparable
        {
            var a = new List<T>(array).ToArray();
            var res = new List<T[]>();
            res.Add(new List<T>(a).ToArray());
            var n = a.Length;
            var next = true;
            while (next)
            {
                next = false;

                // 1
                int i;
                for (i = n - 2; i >= 0; i--)
                {
                    if (a[i].CompareTo(a[i + 1]) < 0) break;
                }
                // 2
                if (i < 0) break;

                // 3
                var j = n;
                do
                {
                    j--;
                } while (a[i].CompareTo(a[j]) > 0);

                if (a[i].CompareTo(a[j]) < 0)
                {
                    // 4
                    var tmp = a[i];
                    a[i] = a[j];
                    a[j] = tmp;
                    Array.Reverse(a, i + 1, n - i - 1);
                    res.Add(new List<T>(a).ToArray());
                    next = true;
                }
            }
            return res;
        }

        //nCr列挙
        //ex)4C2 = (0,1)(0,2)(0,3)(1,2)(1,3)(2,3)
        public static IEnumerable<List<int>> Combination(int n, int r, int st = 0)
        {

            for (int i = st; i < n; i++)
            {
                //残り回数<選ぶ個数 になれば終了
                if (n - i < r) yield break;

                //残り1回ならこれで終了
                if (r == 1)
                {
                    yield return new List<int> { i };
                    continue;
                }

                //iを獲る選択肢を列挙
                foreach (var c in Combination(n, r - 1, i + 1))
                {
                    var comb = new List<int>();
                    comb.Add(i);
                    comb.AddRange(c);
                    yield return comb;
                }

                //iを獲らない選択肢は次のループで行われる
            }
        }
    }
}