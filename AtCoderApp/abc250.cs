using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AtCoderApp
{
    //2022-05-08 ABC250

    //AC (WA修正1回)
    public class abc250_a
    {
        public abc250_a()
        {
            //input-------------
            var hw = In.ReadAry<int>().ToArray();
            var rc = In.ReadAry<int>().ToArray();
            //output------------
            var sq = 4;
            
            if (hw[0] == 1) sq--; //これ必要なの忘れててWA
            if (hw[1] == 1) sq--; //これも

            if (rc[0] == 1 || rc[0] == hw[0]) sq--;
            if (rc[1] == 1 || rc[1] == hw[1]) sq--;

            Out.Write(sq);
        }
    }

    //AC
    public class abc250_b
    {
        public abc250_b()
        {
            //input-------------
            (var N, var A, var B) = In.ReadTuple3<int>();

            //output------------

            //タイルが切れたりすることはないので、縞模様Aと縞模様Bを交互に並べる感じで処理
            var r1 = "";
            var r2 = "";
            for (int i = 0; i < N; i++)
            {
                if (i % 2 == 0)
                {
                    r1 += new string('.', B);
                    r2 += new string('#', B);
                }
                else
                {
                    r1 += new string('#', B);
                    r2 += new string('.', B);
                }
            }

            for (int i = 0; i < N * A; i++)
            {
                if (i % (A * 2) < A)
                    Out.Write(r1);
                else
                    Out.Write(r2);
            }
        }
    }

    //WA、TLE
    public class abc250_c
    {
        public abc250_c()
        {
            //input-------------
            (var N, var Q) = In.ReadTuple2<int>();
            var X = In.ReadMany<int>(Q).ToArray();

            //output------------
            var adj = new int[N + 1];
            for (int i = 1; i < N + 1; i++)
                adj[i] = i;

            for (int i = 0; i < Q; i++)
            {
                if (adj[X[i]] != N)
                {
                    adj[GetNext(adj[X[i]] + 1)]--;
                    adj[X[i]]++;
                }
                else
                {
                    adj[GetNext(N - 1)]++;
                    adj[X[i]]--;
                }
            }

            //output
            Out.Write(string.Join(' ', adj).Substring(2));

            int GetNext(int xi)
            {
                for (int i = 1; i < N + 1; i++)
                {
                    if (adj[i] == xi)
                        return i;
                }
                return -1;
            }
        }
    }

    //時間ギリギリAC
    //多分cntの足し方は大事だった?
    public class abc250_d
    {
        public abc250_d()
        {
            //input-------------
            var N = In.Read<long>();

            //output------------

            //p,qの候補となる素数リストを作る
            long ql = 0;
            if (N.ToString().Length < 4) ql = 8;
            else ql = (long)(1 + Math.Pow(N / 2 + 1, 1.0 / 3.0)); //三乗根とpを考慮した素数上限

            var prime_list = new List<int>();
            for (int i = 2; i < ql; i++)
            {
                if (IsPrime(i))
                    prime_list.Add(i);
            }

            var cnt = 0;//結果
            var bfj = prime_list.Count() - 1;
            for (int i = 0; i < prime_list.Count(); i++)
            {
                for (int j = bfj; j > i; j--)
                {
                    try
                    {
                        //pq組み合わせを1つずつ数えるより、N以下を満たすp,{q1,q2,..}をまるっと足す
                        if (prime_list[i] * (long)Math.Pow(prime_list[j], 3) <= N)
                        {
                            cnt += j - i;
                            bfj = j + 1; //次の操作範囲は今回のj以下でよい。範囲を減らす為に記憶する
                            break;
                        }
                        else
                            continue;
                    }
                    catch //longを超えることがある..
                    {
                        continue;
                    }
                }
            }

            Out.Write(cnt);
        }

        bool IsPrime(long value)
        {
            // 1や0、負数は素数ではない
            if (value < 2)
                return false;

            // 2と3は素数である
            if (value == 2 || value == 3)
                return true;

            // 2以外の偶数であれば素数ではない
            if (value % 2 == 0)
                return false;

            // 平方根を切り上げた整数を求める
            double d = Math.Pow(value, 0.5);
            long max = (long)Math.Ceiling(d);

            // 奇数だけ平方根を切り上げた整数まで調べる
            for (long l = 3; l <= max; l += 2)
            {
                if (value % l == 0)
                    return false;
            }
            // ループから抜けてしまった場合は素数である
            return true;
        }
    }
}