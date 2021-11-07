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
            var tka = new List<int[]>();
            for (int i = 0; i < N; i++)
            {
                var c = In.ReadAry<int>().ToArray();
                tka.Add(c);
            }

            //TLE 3/15
            //calc--------------
            long ret = childs(N, new List<int>());
            Out.Write(ret);

            long childs(int Tx, List<int> iUsed)
            {
                long cnt = tka[Tx - 1][0];
                for (int i = 2; i < 2 + tka[Tx - 1][1]; i++) //対応tkaを見て子供dp処理
                {
                    if (!iUsed.Contains(tka[Tx - 1][i]))
                    {
                        iUsed.Add(tka[Tx - 1][i]);
                        cnt += childs(tka[Tx - 1][i], iUsed);
                    }
                }
                return cnt;
            }
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