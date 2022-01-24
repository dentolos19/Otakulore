using System.Text.Json.Serialization;

namespace Otakulore.Core.AniList;

public class PageInfo
{

    [JsonPropertyName("total")] public int TotalCount { get; init; }
    [JsonPropertyName("currentPage")] public int CurrentPageIndex { get; init; }
    [JsonPropertyName("lastPage")] public int LastPageIndex { get; init; }
    [JsonPropertyName("hasNextPage")] public bool HasNextPage { get; init; }
    [JsonPropertyName("perPage")] public int CountPerPage { get; init; }

}