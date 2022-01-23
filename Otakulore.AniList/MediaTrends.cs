using System.Text.Json.Serialization;

namespace Otakulore.AniList;

public class MediaTrends
{

    [JsonPropertyName("media")] public Media Media { get; init; }

}