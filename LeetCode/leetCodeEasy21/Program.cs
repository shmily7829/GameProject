using System;
using System.Collections.Generic;

namespace leetCodeEasy21
{
    class Program
    {
        /*
        將成為兩個有序鍊錶合併為一個新的有序鍊錶並返回。新鍊錶是通過拆分給定的兩個鍊錶的所有分組組成的。
        Merge two sorted linked lists and return it as a new list.
        The new list should be made by splicing together the nodes of the first two lists.

        示例：
        輸入：1-> 2-> 4，1-> 3-> 4
        輸出：1-> 1-> 2-> 3-> 4-> 4
         */

        static void Main(string[] args)
        {
            int[] nums1 = new int[3] { 1, 2, 4 };
            int[] nums2 = new int[3] { 1, 3, 4 };
            List<int> numList = new List<int>();// 宣告一個list
            numList.AddRange(nums1);            // 將數列1加入
            numList.AddRange(nums2);            // 將數列2加入
            int[] nums3 = numList.ToArray();    // 將陣列合併,宣告為新的陣列
            Array.Sort(nums3);                  // 將陣列排序

            foreach (int i in nums3)            // 顯示排序後的陣列數字
            {
                Console.Write("{0}, ", i);
            }
            Console.Read();
      
            }

        }
    }
}
