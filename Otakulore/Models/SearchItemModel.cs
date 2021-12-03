using Otakulore.Core;

namespace Otakulore.Models;

public class SearchItemModel
{

    public MediaType Type { get; init; }
    public long Id { get; set; }
    public string ImageUrl { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public string Year { get; init; }
    public string Contents { get; init; }
    public string Status { get; init; }
    public float Score { get; init; }

}