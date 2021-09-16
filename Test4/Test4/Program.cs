using System;
using System.Collections.Generic;

namespace Test4
{
    class Program
    {
        public int CalPoints(string[] ops)
        {
            List<int> records = new List<int>();
            int ans = 0;
            foreach (var item in ops)
            {
                int newRecord;
                int length = records.Count;
                bool add = true;
                if (!int.TryParse(item, out newRecord))
                {
                    if (item == "C" && length > 0)
                    {
                        newRecord = records[length - 1] * -1;
                        add = false;
                    }
                    else if (item == "D" && length > 0)
                        newRecord = records[length - 1] * 2;
                    else if (length >= 2)
                        newRecord = records[length - 1] + records[length - 2];
                }

                ans += newRecord;
                if (add)
                    records.Add(newRecord);
                else
                    records.RemoveAt(length - 1);
            }

            return ans;
        }
        static void Main(string[] args)
        {
            string[] ops = { "5" ,"2","C","D","+"};
            //string[] ops = { "5","-2","4","C","D","9","+","+"};
            //string[] ops = { "1"};
            Program program = new Program();
            int output = program.CalPoints(ops);
            Console.WriteLine(output);
        }
    }
}
