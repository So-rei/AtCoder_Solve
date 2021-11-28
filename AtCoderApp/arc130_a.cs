using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoderApp
{

    //2021/11/28
    public class arc130_a
    {
        public arc130_a()
        {
            //input-------------
            var N = In.Read<int>();
            var S = In.Read<string>();

            //output------------
            //文字が連続している箇所を消したときしか条件を満たさないのは明らかで
            //連続箇所の個数nのとき条件を満たすΣ(i,j) = nC2
            long ret = 0; //回答

            int t_cnt = 1;
            char t_c = S[0];
            //foreach (var c in S)
            for (int i = 1; i < S.Length; i++)
            {
                char c = S[i];

                if (t_c == c)
                    t_cnt++;
                else
                {
                    ret += (long)(t_cnt * (t_cnt - 1)) / 2;
                    t_cnt = 1;
                    t_c = c;
                }
            }

            if (t_cnt > 1)
                ret += (long)(t_cnt * (t_cnt - 1)) / 2;

            Out.Write(ret);

            //-------------------------------
        }

    }

}