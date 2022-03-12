using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 2022-03-12
/// </summary>
namespace AtCoderApp
{
    //AC
    public class abc243_a
    {
        public abc243_a()
        {
            //input-------------
            var b = In.ReadAry<int>().ToArray();
            (var V, var A, var B, var C) = (b[0], b[1], b[2], b[3]);

            //calc--------------
            var mod = V % (A + B + C);
            if (mod < A)
            {
                Out.Write("F");
                return;
            }
            else if (mod < A + B)
            {
                Out.Write("M");
                return;
            }
            Out.Write("T");
            return;
        }
    }

    //AC
    public class abc243_b
    {
        public abc243_b()
        {
            //input-------------
            var N = In.Read<int>();
            var A = In.ReadAry<int>().ToArray();
            var B = In.ReadAry<int>().ToArray();

            //calc--------------
            var cnt1 = 0;
            var cnt2 = 0;
            for (int i = 0; i < N; i++)
            {
                if (A[i] == B[i])
                    cnt1++;
                else if (B.Contains(A[i]))
                    cnt2++;
            }

            Out.Write(cnt1);
            Out.Write(cnt2);
            return;
        }
    }

    //1000000001のけた1個間違えててWR1
    public class abc243_c
    {
        public abc243_c()
        {
            //input-------------
            var N = In.Read<int>();
            var XY = new List<(int X, int Y)>();
            for (int i = 0; i < N; i++)
            {
                var c = In.ReadAry<int>().ToArray();
                XY.Add((c[0], c[1]));
            }
            var S = In.Read<string>();

            //calc--------------

            var xys = new List<(int X, int Y, char S)>();
            for (int i = 0; i < N; i++)
                xys.Add((XY[i].X, XY[i].Y, S[i]));

            var _xys = xys.OrderBy(p => p.Y).ToList();
            //そのy軸で一番左にあるRと一番右にあるL
            var YC = -1;
            var xRmin = 1000000001;
            var xLmax = -1;
            foreach (var c in _xys)
            {
                if (YC == c.Y)
                {
                    if (c.S == 'R')
                        xRmin = Math.Min(xRmin, c.X);
                    else
                        xLmax = Math.Max(xLmax, c.X);
                }
                else
                {
                    if (xRmin < xLmax) //衝突発生
                        break;

                    YC = c.Y;
                    if (c.S == 'R')
                    {
                        xRmin = c.X;
                        xLmax = -1;
                    }
                    else
                    {
                        xRmin = 1000000001;
                        xLmax = c.X;
                    }
                }
            }

            if (xRmin < xLmax) //衝突発生
                Out.Write("Yes");
            else
                Out.Write("No");

        }
    }

}