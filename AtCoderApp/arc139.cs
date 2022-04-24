using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtCoderApp
{
    public class arc139_a
    {
        public arc139_a()
        {
            //input-------------
            var N = In.Read<int>();
            var T = In.ReadAry<int>().ToArray();

            //output------------

            //結果X (文字列として扱うために上bitつけとく
            long X = 0b1000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000;
            foreach (var t in T)
            {
                //0のときは末尾をきにしなくて良い
                if (t == 0)
                {
                    X += 1 + X % 2;
                    //Out.Write(Convert.ToString(X - 0b1000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000, 10));
                    continue;
                }

                var xs = Convert.ToString(X, 2);
                long xc = Convert.ToInt64(String.Join("", xs.Skip(xs.Length - t)), 2);
                X += (long)Math.Pow(2, t) - xc;//ex) 0b1010の次2がきたら、 -> 2^2 - 0b10 = 2追加する
                if (X % (long)Math.Pow(2, t + 1) == 0) X += (long)Math.Pow(2, t); //上のロジックだけだと、0b0101のとき次2がきたらとき0b1000になりNG。0b1100にする。

                //Out.Write(Convert.ToString(X - 0b1000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000, 10));
            }

            X -= 0b1000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000;
            Out.Write(Convert.ToString(X, 10));
        }
    }
    //単純にこうだとTLE
    public class arc139_b
    {
        public arc139_b()
        {
            //input-------------
            var T = In.Read<int>();
            var C = new (long N, long A, long B, long X, long Y, long Z)[T];
            for (int i = 0; i < T; i++)
                (C[i].N, C[i].A, C[i].B, C[i].X, C[i].Y, C[i].Z) = In.ReadTuple6<int>();

            //output------------
            foreach (var cc in C)
            {
                var kn = new long[cc.N + 1];//ナップザック
                kn[0] = 0;
                for (int i = 1; i <= cc.N; i++)
                {
                    var kmin = kn[i - 1] + cc.X;
                    if (i - cc.A >= 0) kmin = Math.Min(kmin, kn[i - cc.A] + cc.Y);
                    if (i - cc.B >= 0) kmin = Math.Min(kmin, kn[i - cc.B] + cc.Z);
                    kn[i] = kmin;
                }

                Out.Write(kn[cc.N]);
            }
        }
    }

    //最も効率いいやつを出来るだけつめてからナップザックにしてみたが、まだTLE
    //うーん
    //gcdは最大公約数
    public class arc139_b_2
    {
        public arc139_b_2()
        {
            //input-------------
            var T = In.Read<int>();
            var C = new (long N, long A, long B, long X, long Y, long Z)[T];
            for (int i = 0; i < T; i++)
                (C[i].N, C[i].A, C[i].B, C[i].X, C[i].Y, C[i].Z) = In.ReadTuple6<int>();

            //output------------
            for (int i = 0; i < T; i++)
            {
                var Xy = C[i].X * C[i].A <= C[i].Y;
                var Xz = C[i].X * C[i].B <= C[i].Z;


                if (Xy || Xz)
                {
                    //Xが最も一番効率いいなら即終了
                    if (Xy && Xz)
                    {
                        Out.Write(C[i].X * C[i].N);
                        continue;
                    }
                    //Y>Z>XまたはZ>Y>Xの場合も即終了
                    else if (Xy)
                    {
                        long cn = (C[i].N - C[i].N % C[i].B) / C[i].B;
                        Out.Write(cn * C[i].Z + (C[i].N - cn * C[i].B) * C[i].X);
                        continue;
                    }
                    else if (Xz)
                    {
                        long cn = (C[i].N - C[i].N % C[i].A) / C[i].A;
                        Out.Write(cn * C[i].Y + (C[i].N - cn * C[i].A) * C[i].X);
                        continue;
                    }
                }

                //それ以外の時、一番効率いいやつ詰める
                if ((double)C[i].A / (double)C[i].Y > (double)C[i].B / (double)C[i].Z)
                    //X>Y>Z
                    Out.Write(SetKn(C[i]));
                else
                {
                    //Y>X>Z
                    long ab = C[i].B;
                    C[i].B = C[i].A;
                    C[i].A = ab;
                    long yz = C[i].Y;
                    C[i].Y = C[i].Z;
                    C[i].Z = yz;
                    Out.Write(SetKn(C[i]));
                }
            }

            //a/y -> b/zの場合---------
            //ex)N=12のとき、5,5,1,1 より 5,3,3,1の方がいい可能性がある
            //この処理のため、途中からナップザックする
            //効率が良い方=a/y
            long SetKn((long n, long a, long b, long x, long y, long z) C)
            {
                long cnt = Math.Max(0, ((C.n - C.n % C.a) / C.a) - Gcd(C.a, C.b) / C.a);

                var kn = new long[C.n + 1];
                kn[cnt * C.a] = cnt * C.y;

                for (var i = cnt * C.a + 1; i <= C.n; i++)
                {
                    var kmin = kn[i - 1] + C.x;
                    if (i - C.a >= cnt * C.a) kmin = Math.Min(kmin, kn[i - C.a] + C.y);
                    if (i - C.b >= cnt * C.a) kmin = Math.Min(kmin, kn[i - C.b] + C.z);
                    kn[i] = kmin;
                }

                return kn[C.n];
            }

            //gcd
            static long Gcd(long a, long b)
            {
                return a > b ? GcdRecursive(a, b) : GcdRecursive(b, a);
            }

            static long GcdRecursive(long a, long b)
            {
                return b == 0 ? a : GcdRecursive(b, a % b);
            }
        }
    }
}