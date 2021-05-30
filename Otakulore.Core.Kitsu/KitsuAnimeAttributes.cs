using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Otakulore.Core.Kitsu
{

    public class KitsuAnimeAttributes : KitsuDataAttributes // TODO: add more properties
    {

        [JsonPropertyName("posterImage")] public KitsuImage PosterImage { get; init; }

        [JsonPropertyName("titles")] public IDictionary<string, string> Titles { get; init; }
        [JsonPropertyName("canonicalTitle")] public string CanonicalTitle { get; init; }
        [JsonPropertyName("abbreviatedTitles")] public string[] AbbreviatedTitles { get; init; }
        [JsonPropertyName("slug")] public string SlugTitle { get; init; }

        [JsonPropertyName("synopsis")] public string Synopsis { get; init; }
        [JsonPropertyName("description")] public string Description { get; init; }

        [JsonPropertyName("averageRating")] public string AverageRating { get; init; } // double
        [JsonPropertyName("ratingFrequencies")] public IDictionary<string, string> RatingFrequencies { get; init; } // int, int

        [JsonPropertyName("userCount")] public int UserCount { get; init; }
        [JsonPropertyName("favoritesCount")] public int FavoritesCount { get; init; }
        
        [JsonPropertyName("startDate")] public string StartingDate { get; init; } // DateTime
        [JsonPropertyName("endingDate")] public string EndingDate { get; init; } // DateTime

        [JsonPropertyName("episodeCount")] public int? EpisodeCount { get; init; }
        [JsonPropertyName("episodeLength")] public int? EpisodeLength { get; init; }

        [JsonPropertyName("ageRating")] public string AgeRating { get; init; } // enum
        [JsonPropertyName("status")] public string Status { get; init; } // enum
        
        [JsonPropertyName("showType")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public KitsuMediaFormat Format { get; init; }

        [JsonPropertyName("subtype")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public KitsuMediaFormat Subformat { get; init; }

    }

}