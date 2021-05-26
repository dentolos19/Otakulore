using System;
using System.Text.Json.Serialization;

namespace Otakulore.Api.Kitsu
{

    public class KitsuDataAttributes
    {

        [JsonPropertyName("createdAt")] public DateTime CreationDate { get; init; }
        [JsonPropertyName("updatedAt")] public DateTime LastUpdatedDate { get; init; }

        [JsonPropertyName("coverImage")] public KitsuImage CoverImage { get; init; }

    }

}