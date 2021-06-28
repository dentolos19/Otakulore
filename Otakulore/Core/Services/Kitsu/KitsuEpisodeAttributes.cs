using System.Text.Json.Serialization;

namespace Otakulore.Core.Services.Kitsu
{

    public class KitsuEpisodeAttributes // TODO: implement episodes info into watching view
    {

        [JsonPropertyName("synopsis")] public string Synopsis { get; set; }
        [JsonPropertyName("canonicalTitle")] public string CanonicalTitle { get; set; }
        [JsonPropertyName("seasonNumber")] public int Season { get; set; }
        [JsonPropertyName("number")] public int Episode { get; set; }
        [JsonPropertyName("airDate")] public string AirDate { get; set; } // DateTime
        [JsonPropertyName("length")] public int Length { get; set; }

    }

}