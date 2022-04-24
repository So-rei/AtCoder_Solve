using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AtCoderApp
{
    //2022-04-23
    //ノーレート参加

    //これだとTLE
    public class abc249_d
    {
        public abc249_d()
        {
            //input-------------
            var N = In.Read<int>();
            var A = In.ReadAry<int>().ToArray();

            //output------------
            long cnt = 0;
            //i/j = k → i*k = j
            //jを記憶しておく
            var jary = new int[200001];
            for (int i = 0; i < N; i++)
                jary[A[i]]++;

            //ソートして計算量減らす
            var An = A.OrderBy(p => p);
            foreach (var a in An)
            {
                foreach (var b in An)
                {
                    if (a * b > 200000) break;

                    cnt += jary[a * b];
                }
            }

            Out.Write(cnt);
        }
    }

    //AC
    public class abc249_d_2
    {
        public abc249_d_2()
        {
            //input-------------
            var N = In.Read<int>();
            var A = In.ReadAry<int>().ToArray();

            //output------------
            long cnt = 0;

            //ソートして計算量減らす
            //var An = A.OrderBy(p => p).ToArray();
            int jmax = A.Max();

            //i/j = k → i = k * j
            //jを記憶しておく
            var jary = new long[200001]; //ここintだと後の計算で最大200k*200k>4*10^10になるのでエラーになる！！！
            for (int i = 0; i < N; i++)
                jary[A[i]]++;

            //N*N-1/2回の組み合わせ..                
            for (long j = 1; j <= jmax; j++)
            {
                if (jary[j] == 0) continue;

                for (long k = j; k <= jmax; k++)
                {
                    if (jary[k] == 0) continue;
                    if (j * k > 200000) break; //longじゃないとここでエラーになる

                    //ijk重複可なので以下不要
                    //if (j == 1)
                    //{
                    //    //if (jary[k] == 1) continue;
                    //    //cnt += jary[1] * jary[k] * 2 * (jary[k] - 1);
                    //    continue;
                    //}

                    if (j == k)
                    {
                        cnt += (jary[j] * jary[j]) * jary[j * k];

                        //ijk重複可なので以下じゃない
                        //if (jary[i] == 1) continue;
                        //cnt += (jary[i] * (jary[i] - 1)) * jary[i * k]; 
                        continue;
                    }
                    else
                    {
                        cnt += jary[j] * jary[k] * 2 * jary[j * k];//i,k k,iの2回分
                        continue;
                    }
                }
            }

            Out.Write(cnt);
        }
    }


    //2022-04-24
    //TLEとかWAとか　O(N^3)
    //中のjのループは先頭～終端で単調増加なので、そこをカットできる（らしい）
    //dp[i,j]=1にすれば、26の場合の場合分けが不要になる
    public class abc249_e
    {
        public abc249_e()
        {
            //input-------------
            (var N, var P) = In.ReadTuple2<int>();

            //output------------
            //aがx個、bがy個...の組み合わせを全部出すしか無い
            //a,b,c...の組み合わせは簡単 26 * 25 * 25 * ... * 25 * 26
            //x,y,zの組み合わせがややこしい　2桁以上だと文字数が変わる

            var dp = new int[N, N + 1];//t文字に変換される、x文字で構成されたXの総数
                                       //dp[t,x] += (dp[t-2,x-1] +... dp[t-2,x-9])* 25
                                       //+ (dp[t-3,x-10] + ... dp[t-3,x-99]) * 25
                                       //....dp[t-5,x-N] まで (t>5,x>0のとき)

            for (int t = 2; t < N; t++)//Tは最低2文字
            {
                //'a10'のようにたった1つの文字で構成されている場合(T=5文字以下)　その分を追加
                if (t < 6)
                {
                    for (int j = (int)Math.Pow(10, t - 2); j < (int)Math.Pow(10, t - 1); j++)
                    {
                        if (j > N) break;
                        dp[t, j] += 26;
                    }
                }

                //2ヶ以上の複合文字で構成されている
                if (t > 3)
                {
                    for (int di = 0; di <= 3; di++) //取得する桁数 10^di
                    {
                        if (t - di - 2 < 2) break; //Tは最小2
                        for (int x = 1; x <= N; x++)
                        {
                            for (int j = (int)Math.Pow(10, di); j < Math.Pow(10, di + 1); j++)
                            {
                                if (x - j < 1) break;
                                dp[t, x] += dp[t - di - 2, x - j] * 25 % P;
                            }
                        }
                    }
                }
            }

            long ret = 0; //t<Nのもの全てを合計する
            for (int i = 2; i < N; i++)
            {
                ret += dp[i, N] % P;
            }
            Out.Write(ret % P);
        }
    }
}