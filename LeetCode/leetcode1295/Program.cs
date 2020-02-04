using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode1295
{
    class Program
    {
        public static int FindNumbers(int[] nums)
        {
            int ret = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i].ToString().Length % 2 == 0)
                {
                    ret ++;
                }
            }
            return ret;
        }
        static void Main(string[] args)
        {
            int[] nums = new int[] { 12, 345, 2, 6, 7896 };
            int Ans = FindNumbers(nums);
            Console.WriteLine(Ans);
            Console.ReadLine();
        }
    }
}
