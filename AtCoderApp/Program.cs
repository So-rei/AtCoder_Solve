﻿using System;
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
            var NQ = In.ReadAry<int>().ToArray();
            int N = NQ[0];
            int Q = NQ[1];

            var query = new (int q, int x, int y)[Q];
            for (int i = 0; i < Q; i++)
            {
                var _q = In.ReadAry<int>().ToArray();
                query[i].q = _q[0];
                query[i].x = _q[1];
                if (_q[0] < 3)
                    query[i].y = _q[2];
            }

            //calc--------------
            var grp = new List<List<int>>();
            for (int i = 0; i < Q; i++)
            {
                switch (query[i].q)
                {
                    case 1: //連結
                        int xindex = -1, yindex = -1;
                        for (int c = 0; c < grp.Count(); c++)
                        {
                            if (grp[c].Last() == query[i].x)
                                xindex = c;
                            if (grp[c].First() == query[i].y)
                                yindex = c;

                            if (xindex > 0 && yindex > 0) break;
                        }

                        //両方連結データにある
                        if (xindex > -1 && yindex > -1)
                        {
                            grp[xindex].AddRange(grp[yindex]);
                            grp.RemoveAt(yindex);
                        }
                        else if (xindex > -1) //xがある
                            grp[xindex].Add(query[i].y);
                        else if (yindex > -1) //yがある
                            grp[yindex].Insert(0, query[i].x);
                        else
                            //無い時は新規
                            grp.Add(new List<int> { query[i].x, query[i].y });

                        break;

                    case 2: //分割
                        for (int x = 0; x < grp.Count(); x++)
                        {
                            var index = grp[x].IndexOf(query[i].x);
                            if (index < 0) continue;

                            //データ分割
                            var bottom = grp[x].Skip(index + 1).ToList();

                            if (index == 0)
                                grp.RemoveAt(x);//計算量削減用(長さ1を削除する)
                            else
                                grp[x] = grp[x].Take(index + 1).ToList();

                            if (bottom.Count > 1)//計算量削減用(長さ1は不要)
                                grp.Add(bottom);

                            break;
                        }
                        break;

                    case 3:
                        bool bNotOut = true;
                        foreach (var g in grp)
                        {
                            //連結データにある時は先頭から出力
                            if (g.Contains(query[i].x))
                            {
                                bNotOut = false;
                                Out.Write(g.Count().ToString() + " " + String.Join(' ', g));
                                break;
                            }
                        }

                        //無い時は単独
                        if (bNotOut)
                            Out.Write("1 " + query[i].x);
                        break;
                }
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