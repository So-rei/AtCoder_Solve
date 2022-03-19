using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtCoderApp
{
    //2022-03-19
    //AC
    public class arc137_a
    {
        public arc137_a()
        {
            //input-------------
            var LR = In.ReadAry<long>().ToArray();
            (var L, var R) = (LR[0], LR[1]);

            //calc--------------
            long max = 0;
            for (long i = L; i < R; i++)
            {
                if ((R - i) < max) break;

                //両端から調べていく
                for (long j = R; j > i; j--)
                {
                    if ((j - i) < max) break;

                    if (Gcd(j, i) == 1)//iとjが互いに素かどうか
                        max = Math.Max(max, j - i);
                }
            }
            Out.Write(max);


            static long Gcd(long m, long n)
            {
                if (n == 0) return m;
                return Gcd(n, m % n);
            }

        }
    }
    //4AC 22TLE
    public class arc137_b
    {
        public arc137_b()
        {
            //input-------------
            var N = In.Read<int>();
            //var A = In.ReadAry<int>().ToArray();
            //Aは文字列として取得する
            var A = In.Read<string>().Replace(" ", "");

            //calc--------------
            var li = new List<int>();
            li.Add(A.Count(p => p == '1')); //部分列空の場合

            //A(i)～A(i+j)のNC2通りを計算
            for (int i = 0; i < N; i++)
            {
                for (int j = 1; i + j <= N; j++)
                {
                    int rest = A.Substring(0, i).Count(p => p == '1') + A.Substring(i + j).Count(p => p == '1');
                    int zerocnt = A.Substring(i, j).Count(p => p == '0');
                    int cnt1 = rest + zerocnt;
                    if (!li.Contains(cnt1))
                        li.Add(cnt1);
                }
            }
            Out.Write(li.Count());
        }
    }

    //4AC 22TLE
    //数列でも文字列でも同じ...もとの計算量が多すぎるっぽい
    public class arc137_b_2
    {
        public arc137_b_2()
        {
            //input-------------
            var N = In.Read<int>();
            var A = In.ReadAry<int>().ToArray();

            //calc--------------
            var li = new List<int>();
            li.Add(A.Sum()); //部分列空の場合

            //A(i)～A(i+j)のNC2通りを計算
            for (int i = 0; i < N; i++)
            {
                for (int j = 1; i + j <= N; j++)
                {
                    int rest = A.Take(i).Sum() + A.Skip(i + j).Sum();
                    int zerocnt = A.Skip(i).Take(j).Sum();
                    int cnt1 = rest + (j - zerocnt);
                    if (!li.Contains(cnt1))
                        li.Add(cnt1);
                }
            }
            Out.Write(li.Count());
        }
    }

    //AC
    public class arc137_c
    {
        public arc137_c()
        {
            //input-------------
            var N = In.Read<int>();
            var A = In.ReadAry<int>().ToArray();

            //calc--------------
            //ex)
            //2個,2 3でAlice必勝
            //4 x でも6 xでも必勝
            //ex2)
            //3個,0 1 3(最長1)でAlice必勝
            //1 3 5(最長3) でもAlice必勝
            //5 6 7(最長5) でもAlice必勝

            //最長必要手数は容易に計算できる
            long maxhand = A[0];
            for (int i = 1; i < N; i++)
            {
                maxhand += A[i] - A[i - 1] - 1;
            }
            //A(N-1)とA(N)の間があるかどうか = 先手が最長必要手数を操作できるかどうか
            //0または、最長手数が偶数＆操作できないとき、後手勝利            
            if (maxhand == 0 || (maxhand % 2 == 0 && A[N - 1] - A[N - 2] == 1))
                Out.Write("Bob");
            else
                Out.Write("Alice");

        }
    }
}