using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 2022-02-19 A,B,C,D AC (miss: Dx5 Ex-)
/// </summary>
namespace AtCoderApp
{
    //AC
    public class ab239_a
    {
        public ab239_a()
        {
            //input-------------
            var H = In.Read<int>();

            //calc--------------
            double b = Math.Sqrt((double)H * (double)(12800000 + H));

            Out.Write(b);


        }
    }

    //AC
    public class ab239_b
    {
        public ab239_b()
        {
            //input-------------
            var X = In.Read<long>();

            //calc--------------
            //Math.floorがつかえねえので
            var r = 0;
            if (X < 0 && X % 10 != 0)
                r = -1;

            Out.Write((long)(X / 10) + r);

        }
    }

    //AC
    public class ab239_c
    {
        public ab239_c()
        {
            //input-------------
            var xy = In.ReadAry<int>().ToArray();
            (var X1, var Y1, var X2, var Y2) = (xy[0], xy[1], xy[2], xy[3]);

            //calc--------------

            if (IsExist(X1 + 1, Y1 + 2, X2, Y2) ||
                IsExist(X1 + 1, Y1 - 2, X2, Y2) ||
                IsExist(X1 + 2, Y1 + 1, X2, Y2) ||
                IsExist(X1 + 2, Y1 - 1, X2, Y2) ||
                IsExist(X1 - 1, Y1 + 2, X2, Y2) ||
                IsExist(X1 - 1, Y1 - 2, X2, Y2) ||
                IsExist(X1 - 2, Y1 + 1, X2, Y2) ||
                IsExist(X1 - 2, Y1 - 1, X2, Y2)
               )
                Out.Write("Yes");
            else
                Out.Write("No");

            bool IsExist(int x1, int y1, int x2, int y2)
            {
                if ((Math.Abs(x1 - x2) == 1 && Math.Abs(y1 - y2) == 2) ||
                    (Math.Abs(x1 - x2) == 2 && Math.Abs(y1 - y2) == 1))
                    return true;
                else
                    return false;
            }
        }
    }

    /// <summary>
    /// WA??
    /// </summary>
    public class ab239_d
    {
        public ab239_d()
        {
            //input-------------
            var abcd = In.ReadAry<int>().ToArray();
            (var A, var B, var C, var D) = (abcd[0], abcd[1], abcd[2], abcd[3]);

            //calc--------------
            //たかだか最大10kパタン                
            for (int i = A; i <= B; i++)
            {
                bool bT = false;
                for (int j = C; j <= D; j++)
                {
                    if (IsSosu(i + j))
                    {
                        bT = true;
                        break;
                    }
                }
                if (!bT)
                {
                    Out.Write("Takahasi"); //先攻最適解
                    return;
                }
            }

            Out.Write("Aoki");
            return;

            bool IsSosu(int k)
            {
                //for (int i = 2; i < Math.Ceiling(Math.Sqrt(k)); i++)
                for (int i = 2; i < Math.Ceiling((decimal)(k / 2)); i++)
                {
                    if (k % i == 0)
                        return false;
                }
                return true;
            }
        }
    }

    //AC Takahashi... 計算短絡するほどでもないので消去
    public class ab239_d2
    {

        public ab239_d2()
        {
            //input-------------
            var abcd = In.ReadAry<int>().ToArray();
            (var A, var B, var C, var D) = (abcd[0], abcd[1], abcd[2], abcd[3]);

            //calc--------------
            //たかだか最大10kパタン                
            for (int i = A; i <= B; i++)
            {
                bool bT = false;
                for (int j = C; j <= D; j++)
                {
                    if (IsSosu(i + j))
                    {
                        bT = true;
                        break;
                    }
                }
                if (!bT)
                {
                    Out.Write("Takahashi"); //先攻最適解
                    return;
                }
            }

            Out.Write("Aoki");
            return;

            bool IsSosu(int k)
            {
                //for (int i = 2; i < Math.Ceiling(Math.Sqrt(k)); i++)
                //if (k < 4 || k % 2 == 0) return false;
                for (int i = 2; i < k; i++)
                {
                    if (k % i == 0)
                        return false;
                }
                return true;
            }
        }
    }


