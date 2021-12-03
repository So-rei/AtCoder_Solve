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
            var ND = In.ReadAry<int>().ToArray();
            var N = ND[0];
            var D = ND[1];

            var LR = In.ReadManyAry<int>(N).ToArray();

            int k = 0;
            k++;
            //-------------------------------
        }
    }

}

//Common Class--
static class In
{
    public static T Read<T>() { var s = Console.ReadLine(); return (T)Convert.ChangeType(s, typeof(T)); }
    public static IEnumerable<T> ReadAry<T>() { return Array.ConvertAll(Console.ReadLine().Split(' '), e => (T)Convert.ChangeType(e, typeof(T))); }
    public static IEnumerable<T> ReadMany<T>(long n) { for (long i = 0; i < n; i++) yield return Read<T>(); }
    public static IEnumerable<IList<T>> ReadManyAry<T>(long n) { for (long i = 0; i < n; i++) yield return ReadAry<T>().ToArray(); }
}

static class Out
{
    public static void Write<T>(T item) => Console.WriteLine(item);
    public static void WriteMany<T>(IEnumerable<T> items, string separetor = " ") => Write(string.Join(separetor, items));
}