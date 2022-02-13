using System.Text.Json.Serialization;

namespace Otakulore.Core.AniList;

public class AniResponse
{

    [JsonPropertyName("GenreCollection")] public string[]? GenreCollection { get; init; }
    [JsonPropertyName("MediaTagCollection")] public MediaTag[]? TagCollection { get; init; }
    [JsonPropertyName("MediaListCollection")] public MediaEntryCollection? MediaEntryCollection { get; init; }

    [JsonPropertyName("page")] public Page? Page { get; init; }
    [JsonPropertyName("Media")] public Media? Media { get; init; }
    [JsonPropertyName("User")] public User? User { get; init; }

    [JsonPropertyName("UpdateUser")] public User? UpdatedUser { get; init; }
    [JsonPropertyName("SaveMediaListEntry")] public MediaEntry? UpdatedMediaEntry { get; init; }

}