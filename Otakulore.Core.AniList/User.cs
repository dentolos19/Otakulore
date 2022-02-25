using Newtonsoft.Json;
using Otakulore.Core.Helpers;

namespace Otakulore.Core.AniList;

public class User
{

    public static GqlSelection[] Selections =>
        new GqlSelection[]
        {
            new("id"),
            new("name"),
            new("avatar", Image.Selections),
            new("bannerImage"),
            new("about") { Parameters = { { "asHtml", false } } }
        };

    [JsonProperty("id")] public int Id { get; init; }
    [JsonProperty("name")] public string Name { get; init; }
    [JsonProperty("avatar")] public Cover Avatar { get; init; }
    [JsonProperty("bannerImage")] public string BannerImageUrl { get; init; }
    [JsonProperty("about")] public string About { get; init; }

}