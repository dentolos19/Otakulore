using System.Text.Json.Serialization;

namespace Otakulore.Core.AniList;

public class Page
{

    [JsonPropertyName("pageInfo")] public PageInfo Info { get; init; }
    [JsonPropertyName("media")] public Media[] Content { get; init; }
    [JsonPropertyName("mediaList")] public MediaList[] ContentList { get; init; }
    [JsonPropertyName("mediaTrends")] public MediaTrends[] TrendingContent { get; init; }

}