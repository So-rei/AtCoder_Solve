using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//2022-01-29
namespace AtCoderApp
{
    /// <summary>
    /// 1発AC！
    /// </summary>
    public class arc134_a
    {
        public arc134_a()
        {
            //input-------------
            var nlw = In.ReadAry<long>().ToArray();
            (var N, var L, var W) = (nlw[0], nlw[1], nlw[2]);
            var a = In.ReadAry<long>().ToList();

            //calc--------------
            long cnt = 0; //シート数
            for (long i = 0; i <= L; i++)
            {
                if (a.Count() == 0)
                {
                    //シートがないのであとは機械的にシートを設置してそのまま終了
                    cnt += ((L - i) % W) > 0 ? 1 : 0;
                    cnt += (L - i - (L - i) % W) / W;
                    break;
                }
                else if (i < a[0])
                {
                    //シート外なのでシートが次のa[0]と重なるまで設置
                    long mod = (a[0] - i) % W;
                    long cnt_next = (mod > 0 ? 1 : 0) + (a[0] - i - mod) / W;
                    cnt += cnt_next;
                    i = a[0] + W - 1;
                    a.RemoveAt(0); //見終わったaは削除していくことで計算量減らす
                    continue;
                }
                else
                {
                    //既に置いてあるシートの範囲内
                    i = a[0] + W - 1;
                    a.RemoveAt(0);
                    continue;
                }
            }

            Out.Write(cnt);

        }
    }


    /// <summary>
    /// 時間切れてから考え直した結果 AC
    /// </summary>
    public class arc134_b
    {
        public arc134_b()
        {
            //input-------------
            var N = In.Read<int>();
            var s = In.Read<string>();

            //calc--------------

            //同じ文字は2回交換できない・部分列なので
            //先頭文字x(xi個)について、先頭からxi個と交換(交換後の位置は辞書順に割り付ける)　←これのせいで先頭から順..では処理できない
            //xi～xi個目の交換した位置の間で、xの次の文字y(yi個)について、…
            //以下同様
            var cStr = s.ToArray();

            var maxi = N; //文字xについて交換したとき、交換された位置
            var mini = 0; //文字xについて交換したとき、交換した位置
            foreach (var c in "abcdefghijklmnopqrstuvwxyz")
            {
                while (mini < maxi && cStr[mini] <= c) //次の文字に、今調べている文字以下が入っている場合は交換不要なので飛ばす
                    mini++;
                //交換範囲がなくなった時、終了
                if (maxi - mini <= 1)
                    break;

                for (int i = maxi - 1; i > mini; i--)
                {
                    if (cStr[i] == c && i != mini)
                    {
                        //交換処理
                        cStr[i] = cStr[mini];
                        cStr[mini] = c;

                        //先頭,末尾indexを更新
                        mini++;
                        maxi = i;
                        while (mini < maxi && cStr[mini] <= c) //次の文字に、今調べている文字以下が入っている場合は交換不要なので飛ばす
                            mini++;
                    }
                }
            }

            Out.WriteMany(cStr.ToArray(), "");
        }
    }

    /// <summary>
    /// WA　考え途中で時間切れ
    /// </summary>
    public class arc134_bx
    {
        public arc134_bx()
        {
            //input-------------
            var N = In.Read<int>();
            var s = In.Read<string>();

            //calc--------------
            var cStr = s.ToArray();
            //同じ文字は2回交換できない・部分列なので
            //先頭文字x(xi個)について、先頭からxi個と交換(交換後の位置は辞書順に割り付ける)　←これのせいで先頭から順..では処理できない
            //xi～xi個目の交換した位置の間で、xの次の文字y(yi個)について、…
            //以下同様

            var maxi = N; //文字xについて交換したとき、交換された位置min
            var mini = 0; //文字xについて交換したとき、交換した位置max
            foreach (var c in "abcdefghijklmnopqrstuvwxyz")
            {
                var changed_index = new List<int>(); //cが入ってるx文字目のindex
                var change_char = new List<char>(); //cと交換される(no+1)文字目の文字

                //交換範囲がなくなった時、終了
                //交換対象のs[no]よりも、大きい辞書順しかもう存在しない時終了
                if (maxi - mini <= 1 || cStr[mini] < c)
                    break;

                for (int i = maxi - 1; i > mini; i--)
                {
                    if (cStr[i] == c && i != mini)
                    {
                        //交換する先頭の処理して1文字移動
                        changed_index.Add(i);
                        change_char.Add(s[mini]);
                        cStr[mini] = c;
                        mini++;
                        maxi = i;
                    }
                }

                if (changed_index.Count > 0)
                {
                    //交換される側の処理
                    maxi = changed_index.Max() + 1;

                    var csorted = change_char.OrderBy(p => p).ToArray();
                    for (int j = 0; j < changed_index.Count(); j++)
                    {
                        cStr[changed_index[j]] = csorted[j];
                    }
                }
            }

            Out.WriteMany(cStr.ToArray(), "");
        }
    }
}