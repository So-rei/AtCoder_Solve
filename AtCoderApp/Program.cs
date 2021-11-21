﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoderApp
{
    class Program
    {
        static void Main(string[] args)
        {
            new ac129_a();
        }
    }

    //2021/11/21
    //AC5 WA6 TLE7

    public class ac129_a
    {
        public ac129_a()
        {
            //input-------------
            var NLR = In.ReadAry<long>().ToArray();
            long N = NLR[0];
            long L = NLR[1];
            long R = NLR[2];

            //output------------
            long cnt = 0;

            //当然これだとTLEなので..
            //for (long i = L; i <=R; i++)
            //{
            //    if ((i ^ N) < N)
            //        cnt++;
            //}

            //これでもTLE、WAも出る（？）
            //Nよりも多い桁数(2進)の値なら必ず条件を満たさないことを利用
            //Nと同じ桁数は必ず条件を満たすことを利用
            //int digit = Convert.ToString(N, 2).Count();
            //long th1 = Convert.ToInt64(Math.Pow(2, digit - 1));
            //long th2 = Convert.ToInt64(Math.Pow(2, digit));
            //for (long i = L; i < Math.Min(th1, R); i++)
            //{
            //    if ((i ^ N) < N)
            //        cnt++;
            //}
            //if (th1 <= R)
            //    cnt += Math.Min(th2 - 1, R) - th1 + 1;

            //桁数ごとにループで数える
            string Nstr = Convert.ToString(N, 2);
            int digit = Nstr.Count();

            for (int d = 0; d < digit; d++)
            {
                if (Nstr[digit - 1 - d] == '1') //下からd桁目が1なら,x=1000...1111(2^d個)は全部Nより小さくなる
                {
                    long rMax = Math.Min(Convert.ToInt64(Math.Pow(2, d + 1)) - 1, R);
                    long rMin = Math.Max(Convert.ToInt64(Math.Pow(2, d)), L);
                    if (rMax >= rMin)
                        cnt += rMax - rMin + 1;
                }
            }

            Out.Write(cnt);
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