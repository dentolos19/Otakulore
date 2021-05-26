using System.Text.Json.Serialization;

namespace Otakulore.Api.Kitsu
{

    public class KitsuLinks
    {

        [JsonPropertyName("self")] public string? SelfLink { get; init; }

        [JsonPropertyName("first")] public string? FirstLink { get; init; }
        [JsonPropertyName("prev")] public string? PreviousLink { get; init; }
        [JsonPropertyName("next")] public string? NextLink { get; init; }
        [JsonPropertyName("last")] public string? LastLink { get; init; }

    }

}