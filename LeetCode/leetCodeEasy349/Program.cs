using System;
using System.Collections.Generic;
using System.Linq;
/*
给定两个数组，编写一个函数来计算它们的交集。
示例 1:
输入: nums1 = [1,2,2,1], nums2 = [2,2]
输出: [2]
示例 2:
输入: nums1 = [4,9,5], nums2 = [9,4,9,8,4]
输出: [9,4]
说明:
输出结果中的每个元素一定是唯一的。
我们可以不考虑输出结果的顺序。
*/

namespace leetCodeEasy349
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = Intersection(new int[] { 1, 2, 2, 3 }, new int[] { 2, 3 });

            Console.Read();
        }

        public static int[] Intersection(int[] nums1, int[] nums2)
        {

            var sameArr = nums1.Intersect(nums2).ToArray();
            foreach (int i in sameArr)
            {
                System.Console.Write("{0} ", i);
            }

            return sameArr;

        }
    }
}
