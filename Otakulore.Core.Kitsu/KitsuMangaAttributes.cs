using System.Text.Json.Serialization;

namespace Otakulore.Core.Kitsu
{

    public class KitsuMangaAttributes : KitsuDataAttributes
    {

        [JsonPropertyName("chapterCount")] public int ChapterCount { get; init; }
        [JsonPropertyName("volumeCount")] public int volumeCount { get; init; }
        [JsonPropertyName("serialization")] public string Serialization { get; init; }

        [JsonPropertyName("mangaType")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public KitsuMangaFormat Format { get; init; }

        [JsonPropertyName("subtype")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public KitsuMangaFormat Subformat { get; init; }

    }

}