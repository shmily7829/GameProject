using System;

namespace BubbleSortAlgorithm
{
    class Program
    {
        public static void BubbleSort(int[] list)
        {
            int temp;
            bool Flag = false; //旗標，是否已經將最大數排到最右邊
            for (int i = 1; i <= list.Length - 1 && Flag == false; i++)
            {    // 外層迴圈控制比較回數
                Flag = true;
                for (int k = 1; k <= list.Length - i; k++)
                {  // 內層迴圈控制每回比較次數            
                    if (list[k] < list[k - 1])
                    {  // 比較鄰近兩個物件，右邊比左邊小時就互換。	       
                        temp = list[k];
                        list[k] = list[k - 1];
                        list[k - 1] = temp;
                        Flag = false;//設為初值
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            int[] list = new int[5];
            Random r = new Random();
            Console.WriteLine("Before sort");
            for (int i = 0; i < list.Length; i++)
            {
                list[i] = r.Next(99);
                Console.Write("{0} ", list[i]);
            }

            BubbleSort(list);

            Console.WriteLine("\nAfter sort");
            for (int n = 0; n < list.Length; n++)
                Console.Write("{0} ", list[n]);
            Console.Read();
        }
    }
}
