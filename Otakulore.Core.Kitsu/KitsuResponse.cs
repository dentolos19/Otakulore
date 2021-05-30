using System.Text.Json.Serialization;

namespace Otakulore.Core.Kitsu
{

    public class KitsuResponse<T>
    {

        [JsonPropertyName("data")] public KitsuData<T> Data { get; init; }

    }

}