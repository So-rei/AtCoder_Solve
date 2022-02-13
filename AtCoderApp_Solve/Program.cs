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
            new arc135_b();
        }

        public class arc135_b
        {
            public arc135_b()
            {
                //input-------------
                var N = In.Read<int>();
                var Sn = In.ReadAry<int>().ToArray();

                //calc--------------
                if (N == 1)
                {
                    Out.Write("Yes");
                    Out.Write("0 0 " + Sn[0].ToString());
                    return;
                }

                var r = new int[N+2];
                //条件確認
                //s(n) < s(n+1) のとき、 A(n+3) >=  s(n+1) - s(n)
                //逆も同様 A(n-1) >= s(n+1) - s(n)
                for (int i = 0; i < N + 1; i++)
                {
                    var rR = 0;
                    var rL = 0;

                    if (i < N - 1 && Sn[i] > Sn[i + 1])
                    {
                        var sa = Sn[i] - Sn[i + 1];
                        if (Sn[i] < sa || Sn[i + 1] < sa)
                        {
                            Out.Write("No");
                            return;
                        }
                        rR = sa;
                    }

                    if (i >= 3 && Sn[i - 3] < Sn[i - 2])
                    {
                        var sa = Sn[i - 2] - Sn[i - 3];
                        if (Sn[i - 2] < sa || Sn[i - 3] < sa)
                        {
                            Out.Write("No");
                            return;
                        }
                        rL = sa;
                    }

                    //RL条件競合
                    if ((i >= 3 && rR > rL && rR > Math.Min(Sn[i - 3], Sn[i - 2])) ||
                        (i <= N - 1 && rR < rL && rL > Math.Min(Sn[i], Sn[i + 1])))
                    {
                        Out.Write("No");
                        return;
                    }
                    r[i] = Math.Max(rR, rL);
                }

                //success
                Out.Write("Yes");

                var rr = SetMain(0, r);
                Out.WriteMany(rr.ret);

                //条件によって計算量が減らせるループ
                (bool isOk, int[] ret) SetMain(int index, int[] joken)
                {
                    if (index >= N - 1)
                    {
                        joken[N] = Sn[index - 1] - joken[N - 1] - joken[N - 2];
                        joken[N + 1] = Sn[index] - joken[N] - joken[N - 1];
                        return (true, joken);//全部やったら終了
                    }

                    for (int i = joken[index]; i <= Sn[index] - joken[index] + joken[index + 1] + joken[index + 2]; i++)
                    {
                        if (index >= 2 && (joken[index - 2] + joken[index - 1] + i != Sn[index - 2]))//3つ目以降はサムチェック
                            continue;

                        var nj = new int[N + 2];
                        for (var k = 0; k < joken.Length; k++)
                        {
                            nj[k] = joken[k];
                        }

                        nj[index] = i;
                        var r = SetMain(index + 1, nj);
                        if (r.isOk) return r;
                    }

                    return (false, null);
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
        //h行=>1個の値取得
        public static IEnumerable<T> ReadMany<T>(long n) { for (long i = 0; i < n; i++) yield return Read<T>(); }
        //h行=>n個の配列値取得
        public static IEnumerable<IList<T>> ReadManyAry<T>(long n) { for (long i = 0; i < n; i++) yield return ReadAry<T>().ToArray(); }
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