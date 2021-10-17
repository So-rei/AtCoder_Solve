using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoderApp
{
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        new arc128_a();
    //    }
    //}

    //2021/10/17
    //TLE8
    //金、銀で場合分けしなくてもいけるもよう　これじゃ遅い

    public class arc128_a
    {
        public arc128_a()
        {
            //input-------------
            var N = In.Read<int>();
            var a = In.ReadAry<int>().ToArray();

            //calc----
            decimal pp = 1;//i日目に金gMaxグラム:i日目に銀sMaxグラム の比
            int[] gLoot = new int[N];//i日目の//GMaxルート
            int[] sLoot = new int[N];//i日目の//SMaxルート

            //1日目
            gLoot[0] = 0;
            sLoot[0] = 1;
            pp = a[0];

            ////i日目
            //for (int i = 1; i < N; i++)
            //{
            //    //銀100:金1 ->次が5なら、　銀100:金20 = 比率5
            //    if (pp > a[i])
            //    {
            //        sLoot[i] = 0;
            //        for (int ii = 0; ii < i; ii++)
            //            gLoot[ii] = sLoot[ii];
            //        gLoot[i] = 1;
            //    }
            //    //銀100:金1 ->次が1000なら、　銀1000:金1 = 比率1000
            //    else
            //    {
            //        gLoot[i] = 0;
            //        for (int ii = 0; ii < i; ii++)
            //            sLoot[ii] = gLoot[ii];
            //        sLoot[i] = 1;
            //    }
            //    pp = a[i];
            //}
            //これでもまだおそい
            //i日目
            for (int i = 1; i < N; i++)
            {
                //i日目に金gMaxグラム:i日目に銀sMaxグラム の比で判断

                //銀100:金1 ->次が5なら、　銀100:金20 = 比率5
                if (a[i - 1] > a[i])
                {
                    sLoot[i] = 0;
                    Array.Copy(sLoot, 0, gLoot, 0, i);
                    gLoot[i] = 1;
                }
                //銀100:金1 ->次が1000なら、　銀1000:金1 = 比率1000
                else
                {
                    gLoot[i] = 0;
                    Array.Copy(gLoot, 0, sLoot, 0, i);
                    sLoot[i] = 1;
                }
            }

            //output----
            Out.WriteMany(gLoot);
        }
    }
}