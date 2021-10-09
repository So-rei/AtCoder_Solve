using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoderApp
{
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        new abc222_d();
    //    }
    //}

    //20211009
    //例は正解だがほとんどWRやTLEになる...なにか抜けてる

    public class abc222_d
    {
        public abc222_d()
        {
            //input-------------
            var N = In.Read<int>();
            var a = In.ReadAry<int>().ToArray();
            var b = In.ReadAry<int>().ToArray();

            //calc----
            long lRet = 0;
            var Ccalced = new Dictionary<(int next, int index), long>(); //途中計算分を記憶
            //初回計算のみループで2回目以降は再帰関数
            for (int i = a[0]; i < b[0] + 1; i++)
            {
                lRet += LoopCnt(i, 1);
            }

            //output----
            Out.Write((long)(lRet % 998244353));
            return;

            long LoopCnt(int bx, int index)
            {
                //計算履歴がある場合はその値
                if (Ccalced.ContainsKey((bx, index)))
                {
                    return Ccalced[(bx, index)];
                }

                long iRet = 0;

                //最終回
                if (index == N - 1)
                {
                    var iMin = Math.Max(a[index], bx);
                    iRet = b[index] - iMin < 0 ? 0 : b[index] - iMin + 1;
                    Ccalced.Add((bx, index), iRet);
                    return iRet;
                }

                //index回目
                for (int bNext = bx; bNext < b[index] + 1; bNext++)
                {
                    iRet += LoopCnt(bNext, index + 1);
                }
                Ccalced.Add((bx, index), iRet);
                return iRet;
            }

        }
    }

    //Common Class--
    //static class In
    //{
    //    public static T Read<T>() { var s = Console.ReadLine(); return (T)Convert.ChangeType(s, typeof(T)); }
    //    public static IEnumerable<T> ReadAry<T>() { return Array.ConvertAll(Console.ReadLine().Split(' '), e => (T)Convert.ChangeType(e, typeof(T))); }
    //    public static IEnumerable<T> ReadMany<T>(long n) { for (long i = 0; i < n; i++) yield return Read<T>(); }
    //}

    //static class Out
    //{
    //    public static void Write<T>(T item) => Console.WriteLine(item);
    //    public static void WriteMany<T>(IEnumerable<T> items, string separetor = " ") => Write(string.Join(separetor, items));
    //}
}