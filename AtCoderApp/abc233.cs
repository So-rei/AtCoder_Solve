//2021-12-25
//abc 233
//No Rated

using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoderApp
{
    public class abc233_d
    {
        //これだと当然TLE TLE23/AC7
        public abc233_d()
        {
            //input-------------
            var NK = In.ReadAry<long>().ToArray();
            var (N, K) = (NK[0], NK[1]);
            var A = In.ReadAry<Int64>().ToArray();

            //calc--------------
            var cnt = 0;
            for (int i = 0; i < N; i++)
            {
                for (int j = i; j < N; j++)
                {
                    if (A.Skip(i).Take(j - i + 1).Sum() == K)
                        cnt++;
                }
            }

            Out.Write(cnt);

        }
    }
}