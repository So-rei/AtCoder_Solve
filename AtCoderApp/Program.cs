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
            var Chi = childs(N).Distinct();
            long ret = tka[N - 1][0];
            foreach (int i in Chi)
                ret += tka[i - 1][0];
            Out.Write(ret);

            IEnumerable<int> childs(int Tx)
            {
                for (int i = 2; i < 2 + tka[Tx - 1][1]; i++) //対応tkaを見て子供dp処理
                {
                    yield return tka[Tx - 1][i];
                    foreach (var c in childs(tka[Tx - 1][i]))
                        yield return c;
                }
                yield break;
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