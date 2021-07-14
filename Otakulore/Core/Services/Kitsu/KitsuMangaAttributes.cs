using System.Text.Json.Serialization;

namespace Otakulore.Core.Services.Kitsu
{

    public class KitsuMangaAttributes : KitsuMediaAttributes
    {

        [JsonPropertyName("chapterCount")] public int? ChapterCount { get; set; }
        [JsonPropertyName("volumeCount")] public int? VolumeCount { get; set; }

        [JsonPropertyName("subType")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public KitsuMangaFormat Format { get; set; }

    }

}