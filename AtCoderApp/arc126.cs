using System;
using System.Collections.Generic;
using System.Linq;

namespace AtCoderApp
{
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        new arc216_a();
    //    }
    //}

    //20210919
    //AC
    public class arc216_a
    {
        public arc216_a()
        {
            //input
            var T = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < T; i++)
            {
                var CaseN = Console.ReadLine().Split(' ').Select(p => Convert.ToInt64(p)).ToArray();
                CalcStart(CaseN);
            }
            //-> 3,4､2の順で使う


            return;
        }

        private void CalcStart(long[] c)
        {
            long max = 0;
            //calc
            //10 = 3 * 2 + 4
            //10 = 3 * 2 + 2 * 2
            //10 = 2 + 4 * 2
            //10 = 2 * 3 + 4
            //10 = 2 * 5

            var root1 = Math.Min(c[1] / 2, c[2]);
            max += root1;
            c[1] -= root1 * 2;
            c[2] -= root1;

            var root2 = Math.Min(c[0] / 2, c[1] / 2);
            max += root2;
            c[0] -= root2 * 2;
            c[1] -= root2 * 2;

            var root3 = Math.Min(c[0], c[2] / 2);
            max += root3;
            c[0] -= root3;
            c[2] -= root3 * 2;

            var root4 = Math.Min(c[0] / 3, c[2]);
            max += root4;
            c[0] -= root4 * 3;
            c[2] -= root4;

            var root5 = c[0] / 5;
            max += root5;

            Console.WriteLine(max);
        }
    }


    //20210919
    //TLE62/68
    public class arc216_b
    {
        public arc216_b()
        {
            //input
            var NM = Console.ReadLine().Split(' ');
            var N = Convert.ToInt32(NM[0]);
            var M = Convert.ToInt32(NM[1]);

            var ab = new List<int[]>();
            for (int i = 0; i < M; i++)
            {
                var newab = Console.ReadLine().Split(' ').Select(p => Convert.ToInt32(p)).ToArray();
                ab.Add(new int[] { i, newab[0], newab[1] });
            }

            //calc
            var FlagedAB = new List<Resource>();

            for (int i = 0; i < M; i++)
            {
                var _ab = ab[i];

                var Ret = new Resource();
                Ret.Index = _ab[0];
                //選択線分と重複する線分をかぞえる
                Ret.HitIndex = ab.Where(p => p[0] != i && ((_ab[1] >= p[1] && _ab[2] <= p[2]) || (_ab[1] <= p[1] && _ab[2] >= p[2]))).Select(q => q[0]).ToList();
                Ret.rad = Math.Abs(_ab[1] - _ab[2]); //小さいほど先
                Ret.near = Math.Min(Math.Min(_ab[1], N - _ab[1]), Math.Min(_ab[2], N - _ab[2])); //小さいほど先

                FlagedAB.Add(Ret);
            }

            //ソート
            //重複本数が少ない線分 -> 角度が90度に近い線分 ->両端に近い線分　の順で使用（？）
            var SortedAB = FlagedAB.OrderBy(p => p.HitIndex.Count()).ThenBy(p => p.rad).ThenBy(p => p.near).ToList();

            var K = 0;//結果
            while (SortedAB.Count() > 0)
            {
                var o = SortedAB.First();

                //重複分を消す
                SortedAB.Remove(o);
                SortedAB.RemoveAll(p => o.HitIndex.Contains(p.Index));

                K++;
            }

            Console.WriteLine(K);

            return;
        }

        public class Resource
        {
            public int Index;
            public List<int> HitIndex = new List<int>();
            public int rad;
            public int near;
        }
    }

    //20210919
    //TLE63/68
    //public class arc216_b
    //{        
    //    public arc216_b()
    //    {
    //        //input
    //        var NM = Console.ReadLine().Split(' ');
    //        var N = Convert.ToInt32(NM[0]);
    //        var M = Convert.ToInt32(NM[1]);

    //        var ab = new List<int[]>();
    //        for (int i = 0; i < M; i++)
    //        {
    //            var newab = Console.ReadLine().Split(' ').Select(p => Convert.ToInt32(p)).ToArray();
    //            ab.Add(newab);
    //        }

    //        //calc

    //        var FlagedAB = new Dictionary<int, Resource>();

    //        for (int i = 0; i < M; i++)
    //        {
    //            var _ab = ab[i];

    //            var Ret = new Resource();

    //            //選択線分と重複する線分をかぞえる
    //            for (int i2 = 0; i2 < M; i2++)
    //            {
    //                if (i == i2) continue;

    //                if ((_ab[0] >= ab[i2][0] && _ab[1] <= ab[i2][1]) || (_ab[0] <= ab[i2][0] && _ab[1] >= ab[i2][1]))
    //                    Ret.HitIndex.Add(i2);
    //            }

    //            Ret.rad = Math.Abs(_ab[0] - _ab[1]); //小さいほど先
    //            Ret.near = Math.Min(Math.Min(_ab[0], N - _ab[0]), Math.Min(_ab[1], N - _ab[1])); //小さいほど先

    //            FlagedAB.Add(i,Ret);
    //        }

    //        //ソート
    //        //重複本数が少ない線分 -> 角度が90度に近い線分 ->両端に近い線分　の順で使用（？）
    //        var SortedAB = FlagedAB.OrderBy(p => p.Value.HitIndex.Count()).ThenBy(p => p.Value.rad).ThenBy(p => p.Value.near).ToList();

    //        var K = 0;//結果
    //        for (int i = 0; i < M; i++)
    //        {
    //            //選択済みの線分と重複する線分
    //            if (SortedAB[i].Value.IsErased)
    //            {
    //                continue;
    //            }
    //            //重複なし
    //            if (SortedAB[i].Value.HitIndex.Count() == 0)
    //            {
    //                K++;
    //                continue;
    //            }
    //            //計算終了
    //            if (!SortedAB.Exists(p => p.Value.IsErased == false))
    //            {
    //                break;
    //            }

    //            //重複分を消す
    //            foreach (var _HitIndex in SortedAB[i].Value.HitIndex)
    //            {
    //                //SortedAB[_HitIndex].Value.IsErased = true;
    //                SortedAB.Where(p => p.Key == _HitIndex).First().Value.IsErased = true;
    //            }
    //            K++;
    //        }

    //        Console.WriteLine(K);

    //        return;
    //    }

    //    public class Resource
    //    {
    //        public int Index;
    //        public List<int> HitIndex = new List<int>();
    //        public int rad;
    //        public int near;

    //        public bool IsErased = false;
    //    }
    //}

    //TLE63/68
    //public class arc216_b
    //{
    //    public arc216_b()
    //    {
    //        //input
    //        var NM = Console.ReadLine().Split(' ');
    //        var N = Convert.ToInt32(NM[0]);
    //        var M = Convert.ToInt32(NM[1]);

    //        var ab = new List<int[]>();
    //        for (int i = 0; i < M; i++)
    //        {
    //            var newab = Console.ReadLine().Split(' ').Select(p => Convert.ToInt32(p)).ToArray();
    //            ab.Add(new int[] { i, newab[0], newab[1] });
    //        }

    //        //calc
    //        var FlagedAB = new List<Resource>();

    //        for (int i = 0; i < M; i++)
    //        {
    //            var _ab = ab[i];

    //            var Ret = new Resource();
    //            Ret.Index = _ab[0];
    //            //選択線分と重複する線分をかぞえる
    //            Ret.HitIndex = ab.Where(p => p[0] != i && ((_ab[1] >= p[1] && _ab[2] <= p[2]) || (_ab[1] <= p[1] && _ab[2] >= p[2]))).Select(q => q[0]).ToList();
    //            Ret.rad = Math.Abs(_ab[1] - _ab[2]); //小さいほど先
    //            Ret.near = Math.Min(Math.Min(_ab[1], N - _ab[1]), Math.Min(_ab[2], N - _ab[2])); //小さいほど先

    //            FlagedAB.Add(Ret);
    //        }

    //        //ソート
    //        //重複本数が少ない線分 -> 角度が90度に近い線分 ->両端に近い線分　の順で使用（？）
    //        var SortedAB = FlagedAB.OrderBy(p => p.HitIndex.Count()).ThenBy(p => p.rad).ThenBy(p => p.near).ToList();

    //        var K = 0;//結果
    //        for (int i = 0; i < M; i++)
    //        {
    //            //選択済みの線分と重複する線分
    //            if (SortedAB[i].IsErased)
    //            {
    //                continue;
    //            }
    //            //重複なし
    //            if (SortedAB[i].HitIndex.Count() == 0)
    //            {
    //                K++;
    //                continue;
    //            }
    //            //計算終了
    //            if (!SortedAB.Exists(p => p.IsErased == false))
    //            {
    //                break;
    //            }

    //            //重複分を消す
    //            foreach (var _HitIndex in SortedAB[i].HitIndex)
    //            {
    //                //SortedAB[_HitIndex].Value.IsErased = true;
    //                SortedAB.Where(p => p.Index == _HitIndex).First().IsErased = true;
    //            }
    //            K++;
    //        }

    //        Console.WriteLine(K);

    //        return;
    //    }

    //    public class Resource
    //    {
    //        public int Index;
    //        public List<int> HitIndex = new List<int>();
    //        public int rad;
    //        public int near;

    //        public bool IsErased = false;
    //    }
    //}
}