    /// <summary>
    /// E 時間切れ。時間切れ後に解いたがAC6/TLE16
    /// </summary>
    public class abc239_e
    {
        public abc239_e()
        {
            //input-------------
            var NQ = In.ReadAry<int>().ToArray();
            (var N, var Q) = (NQ[0], NQ[1]);
            var X = In.ReadAry<int>().ToArray();
            var AB = new List<(int A, int B)>();
            for (int i = 0; i < N - 1; i++)
            {
                var _ab = In.ReadAry<int>().ToArray();
                AB.Add((_ab.Min(), _ab.Max())); //そーとしておく
            }
            var VK = new List<(int V, int K)>();
            for (int i = 0; i < Q; i++)
            {
                var _vk = In.ReadAry<int>().ToArray();
                VK.Add((_vk[0], _vk[1]));
            }

            //calc--------------
            var ABsort = AB.OrderBy(p => p.A).ToList(); //根がどっちかわからないということがおこらないようにする
            var li = new Dictionary<int, List<int>>();
            li.Add(1, new List<int>());
            for (int i = 2; i <= N; i++)
                li.Add(i, new List<int>());

            //どちらが1につながっているのかきっちりさせないといけない
            int cnt = 0;
            foreach (var ab in ABsort.Where(p => p.A == 1))
            {
                li[1].Add(ab.B);
                cnt++;
            }
            while (cnt < N - 1)
            {
                foreach (var ab in ABsort.Where(p => p.B != 1 && !li[1].Contains(p.B) && li[1].Contains(p.A)))
                {
                    li[ab.A].Add(ab.B);
                    li[1].Add(ab.B);
                    cnt++;
                }
                foreach (var ab in ABsort.Where(p => p.A != 1 && !li[1].Contains(p.A) && li[1].Contains(p.B)))
                {
                    li[ab.B].Add(ab.A);
                    li[1].Add(ab.A);
                    cnt++;
                }
            }

            for (int i = 0; i < Q; i++)
            {
                //枝葉リストを作る
                var r = new List<int>();
                r.Add(VK[i].V); //自分
                foreach (int k in li[VK[i].V])
                {
                    r.Add(k); //枝1
                    r.AddRange(getChild(k)); //枝1以降
                }

                var rr = r.Distinct().Select(p => X[p - 1]).OrderByDescending(q => q).ToList(); //X取得・大きい方から
                Out.Write(rr[VK[i].K - 1]);
            }


            //枝葉リスト取得
            List<int> getChild(int no)
            {
                var cList = new List<int>() { no };
                foreach (var l in li[no])
                    cList.AddRange(getChild(l));

                return cList;
            }
        }
    }

    /// <summary>
    /// これでもTLE
    /// </summary>
    public class abc239_e_2
    {
        public abc239_e_2()
        {
            //input-------------
            var NQ = In.ReadAry<int>().ToArray();
            (var N, var Q) = (NQ[0], NQ[1]);
            var X = In.ReadAry<int>().ToArray();
            var AB = new List<(int A, int B)>();
            for (int i = 0; i < N - 1; i++)
            {
                var _ab = In.ReadAry<int>().ToArray();
                AB.Add((_ab.Min(), _ab.Max())); //そーとしておく
            }
            var VK = new List<(int V, int K)>();
            for (int i = 0; i < Q; i++)
            {
                var _vk = In.ReadAry<int>().ToArray();
                VK.Add((_vk[0], _vk[1]));
            }

            //calc--------------
            var ABsort = AB.OrderBy(p => p.A).ToList(); //根がどっちかわからないということがおこらないようにする
            var li = new Dictionary<int, List<int>>();
            li.Add(1, new List<int>());
            li[1].AddRange(Enumerable.Range(1, N));
            for (int i = 2; i <= N; i++)
                li.Add(i, new List<int>());

            //つながりのうち、根がどっちか側にあるかを考慮しないといけない
            //確実に1とつながってる場所から始めることで計算量を最小にできるはず
            foreach (var ab in ABsort.Where(p => p.A == 1))
                getChild(ab.B, 1);

            for (int i = 0; i < Q; i++)
            {
                var rr = li[VK[i].V].Distinct().Select(p => X[p - 1]).OrderByDescending(q => q).ToList(); //X取得・大きい方から
                Out.Write(rr[VK[i].K - 1]);
            }

            //--------------------------------------------------------------------------------
            //noの枝リスト取得
            IEnumerable<int> getChild(int no, int Root) //Root:根とつながってる腕のno
            {
                var r = new List<int>();
                r.AddRange(ABsort.Where(p => (p.A == no && p.B != Root)).Select(p => p.B));
                r.AddRange(ABsort.Where(p => (p.B == no && p.A != Root)).Select(p => p.A));

                if (r.Count() == 0) //子がいない
                {
                    r.Add(no); //自身を追加
                    li[no] = r; //保存
                    return r;
                }
                else//子がいるときはさらに子を取得..
                {
                    var rc = new List<int>();
                    foreach (var c in r)
                        rc.AddRange(getChild(c, no));

                    rc.Add(no); //自身を追加
                    li[no] = rc; //保存
                    return rc;
                }
            }
        }
    }
}