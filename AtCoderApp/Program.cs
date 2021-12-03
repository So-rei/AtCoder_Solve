using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoderApp
{
    class Program
    {
        static void Main(string[] args)
        {
            new abc230_dx();
        }
    }

    public class abc230_dx
    {
        public abc230_dx()
        {
            //input-------------
            var ND = In.ReadAry<int>().ToArray();
            var N = ND[0];
            var D = ND[1];

            var LR = new int[N][];
            for (int i = 0; i < N; i++)
            {
                var _LR = In.ReadAry<int>().ToArray();
                LR[i] = (new int[]{ _LR[0], _LR[1] });
            }

            //output------------
            LR = LR.OrderBy(p => p[1]).ToArray();

            int cnt = 0;
            int right = 0;
            for (int i = 0; i < N; i++)
            {
                if (right < LR[i][0]) //まだ殴られていない壁だったら
                {
                    right = LR[i][1] + D - 1; //そこが左端にかかるように殴る
                    cnt++;
                }
            }

            //while (true)
            //{
            //    var point = LR.Min(p => p[1]) + D - 1; //一番左にある壁の右端を殴る
            //    LR = LR.Where(p => p[0] > point).ToArray(); //壊れたやつは削除             
            //    cnt++;
            //    if (LR.Count() == 0) break;
            //}

            Out.Write(cnt);
            
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
    public static IEnumerable<IEnumerable<T>> ReadManyAry<T>(long n) { for (long i = 0; i < n; i++) yield return ReadAry<T>(); }
}

static class Out
{
    public static void Write<T>(T item) => Console.WriteLine(item);
    public static void WriteMany<T>(IEnumerable<T> items, string separetor = " ") => Write(string.Join(separetor, items));
}