using Newtonsoft.Json;

namespace Otakulore.Core.AniList;

public class Page
{

    [JsonProperty("pageInfo")] public PageInfo Info { get; init; }
    [JsonProperty("media")] public Media[]? Media { get; init; }
    [JsonProperty("mediaList")] public MediaEntry[]? MediaEntries { get; init; }

}