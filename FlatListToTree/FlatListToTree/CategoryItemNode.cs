using System;
using System.Collections.Generic;
using System.Text;

namespace FlatListToTree
{
    public class CategoryItemNode
    {
        private List<CategoryItemNode> _children = new List<CategoryItemNode>();

        public List<CategoryItemNode> Children
        {
            get
            {
                return _children;
            }

            set
            {
                _children = value;
            }
        }

        public CategoryItemNode Parent { get; set; }
        public FlatModel Value { get; set; }
    }
}
