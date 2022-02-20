﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtCoderApp
{
    class Program
    {
        static void Main(string[] args)
        {
            new abc239_e();
        }

        public class abc239_e
        {
            public abc239_e()
            {
                //input-------------
                var NQ = In.ReadAry<int>().ToArray();
                (var N, var Q) = (NQ[0], NQ[1]);
                var X = In.ReadAry<int>().ToArray();
                var AB = new List<(int A, int B)>();
                for (int i = 0; i < N-1; i++)
                {
                    var _ab = In.ReadAry<int>().ToArray();
                    AB.Add((_ab.Min(), _ab.Max())); //そーとしておく
                }
                var VK = new List<(int V, int K)>();
                for (int i = 0; i < Q; i++)
                {
                    var _vk = In.ReadAry<int>().ToArray();
                    VK.Add((_vk[0], _vk[1]));
                }

                //calc--------------
                var ABsort = AB.OrderBy(p => p.A).ToList(); //根がどっちかわからないということがおこらないようにする
                var li = new Dictionary<int, List<int>>();
                li.Add(1, new List<int>());
                li[1].AddRange(Enumerable.Range(1, N));
                for (int i = 2; i <= N; i++)
                    li.Add(i, new List<int>());

                //つながりのうち、根がどっちか側にあるかを考慮しないといけない
                //確実に1とつながってる場所から始めることで計算量を最小にできるはず
                foreach (var ab in ABsort.Where(p => p.A == 1))
                    getChild(ab.B, 1);

                for (int i = 0; i < Q; i++)
                {
                    var rr = li[VK[i].V].Distinct().Select(p => X[p - 1]).OrderByDescending(q => q).ToList(); //X取得・大きい方から
                    Out.Write(rr[VK[i].K - 1]);
                }

                //--------------------------------------------------------------------------------
                //noの枝リスト取得
                IEnumerable<int> getChild(int no, int Root) //Root:根とつながってる腕のno
                {
                    var r = new List<int>();
                    r.AddRange(ABsort.Where(p => (p.A == no && p.B != Root)).Select(p => p.B));
                    r.AddRange(ABsort.Where(p => (p.B == no && p.A != Root)).Select(p => p.A));

                    if (r.Count() == 0) //子がいない
                    {
                        r.Add(no); //自身を追加
                        li[no] = r; //保存
                        return r;
                    }
                    else//子がいるときはさらに子を取得..
                    {
                        var rc = new List<int>();
                        foreach (var c in r)
                            rc.AddRange(getChild(c, no));

                        rc.Add(no); //自身を追加
                        li[no] = rc; //保存
                        return rc;
                    }
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