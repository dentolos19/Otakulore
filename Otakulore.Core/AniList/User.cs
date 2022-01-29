using System.Text.Json.Serialization;
using Otakulore.Core.Helpers;

namespace Otakulore.Core.AniList;

public class User
{

    public static GqlSelection[] Selections =>
        new GqlSelection[]
        {
            new("id"),
            new("name"),
            new("avatar", new GqlSelection[]
            {
                new("large"),
                new("medium")
            }),
            new("bannerImage"),
            new("about") { Parameters = { { "asHtml", false } } }
        };

    [JsonPropertyName("id")] public int Id { get; init; }
    [JsonPropertyName("name")] public string Name { get; init; }
    [JsonPropertyName("avatar")] public Cover Avatar { get; init; }
    [JsonPropertyName("bannerImage")] public string BannerImageUrl { get; init; }
    [JsonPropertyName("about")] public string About { get; init; }

}