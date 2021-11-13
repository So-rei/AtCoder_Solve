using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoderApp
{
    class Program
    {
        static void Main(string[] args)
        {
            new abc227_c();
        }
    }

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
            for (int n = 2; n <= N; n++)
            {
                for (long a = 1; a <= Math.Pow(N, 0.3333333333334); a++)
                {
                    if (a > n) break;
                    for (long b = a; b <= Math.Sqrt(N); b++)
                    {
                        if (a * b > n) break;
                        for (long c = b; c < N + 1; c++)
                        {
                            var sum = a * b * c;
                            if (sum == n)
                            {
                                ret++;
                                s.Add(a.ToString() + "," + b.ToString() + "," + c.ToString());
                            }
                            else if (sum > n)
                                break;
                        }
                    }
                }
            }

            Out.Write(ret);

            //for (int n = 2; n < N; n++)
            //{
            //    var ptn = PrimeFactors(n);

            //}

            //static IEnumerable<int> PrimeFactors(int n)
            //{
            //    int i = 2;
            //    int tmp = n;

            //    while (i * i <= n) //※1
            //    {
            //        if (tmp % i == 0)
            //        {
            //            tmp /= i;
            //            yield return i;
            //        }
            //        else
            //        {
            //            i++;
            //        }
            //    }
            //    if (tmp != 1) yield return tmp;//最後の素数も返す
            //}
        }

    }
}

//Common Class--
static class In
{
    public static T Read<T>() { var s = Console.ReadLine(); return (T)Convert.ChangeType(s, typeof(T)); }
    public static IEnumerable<T> ReadAry<T>() { return Array.ConvertAll(Console.ReadLine().Split(' '), e => (T)Convert.ChangeType(e, typeof(T))); }
    public static IEnumerable<T> ReadMany<T>(long n) { for (long i = 0; i < n; i++) yield return Read<T>(); }
}

static class Out
{
    public static void Write<T>(T item) => Console.WriteLine(item);
    public static void WriteMany<T>(IEnumerable<T> items, string separetor = " ") => Write(string.Join(separetor, items));
}