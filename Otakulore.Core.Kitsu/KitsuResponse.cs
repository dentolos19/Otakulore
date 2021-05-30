using System.Text.Json.Serialization;

namespace Otakulore.Core.Kitsu
{

    public class KitsuResponse
    {

        [JsonPropertyName("data")] public KitsuData Data { get; init; }

    }

}