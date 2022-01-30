using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtCoderApp
{
    //凡ミス1回
    public class abc237_a
    {
        public abc237_a()
        {
            //input-------------
            var N = In.Read<long>();

            //calc--------------
            if (N >= int.MinValue && N <= int.MaxValue) //<でまちがえた
                Out.Write("Yes");
            else
                Out.Write("No");
        }
    }

    //1発AC
    public class abc237_b
    {
        public abc237_b()
        {
            //input-------------
            var hw = In.ReadAry<int>().ToArray();
            (var H, var W) = (hw[0], hw[1]);
            var A = new List<int[]>();
            for (int i = 0; i < H; i++)
                A.Add(In.ReadAry<int>().ToArray());

            //calc--------------
            for (int j = 0; j < W; j++)
            {
                var ary = new List<int>();
                for (int i = 0; i < H; i++)
                {
                    ary.Add(A[i][j]);
                }
                Out.WriteMany(ary);
            }

        }
    }

    //TLE
    public class abc237_c
    {
        public abc237_c()
        {
            //input-------------
            var S = In.Read<string>().Reverse().ToList(); //処理の簡単さの為に、ひっくり返して覚えておく

            //calc--------------
            var cnt = S.Count();
            for (int i = 0; i < cnt; i++)
            {
                if (KaiCheck(S))
                {
                    Out.Write("Yes");
                    return;
                }
                else
                {
                    if (S[i] == 'a')
                    {
                        S.Add('a');
                        continue;
                    }
                    else //対応する末尾に'a'がない
                    {
                        Out.Write("No");
                        return;
                    }
                }
            }

            bool KaiCheck(List<char> s)
            {
                int i = 0;
                int imax = s.Count() - 1;
                while (s[i] == s[imax])
                {
                    i++;
                    imax--;
                    if (i >= imax)
                        return true; //最後まで回文である
                }
                return false;
            }
        }
    }

    //TLE原因はKaiCheck()を何度もしたこと→書き直しAC！
    public class abc237_c_2
    {
        public abc237_c_2()
        {
            //input-------------
            var S = In.Read<string>().Reverse().ToList(); //処理の簡単さの為に、ひっくり返して覚えておく

            //calc--------------
            var cnt = S.Count();
            var nz = 0;//aが後ろから何文字連続しているか
            for (int i = 0; i < cnt; i++)
            {
                if (S[i] != 'a')
                {
                    nz = i;
                    break;
                }
            }
            var na = 0;//aが先頭何文字連続しているか
            for (int i = cnt - 1; i >= 0; i--)
            {
                if (S[i] != 'a')
                {
                    na = cnt - 1 - i;
                    break;
                }
            }

            //a足すことで解決できないパターンを除去
            if (nz < na)
            {
                Out.Write("No");
                return;
            }

            //{nz-na}文字数だけaを足してチェックする
            S.AddRange(new string('a', nz - na));
            if (KaiCheck(S))
                Out.Write("Yes");
            else
                Out.Write("No");

            //チェック関数-----------------
            bool KaiCheck(List<char> s)
            {
                int i = 0;
                int imax = s.Count() - 1;
                while (s[i] == s[imax])
                {
                    i++;
                    imax--;
                    if (i >= imax)
                        return true; //最後まで回文である
                }
                return false;
            }
        }
    }

    //TLE
    public class abc237_d
    {
        public abc237_d()
        {
            //input-------------
            var N = In.Read<int>();
            var S = In.Read<string>();

            //calc--------------
            //まともにコレクションのいちを１つずつ挿入したりして動かしてたらえらい重そう
            //→0...Nが必ず1つずつ存在するので、その場所値だけで管理する
            var IX = new Dictionary<int, int>(); //{i,iがどの位置にあるか}
            for (int i = 0; i <= N; i++)
                IX.Add(i, 0);

            for (int i = 1; i <= N; i++)
            {
                if (S[i - 1] == 'L')
                {
                    //左に入る

                    IX[i] = IX[i - 1];
                    IX[i - 1]++;
                }
                else
                {
                    //右に入る
                    IX[i] = IX[i - 1] + 1;
                }

                //それ以外の入れた数字の位置(IX[i])よりも右にあるものは1つ右に移動
                for (int k = 0; k < N; k++)
                {
                    if (IX[i] < IX[k])
                        IX[k]++;
                }
            }

            var ret = new List<int>();
            foreach (var c in IX.OrderBy(p => p.Value)) //Valueでソートするのは重いが,,,
            {
                ret.Add(c.Key);
            }
            Out.WriteMany(ret);
        }
    }

    //時間切れ後の挑戦。AC
    public class abc237_d_2
    {
        public abc237_d_2()
        {
            //input-------------
            var N = In.Read<int>();
            var S = In.Read<string>();

            //calc--------------
            //s[i]について、s[i+1]が"L"ならs[i]より右にいる最終的な数,s[i+1]が"R"なら左にいる最終的な数　が確定する
            //ここから最終的な位置indexが求められる。これでDP
            var Si = new List<int[]>();
            int L_Cnt = 0; //これまで何回L,Rで確定して右端or左端に寄せたか
            int R_Cnt = 0;
            for (int i = -1; i < N - 1; i++)
            {
                if (S[i + 1] == 'L')
                {
                    Si.Add(new int[] { i + 1, N - R_Cnt });
                    R_Cnt++;
                }
                else
                {
                    Si.Add(new int[] { i + 1, L_Cnt });
                    L_Cnt++;
                }
            }

            //最後の1回は単純計算
            if (S[N - 1] == 'L')
                Si.Add(new int[] { N, Si.Last()[1] - 1 });
            else
                Si.Add(new int[] { N, Si.Last()[1] + 1 });

            //output
            var ret = new List<int>();
            foreach (var r in Si.OrderBy(p => p[1]))
            {
                ret.Add(r[0]);
            }

            Out.WriteMany(ret);
        }
    }
}