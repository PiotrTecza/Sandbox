using System;

namespace FlatListToTree
{
    public class FlatModel
    {
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
    }
}
