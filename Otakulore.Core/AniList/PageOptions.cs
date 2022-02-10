namespace Otakulore.Core.AniList;

public class PageOptions
{

    public int Index { get; set; }
    public int Size { get; set; }

    public PageOptions(int index = 1, int size = 50)
    {
        Index = index;
        Size = size;
    }

}