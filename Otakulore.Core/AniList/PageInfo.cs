using System.Text.Json.Serialization;
using Otakulore.Core.Helpers;

namespace Otakulore.Core.AniList;

public class PageInfo
{

    public static GqlSelection[] Selections =>
        new GqlSelection[]
        {
            new("currentPage"),
            new("lastPage"),
            new("hasNextPage")
        };

    [JsonPropertyName("currentPage")] public int CurrentPageIndex { get; init; }
    [JsonPropertyName("lastPage")] public int LastPageIndex { get; init; }
    [JsonPropertyName("hasNextPage")] public bool HasNextPage { get; init; }

}