//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace AtCoderApp
//{
//    //class Program
//    //{
//    //    static void Main(string[] args)
//    //    {
//    //        new aB220_c();
//    //    }
//    //}

//20210926 これだとTLE2発生
//ちゃんと孫計算まで出来るようにしないと多分ダメ

//    public class aB220_c
//    {
//        public aB220_c()
//        {
//            //input-------------
//            var N = In.Read<int>();
//            var AN = In.ReadAry<long>().ToArray();
//            long X = In.Read<long>();

//            //calc----
//            const int Split = 10000;
//            //分割して合計を足して計算量削減する
//            if (N < Split)
//            {
//                //1回
//                Out.Write(Calc(AN));
//            }
//            else
//            {
//                var SumAN = new List<(long ary, int cnt)>();
//                for (var i = 0; i <= Math.Ceiling((double)(N / Split)); i++)
//                {
//                    var tmpAry = AN.Where((p, index) => index >= (i * Split) && index < ((i + 1) * Split));
//                    SumAN.Add((tmpAry.Sum(), tmpAry.Count()));
//                }

//                var tmp_ret = ArrayCalc(SumAN);

//                //ex)3つの配列で5とでたら2周めのindex1個目で超えたということ なので..
//                var Lastindex = tmp_ret.loopCnt % SumAN.Count();
//                var LastArray = AN.Where((p, index) => index >= (Lastindex * Split) && index < ((Lastindex + 1) * Split));

//                Out.Write(tmp_ret.Cnt + Calc(LastArray, tmp_ret.Sum));
//            }

//            //local c
//            int Calc(IEnumerable<long> cAList, long startValue = 0)
//            {
//                int loopCnt = 0;
//                long cSum = startValue;

//                while (true)
//                {
//                    foreach (var c in cAList)
//                    {
//                        if (cSum + c > X) return loopCnt + 1;
//                        cSum += c;
//                        loopCnt++;
//                    }
//                }
//            }
//            //local c
//            (int loopCnt, int Cnt, long Sum) ArrayCalc(IEnumerable<(long ary, int cnt)> cAList)
//            {
//                int loopCnt = 0;
//                int Cnt = 0;
//                long cSum = 0;

//                while (true)
//                {
//                    foreach (var c in cAList)
//                    {
//                        if (cSum + c.ary > X) return (loopCnt, Cnt, cSum);
//                        cSum += c.ary;
//                        loopCnt++;

//                        Cnt += c.cnt;
//                    }
//                }
//            }
//        }
//    }

//    //Common Class--
//    static class In
//    {
//        public static T Read<T>() { var s = Console.ReadLine(); return (T)Convert.ChangeType(s, typeof(T)); }
//        public static IEnumerable<T> ReadAry<T>() { return Array.ConvertAll(Console.ReadLine().Split(' '), e => (T)Convert.ChangeType(e, typeof(T))); }
//        public static IEnumerable<T> ReadMany<T>(long n) { for (long i = 0; i < n; i++) yield return Read<T>(); }
//    }

//    static class Out
//    {
//        public static void Write<T>(T item) => Console.WriteLine(item);
//        public static void WriteMany<T>(IEnumerable<T> items, string separetor = " ") => Write(string.Join(separetor, items));
//    }
//}