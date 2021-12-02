using Otakulore.Core;

namespace Otakulore.Models;

public class SearchItemModel
{

    public SearchType Type { get; init; }
    public string ImageUrl { get; init; }
    public string Title { get; init; }
    public string Year { get; init; }
    public string Contents { get; init; }
    public string Status { get; init; }
    public string Description { get; init; }
    public float Score { get; init; }

}