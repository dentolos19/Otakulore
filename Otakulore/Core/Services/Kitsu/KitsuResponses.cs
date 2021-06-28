using System.Text.Json.Serialization;

namespace Otakulore.Core.Services.Kitsu
{

    public class KitsuResponses<T>
    {

        [JsonPropertyName("data")] public KitsuData<T>[] Data { get; set; }

    }

}