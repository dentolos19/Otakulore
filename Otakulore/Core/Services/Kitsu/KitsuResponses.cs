using System.Text.Json.Serialization;

namespace Otakulore.Core.Services.Kitsu
{

    public class KitsuResponses
    {

        [JsonPropertyName("data")] public KitsuData[] Data { get; set; }

    }

}