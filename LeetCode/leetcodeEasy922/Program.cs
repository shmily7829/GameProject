using System;
using System.Collections.Generic;
namespace leetcodeEasy922
{
    class Program
    {
        static public int[] SortArrayByParityII(int[] A)
        {
            //思路 遍歷數組，奇偶分離，然後重新放進原數組。
            //int[] odd = new int[A.Length / 2]; //奇數組
            //int[] even = new int[A.Length / 2]; //偶數組
            //int j = 0 ;
            //int k = 0 ;
            //int[] result = new int[A.Length]; //被分出來的數字要丟到result

            //for (int i = 0; i < A.Length; i++)
            //{
            //    if (A[i] % 2 == 0) //偶數
            //    {
            //        even[j++] = A[i];
            //    }
            //    else
            //    {
            //        odd[k++] = A[i];
            //    }
            //}
            //j = 0;
            //k = 0;
            //for (int i = 0; i < A.Length; i += 2)
            //{
            //    result[i] = even[j++];
            //    result[i + 1] = odd[k++];
            //}
            //return result;

            int oneIndex = 1;//單數
            int twoIndex = 0;//雙數
            int[] result = new int[A.Length];
            for (int i = 0; i < A.Length; i++)
            {
                if (A[i] % 2 == 0)
                {
                    result[twoIndex] = A[i];
                    twoIndex += 2;// twoIndex + 2;
                }
                else
                {
                    result[oneIndex] = A[i];
                    oneIndex += 2;
                }
            }
            return result;
        }
        static void Main(string[] args)
        {
            //List<int> listA = new List<int> {};
            int[] listA = new int[] { 4, 2, 5, 7 };
            int[] SortArr = SortArrayByParityII(listA);
            foreach (int i in SortArr)
            {
                Console.WriteLine("{0}", i);
            }
            Console.Read();
        }
    }
}
