namespace Otakulore.Core.AniList;

public class AniPagination<T>
{

    public int CurrentPageIndex { get; }
    public int LastPageIndex { get; }
    public bool HasNextPage { get; }
    public IList<T> Data { get; }

    public AniPagination(PageInfo info, IList<T> data)
    {
        CurrentPageIndex = info.CurrentPageIndex;
        LastPageIndex = info.LastPageIndex;
        HasNextPage = info.HasNextPage;
        Data = data;
    }

}