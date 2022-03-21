using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 2022-03-20
/// </summary>
namespace AtCoderApp
{
    //AC
    public class abc244_a
    {
        public abc244_a()
        {
            //input-------------
            var N = In.Read<int>();
            var S = In.Read<string>();

            //calc--------------
            Out.Write(S[N - 1]);
        }
    }
    //AC
    public class abc244_b
    {
        public abc244_b()
        {
            //input-------------
            var N = In.Read<int>();
            var T = In.Read<string>();

            //calc--------------
            (var x, var y) = (0, 0);
            (var mx, var my) = (1, 0); //向きx,y
            foreach (var t in T)
            {
                if (t == 'S')
                {
                    x += mx;
                    y += my;
                }
                else if (mx != 0)
                {
                    my = mx * (-1);
                    mx = 0;
                    continue;
                }
                else if (my != 0)
                {
                    mx = my;
                    my = 0;
                    continue;
                }
            }
            Out.Write(x.ToString() + " " + y.ToString());
        }
    }

    //応答問題これでいい？？
    //AC
    public class abc244_c
    {
        public abc244_c()
        {
            //input-------------
            var N = In.Read<int>();

            //calc--------------
            var nl = new int[2 * N + 1];
            nl[0] = 1;
            Out.Write(1);

            for (int i = 0; i < N; i++)
            {
                var nx = In.Read<int>();
                nl[nx - 1] = 1;

                var ans_x = nl.Select((p, index) => (p, index)).Where(q => q.p != 1).ToArray()[0].index;
                nl[ans_x] = 1;
                Out.Write(ans_x + 1);
            }

            //終わり
            var n0 = In.Read<int>();
            return;
        }
    }

    //AC
    public class abc244_d
    {
        public abc244_d()
        {
            //input-------------
            var s = In.ReadAry<string>().ToArray();
            var t = In.ReadAry<string>().ToArray();

            //calc--------------

            //1 2 3 -> (1 3 2),(2 1 3),(3 2 1) は1回,3回,... 2n+1回 = No
            //1 2 3 -> (1 2 3),(2 3 1),(3 1 2) は2回,... = Yes
            if ((s[1] + s[0]) == (t[0] + t[1]) ||
                (s[2] + s[1]) == (t[1] + t[2]) ||
                (s[2] + s[0]) == (t[0] + t[2]))
                Out.Write("No");
            else
                Out.Write("Yes");
        }
    }

    //危険なのでパス
    public class abc244_e
    {
        public abc244_e()
        {
            //input-------------
            var rx = In.ReadAry<int>().ToArray();
            (var N, var M, var K, var S, var T, var X) = (rx[0], rx[1], rx[2], rx[3], rx[4], rx[5]);
            var UV = new List<(int U, int V)>();
            for (int i = 0; i < M; i++)
            {
                var uv = In.ReadAry<int>().ToArray();
                UV.Add((uv[0], uv[1]));
            }
            var MOD = 998244353;

            //calc--------------

        }
    }

    //わからん
    public class abc244_f
    {
        public abc244_f()
        {
            //input-------------
            var nm = In.ReadAry<int>().ToArray();
            (var N, var M) = (nm[0], nm[1]);
            var UV = new List<(int U, int V)>();
            for (int i = 0; i < M; i++)
            {
                var uv = In.ReadAry<int>().ToArray();
                UV.Add((uv[0], uv[1]));
            }

            //calc--------------
            //0は必ず空列なので無視
            for (int i = 1; i < Math.Pow(2, N) - 1; i++)
            {
                var S = Convert.ToString(i, 2).PadLeft(N, '0');
                for (int x = 0; x < N; x++)
                {
                    if (S[x] == '1')
                    {

                    }
                }
            }


        }
    }
}