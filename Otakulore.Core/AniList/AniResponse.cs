using System.Text.Json.Serialization;

namespace Otakulore.Core.AniList;

public class AniResponse
{

    [JsonPropertyName("page")] public Page? Page { get; init; }
    [JsonPropertyName("Media")] public Media? Media { get; init; }
    [JsonPropertyName("UpdateUser")] public User User { get; init; }

}