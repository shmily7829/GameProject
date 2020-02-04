using System;

namespace leetcodeEasy976
{
    /*
     假设a>b>c的话，我们实际上只需要判断b+c>a即可，也就是判断最小的两个值的和是不是最大值即可。那么问题就变得简单了，我们只需要将A反排序，然后判断A[i+1]+A[i+2]>A[i]即可，如果成立的话，那么第一个成立的三条边肯定就是最大的周长了
     */
    class Program
    {
        public static int LargestPerimeter(int[] A)
        {
            Array.Sort(A);
            //i=陣列中的第n個位置
            //遍歷陣列內容，從最大開始找 A.Length - 1 = 最大的數
            //陣列中至少要有3個數，故i >= 2 (0,1,2)
            {
                for (int i = A.Length - 1; i >= 2; i--) 
                //判斷 倒數第二 + 倒數第三 > 最大數
                //不符合就跳出回到for迴圈，排除陣列中的最大數，繼續比較剩餘的最大的三個數
                if (A[i - 1] + A[i - 2] > A[i])

                {
                    return A[i - 1] + A[i - 2] + A[i];
                }
            }
            return 0;

        }
        static void Main(string[] args)
        {
            int[] A = new int[5] { 1, 1, 1, 1, 3 };

            int Largest = LargestPerimeter(A);
            Console.WriteLine(Largest);
            Console.ReadLine();
        }
    }
}
