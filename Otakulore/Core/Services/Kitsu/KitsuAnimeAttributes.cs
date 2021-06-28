using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Otakulore.Core.Services.Kitsu
{

    public class KitsuAnimeAttributes
    {

        [JsonPropertyName("synopsis")] public string Synopsis { get; set; }
        [JsonPropertyName("titles")] public IDictionary<string, string> Titles { get; set; }
        [JsonPropertyName("canonicalTitle")] public string CanonicalTitle { get; set; }
        [JsonPropertyName("averageRating")] public string AverageRating { get; set; } // double
        [JsonPropertyName("startDate")] public string StartingDate { get; set; } // DateTime
        [JsonPropertyName("endDate")] public string EndingDate { get; set; } // DateTime
        [JsonPropertyName("ageRating")] public string AgeRating { get; set; }
        [JsonPropertyName("status")] public string Status { get; set; }
        [JsonPropertyName("posterImage")] public KitsuImage PosterImage { get; set; }
        [JsonPropertyName("coverImage")] public KitsuImage CoverImage { get; set; }
        [JsonPropertyName("episodeCount")] public int? EpisodeCount { get; set; }
        [JsonPropertyName("episodeLength")] public int? EpisodeLength { get; set; }

        [JsonPropertyName("showType")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public KitsuAnimeFormat Format { get; set; }

    }

}