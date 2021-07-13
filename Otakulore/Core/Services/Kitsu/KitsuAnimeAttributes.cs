using System.Text.Json.Serialization;

namespace Otakulore.Core.Services.Kitsu
{

    public class KitsuAnimeAttributes : KitsuMediaAttributes
    {

        [JsonPropertyName("episodeCount")] public int? EpisodeCount { get; set; }

        [JsonPropertyName("subType")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public KitsuAnimeFormat Format { get; set; }

    }

}