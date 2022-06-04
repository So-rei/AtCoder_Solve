using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AtCoderApp
{
    //2022-06-04

    //AC
    public class abc254_a
    {
        public abc254_a()
        {
            //input-------------
            var N = In.Read<int>();

            //output------------
            Out.Write(N.ToString().Substring(1));

        }
    }

    //AC
    public class abc254_b
    {
        public abc254_b()
        {
            //input-------------
            var N = In.Read<int>();

            //output------------
            //半分だけ作る
            var st = new List<int[]>();

            //最初2つは明らかなので初期値として設定
            st.Add(new int[] { 1 });
            st.Add(new int[] { 1 });

            for (int i = 2; i < N; i++)
            {
                st.Add(new int[(int)(i + 2) / 2]);
                st[i][0] = 1;
                for (int j = 1; j < st[i].Length; j++)
                {
                    st[i][j] = st[i - 1][j - 1] + (st[i - 1].Length > j ? st[i - 1][j] : st[i - 1][j - 1]);
                }
            }

            //半分＋真ん中に気をつけて、残り半分
            for (int i = 0; i < N; i++)
            {
                var ret = string.Join(' ', st[i]);
                if (i % 2 == 0)
                    ret += ' ' + string.Join(' ', st[i].Reverse().Skip(1));
                else
                    ret += ' ' + string.Join(' ', st[i].Reverse());

                Out.Write(ret);
            }
        }
    }

    //AC
    public class abc254_c
    {
        public abc254_c()
        {
            //input-------------
            (var N, var K) = In.ReadTuple2<int>();
            var a = In.ReadAry<int>().ToArray();

            //output------------
            //Kつのソート

            var ak = new List<int[]>();
            for (int i = 1; i <= K; i++)
                ak.Add(new int[1 + (N - i) / K]);
            for (int i = 0; i < N; i++)
                ak[i % K][(i - i % K) / K] = a[i];

            for (int i = 0; i < K; i++)
                ak[i] = ak[i].OrderBy(p => p).ToArray();

            //ソート後K列を並べ直して、いけるかどうか
            int tmp = -1;
            for (int i = 0; i < N; i++)
            {
                int next = ak[i % K][(i - i % K) / K];
                if (tmp > next)
                {
                    Out.Write("No");
                    return;
                }
                tmp = next;
            }
            Out.Write("Yes");
        }
    }

    //TLE11 AC3
    //１個１個Nまで見ていては流石に遅い
    public class abc254_d
    {
        public abc254_d()
        {
            //input-------------
            var N = In.Read<int>();

            //output------------
            var cplist = new List<int[]>();
            var cnt = 0;

            //素因数分解して、不揃いのものだけ抽出
            for (int i = 1; i <= N; i++)
            {
                var c = PrimeFactors(i).GroupBy(i => i).Where(g => g.Count() % 2 != 0).Select(p => p.Key).OrderBy(q => q);
                cplist.Add(c.ToArray());
            }
            var uNcnt = cplist.Count(p => p.Count() == 0);//N以下の平方数の数

            //平方数&平方数 or 不揃い*不揃いで平方数になる
            for (int i = 0; i < N; i++)
            {
                if (cplist[i].Count() == 0) cnt += uNcnt;
                else
                {
                    var pure_cnt = cplist[i].Count();
                    int matched = cplist.Where(p => p.Count() == pure_cnt).Count(p => {
                        for (int k = 0; k < pure_cnt; k++)
                        {
                            if (p[k] != cplist[i][k]) return false;
                        }
                        return true;
                    });

                    cnt += matched;
                }
            }

            Out.Write(cnt);



            static IEnumerable<int> PrimeFactors(int n)
            {
                int i = 2;
                int tmp = n;

                while (i * i <= n) //※1
                {
                    if (tmp % i == 0)
                    {
                        tmp /= i;
                        yield return i;
                    }
                    else
                    {
                        i++;
                    }
                }
                if (tmp != 1) yield return tmp;//最後の素数も返す
            }
        }
    }

    //作り直して時間内AC
    public class abc254_d2
    {
        public abc254_d2()
        {
            //input-------------
            var N = In.Read<int>();

            //output------------
            var cnt = 0;//結果総数

            //素因数分解して、不揃いのものだけ抽出
            //ex. 360 = 2^3 * 3^2 * 5 = 2と5が「平方ではない要素」として記憶する
            //解 = (N以下の平方数の数の組み合わせ) + (「平方ではない要素」が完全一致する数の組み合わせ)

            var cplist = new Dictionary<string, int>(); //記憶リスト
            var uNcnt = 1;//N以下の平方数の数 1を含む

            for (int i = 2; i <= N; i++)
            {
                //平方ではない要素を抽出
                var c = PrimeFactors(i).GroupBy(i => i).Where(g => g.Count() % 2 != 0).Select(p => p.Key).OrderBy(q => q).ToArray();
                if (c.Count() == 0)
                {
                    //平方数
                    uNcnt++;
                    continue;
                }

                string cKey = String.Join(' ', c);
                if (cplist.ContainsKey(cKey))
                    cplist[cKey]++;
                else
                    cplist.Add(cKey, 1);
            }

            //平方数&平方数（同じ値のペアも含む）
            cnt += uNcnt * uNcnt;

            //不揃い*不揃いで平方数（同じ値のペアも含む）
            foreach (var cp in cplist)
                cnt += cp.Value * cp.Value;

            Out.Write(cnt);


            static IEnumerable<int> PrimeFactors(int n)
            {
                int i = 2;
                int tmp = n;

                while (i * i <= n) //※1
                {
                    if (tmp % i == 0)
                    {
                        tmp /= i;
                        yield return i;
                    }
                    else
                    {
                        i++;
                    }
                }
                if (tmp != 1) yield return tmp;//最後の素数も返す
            }
        }
    }
}