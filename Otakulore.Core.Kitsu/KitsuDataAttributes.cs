using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Otakulore.Core.Kitsu
{

    public class KitsuDataAttributes
    {

        [JsonPropertyName("posterImage")] public KitsuImage PosterImage { get; init; }
        [JsonPropertyName("coverImage")] public KitsuImage? CoverImage { get; init; }
        [JsonPropertyName("titles")] public IDictionary<string, string?> Titles { get; init; }
        [JsonPropertyName("canonicalTitle")] public string CanonicalTitle { get; init; }
        [JsonPropertyName("abbreviatedTitles")] public string[] AbbreviatedTitles { get; init; }
        [JsonPropertyName("slug")] public string SlugTitle { get; init; }
        [JsonPropertyName("averageRating")] public string AverageRating { get; init; } // double
        [JsonPropertyName("favoritesCount")] public int FavoritesCount { get; init; }
        [JsonPropertyName("synopsis")] public string Synopsis { get; init; }
        [JsonPropertyName("description")] public string Description { get; init; }
        [JsonPropertyName("startDate")] public string StartingDate { get; init; } // DateTime
        [JsonPropertyName("endingDate")] public string? EndingDate { get; init; } // DateTime
        [JsonPropertyName("ageRating")] public string? AgeRating { get; init; }

        [JsonPropertyName("status")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public KitsuMediaStatus Status { get; init; }

        // TODO: add "nextrelease" property
        // TODO: add "tba" property

    }

}