using System.Text.Json.Serialization;

namespace Otakulore.AniList;

public class Cover
{

    [JsonPropertyName("extraLarge")] public string ExtraLargeImageUrl { get; init; }
    [JsonPropertyName("large")] public string LargeImageUrl { get; init; }
    [JsonPropertyName("medium")] public string MediumImageUrl { get; init; }
    [JsonPropertyName("color")] public string Color { get; init; }

}