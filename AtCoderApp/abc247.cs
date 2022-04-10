using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtCoderApp
{
    //AC
    public class abc247_a
    {
        public abc247_a()
        {
            //input-------------
            var S = In.Read<string>();

            //output------------
            var ret = "0" + S.Substring(0, S.Length - 1);
            Out.Write(ret);
        }
    }

    //AC
    public class abc247_b
    {
        public abc247_b()
        {
            //input-------------
            var N = In.Read<int>();
            var st = new List<(int index, string s, string t)>();//indexつけとく
            for (int i = 0; i < N; i++)
            {
                var _st = In.ReadTuple2<string>();
                st.Add((i, _st.Item1, _st.Item2));
            }

            //output------------
            foreach (var _st in st)
            {
                if (st.Exists(p => p.index != _st.index && (p.s == _st.s || p.t == _st.s)) && st.Exists(p => p.index != _st.index && (p.s == _st.t || p.t == _st.t)))
                {
                    Out.Write("No");
                    return;
                }
            }
            Out.Write("Yes");

        }
    }
    
    //AC
    public class abc247_c
    {
        public abc247_c()
        {
            //input-------------
            var N = In.Read<int>();

            //output------------
            var result = Sx(N);
            Out.WriteMany(result);

            List<int> Sx(int n)
            {
                //計算量対策
                if (n == 1) return new List<int>() { 1 };
                else if (n == 2) return new List<int>() { 1, 2, 1 };
                else if (n == 3) return new List<int>() { 1, 2, 1, 3, 1, 2, 1 };
                else if (n == 4) return new List<int>() { 1, 2, 1, 3, 1, 2, 1, 4, 1, 2, 1, 3, 1, 2, 1 };

                var sx1 = Sx(n - 1);
                var ret = new List<int>();
                ret.AddRange(sx1);
                ret.Add(n);
                ret.AddRange(sx1);
                return ret;
            }
        }
    }

    //dはPythonでといた

    //TLE
    public class abc247_e
    {
        public abc247_e()
        {
            //input-------------
            (var N, var X, var Y) = In.ReadTuple3<int>();
            var A = In.ReadAry<int>().ToArray();

            //output------------
            //全部で(NC2 + N)通り
            //ハズレ値でぶった切ったリストを作る
            var ds = new List<int[]>();
            int st = 0;
            for (int i = 0; i < N; i++)
            {
                if (A[i] > X || A[i] < Y)
                {
                    if (i - st != 0)
                        ds.Add(A.Skip(st).Take(i - st).ToArray());

                    st = i + 1;
                }
            }
            if (N - st != 0)
                ds.Add(A.Skip(st).Take(N - st).ToArray());

            var cnt = 0; //答え

            //小細工で改善を図ってみたがまだAC15 TLE7
            foreach (var _ds in ds)
            {
                var Ix = new List<int>();
                var Iy = new List<int>();
                for (int k = 0; k < _ds.Length; k++)
                {
                    if (X == _ds[k]) Ix.Add(k);
                    if (Y == _ds[k]) Iy.Add(k);
                }
                if (Ix.Count() == 0 || Iy.Count() == 0) continue; //条件満たさないものは無視

                //XリストとYリストが1つ以上含まれている範囲(L,R)を探す(indexだけ見ることで高速化)
                for (int i = 0; i <= Math.Min(Ix.Max(), Iy.Max()) && i < _ds.Length; i++)
                {
                    for (int j = Math.Max(Ix.Min(), Iy.Min()); j < _ds.Length; j++) //j=i..
                    {
                        if (Ix.Exists(p => p >= i && p <= j) && Iy.Exists(q => q >= i && q <= j))
                            cnt++;
                    }
                }
            }

            //AC13 TLE9
            //foreach (var _ds in ds)
            //{
            //    var Ix = _ds.Select((p, index) => (p, index)).Where(q => q.p == X).Select(r => r.index);
            //    var Iy = _ds.Select((p, index) => (p, index)).Where(q => q.p == Y).Select(r => r.index);

            //    //XリストとYリストが1つ以上含まれている範囲(L,R)を探す(indexだけ見ることで高速化)
            //    for (int i = 0; i < _ds.Length; i++)
            //    {
            //        for (int j = i; j < _ds.Length; j++)
            //        {
            //            if (Ix.Count(p => p >= i && p <= j) > 0 && Iy.Count(q => q >= i && q <= j) > 0)
            //                cnt++;
            //        }
            //    }
            //}

            //解説を見て作ったら何故かWAになる。？？
            foreach (var _ds in ds)
            {
                int cX = 0; int cY = 0; int j = 0;
                for (int i = 0; i < _ds.Length; i++)
                {
                    while (j < _ds.Length && (cX == 0 || cY == 0))
                    {
                        if (_ds[j] == X) cX++;
                        if (_ds[j] == Y) cY++;
                        j++;
                    }
                    if (cX > 0 && cY > 0)
                    {
                        cnt += _ds.Length + 1 - j; //尺取法でここを短絡できる
                    }
                    if (_ds[i] == X) cX--;//尺の先頭の処理
                    if (_ds[i] == Y) cY--;//〃
                }
            }

            Out.Write(cnt);
        }
        
        //解説を写経したらなぜかWAになる。。？？？？
        public class abc247_e_2
        {
            public abc247_e_2()
            {
                //input-------------
                (var N, var X, var Y) = In.ReadTuple3<int>();
                var A = In.ReadAry<int>().ToArray();

                //output------------
                //全部で(NC2 + N)通り
                //ハズレ値でぶった切ったリストを作る
                long cnt = 0; //答え
                int i = 0;
                while (i != N)
                {
                    var ds = new List<int>();
                    while (i != N && A[i] <= X && A[i] >= Y)
                    {
                        ds.Add(A[i]);
                        i++;
                    }
                    if (ds.Count() > 0)
                        cnt += MainCalc(ds);
                    else
                        i++;
                }

                Out.Write(cnt);


                long MainCalc(List<int> _ds)
                {
                    int res = 0;
                    int i = 0; int j = 0;
                    int cX = 0; int cY = 0;
                    while (i != _ds.Count())
                    {
                        while (j != _ds.Count() && (cX == 0 || cY == 0))//j=i..
                        {
                            if (_ds[j] == X) cX++;
                            if (_ds[j] == Y) cY++;
                            j++;
                        }
                        if (cX > 0 && cY > 0)
                        {
                            res += _ds.Count() + 1 - j;
                        }
                        if (_ds[i] == X) cX--;
                        if (_ds[i] == Y) cY--;
                        i++;
                    }

                    return res;
                }
            }
        }
    }
}