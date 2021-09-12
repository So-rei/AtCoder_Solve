using System;


public class AtCoder
{

    static void Main()
    {
        //入力*
        string instr;
        instr = Console.ReadLine();

        //宣言
        string result;

        string[] in_str_divide;
        in_str_divide = (instr.Split(' '));

        int W,H;

        //処理

        W = Convert.ToInt32(in_str_divide[0]);
        H = Convert.ToInt32(in_str_divide[1]);

        //[W,H] = [W-1,H] + [W,H-1]

        result = Convert.ToString(new Main_Calc().calc_data(W, H) % 1000000007);

        //出力*
        result = result + "\r\n";
        System.Console.WriteLine(result);
    }

    public class Main_Calc
    {
        public int[,] dataset = new int[100000, 100000];

        public int calc_data(int i, int j)
        {

            int iresult;
            if (this.dataset[i, j] != 0)
            {
                return dataset[i, j];
            }
            if (dataset[j, i] != 0)
            {
                return dataset[j, i];
            }

            if (i == 2)
            {
                return j;
            }
            if (j == 2)
            {
                return i;
            }

            iresult = calc_data(i - 1, j) + calc_data(i, j - 1);

            dataset[i, j] = iresult;
            return iresult;
        }
    }
}
