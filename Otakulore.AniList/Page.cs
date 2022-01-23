using System.Text.Json.Serialization;

namespace Otakulore.AniList;

public class Page
{

    [JsonPropertyName("pageInfo")] public PageInfo Info { get; init; }
    [JsonPropertyName("media")] public Media[] Content { get; init; }

}