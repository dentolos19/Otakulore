using System.Text.Json.Serialization;

namespace Otakulore.Core.AniList;

public class AniResponse
{

    [JsonPropertyName("page")] public Page Page { get; init; }
    [JsonPropertyName("Media")] public Media Media { get; init; }
    [JsonPropertyName("User")] public User? User { get; init; }

    [JsonPropertyName("UpdateUser")] public User? UpdatedUser { get; init; }
    [JsonPropertyName("SaveMediaListEntry")] public MediaEntry UpdatedMediaEntry { get; init; }

}