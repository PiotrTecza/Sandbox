using System;
using System.Collections.Generic;
using System.Linq;

namespace FlatListToTree
{
    public class TreeModelBuilder
    {
        public TreeModel Build(List<FlatModel> flatModelList)
        {
            var treeDict = new Dictionary<Guid, TreeModel>();
            var potentialRoots = new Dictionary<Guid, TreeModel>();

            foreach (var flatModel in flatModelList)
            {
                var treeModel = MapToTreeModel(flatModel);
                if (treeDict.TryGetValue(treeModel.Id, out var tempParent))
                {
                    treeModel.Children = tempParent.Children;
                    treeDict[treeModel.Id] = treeModel;
                    potentialRoots.Remove(treeModel.Id);
                }
                else
                {
                    treeDict.Add(treeModel.Id, treeModel);
                }

                if (treeDict.TryGetValue(flatModel.ParentId, out var parent))
                {
                    parent.Children.Add(treeModel);
                }
                else
                {
                    tempParent = new TreeModel { Id = flatModel.ParentId };
                    tempParent.Children.Add(treeModel);
                    treeDict.Add(tempParent.Id, tempParent);
                    potentialRoots.Add(tempParent.Id, tempParent);
                }
            }

            return potentialRoots.Values.First();
        }

        private static TreeModel MapToTreeModel(FlatModel flatModel)
        {
            return new TreeModel
            {
                Id = flatModel.Id
            };
        }
    }
}
