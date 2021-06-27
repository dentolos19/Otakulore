using System.Text.Json.Serialization;

namespace Otakulore.Core.Services.Kitsu
{

    public class KitsuResponse
    {

        [JsonPropertyName("data")] public KitsuData Data { get; set; }

    }

}