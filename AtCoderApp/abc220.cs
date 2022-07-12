using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoderApp
{
    public class abc220_a
    {
        public abc220_a()
        {
            //input-------------
            var ABC = In.ReadAry<int>().ToArray();
            decimal A = ABC[0];
            decimal B = ABC[1];
            decimal C = ABC[2];

            //out
            if (C > B)
            {
                Out.Write(-1);
                return;
            }

            var d = Math.Ceiling(A / C);
            var r = d * C;
            if (r > B)
            {
                Out.Write(-1);
                return;
            }

            Out.Write(r);

        }
    }
    public class abc220_b
    {
        public abc220_b()
        {
            //input-------------
            var K = In.Read<int>();
            var AB = In.ReadAry<string>().ToArray();
            string A = AB[0];
            string B = AB[1];

            //out----
            long iA = Calc(A);
            long iB = Calc(B);

            Out.Write((long)(iA * iB));

            //local c
            int Calc(string _AB)
            {
                int ret = 0;
                int digit = 0;
                for (var i = _AB.Length - 1; i >= 0; i--)
                {
                    ret += (int)Math.Pow(K, digit) * Convert.ToInt32(_AB[i].ToString());

                    digit++;
                }
                return ret;
            }
        }
    }


    ////20210926 これだとTLE2発生
    ////ちゃんと孫計算まで出来るようにしないと多分ダメ
    public class abc220_c
    {
        public abc220_c()
        {
            //input-------------
            var N = In.Read<int>();
            var AN = In.ReadAry<long>().ToArray();
            long X = In.Read<long>();

            //calc----
            const int Split = 10000;
            //分割して合計を足して計算量削減する
            if (N < Split)
            {
                //1回
                Out.Write(Calc(AN));
            }
            else
            {
                var SumAN = new List<(long ary, int cnt)>();
                for (var i = 0; i <= Math.Ceiling((double)(N / Split)); i++)
                {
                    var tmpAry = AN.Where((p, index) => index >= (i * Split) && index < ((i + 1) * Split));
                    SumAN.Add((tmpAry.Sum(), tmpAry.Count()));
                }

                var tmp_ret = ArrayCalc(SumAN);

                //ex)3つの配列で5とでたら2周めのindex1個目で超えたということ なので..
                var Lastindex = tmp_ret.loopCnt % SumAN.Count();
                var LastArray = AN.Where((p, index) => index >= (Lastindex * Split) && index < ((Lastindex + 1) * Split));

                Out.Write(tmp_ret.Cnt + Calc(LastArray, tmp_ret.Sum));
            }

            //local c
            int Calc(IEnumerable<long> cAList, long startValue = 0)
            {
                int loopCnt = 0;
                long cSum = startValue;

                while (true)
                {
                    foreach (var c in cAList)
                    {
                        if (cSum + c > X) return loopCnt + 1;
                        cSum += c;
                        loopCnt++;
                    }
                }
            }
            //local c
            (int loopCnt, int Cnt, long Sum) ArrayCalc(IEnumerable<(long ary, int cnt)> cAList)
            {
                int loopCnt = 0;
                int Cnt = 0;
                long cSum = 0;

                while (true)
                {
                    foreach (var c in cAList)
                    {
                        if (cSum + c.ary > X) return (loopCnt, Cnt, cSum);
                        cSum += c.ary;
                        loopCnt++;

                        Cnt += c.cnt;
                    }
                }
            }
        }
    }

    //AC
    public class abc220_c2
    {
        public abc220_c2()
        {
            //input-------------
            var N = In.Read<int>();
            var AN = In.ReadAry<long>().ToArray();
            long X = In.Read<long>();

            //calc----
            const int Split = 64; //だいたいこのぐらいまで行けば十分なのでは
            //分割して合計を足して計算量削減する

            //2周目があるかどうかはすぐ分かるので除去
            long cX = X % AN.Sum();
            long Cnt = (X / AN.Sum()) * N;

            HalfCalc(AN, cX, Cnt);
            return;

            //ローカル関数------------------------------------------------------------------
            //途中で「配列の順番」をもとに処理してるので、①②を両方IEnumerableで処理してはダメ！！
            //配列、IReadOnlyCollection、ListならOK
            void HalfCalc(long[] AList, long cX, long Cnt) //①
            {
                if (AList.Count() < Split) //Split個以下
                {
                    LastCalc(AList, cX, Cnt);
                    return;
                }

                //Split個以上の場合、分割計算
                var ANhalf1 = AList.Where((p, index) => index < Math.Ceiling((double)AList.Count() / 2)).ToArray(); //②
                //var ANhalf1 = AList[0..(int)(AList.Count() / 2 + 1)]; //c# 8.0～

                if (cX - ANhalf1.Sum() < 0)//前半でおわる
                    HalfCalc(ANhalf1, cX, Cnt);
                else //後半
                {
                    cX -= ANhalf1.Sum();
                    Cnt += ANhalf1.Count();

                    var ANhalf2 = AList.Where((p, index) => index >= Math.Ceiling((double)AList.Count() / 2)).ToArray();
                    //var ANhalf2 = AList[(int)(AList.Count() / 2 + 1)..]; //c# 8.0～
                    HalfCalc(ANhalf2, cX, Cnt);
                }
                return;
            }

            //最後の部分配列、残り数値、今までの回数
            //最後は少量だしForEachでもいいようなループしかしてないので、IEnumerableで受けてもオーバーしない
            void LastCalc(long[] AList, long cX, long Cnt)
            {
                for (int i = 0; i < AList.Length; i++)
                {
                    cX -= AList[i];
                    if (cX < 0)
                    {
                        Out.Write((long)(Cnt + i + 1));
                        return;
                    }
                }
            }
        }
    }

    //2022-07-13 練習中
    //AC
    public class abc220_d
    {
        public abc220_d()
        {
            //input-------------
            var N = In.Read<int>();
            var A = In.ReadAry<int>().ToArray();

            long MOD = 998244353;
            //output------------
            //当然、2N-1通りまじめにやるわけにはいかない
            //が、x+yとxyを関連付けるような数学的定理はあんまりない
            //つまり、 (x+y)%10 == (xy)%10 のとき1通りとするような処理が数学的に自動ではできない。普通に%計算するしかない
            //DPで(0,1...9)の10パターン * N　とすればO(N)で間に合うはず

            var dp = new long[N, 10];//i回目がおわったあと、一番左がt(0～9)である組み合わせの数
            dp[0, A[0]] = 1;//0回目の状態
            for (int i = 0; i < N - 1; i++)
                GetPtn(i);

            void GetPtn(int n)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (dp[n, i] == 0) continue;

                    int f = (i + A[n + 1]) % 10;
                    int g = (i * A[n + 1]) % 10;
                    dp[n + 1, f] += dp[n, i];
                    dp[n + 1, g] += dp[n, i];
                    dp[n + 1, f] %= MOD; //足してからMODを取る!
                    dp[n + 1, g] %= MOD;
                }
            }

            //結果
            for (int i = 0; i < 10; i++)
                Out.Write(dp[N - 1, i]);

        }
    }
}