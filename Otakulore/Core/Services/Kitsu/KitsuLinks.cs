using System.Text.Json.Serialization;

namespace Otakulore.Core.Services.Kitsu
{

    public class KitsuLinks
    {

        [JsonPropertyName("prev")] public string PreviousPaginationUrl { get; set; }
        [JsonPropertyName("next")] public string NextPaginationUrl { get; set; }

    }

}