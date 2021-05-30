using System.Text.Json.Serialization;

namespace Otakulore.Core.Kitsu
{

    public class KitsuImage
    {

        [JsonPropertyName("tiny")] public string TinyImageUrl { get; init; }
        [JsonPropertyName("small")] public string SmallImageUrl { get; init; }
        [JsonPropertyName("medium")] public string MediumImageUrl { get; init; }
        [JsonPropertyName("large")] public string LargeImageUrl { get; init; }
        [JsonPropertyName("original")] public string OriginalImageUrl { get; init; }

    }

}