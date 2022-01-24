using System.Text.Json.Serialization;

namespace Otakulore.Core.AniList;

public class MediaTitle
{

    [JsonPropertyName("romaji")] public string Romaji { get; init; }
    [JsonPropertyName("english")] public string? English { get; init; }

}