using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtCoderApp
{

    //WA
    public class arc141_a
    {
        public arc141_a()
        {
            //input-------------
            var T = In.Read<int>();
            var N = In.ReadMany<string>(T).ToArray();

            //output------------

            for (int i = 0; i < T; i++)
            {
                int di = N[i].Length;
                long imax = Convert.ToInt64(new string(N[i][0], di)); //デフォルト値:周期1のとき

                for (int t = 2; t <= di / 2; t++)
                {
                    //周期2以上でちょうど割り切れる周期tのとき
                    if (di % t == 0)
                    {
                        //ex) 434245 -> 424242
                        //ex) 454647 -> 454545
                        long sp0 = Convert.ToInt32(N[i].Substring(0, t));
                        bool isUnder = false;
                        for (int r = t; r < di; r += t)
                        {
                            if (sp0 > Convert.ToInt32(N[i].Substring(r, t)))
                            {
                                isUnder = true;
                                break;
                            }
                        }
                        if (isUnder) sp0--;
                        long rp = Convert.ToInt64(string.Concat(Enumerable.Repeat(sp0.ToString(), (int)(di / t))));

                        imax = Math.Max(imax, rp);
                    }
                }
                Out.Write(imax);
            }

        }
    }

    //修正したがまだWA??
    public class arc141_a2
    {
        public arc141_a2()
        {
            //input-------------
            var T = In.Read<int>();
            var N = In.ReadMany<string>(T).ToArray();

            //output------------

            for (int i = 0; i < T; i++)
            {
                int di = N[i].Length;
                long imax = Convert.ToInt64(new string('9', di - 1)); //longにすること！　初期値:1つ下の桁の999..999にしておかないといけない！

                for (int t = 1; t <= di / 2; t++)
                {
                    //周期1以上でちょうど割り切れる周期tのとき
                    if (di % t == 0)
                    {
                        //ex) 431245 -> 42 42 42 -> 430 430
                        //ex) 454647 -> 45 45 45 -> 454 454
                        long sp0 = Convert.ToInt32(N[i].Substring(0, t));
                        bool isUnder = false;
                        for (int r = t; r < di; r += t)
                        {
                            if (sp0 > Convert.ToInt32(N[i].Substring(r, t)))
                            {
                                isUnder = true;
                                break;
                            }
                        }
                        if (isUnder) sp0--;
                        if (sp0 == 0) continue; //1桁の時0になることがある

                        long rp = Convert.ToInt64(string.Concat(Enumerable.Repeat(sp0.ToString(), (int)(di / t))));
                        imax = Math.Max(imax, rp);
                    }
                }
                Out.Write(imax);
            }

        }
    }

    //時間切れ後修正 AC
    public class arc141_a3
    {
        public arc141_a3()
        {
            //input-------------
            var T = In.Read<int>();
            var N = In.ReadMany<string>(T).ToArray();

            //output------------

            for (int i = 0; i < T; i++)
            {
                int di = N[i].Length;
                long imax = Convert.ToInt64(new string('9', di - 1)); //longにすること！　初期値:1つ下の桁の999..999にしておかないといけない！

                for (int t = 1; t <= di / 2; t++)
                {
                    //周期1以上でちょうど割り切れる周期tのとき
                    if (di % t == 0)
                    {
                        //---
                        //ex) 431245 -> 42 42 42 -> 430 430
                        //ex) 454647 -> 45 45 45 -> 454 454

                        long sp0 = Convert.ToInt32(N[i].Substring(0, t));
                        bool isUnder = false;
                        bool isOver = false; //ex) 454641 -> 45 45 45 にならないとだめ！ このフラグがないとコレが見れない
                        for (int r = t; r < di; r += t)
                        {
                            if (!isOver && sp0 < Convert.ToInt32(N[i].Substring(r, t)))
                                isOver = true;

                            if (!isOver && sp0 > Convert.ToInt32(N[i].Substring(r, t)))
                            {
                                isUnder = true;
                                break;
                            }
                        }
                        //---
                        //↑ごちゃごちゃやり過ぎ、普通に454545..と生成してから大小比較したほうが楽だった

                        if (isUnder) sp0--;
                        if (sp0 == 0) continue; //1桁の時0になることがある

                        long rp = Convert.ToInt64(string.Concat(Enumerable.Repeat(sp0.ToString(), (int)(di / t))));
                        imax = Math.Max(imax, rp);

                    }
                }
                Out.Write(imax);
            }
        }
    }


    public class arc141_b
    {
        public arc141_b()
        {
            //input-------------
            (var N, var M) = In.ReadTuple2<long>();
            long MOD = 998244353;
            //output------------

            //i,Ai,Biでdp??
            //i,Ai+1,Biの計算がいっぺんにできる、+2,+3...も同様
            //いやサイズがでかすぎる？？

            //条件を満たすには..
            //*B(i-1)の最上位より上のbitがA(i)にある
            //*B(i-1)=101,Ai=010 ->Bi=111 のように、うまく歯抜けと噛み合う

        }
    }


    //時間切れてから解答
    //一応間違ってはいなさそうだけどTLE43 AC4
    public class arc141_d
    {
        public arc141_d()
        {
            //input-------------
            (var N, var M) = In.ReadTuple2<long>();
            var A = In.ReadAry<int>().ToArray();

            //output------------
            var plist = new Dictionary<int, int>();//最大限ばらけるようにとった候補とその値

            var Asort = A.Distinct().OrderBy(p => p).ToList();//重複する値は飛ばしてソート

            ////まず素数を入れる
            //for (int i = 0; i < N; i++)
            //{
            //    if (Algo.IsPrime(Asort[i]))
            //        plist.Add(Asort[i]);
            //}

            //素数ではないその他について
            for (int i = 0; i < N; i++)
            {
                //if (Asort[i] == 1 || plist.Contains(Asort[i])) continue;

                //割れるものがリストになかった時、追加
                //割れるものがリストに1個しかなかった時、その値と入れ替える
                //(ソートしてあるため、当然候補はどんどん大きい値になる)
                var divlist = plist.Where(p => Asort[i] % p.Key == 0);

                if (divlist.Count() == 0)
                    plist.Add(Asort[i], -1);
                else if (divlist.Count() == 1)
                {
                    plist.Remove(divlist.First().Key);
                    plist.Add(Asort[i], -1);
                }
            }

            //解答
            for (int i = 0; i < N; i++)
            {
                //1はややこしくなるのでカット
                if (A[i] == 1 && M != 1)
                {
                    Out.Write("No");
                    continue;
                }

                //候補からかぶっちゃうものを除外
                if (1 + plist.Count(p => p.Key % A[i] != 0 && A[i] % p.Key != 0) >= M)
                    Out.Write("Yes");
                else
                    Out.Write("No");
            }
        }
    }
}