using System.Text.Json.Serialization;

namespace Otakulore.Core.Kitsu
{

    public class KitsuMeta
    {

        [JsonPropertyName("count")] public int Count { get; init; }

    }

}