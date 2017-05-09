using System;
using System.Collections.Generic;
using System.Linq;

namespace FlatListToTree
{
    public class TreeModelBuilder2 : ITreeModelBuilder
    {
        public string StrategyName => "Strategy2";

        public TreeModel Build(List<FlatModel> flatModelList)
        {
            var root = BuildTreeAndGetRoots(flatModelList);
            var result = MapToTreeModelAll(root);
            return result;
        }

        private CategoryItemNode BuildTreeAndGetRoots(IList<FlatModel> actualObjects)
        {
            Dictionary<Guid, CategoryItemNode> lookup = actualObjects.ToDictionary(x => x.Id, x => new CategoryItemNode {Value = x});

            foreach (var item in lookup.Values)
            {
                CategoryItemNode proposedParent;
                if (lookup.TryGetValue(item.Value.ParentId, out proposedParent))
                {
                    item.Parent = proposedParent;
                    proposedParent.Children.Add(item);
                }
            }
            return lookup.Values.First(x => x.Parent == null);
        }

        private static TreeModel MapToTreeModelAll(CategoryItemNode categoryModel)
        {
            TreeModel root = null;
            var stack = new Stack<StackModel>();
            stack.Push(new StackModel(null, categoryModel));

            while (stack.Count > 0)
            {
                var stackModel = stack.Pop();
                var model = MapToTreeModel(stackModel.CategoryItemNode);

                if (stackModel.TreeModel == null)
                {
                    root = model;
                }
                else
                {
                    stackModel.TreeModel.Children.Add(model);
                }

                foreach (var categoryItemNode in stackModel.CategoryItemNode.Children)
                {
                    stack.Push(new StackModel(model, categoryItemNode));
                }
            }

            return root;
        }

        private static TreeModel MapToTreeModel(CategoryItemNode categoryModel)
        {
            return new TreeModel()
            {
                Id = categoryModel.Value.Id
            };
        }

        private class StackModel
        {
            public TreeModel TreeModel { get; set; }
            public CategoryItemNode CategoryItemNode { get; set; }

            public StackModel(TreeModel treeModel, CategoryItemNode categoryItemNode)
            {
                TreeModel = treeModel;
                CategoryItemNode = categoryItemNode;
            }
        }

    }
}
