using System.Text.Json.Serialization;

namespace Otakulore.Core.AniList;

public class User
{

    [JsonPropertyName("id")] public int Id { get; init; }
    [JsonPropertyName("name")] public string Name { get; init; }
    [JsonPropertyName("avatar")] public Cover Avatar { get; init; }
    [JsonPropertyName("bannerImage")] public string BannerImageUrl { get; init; }
    [JsonPropertyName("about")] public string About { get; init; }

}