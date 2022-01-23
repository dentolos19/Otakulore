using System.Text.Json.Serialization;

namespace Otakulore.AniList;

public class QueryResponse
{

    [JsonPropertyName("page")] public Page Page { get; init; }

}