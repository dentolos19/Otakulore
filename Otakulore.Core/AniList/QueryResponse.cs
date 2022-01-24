using System.Text.Json.Serialization;

namespace Otakulore.Core.AniList;

public class QueryResponse
{

    [JsonPropertyName("page")] public Page? Page { get; init; }
    [JsonPropertyName("Media")] public Media? Media { get; init; }

}