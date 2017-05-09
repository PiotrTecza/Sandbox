using System.Collections.Generic;

namespace CollectionCompare
{
    public class CompareResult
    {
        public List<int> Added { get; set; }
        public List<int> Removed { get; set; }
        public List<int> Existing { get; set; }
        public double TotalMiliseconds { get; set; }

        public CompareResult()
        {
            Added = new List<int>();
            Removed = new List<int>();
            Existing = new List<int>();
        }
    }
}
