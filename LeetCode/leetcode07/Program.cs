using System;

/*
  
给出一个 32 位的有符号整数，你需要将这个整数中每位上的数字进行反转。

示例 1:

输入: 123
输出: 321
*/
namespace leetcode07
{
    class Program
    {
        public static int Reverse(int input)
        {

            long result = 0;
            while (input != 0)
            {

                int temp = input % 10; //取出餘數存起來
                result = result * 10 + temp; //把存起來的餘數放到result, 每次要多加10位數
                input = input / 10; //原本的input的餘數已經放到前面了所以要/10，最後就會變成0

            }
            if (result > int.MaxValue || result < int.MinValue)
            {
                return 0;
            }
            return (int)result;

        }
        static void Main(string[] args)
        {
            int x = 3256;
            long r = Reverse(x);
            Console.WriteLine(r);
            Console.ReadLine();

        }
    }
}
