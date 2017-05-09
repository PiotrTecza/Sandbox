using System;

namespace CollectionCompare
{
    class Program
    {
        static void Main(string[] args)
        {
            ICompareStrategy sortAndIterate = new SortAndIterateStrategy();
            ICompareStrategy simpleStrategy = new SimpleStrategy();
            ICompareStrategy hashStrategy = new HashStrategy();

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Specify items count");
                var itemsCount = Int32.Parse(Console.ReadLine());

                Console.WriteLine();
                Console.WriteLine("SimpleStrategy");
                ExecuteStratey(itemsCount, simpleStrategy);

                Console.WriteLine();
                Console.WriteLine("SortAndIterate");
                ExecuteStratey(itemsCount, sortAndIterate);

                Console.WriteLine();
                Console.WriteLine("HashStrategy");
                ExecuteStratey(itemsCount, hashStrategy);

            }
        }

        static void ExecuteStratey(int itemsCount, ICompareStrategy strategy)
        {
            var result = strategy.Execute(itemsCount);

            Console.WriteLine("Added: " + result.Added.Count);
            Console.WriteLine("Removed: " + result.Removed.Count);
            Console.WriteLine("Existing: " + result.Existing.Count);
            Console.WriteLine("TotalMiliseconds: " + result.TotalMiliseconds);
        }
    }
}