using System.Collections.Generic;

namespace FlatListToTree
{
    public interface ITreeModelBuilder
    {
        TreeModel Build(List<FlatModel> flatModelList);
        string StrategyName { get; }
    }
}