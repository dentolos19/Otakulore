using System.Text.Json.Serialization;

namespace Otakulore.Core.Services.Kitsu
{

    public class KitsuEpisodeAttributes
    {

        [JsonPropertyName("synopsis")] public string Synopsis { get; set; }
        [JsonPropertyName("canonicalTitle")] public string CanonicalTitle { get; set; }
        [JsonPropertyName("seasonNumber")] public int Season { get; set; }
        [JsonPropertyName("number")] public int Episode { get; set; }
        [JsonPropertyName("airDate")] public string AirDate { get; set; } // DateTime
        [JsonPropertyName("thumbnail")] public KitsuImage Thumbnail { get; set; }

    }

}