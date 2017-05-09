using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CollectionCompare
{
    public class SimpleStrategy : ICompareStrategy
    {
        private List<int> list1, list2;

        public CompareResult Execute(int itemsCount)
        {
            PrepareData(itemsCount);
            var result = Compare();
            return result;
        }

        private CompareResult Compare()
        {
            var sw = Stopwatch.StartNew();

            var result = new CompareResult
            {
                Added = list1.Except(list2).ToList(),
                Removed = list2.Except(list1).ToList(),
                Existing = list1.Intersect(list2).ToList(),
                TotalMiliseconds = sw.ElapsedMilliseconds
            };

            return result;

        }

        private void PrepareData(int itemsCount)
        {
            list1 = new List<int>();
            list2 = new List<int>();
            var random = new Random();

            for (var i = itemsCount; i >= 1; i--)
            {
                list1.Add(random.Next(itemsCount * 10));
                list2.Add(random.Next(itemsCount * 10));
            }
        }
    }
}
