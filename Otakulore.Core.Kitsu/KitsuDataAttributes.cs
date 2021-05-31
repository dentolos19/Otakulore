using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Otakulore.Core.Kitsu
{

    public class KitsuDataAttributes
    {

        [JsonPropertyName("slug")] public string SlugTitle { get; init; }
        [JsonPropertyName("synopsis")] public string Synopsis { get; init; }
        [JsonPropertyName("titles")] public IDictionary<string, string?> Titles { get; init; }
        [JsonPropertyName("canonicalTitle")] public string CanonicalTitle { get; init; }
        [JsonPropertyName("abbreviatedTitles")] public string[] AbbreviatedTitles { get; init; }
        [JsonPropertyName("averageRating")] public string AverageRating { get; init; } // double
        [JsonPropertyName("startDate")] public string? StartingDate { get; init; } // DateTime
        [JsonPropertyName("endingDate")] public string? EndingDate { get; init; } // DateTime
        [JsonPropertyName("ageRating")] public string? AgeRating { get; init; }
        [JsonPropertyName("posterImage")] public KitsuImage PosterImage { get; init; }
        [JsonPropertyName("coverImage")] public KitsuImage? CoverImage { get; init; }
        [JsonPropertyName("episodeCount")] public int? EpisodeCount { get; init; }
        [JsonPropertyName("episodeLength")] public int? EpisodeLength { get; init; }

        [JsonPropertyName("subtype")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public KitsuMediaFormat Subformat { get; init; }

        [JsonPropertyName("status")]
        // [JsonConverter(typeof(JsonStringEnumConverter))]
        public string? Status { get; init; } // KitsuMediaStatus?

        [JsonPropertyName("showType")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public KitsuMediaFormat Format { get; init; }

        // TODO: add "nextrelease" property

    }

}