using System.Text.Json.Serialization;

namespace Otakulore.Core.AniList;

public class Page
{

    [JsonPropertyName("pageInfo")] public PageInfo Info { get; init; }
    [JsonPropertyName("media")] public Media[]? Media { get; init; }
    [JsonPropertyName("mediaList")] public MediaEntry[]? MediaEntries { get; init; }

}