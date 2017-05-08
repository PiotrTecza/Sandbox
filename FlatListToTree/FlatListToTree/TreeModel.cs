using System;
using System.Collections.Generic;

namespace FlatListToTree
{
    public class TreeModel
    {
        public Guid Id { get; set; }
        public List<TreeModel> Children { get; set; }

        public TreeModel()
        {
            Children = new List<TreeModel>();
        }
    }
}
