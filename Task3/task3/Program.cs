using System;
using System.Collections.Generic;

public class GFG
{

    static int count_element(int N, int K, int[] arr)
    {
      
        Dictionary<int, int> mp = new Dictionary<int, int>();

        for (int i = 0; i < N; ++i)
            if (mp.ContainsKey(arr[i]))
            {
                mp[arr[i]] = mp[arr[i]] + 1;
            }
            else
            {
                mp.Add(arr[i], 1);
            }

        int answer = 0;

        foreach (KeyValuePair<int, int> i in mp)
        {

         
            if (mp.ContainsKey(i.Key + K))

                answer += i.Value;
        }

        return answer;
    }


    public static void Main(String[] args)
    {
  
        //int[] arr = { 1,2,3 };
        //int[] arr = { 1,1,3,3,5,5,7,7 };
        //int[] arr = { 1,3,2,3,5,0 };
        //int[] arr = { 1,1,2,2 };
        int[] arr = { 1,1,2 };

  
        int N = arr.Length;

   
        int K = 1;

        Console.Write(count_element(N, K, arr));
    }
}