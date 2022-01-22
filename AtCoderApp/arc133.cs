using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 2022-01-22
/// </summary>
namespace AtCoderApp
{
    //AC*
    public class arc133_a
    {
        public arc133_a()
        {
            //input-------------
            var N = In.Read<int>();
            var A = In.ReadAry<int>().ToArray();

            //calc--------------
            //最初に減った場所を探し、その減る前の値=xとする
            int x = A[N - 1];//一度も減少しなかった場合は最後の数字を消す
            for (int i = 0; i < N - 1; i++)
            {
                if (A[i] > A[i + 1])
                {
                    x = A[i];
                    break;
                }
            }

            Out.WriteMany(A.ToList().Where(p => p != x).ToArray());
        }
    }

    //AC6 TLE22
    public class arc133_b
    {
        public arc133_b()
        {
            //input-------------
            var N = In.Read<int>();
            var P = In.ReadAry<int>().ToArray();
            var Q = In.ReadAry<int>().ToArray();

            //calc--------------
            //NC2,NC3,NC4...と数えていくのは現実的に無理
            //漏れなく最短で見れるようなロジックを考える
            int r = 0;//解
            Calc_Main(0, 0);

            Out.Write(r);
            return;

            void Calc_Main(int pindex, int qindex, int tr = 0)
            {
                if (N <= Math.Max(pindex, qindex)) return;//index最後終了
                if (r - tr > N - Math.Max(pindex, qindex)) return;//r以上になる見込みがないときは終了

                //Q[qindex]を使うケースを計算
                for (int p = pindex; p < N; p++)
                {
                    if (Q[qindex] % P[p] == 0)
                    {
                        r = Math.Max(r, tr + 1);
                        Calc_Main(p + 1, qindex + 1, tr + 1); //使うケース
                        break;
                    }
                }

                Calc_Main(pindex, qindex + 1, tr);//Q[qindex]を使わなかったケースを足す
            }
        }
    }

    //AC4 TLE24
    public class arc133_b2
    {
        public arc133_b2()
        {
            //input-------------
            var N = In.Read<int>();
            var P = In.ReadAry<int>().ToArray();
            var Q = In.ReadAry<int>().ToArray();

            //calc--------------
            //NC2,NC3,NC4...と数えていくのは現実的に無理
            //漏れなく最短で見れるようなロジックを考える
            int r = 0;//解
            r = Calc_Main(0, 0);

            Out.Write(r);
            return;

            int Calc_Main(int pindex, int qindex)
            {
                if (N <= Math.Max(pindex, qindex)) return 0;//index最後終了

                int tmp_r = 0;
                //Q[qindex]を使うケースを計算
                for (int p = pindex; p < N; p++)
                {
                    if (Q[qindex] % P[p] == 0)
                    {
                        tmp_r = 1 + Calc_Main(p + 1, qindex + 1); //使うケース
                        break;
                    }
                }

                return Math.Max(tmp_r, Calc_Main(pindex, qindex + 1));//Q[qindex]を使わなかったケースを足す
            }
        }
    }

    //AC10 WA14
    public class arc133_c
    {
        public arc133_c()
        {
            //input-------------
            var hwk = In.ReadAry<int>().ToArray();
            (var H, var W, var K) = (hwk[0], hwk[1], hwk[2]);
            var A = In.ReadAry<int>().ToArray();
            var B = In.ReadAry<int>().ToArray();

            //calc--------------
            //タテヨコ全てK-1であったときが最大値であることは明らか
            //その値から最小いくら減らせば条件を満たすかを考える
            int amod = (K - 1) * W % K;//ヨコ1行全てがK-1だったとき、そのmod
            int bmod = (K - 1) * H % K;//タテ1列全てがK-1だったとき、そのmod

            long acheck = 0; //A条件をすべて満たすために最低限減らさないといけない量
            for (int a = 0; a < H; a++)
            {
                if (amod >= A[a])
                    acheck += amod - A[a];
                else
                    acheck += amod + K - A[a];
            }
            long bcheck = 0; //B条件をすべて満たすために最低限減らさないといけない量
            for (int b = 0; b < W; b++)
            {
                if (bmod >= B[b])
                    bcheck += bmod - B[b];
                else
                    bcheck += bmod + K - B[b];
            }

            if (Math.Abs(acheck - bcheck) % K != 0)//A条件とB条件が噛み合わないときはエラー-1
                Out.Write(-1);
            else
            {
                long r = (K - 1) * H * W - Math.Max(acheck, bcheck);
                Out.Write(r);
            }
        }
    }
}