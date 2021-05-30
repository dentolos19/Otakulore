using System.Text.Json.Serialization;

namespace Otakulore.Core.Kitsu
{

    public class KitsuData<T>
    {

        [JsonPropertyName("id")]
        public string Id { get; init; } // int

        [JsonPropertyName("type")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public KitsuDataType Type { get; init; }

        [JsonPropertyName("attributes")]
        public T Attributes { get; init; }

    }

}