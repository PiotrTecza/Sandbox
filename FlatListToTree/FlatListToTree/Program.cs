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
            while (true)
            {
                Console.WriteLine("Number of nodes to generate:");
                var nodeCount = Int32.Parse(Console.ReadLine());
                var flatList = GenerateData(nodeCount);

                var sw = Stopwatch.StartNew();
                var treeModel = new TreeModelBuilder().Build(flatList);
                Console.WriteLine($"Strategy1 - Time in ms: {sw.ElapsedMilliseconds}");

                sw = Stopwatch.StartNew();
                treeModel = new TreeModelBuilder2().Build(flatList);
                Console.WriteLine($"Strategy2 - Time in ms: {sw.ElapsedMilliseconds}");
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