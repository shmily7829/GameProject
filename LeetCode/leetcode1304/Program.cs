using System;

namespace leetcode1304
{
    class Program
    {
        public static int[] SumZero(int n)
        {

            int[] result = new int[n];
            int sum = 0;
            for (int i = 0; i < n; i++) //第i個數字
            {
                if (i != n - 1)
                {
                    sum += i; //sum = sum + i
                    result[i] = i; //把i排進去
                }
                else
                {
                    result[i] = -sum; //最後一個數字的結果為前面總和的負數
                }
            }
            return result;

            /*
            int[] result = new int[n];
            result[0] = n;
            return result;
            */
        }
        static void Main(string[] args)
        {
            int n = 5;
            int[] list = SumZero(n);

            foreach (int i in list)
            {
                Console.Write("{0} ",i);
            }
            Console.ReadLine();
        }
    }
}
