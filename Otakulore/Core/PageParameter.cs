namespace Otakulore.Core;

public class PageParameter
{

    public IProvider Provider { get; init; }
    public MediaType MediaType { get; init; }
    public MediaSource MediaSource { get; init; }
    public long Id { get; init; }

}