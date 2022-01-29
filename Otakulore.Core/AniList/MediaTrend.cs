using System.Text.Json.Serialization;

namespace Otakulore.Core.AniList;

public class MediaTrend
{

    [JsonPropertyName("media")] public Media Media { get; init; }

}