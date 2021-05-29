using System.Text.Json.Serialization;

namespace Otakulore.Core.Kitsu
{

    public class KitsuData<T> // TODO: add data relationships
    {

        [JsonPropertyName("id")]
        public string Id { get; init; } // int

        [JsonPropertyName("type")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public KitsuDataType Type { get; init; }

        [JsonPropertyName("links")]
        public KitsuLinks Links { get; init; }

        [JsonPropertyName("attributes")]
        public T Attributes { get; init; }

    }

}