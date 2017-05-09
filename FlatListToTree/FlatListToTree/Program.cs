using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FlatListToTree
{
    class Program
    {
        static void Main(string[] args)
        {
            var strategyList = new List<ITreeModelBuilder>
                {
                    new TreeModelBuilder(),
                    new TreeModelBuilder2(),
                    new TreeModelBuilder3()
                };

            while (true)
            {
                Console.WriteLine("Number of nodes to generate:");
                var nodeCount = Int32.Parse(Console.ReadLine());
                var flatList = GenerateData(nodeCount);

                var results = new List<Tuple<string, long>>();

                foreach (var strategy in strategyList)
                {
                    var sw = Stopwatch.StartNew();
                    var treeModel = strategy.Build(flatList);
                    results.Add(new Tuple<string,long>(strategy.StrategyName, sw.ElapsedMilliseconds));
                }

                results = results.OrderBy(x => x.Item2).ToList();
                foreach(var result in results)
                {
                    Console.WriteLine($"{result.Item1} Time in ms: {result.Item2}");
                }
            }
        }

        static List<FlatModel> GenerateData(int nodeCount)
        {
            var result = new List<FlatModel>();
            var id = Guid.NewGuid();

            for(int i = 0; i<nodeCount; i++)
            {
                var parentId = Guid.NewGuid();
                result.Add(new FlatModel { Id = id, ParentId = parentId });
                id = parentId;
            }

            return result.OrderBy(x => x.Id).ToList();
        }

        
    }
}