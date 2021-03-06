using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoderApp
{
    public class abc227_a
    {
        public abc227_a()
        {
            //input-------------
            var NKA = In.ReadAry<int>().ToArray();
            int N = NKA[0];
            int K = NKA[1];
            int A = NKA[2];

            //output------------
            int walk = (K - 1) % N;
            int ret = 1 + (A + walk - 1) % N;

            Out.Write(ret);
        }

        //最大公約数
        public static int Gcd(int a, int b)
        {
            return a > b ? GcdRecursive(a, b) : GcdRecursive(b, a);
        }

        private static int GcdRecursive(int a, int b)
        {
            return b == 0 ? a : GcdRecursive(b, a % b);
        }
    }

    public class abc227_b
    {
        public abc227_b()
        {
            //input-------------
            int N = In.Read<int>();
            var S = In.ReadAry<int>().ToArray();

            //output------------
            var cnt = 0;
            foreach (var s0 in S)
            {
                for (int a = 1; a < 150; a++)
                {
                    //整数で割り消れる
                    if ((s0 - 3 * a) >= (4 * a + 3) &&
                        ((double)(s0 - 3 * a) / (4 * a + 3) * 10) % 10 == 0)
                    {
                        cnt++;
                        break;
                    }

                    //for (int b = 1; b < 150; b++)
                    //{

                    //    var r = 4 * a * b + 3 * a + 3 * b;
                    //    if (r > s0 || r > 1000) break; //maxS<=1000
                    //    if (r == s0)
                    //    {
                    //        cnt++;
                    //        goto next;
                    //    }
                    //}
                }

                //next:;
            }

            Out.Write(N - cnt);
        }
    }

    //これではだめ
    //1～Nの素数を全て取ろうとすることがそもそも遅すぎる。
    public class abc227_c
    {
        public abc227_c()
        {
            //input-------------
            var N = In.Read<long>();

            //output------------
            ulong ret = 1;//1,1,1はかならずあるので1
            var s = new List<string>();

            //これだと当然TLE----------------------
            //for (int n = 2; n <= N; n++)
            //{
            //    for (long a = 1; a <= Math.Pow(N, 0.3333333333334); a++)
            //    {
            //        if (a > n) break;
            //        for (long b = a; b <= Math.Sqrt(N); b++)
            //        {
            //            if (a * b > n) break;
            //            for (long c = b; c < N + 1; c++)
            //            {
            //                var sum = a * b * c;
            //                if (sum == n)
            //                {
            //                    ret++;
            //                    s.Add(a.ToString() + "," + b.ToString() + "," + c.ToString());
            //                }
            //                else if (sum > n)
            //                    break;
            //            }
            //        }
            //    }
            //}

            var s2 = new List<string>();
            for (int n = 2; n < N; n++)
            {
                var ptn = PrimeFactors(n).OrderBy(p => p).ToArray();
                if (ptn.Count() == 1)
                {
                    ret += 1; //(1,1,n)の１パターンのみ
                    continue;
                }

                //n = x^p * y^q * z^r *...のpqrを取得

                int p = ptn[0];
                int tmp_cnt = 1;
                var pq = new List<int>();//pqr..
                var cnt = 1;//xyz..の数
                for (int i = 1; i < ptn.Count(); i++)
                {
                    if (p != ptn[i])
                    {
                        pq.Add(tmp_cnt);
                        tmp_cnt = 1;
                        p = ptn[i];
                        cnt++;
                    }
                    else
                        tmp_cnt++;
                }
                if (tmp_cnt > 0)
                    pq.Add(tmp_cnt);

                //なんかこの辺をうまいことすればいけるはず??
                int r0 = 1 + ptn.Count();
                pq.ForEach(q => r0 -= (q - 1));
                ret += (ulong)r0;
                s2.Add(n.ToString() + ":N," + r0.ToString() + "ｺ");
            }

            static IEnumerable<int> PrimeFactors(int n)
            {
                int i = 2;
                int tmp = n;

                while (i * i <= n) //※1
                {
                    if (tmp % i == 0)
                    {
                        tmp /= i;
                        yield return i;
                    }
                    else
                    {
                        i++;
                    }
                }
                if (tmp != 1) yield return tmp;//最後の素数も返す
            }

            Out.Write(ret);

        }

    }

    //正解3,TLE5、残りはなんかコンパイルエラー？
    public class abc227_d
    {
        public abc227_d()
        {
            //input-------------
            var NK = In.ReadAry<int>().ToArray();
            int N = NK[0];
            int K = NK[1];
            var A = In.ReadAry<int>().ToArray();

            //1≤K≤N≤2×105
            //1≤Ai​≤1012

            //output------------
            var Ax = A.OrderByDescending(t => t); //社員が多い場所から取っていく
            IOrderedEnumerable<int> nextAx = null;
            var cnt = 0;
            while (true)
            {
                if (!SortAndDecrase(Ax, ref nextAx))
                    break;

                Ax = nextAx;
                cnt++;
            }

            Out.Write(cnt);


            bool SortAndDecrase(IOrderedEnumerable<int> Ax, ref IOrderedEnumerable<int> nextAx)
            {
                nextAx = Ax.Select((p, index) =>
                {
                    return p - (index < K ? 1 : 0);
                }).OrderByDescending(t => t);

                return (!(nextAx.Last() < 0)); //引けない状態だったら終了
            }
        }

    }
}