using System.Text.Json.Serialization;

namespace Otakulore.Core.Kitsu
{

    public class KitsuLinks
    {

        [JsonPropertyName("self")] public string SelfLink { get; set; }

        [JsonPropertyName("first")] public string FirstLink { get; set; }
        [JsonPropertyName("prev")] public string PreviousLink { get; set; }
        [JsonPropertyName("next")] public string NextLink { get; set; }
        [JsonPropertyName("last")] public string LastLink { get; set; }

    }

}