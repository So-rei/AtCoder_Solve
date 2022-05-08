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
            new abc250_d();
        }

        public class abc250_d
        {
            public abc250_d()
            {
                //input-------------
                var r = Algo.PrimeList(200, -1).ToArray();
                //output------------
            }
        }

    }//End



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
        public static (T, T, T, T, T,T) ReadTuple6<T>() { var c = ReadAry<T>().ToArray(); return (c[0], c[1], c[2], c[3], c[4],c[5]); }
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
        //gcd（最大公約数）を取得
        public static long Gcd(long a, long b)
        {
            return a > b ? GcdRecursive(a, b) : GcdRecursive(b, a);
        }

        private static long GcdRecursive(long a, long b)
        {
            return b == 0 ? a : GcdRecursive(b, a % b);
        }

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

        //素数かどうかチェックする
        public static bool IsPrime(long value)
        {
            // 1や0、負数は素数ではない
            if (value < 2)
                return false;

            // 2と3は素数である
            if (value == 2 || value == 3)
                return true;

            // 2以外の偶数であれば素数ではない
            if (value % 2 == 0)
                return false;

            // 平方根を切り上げた整数を求める
            double d = Math.Pow(value, 0.5);
            long max = (long)Math.Ceiling(d);

            // 奇数だけ平方根を切り上げた整数まで調べる
            for (long l = 3; l <= max; l += 2)
            {
                if (value % l == 0)
                    return false;
            }
            // ループから抜けてしまった場合は素数である
            return true;
        }

        //素数リストを出力する
        //max何個まで、maxいくつまで　の指定可
        public static IEnumerable<int> PrimeList(int cnt = -1, int max = -1) 
        {
            //1000までは高速に出す
            var s = "2 3 5 7 11 13 17 19 23 29 31 37 41 43 47 53 59 61 67 71 73 79 83 89 97 "+
                    "101 103 107 109 113 127 131 137 139 149 151 157 163 167 173 179 181 191 193 197 199 "+
                    "211 223 227 229 233 239 241 251 257 263 269 271 277 281 283 293 "+
                    "307 311 313 317 331 337 347 349 353 359 367 373 379 383 389 397 " +
                    "401 409 419 421 431 433 439 443 449 457 461 463 467 479 487 491 499 " +
                    "503 509 521 523 541 547 557 563 569 571 577 587 593 599 " +
                    "601 607 613 617 619 631 641 643 647 653 659 661 673 677 683 691 " +
                    "701 709 719 727 733 739 743 751 757 761 769 773 787 797 " +
                    "809 811 821 823 827 829 839 853 857 859 863 877 881 883 887 " +
                    "907 911 919 929 937 941 947 953 967 971 977 983 991 997";

            foreach (var sp in s.Split(' '))
            {
                int prime = Convert.ToInt32(sp);
                if (max != -1 && prime > max)
                    yield break;
                else
                {
                    yield return Convert.ToInt32(sp);
                    cnt--;
                    if (cnt == 0) yield break;
                }
            }

            //1000以上が必要なときは普通に取得
            if (cnt > 0)
            {
                for (int i = 1001; i < int.MaxValue; i++)
                {
                    if (IsPrime(i))
                    {
                        yield return i;
                        cnt--;
                    }

                    if (cnt == 0) yield break;
                }
            }
            else
            {
                for (int i = 1001; i <= max; i++)
                {
                    if (IsPrime(i))
                        yield return i;
                }
            }
        }
    }
}