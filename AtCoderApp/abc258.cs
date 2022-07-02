using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AtCoderApp
{
    public class abc258_a
    {
        public abc258_a()
        {
            //input-------------
            var K = In.Read<int>();

            //output------------
            var hh = 21;
            var mm = 0;
            hh += (int)(K / 60);
            mm += K % 60;

            Out.Write(hh.ToString("00") + ":" + mm.ToString("00"));
            return;
        }
    }

    //AC
    public class abc258_b
    {
        public abc258_b()
        {
            //input-------------
            var N = In.Read<int>();
            var A = In.ReadMany<string>(N).ToArray();

            //output------------
            var AL = new int[N, N];
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    AL[i, j] = Convert.ToInt32(A[i][j].ToString());

            long retMax = 0;
            int maxhead = -1;
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    maxhead = Math.Max(maxhead, AL[i, j]);

            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    if (AL[i, j] == maxhead) //開始位置=一番でかい数字なのは確定
                    {
                        long tmp_num = 0;
                        for (int t = 1; t <= 9; t++)
                        {
                            //方向ぶん全部やる
                            //muki:テンキー方向
                            if (t == 5) continue;
                            tmp_num = Math.Max(tmp_num, GetNext(i, j, t));
                        }
                        retMax = Math.Max(retMax, tmp_num);
                    }

            Out.Write(retMax);
            //-----

            long GetNext(int _ii, int _jj, int muki)
            {
                string _r = "";
                int x = 0;
                int y = 0;
                int loop = N;

                while (loop > 0)
                {
                    _r += A[(N + _ii + x) % N][(N + _jj + y) % N];

                    if (muki % 3 == 0)
                        y++;
                    if (muki % 3 == 1)
                        y--;

                    if (muki < 4)
                        x--;
                    if (muki > 6)
                        x++;

                    loop--;
                }
                return Convert.ToInt64(_r);
            }

            return;
        }
    }

    //AC5 TLE16
    public class abc258_c
    {
        public abc258_c()
        {
            //input-------------
            (var N, var Q) = In.ReadTuple2<int>();
            var S = In.Read<string>();

            var TX = new List<(int t, int x)>();
            for (int i = 0; i < Q; i++)
                TX.Add(In.ReadTuple2<int>());

            //output------------
            //直接文字列では扱わない。box化する
            var box = new int[N];
            for (int i = 0; i < N; i++)
                box[i] = i;

            for (int i = 0; i < Q; i++)
            {
                //出力
                if (TX[i].t == 2)
                {
                    for (int j = 0; j < N; j++)
                        if (box[j] == TX[i].x - 1)
                        {
                            Out.Write(S[j]);
                            continue;
                        }
                    //Out.Write(S[box.Select((p,index) => (p,index)).Single(q => q.p == TX[i].x - 1).index]);
                    
                    continue;
                }

                //main

                for (int c = 0; c < N; c++)
                {
                    box[c] += TX[i].x;
                    box[c] %= N;
                }
            }

            return;
        }
    }

    //時間切れAC
    //index位置が+と%だけで処理できるのだから、こんな方法で良かった。。
    public class abc258_c2
    {
        public abc258_c2()
        {
            //input-------------
            (var N, var Q) = In.ReadTuple2<int>();
            var S = In.Read<string>();

            var TX = new List<(int t, int x)>();
            for (int i = 0; i < Q; i++)
                TX.Add(In.ReadTuple2<int>());

            //output------------
            //直接文字列では扱わない
            long tSum = 0;
            for (int i = 0; i < Q; i++)
            {
                //出力
                if (TX[i].t == 2)
                {
                    Out.Write(S[(int)((N + TX[i].x - 1 - tSum % N) % N)]);
                }
                else //1処理
                {                    
                    tSum += TX[i].x;
                }
            }

            return;
        }
    }

    //時間切れで試し解き
    //1回間違えたがAC.
    public class abc258_d
    {
        public abc258_d()
        {
            //input-------------
            (var N, var X) = In.ReadTuple2<int>();

            var AB = new List<(int index, long A, long B)>();
            for (int i = 0; i < N; i++)
            {
                var _AB = In.ReadAry<long>().ToArray();
                AB.Add((i, _AB[0], _AB[1]));
            }

            //output------------
            //最も効率がいいやつはなるべく周回するはず。
            //=2回以上周るコースはたった1つのはず。
            //ステージ1～i-1 を1回　+ ステージiをX-i+1回　このかたち以外ありえない
            long ret = AB[0].A + AB[0].B * X; //最終結果
            long tmp_cnt = AB[0].A + AB[0].B; //ステージ1～i-1を1回のコスト
            for (int i = 1; i < Math.Min(N, X); i++) //■WA修正　範囲はN,X
            {
                ret = Math.Min(ret, tmp_cnt + AB[i].A + AB[i].B * (X - i));
                tmp_cnt += AB[i].A + AB[i].B;
            }

            Out.Write(ret);

            return;
        }
    }

    //試し解き
    //AC17,TLE1,RE2??
    //この方針で結構行けると思うんだけど、原因がつかめん
    public class abc258_e
    {
        public abc258_e()
        {
            //input-------------
            (var N, var Q, var X) = In.ReadTuple3<long>();
            var W = In.ReadAry<long>().ToArray();
            var K = In.ReadMany<long>(Q).ToArray();

            //output------------
            long Woneset = 0;//一周分のジャガイモの重量
            try
            {
                Woneset = W.Sum();
            }
            catch
            {
                Woneset = 0; //C#じゃここがlong超える場合は対処不可能...
            }
            var boxes = new long[N];//i番目が最初のジャガイモである時、箱に入れることができるジャガイモの数
            for (int i = 0; i < N; i++)
                boxes[i] = -1;
            var box_st = new int[N];//i番目のクエリは何番目のジャガイモから始まるか?
                                    //N<Kqの場合はココに入り切らないが、その場合は確実に循環しているので、そこから取得する
            int roop_cnt = -1;//何回で循環するか?
            int notroop = 0;//初回しか通らないルートができることがある。その時用。最初のループしない期間の値(<roopcnt)

            //初期値(0回目)セット
            box_st[0] = 0;
            //2回目以降
            for (int i = 1; i < N + 1; i++)
            {
                if (boxes[box_st[i - 1]] == -1)
                    boxes[box_st[i - 1]] = GetBoxes(box_st[i - 1]); //まだ計算していない場合はカウントする
                else
                {
                    //ループが発生した             
                    notroop = box_st.Select((p, index) => (p, index)).First(q => q.p == box_st[i - 1]).index;
                    roop_cnt = i - 1 - notroop;
                    break;
                }

                box_st[i] = (int)((box_st[i - 1] + boxes[box_st[i - 1]]) % N);
            }

            for (int i = 0; i < Q; i++)
            {
                if (notroop >= K[i])
                    Out.Write(boxes[box_st[(K[i] - 1)]]); //初回ルート
                else
                    Out.Write(boxes[box_st[notroop + (K[i] - 1 - notroop) % roop_cnt]]);//それ以降
            }


            //---
            long GetBoxes(int index)
            {
                //＊周+N個
                long tmp_box = (int)Math.Floor((double)X / Woneset) * N;
                long tmp_X = X % Woneset;

                int ic = index;
                while (tmp_X > 0)
                {
                    if (ic == N) ic = 0;
                    tmp_box++;
                    tmp_X -= W[ic];
                    ic++;
                }

                return tmp_box;
            }

            return;
        }
    }
}