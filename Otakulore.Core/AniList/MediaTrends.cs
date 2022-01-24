using System.Text.Json.Serialization;

namespace Otakulore.Core.AniList;

public class MediaTrends
{

    [JsonPropertyName("media")] public Media Media { get; init; }

}