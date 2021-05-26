using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Otakulore.Api.Kitsu
{

    public class KitsuDataAttributes
    {

        [JsonPropertyName("createdAt")] public DateTime CreationDate { get; init; }
        [JsonPropertyName("updatedAt")] public DateTime LastUpdatedDate { get; init; }

        [JsonPropertyName("titles")] public IDictionary<string, string> Titles { get; init; }
        [JsonPropertyName("canonicalTitle")] public string CanonicalTitle { get; init; }
        [JsonPropertyName("abbreviatedTitles")] public string[] AbbreviatedTitles { get; init; }
        [JsonPropertyName("slug")] public string SlugTitle { get; init; }

        [JsonPropertyName("averageRating")] public double AverageRating { get; init; }
        [JsonPropertyName("ratingFrequencies")] public IDictionary<int, int> RatingFrequencies { get; init; }

        [JsonPropertyName("synopsis")] public string Synopsis { get; init; }
        [JsonPropertyName("description")] public string Description { get; init; }

    }

}