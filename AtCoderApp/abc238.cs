using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtCoderApp
{
    public class abc238_a
    {
        public abc238_a()
        {
            //input-------------
            var N = In.Read<long>();

            //calc--------------
            if (N > 62 || (Math.Pow(2, N) > N * N)) //ばかでかい累乗をしないように省略をいれる
                Out.Write("Yes");
            else
                Out.Write("No");
        }
    }
    public class abc238_b
    {
        public abc238_b()
        {
            //input-------------
            var N = In.Read<int>();
            var A = In.ReadAry<int>();

            //calc--------------
            var radmod = 0; //今どの角度だけずらして切っているか
            var cl = new List<int>();
            cl.Add(0);
            foreach (var a in A)
            {
                radmod = (radmod + a) % 360;
                cl.Add(radmod);
            }
            cl.Sort();

            int max = 0;
            for (int i = 0; i < cl.Count() - 1; i++)
            {
                max = Math.Max(max, cl[i + 1] - cl[i]);
            }
            max = Math.Max(max, 360 - cl.Last()); //最後

            Out.Write(max);
        }
    }

    /// <summary>
    /// TLE
    /// </summary>
    public class abc238_d
    {
        public abc238_d()
        {
            //input-------------
            var T = In.Read<long>();

            var AS = new List<long[]>();
            for (int i = 0; i < T; i++)
            {
                AS.Add(In.ReadAry<long>().ToArray());
            }

            //calc--------------
            for (int i = 0; i < T; i++)
            {
                bool bSuccess = false;
                long a = AS[i][0];
                //x+y=sより x,yの組み合わせは全部でs組考えられる かつxy対偶なのでその半分で良い
                for (long x = a; x < AS[i][1] / 2; x++) //x>a条件で計算量減らす
                {
                    var y = AS[i][1] - x;
                    if ((x & y) == a)
                    {
                        bSuccess = true;
                        break;
                    }
                }

                if (bSuccess)
                    Out.Write("Yes");
                else
                    Out.Write("No");
            }

        }
    }

    //*********************************************
    //C問題はPythonで解いた
    //*********************************************

    //これでもTLE
    public class abc238_d2
    {
        public abc238_d2()
        {
            //input-------------
            var T = In.Read<long>();

            var AS = new List<long[]>();
            for (int i = 0; i < T; i++)
            {
                AS.Add(In.ReadAry<long>().ToArray());
            }

            //calc--------------
            for (int i = 0; i < T; i++)
            {
                bool bSuccess = false;
                long a = AS[i][0];
                long s = AS[i][1];

                //a,s条件チェック
                if (a > 0 && a * 2 > s)
                {
                    Out.Write("No");
                    continue;
                }

                //x=a...s/2まで考えられる
                for (long x = a; x < s / 2; x++)
                {
                    if ((x & (s - x)) == a)
                    {
                        bSuccess = true;
                        break;
                    }
                }

                /*
                 * 解説より...
                 *  a * 2 > sまでは正解。
                 * その後、x=aからs/2まで全てを調べる必要はない。a & (s - 2*a)の一致チェックだけで良い
                 * ex) a=5,s=18のとき、6&12= 0110 & 1100 =0100や、7&11=0111 & 1011 = 0011...と調べる必要がない。
                 *  5&8=0101 & 1000 = が0かどうかだけ調べればよい！（この例はOK. 5 & 13 = 5)
                 */

                if (bSuccess)
                    Out.Write("Yes");
                else
                    Out.Write("No");
            }

        }
    }
}