using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoderApp
{

    public class ac129
    {
        public ac129()
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

            //桁数ごとにループで数える(AC)
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