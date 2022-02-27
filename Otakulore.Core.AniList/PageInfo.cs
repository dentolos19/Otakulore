using Newtonsoft.Json;
using Otakulore.Core.Helpers;

namespace Otakulore.Core.AniList;

public class PageInfo
{

    internal static GqlSelection[] Selections =>
        new GqlSelection[]
        {
            new("currentPage"),
            new("lastPage"),
            new("hasNextPage")
        };

    [JsonProperty("currentPage")] public int CurrentPageIndex { get; init; }
    [JsonProperty("lastPage")] public int LastPageIndex { get; init; }
    [JsonProperty("hasNextPage")] public bool HasNextPage { get; init; }

}