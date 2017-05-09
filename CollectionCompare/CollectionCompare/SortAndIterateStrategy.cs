using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CollectionCompare
{
    public class SortAndIterateStrategy : ICompareStrategy
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

            list1.Sort();
            list2.Sort();

            var result = new CompareResult();

            var sourceIterator = new EnumerableIterator(list1);
            var destinationIterator = new EnumerableIterator(list2);

            while (sourceIterator.HasCurrent && destinationIterator.HasCurrent)
            {
                if (sourceIterator.Current < destinationIterator.Current)
                {
                    result.Added.Add(sourceIterator.Current);
                    sourceIterator.MoveNext();
                }
                else if (sourceIterator.Current < destinationIterator.Current)
                {
                    result.Removed.Add(destinationIterator.Current);
                    destinationIterator.MoveNext();
                }
                else
                {
                    result.Existing.Add(destinationIterator.Current);
                    sourceIterator.MoveNext();
                    destinationIterator.MoveNext();
                }
            }

            while (sourceIterator.HasCurrent)
            {
                result.Added.Add(sourceIterator.Current);
                sourceIterator.MoveNext();
            }

            while (destinationIterator.HasCurrent)
            {
                result.Removed.Add(destinationIterator.Current);
                destinationIterator.MoveNext();
            }

            result.TotalMiliseconds = sw.ElapsedMilliseconds;

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
                list2.Add(random.Next(itemsCount * 100));
            }
        }
        internal class EnumerableIterator
        {
            private readonly IEnumerator<int> _enumerator;

            public EnumerableIterator(IEnumerable<int> enumerable)
            {
                _enumerator = enumerable.GetEnumerator();
                MoveNext();
            }

            public bool HasCurrent { get; private set; }

            public int Current
            {
                get { return _enumerator.Current; }
            }

            public void MoveNext()
            {
                HasCurrent = _enumerator.MoveNext();
            }
        }
    }
}
