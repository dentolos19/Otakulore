using System.Text.Json.Serialization;

namespace Otakulore.Core.Kitsu
{

    public class KitsuResponses<T>
    {

        [JsonPropertyName("data")] public KitsuData<T>[] Data { get; init; }
        [JsonPropertyName("meta")] public KitsuMeta Meta { get; init; }
        [JsonPropertyName("links")] public KitsuLinks Links { get; init; }

    }

}