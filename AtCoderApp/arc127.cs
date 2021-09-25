using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoderApp
{
    public class arc217
    {
        public arc217()
        {
            //input-------------
            var N = In.Read<long>();
            var Ndigit = N.ToString().Length;

            //output------------
            long Sum = 0;

            //K(1) = Σf(n) n1->9 = 1
            //K(2) = Σf(n) n10->99 = 1 * 9 + 2 * 1 = 11
            //K(3) = Σf(n) n100->999 = 1 * 90 + 2 * 9 + 3 * 1 = 111
            //K(i) = 1111...1

            bool restFlg = N.ToString()[0] == '1';
            var maxK = Ndigit + (restFlg ? 0 : 1);
            //ex. 555
            // = K(1) + K(2) + K(3) = 123
            //ex. 125
            // = K(1) + K(2) + Σf(n) n100 ->125 
            for (var i = 1; i < maxK; i++)
            {
                long Ki = 0;
                for (var d = 1; d <= i; d++)
                    Ki += (long)Math.Pow(10, (d - 1));

                Sum += Ki;
            }

            //Kで計算できない半端があるかどうか
            //MaxOne = 連続する1の回数
            int MaxOne = 0;
            while (MaxOne < Ndigit && N.ToString()[MaxOne] == '1')
                MaxOne++;

            if (MaxOne != 0)
            {
                //111..1の次の桁が0かどうか
                bool bZero = (MaxOne < Ndigit && N.ToString()[MaxOne] == '0');

                //ex. 125 → MaxOne = 1, bZero = False
                // Σf(n) n=100 ->125 = 26 + 10 + 1 = 37
                //ex. 1127 → MaxOne = 2, bZero = False
                // Σf(n) n=1000 ->1127 = 128 + 28 + 10 + 1 = 167
                //ex. 11031 → MaxOne = 2, bZero = True
                // Σf(n) n=10000 ->11031 = 1032 + 32 = 1064
                //ex.11201 → MaxOne = 2, bZero = False
                //Σf(n) n10000 ->11201 = 1202 + 202 + 100 + 10 + 1 = 1515
                //ex. 1111 → MaxOne = 4, bZero = False
                // Σf(n) n=1000 ->1111 = 112 + 12 + 2 + 1 = 127

                for (var i = 1; i <= MaxOne; i++)
                {
                    Sum += ((N % (long)Math.Pow(10, Ndigit - i)) + 1);
                }

                if (!bZero)
                {
                    for (var n = MaxOne + 1; n <= Ndigit; n++)
                    {
                        Sum += (long)Math.Pow(10, Ndigit - n);
                    }
                }
            }

            Out.Write(Sum);

        }
    }
}