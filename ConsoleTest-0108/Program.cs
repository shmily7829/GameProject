using System;
using System.Collections.Generic;

namespace ConsoleTest_0108
{
    class Program
    {

        /*
        给定一个整数数组 nums 和一个目标值 target，
        请你在该数组中找出和为目标值的那两个整数，并返回他们的数组下标。
        你可以假设每种输入只会对应一个答案。
        但是，你不能重复利用这个数组中同样的元素。
        示例:
        给定 nums = [2, 7, 11, 15], target = 9
        因为 nums[0] + nums[1] = 2 + 7 = 9
        所以返回 [0, 1]
        */
        static int[] TwoSum(int[] nums, int target)
        {
            int[] result = new int[2]; //建立一個含有兩個數的陣列

            for (int i = 0; i < nums.Length; i++)//比對每一個數字
            {
                for (int k = i + 1; k < nums.Length; k++)
                {
                    if (nums[i] + nums[k] == target)
                    {
                        result[0] = i;
                        result[1] = k;
                        return result;
                    }
                }            
            }        
            return result;
        }    
        static void Main(string[] args)
        {      
            int[] nums = new int[4] { 2, 3, 6, 15 };
            int target = 9;
            int[] result = TwoSum(nums, target);
            Console.WriteLine("a是第{0}",result[0]);
            Console.WriteLine("b是第{0}",result[1]);
        }
    }
}
