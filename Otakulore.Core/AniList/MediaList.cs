using System.Text.Json.Serialization;

namespace Otakulore.Core.AniList;

public class MediaList
{

    [JsonPropertyName("status")] public MediaListStatus Status { get; init; }
    [JsonPropertyName("progress")] public int Progress { get; init; }
    [JsonPropertyName("media")] public Media Media { get; init; }

}