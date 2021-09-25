using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoderApp
{
    class Program
    {
        static void Main(string[] args)
        {
            new arc218();
        }
    }

    public class arc218
    {
        public arc218()
        {
            //input-------------
            var N = In.Read<long>();
            var Ndigit = N.ToString().Length;


            Out.Write("");

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
}