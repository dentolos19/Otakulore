using System.Text.Json.Serialization;

namespace Otakulore.Api.Kitsu
{

    public class KitsuData<T>
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

        [JsonPropertyName("relationships")]
        public KitsuDataRelationships Relationships { get; init; }

    }

}