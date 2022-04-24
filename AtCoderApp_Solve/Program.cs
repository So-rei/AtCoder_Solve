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
            new arc139_b();
        }

        public class arc139_b
        {
            public arc139_b()
            {
                //input-------------
                var T = In.Read<int>();
                var C = new (long N, long A, long B, long X, long Y, long Z)[T];
                for (int i = 0; i < T; i++)
                    (C[i].N, C[i].A, C[i].B, C[i].X, C[i].Y, C[i].Z) = In.ReadTuple6<int>();

                //output------------
                for (int i = 0; i < T; i++)
                {
                    var Xy = C[i].X * C[i].A <= C[i].Y;
                    var Xz = C[i].X * C[i].B <= C[i].Z;


                    if (Xy || Xz)
                    {
                        //Xが最も一番効率いいなら即終了
                        if (Xy && Xz)
                        {
                            Out.Write(C[i].X * C[i].N);
                            continue;
                        }
                        //Y>Z>XまたはZ>Y>Xの場合も即終了
                        else if (Xy)
                        {
                            long cn = (C[i].N - C[i].N % C[i].B) / C[i].B;
                            Out.Write(cn * C[i].Z + (C[i].N - cn * C[i].B) * C[i].X);
                            continue;
                        }
                        else if (Xz)
                        {
                            long cn = (C[i].N - C[i].N % C[i].A) / C[i].A;
                            Out.Write(cn * C[i].Y + (C[i].N - cn * C[i].A) * C[i].X);
                            continue;
                        }
                    }
                    
                    //それ以外の時、一番効率いいやつ詰める
                    if ((double)C[i].A / (double)C[i].Y > (double)C[i].B / (double)C[i].Z)
                        //X>Y>Z
                        Out.Write(SetKn(C[i]));
                    else
                    {
                        //Y>X>Z
                        long ab = C[i].B;
                        C[i].B = C[i].A;
                        C[i].A = ab;
                        long yz = C[i].Y;
                        C[i].Y = C[i].Z;
                        C[i].Z = yz;
                        Out.Write(SetKn(C[i]));
                    }
                }

                //a/y -> b/zの場合---------
                //ex)N=12のとき、5,5,1,1 より 5,3,3,1の方がいい可能性がある
                //この処理のため、途中からナップザックする
                //効率が良い方=a/y
                long SetKn((long n, long a, long b, long x, long y, long z) C)
                {
                    long cnt = Math.Max(0,((C.n - C.n % C.a) / C.a) - Gcd(C.a,C.b) / C.a);

                    var kn = new long[C.n+1];
                    kn[cnt * C.a] = cnt * C.y;

                    for (var i = cnt * C.a + 1; i <= C.n; i++)
                    {
                        var kmin = kn[i - 1] + C.x;
                        if (i - C.a >= cnt * C.a) kmin = Math.Min(kmin, kn[i - C.a] + C.y);
                        if (i - C.b >= cnt * C.a) kmin = Math.Min(kmin, kn[i - C.b] + C.z);
                        kn[i] = kmin;
                    }

                    return kn[C.n];
                }

                //gcd
                static long Gcd(long a, long b)
                {
                    return a > b ? GcdRecursive(a, b) : GcdRecursive(b, a);
                }

                static long GcdRecursive(long a, long b)
                {
                    return b == 0 ? a : GcdRecursive(b, a % b);
                }
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
    }
}