using System.Text.Json.Serialization;

namespace Otakulore.Api.Kitsu
{

    public class KitsuMeta
    {

        [JsonPropertyName("count")] public int Count { get; init; }

    }

}