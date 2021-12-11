
//2021/12/11

//public class abc231_a
//{
//    public abc231_a()
//    {
//        //input-------------
//        var D = In.Read<int>();

//        //output------------
//        Out.Write((double)((double)D / 100));

//    }
//}

//public class abc231_b
//{
//    public abc231_b()
//    {
//        //input-------------
//        var N = In.Read<int>();
//        var S = new string[N];
//        for (int i = 0; i < N; i++)
//            S[i] = In.Read<string>();

//        //output------------
//        var sName = S[0];
//        var cnt = 0;
//        S.ToList().Distinct().ToList().ForEach(p =>
//        {
//            var c = S.ToList().Count(q => q == p);
//            if (cnt < c)
//            {
//                cnt = c;
//                sName = p;
//            }
//        });

//        Out.Write(sName);
//    }
//}

//さすがにこれじゃTLE
//public class abc231_c
//{
//    public abc231_c()
//    {
//        //input-------------
//        var NQ = In.ReadAry<long>().ToArray();
//        var (N, Q) = (NQ[0], NQ[1]);

//        var A = In.ReadAry<long>().ToArray();
//        var x = new long[Q];
//        for (int i = 0; i < Q; i++)
//            x[i] = In.Read<long>();

//        //output------------
//        //データ多すぎるので減らす
//        var a0 = new List<long>();
//        var a1 = new List<long>();
//        var a2 = new List<long>();
//        a0 = A.Where(p => p < Math.Pow(10, 3)).ToList();
//        a1 = A.Where(p => p >= Math.Pow(10, 3) && p < Math.Pow(10, 6)).ToList();
//        a2 = A.Where(p => p >= Math.Pow(10, 6)).ToList();

//        foreach (var cx in x)
//        {
//            if (cx < Math.Pow(10, 3))
//            {
//                Out.Write((long)(a0.Count(p => p >= cx) + a1.Count() + a2.Count()));
//            }
//            else if (cx < Math.Pow(10, 6))
//            {
//                Out.Write((long)(a1.Count(p => p >= cx) + a2.Count()));
//            }
//            else
//            {
//                Out.Write((long)(a2.Count(p => p >= cx)));
//            }
//        }

//    }
//}

//こっちなら間に合う
//public class abc231_c
//{
//    public abc231_c()
//    {
//        //input-------------
//        var NQ = In.ReadAry<int>().ToArray();
//        var (N, Q) = (NQ[0], NQ[1]);

//        var A = In.ReadAry<long>().ToArray();
//        var x = new long[Q];
//        for (int i = 0; i < Q; i++)
//            x[i] = In.Read<long>();

//        //output------------
//        //データ多すぎるので減らす OrderBy,DP
//        var Am = A.OrderBy(p => p).ToList();

//        foreach (var cx in x)
//            Search(cx, 0, N);

//        //----------------------------------------
//        void Search(long c, int st, int ed)
//        {
//            if (ed - st < 40)
//            {
//                Out.Write(Am.GetRange(st, ed - st).Count(p => p >= c) + (N - ed));
//                return;
//            }

//            if (Am[st + (ed - st) / 2] < c)
//                Search(c, st + (ed - st) / 2, ed);
//            else
//                Search(c, st, ed - (ed - st) / 2);
//        }

//    }
//}

//WR12,TLE2 Where+Containsが重い…？計算処理改善
//public class abc231_d
//{
//    public abc231_d()
//    {
//        //input-------------
//        var NM = In.ReadAry<int>().ToArray();
//        var (N, M) = (NM[0], NM[1]);

//        var A = new int[M];
//        var B = new int[M];
//        for (int i = 0; i < M; i++)
//        {
//            var c = In.ReadAry<int>().ToArray();
//            (A[i], B[i]) = (c[0], c[1]);
//        }

//        //output------------
//        //同じ数字が3回以上登場したらNo
//        //(x,y)(y,z)(z,x)のようなループがあったらNo
//        var jo = new List<List<int>>();
//        for (int i = 0; i < M; i++)
//        {
//            var ja = jo.Where(p => p.Contains(A[i])).ToList();
//            var jb = jo.Where(p => p.Contains(B[i])).ToList();
//            switch (ja.Count() + jb.Count())
//            {
//                case 0:
//                    jo.Add(new List<int>() { A[i], B[i] });
//                    break;
//                case 1:
//                    if (ja.Count() == 1)
//                    {
//                        if (!SetCol(ja[0], A[i], B[i])) return; //既存組み合わせの末尾にくっつく
//                    }
//                    else
//                    {
//                        if (!SetCol(jb[0], B[i], A[i])) return; //既存組み合わせの末尾にくっつく
//                    }
//                    break;
//                default:
//                    if (ja.Count() == 1 && jb.Count() == 1)
//                    {
//                        if (!SetConcat(ja[0], jb[0], A[i], B[i])) return; //既存組み合わせ同士がくっつく
//                    }
//                    else
//                    {
//                        Out.Write("No");
//                        return;
//                    }
//                    break;
//            }
//        }

//        Out.Write("Yes");




//        //----------------------------------------
//        bool SetCol(List<int> ary, int a, int b)
//        {
//            if (ary[0] == a)
//            {
//                //前に追加
//                ary.Insert(0, b);
//                return true;
//            }
//            else if (ary.Last() == a)
//            {
//                //後ろに追加
//                ary.Add(b);
//                return true;
//            }
//            else //間にあったらアウトー
//            {
//                Out.Write("No");
//                return false;
//            }
//        }
//        bool SetConcat(List<int> ary0, List<int> ary1, int a, int b)
//        {

    //var _ary0 = ary0;
    //var _ary1 = ary1;
    //jo.Remove(ary0);
    //jo.Remove(ary1);
    //if (_ary0[0] == a && _ary1.Last() == b)
    //{
    //    jo.Add(_ary1.Concat(_ary0).ToList());
    //    return true;
    //}
    //else if (_ary0.Last() == a && _ary1[0] == b)
    //{
    //    jo.Add(_ary0.Concat(_ary1).ToList());
    //    return true;
    //}
    //else if (_ary0[0] == a && _ary1[0] == b)
    //{
    //    _ary0.Reverse();
    //    jo.Add(_ary0.Concat(_ary1).ToList());
    //    return true;
    //}
    //else if (_ary0.Last() == a && _ary1.Last() == b)
    //{
    //    _ary1.Reverse();
    //    jo.Add(_ary0.Concat(_ary1).ToList());
    //    return true;
    //}
    //else
    //{
    //    //結合できない
    //    Out.Write("No");
    //    return false;
    //}
//        }

//    }
//}