using System.Text.Json.Serialization;

namespace Otakulore.Api.Kitsu
{

    public class KitsuData
    {

        [JsonPropertyName("id")] public uint Id { get; init; }
        [JsonPropertyName("type")] public KitsuDataType Type { get; init; }
        [JsonPropertyName("links")] public KitsuLinks Links { get; init; }
        [JsonPropertyName("links")] public KitsuDataAttributes Attributes { get; init; }
        [JsonPropertyName("links")] public KitsuDataRelationships Relationships { get; init; }

    }

}