using System;
using System.Collections.Generic;
using System.Linq;

namespace FlatListToTree
{
    public class TreeModelBuilder3 : ITreeModelBuilder
    {
        public string StrategyName => "Strategy3";

        public TreeModel Build(List<FlatModel> flatModelList)
        {
            var treeDict = flatModelList.ToDictionary(x => x.Id, x => MapToTreeModel(x));
            TreeModel root = null;

            foreach (var kvp in treeDict)
            {
                if(treeDict.TryGetValue(kvp.Value.Id, out var parent))
                {
                    parent.Children.Add(kvp.Value);
                }
                else
                {
                    root = kvp.Value;
                }

                kvp.Value.Id = kvp.Key;
            }

            return root;
        }

        private static TreeModel MapToTreeModel(FlatModel flatModel)
        {
            return new TreeModel
            {
                Id = flatModel.ParentId
            };
        }
    }
}
