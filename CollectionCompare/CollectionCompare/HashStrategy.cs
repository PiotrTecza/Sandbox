using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CollectionCompare
{
    public class HashStrategy : ICompareStrategy
    {
        private Dictionary<int,int> dict;
        private List<int> list2;

        public CompareResult Execute(int itemsCount)
        {
            PrepareData(itemsCount);
            var result = Compare();
            return result;
        }

        private CompareResult Compare()
        {
            var sw = Stopwatch.StartNew();

            var result = new CompareResult();

            foreach (var i in list2)
            {
                if (dict.Remove(i))
                {
                    result.Existing.Add(i);
                }
                else
                {
                    result.Removed.Add(i);
                }
            }

            result.Added = dict.Keys.ToList();
            result.TotalMiliseconds = sw.ElapsedMilliseconds;

            return result;

        }

        private void PrepareData(int itemsCount)
        {
            dict = new Dictionary<int, int>();
            list2 = new List<int>();
            var random = new Random();

            for (var i = itemsCount; i >= 1; i--)
            {
                dict.Add(i,i);
                list2.Add(random.Next(itemsCount * 10));
            }
        }
    }
}
