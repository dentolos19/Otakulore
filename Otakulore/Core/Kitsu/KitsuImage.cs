using System.Text.Json.Serialization;

namespace Otakulore.Core.Kitsu
{

    public class KitsuImage
    {

        [JsonPropertyName("tiny")] public string TinyImageUrl { get; set; }
        [JsonPropertyName("small")] public string SmallImageUrl { get; set; }
        [JsonPropertyName("medium")] public string MediumImageUrl { get; set; }
        [JsonPropertyName("large")] public string LargeImageUrl { get; set; }
        [JsonPropertyName("original")] public string OriginalImageUrl { get; set; }

    }

}