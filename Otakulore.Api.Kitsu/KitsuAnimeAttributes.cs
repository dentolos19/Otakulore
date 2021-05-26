using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Otakulore.Api.Kitsu
{

    public class KitsuAnimeAttributes : KitsuDataAttributes
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

        [JsonPropertyName("userCount")] public uint UserCount { get; init; }
        [JsonPropertyName("favoritesCount")] public uint FavoritesCount { get; init; }
        
        [JsonPropertyName("startDate")] public string StartingDate { get; init; }
        [JsonPropertyName("endingDate")] public string EndingDate { get; init; }

        [JsonPropertyName("episodeCount")] public int EpisodeCount { get; init; }
        [JsonPropertyName("episodeLength")] public int? EpisodeLength { get; init; }
        [JsonPropertyName("totalLength")] public int? TotalEpisodeLength { get; init; }

        [JsonPropertyName("showType")] public string Format { get; init; } // enum 

    }

}