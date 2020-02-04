using System;
using System.Collections.Generic;
using System.Linq;

namespace leetcodeEasy1122
{
    class Program
    {
        static public int[] RelativeSortArray(int[] arr1, int[] arr2)
        {
            /*
            dictionary語法: Dictionary<TKey,TValue>
            輸入你要查的單字(Key)例如apple，他就會回傳相對應的值(Value) 蘋果。
            */
            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            for (int i = 0; i < arr1.Length; i++)
            {
                //Dictionary<TKey,TValue>.ContainsKey(TKey) 方法:
                //判斷 IDictionary<TKey,TValue> 是否包含具有指定之索引鍵的項目。
                //count= ,初始為0
                if (dictionary.ContainsKey(arr1[i])) 
                {
                    dictionary[arr1[i]]++;
                }
                else
                {
                    //Dictionary<TKey,TValue>.Add(TKey, TValue) 方法
                    //將指定的索引鍵和值加入字典。
                    //count+1
                    dictionary.Add(arr1[i], 1);
                }
            }

            int[] result = new int[arr1.Length];
            int current = 0;
            for (int i = 0; i < arr2.Length; i++)
            {
                int key = arr2[i];
                if (dictionary.ContainsKey(key))
                {
                    for (int times = 0; times < dictionary[key]; times++)
                    {
                        result[current] = key;
                        current++;
                    }
                    //Dictionary<TKey, TValue>.Remove 方法
                    //將具有指定索引鍵的值從 Dictionary<TKey,TValue> 中移除。
                    dictionary.Remove(key);
                }
            }
            //把得到的dictionary的key設成list然後排序
            List<int> list = dictionary.Keys.ToList();
            list.Sort();
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < dictionary[list[i]]; j++)
                {
                    result[current] = list[i];
                    current++;
                }
            }
            return result;

        }
        
        static void Main(string[] args)
        {
            int[] arr1 = new int[] { 2, 3, 1, 3, 2, 4, 6, 7, 9, 2, 19 };
            int[] arr2 = new int[] { 2, 1, 4, 3, 9, 6 };
            int[] list = RelativeSortArray(arr1, arr2);
            foreach (int i in list)
            {
                Console.Write("{0} ",i);
            }
            Console.Read();

        }
    }
}
