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
            new abc245_e();
        }

        public class abc245_e
        {
            public abc245_e()
            {
                //input-------------
                var nm = In.ReadAry<int>().ToArray();
                (var N, var M) = (nm[0], nm[1]);
                var A = In.ReadAry<int>().ToArray();
                var B = In.ReadAry<int>().ToArray();
                var C = In.ReadAry<int>().ToArray();
                var D = In.ReadAry<int>().ToArray();

                //calc--------------

                //無駄な面積最小=最善手　である
                //大きいものから順になるべく小さい箱に入れていく
                var AB = new List<(int A, int B)>();
                var CD = new List<(int C, int D)>();

                for (int i = 0; i < N; i++)
                    AB.Add((A[i], B[i]));
                for (int i = 0; i < M; i++)
                    CD.Add((C[i], D[i]));

                AB = AB.OrderByDescending(p => p.A).ThenByDescending(q => q.B).ToList();
                CD = CD.OrderBy(p => p.C).ThenBy(q => q.D).ToList();

                var nAB = new List<(int A, int B)>(AB);
                var nCD = new List<(int C, int D)>(CD);

                for (int i = 0; i < AB.Count(); i++)
                {
                    //ぴったりが最優先
                    var rSame = CD.Where(p => AB[i].A == p.C && AB[i].B == p.D);
                    if (rSame.Count() > 0)
                    {
                        nAB.Remove(AB[i]);
                        nCD.Remove(rSame.First());
                        continue;
                    }
                    //ただ1つしかない場合も優先
                    var rOne = CD.Where(p => AB[i].A <= p.C && AB[i].B <= p.D);
                    if (rOne.Count() == 1)
                    {
                        nAB.Remove(AB[i]);
                        nCD.Remove(rOne.First());
                        continue;
                    }
                }

                //残りパタン
                var b = MainCalc(nCD,0);
                if (b) Out.Write("Yes");
                else Out.Write("No");
                return;


                bool MainCalc(List<(int C, int D)> cd, int r)
                {
                    for (int i = r; i < nAB.Count(); i++)
                    {
                        var bone = true;
                        var box = cd.Where(p => nAB[i].A <= p.C && nAB[i].B <= p.D);
                        if (box.Count() == 0) return false;

                        foreach (var bx in box)
                        {
                            var ccd = cd.Where(p => p.C != bx.C && p.D != bx.D).ToList();
                            bone = MainCalc(ccd, r+1);
                            if (!bone) return false;
                        }
                    }
                    return true;
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