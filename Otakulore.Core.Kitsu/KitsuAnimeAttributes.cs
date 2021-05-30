using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Otakulore.Core.Kitsu
{

    public class KitsuAnimeAttributes : KitsuDataAttributes
    {
        
        [JsonPropertyName("episodeCount")] public int? EpisodeCount { get; init; }
        [JsonPropertyName("episodeLength")] public int? EpisodeLength { get; init; }

        [JsonPropertyName("showType")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public KitsuAnimeFormat Format { get; init; }

        [JsonPropertyName("subtype")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public KitsuAnimeFormat Subformat { get; init; }

    }

}