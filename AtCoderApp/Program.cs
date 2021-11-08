using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoderApp
{
    class Program
    {
        static void Main(string[] args)
        {
            new xxx();
        }
    }

    public class xxx
    {
        public xxx()
        {
            //input-------------
            var N = In.Read<int>();
            var xy = new List<int[]>();
            for (int i = 0; i < N; i++)
            {
                var c = In.ReadAry<int>().ToArray();
                xy.Add(c);
            }

            //output------------
            var ptn = new List<string>(); //魔法の組み合わせ
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (i == j) continue;
                    var cx = xy[i][0] - xy[j][0];
                    var cy = xy[i][1] - xy[j][1];
                    int gcd = Math.Abs(Gcd(cx, cy));//最大公約数 ※マイナスは＋にすることに注意
                    ptn.Add((cx / gcd).ToString() + "," + (cy / gcd).ToString());
                }
            }
            Out.Write(ptn.Distinct().Count());
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