namespace CollectionCompare
{
    public interface ICompareStrategy
    {
        CompareResult Execute(int itemsCount);
    }
}
