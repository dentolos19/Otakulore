using Newtonsoft.Json;
using Otakulore.Core.Helpers;

namespace Otakulore.Core.AniList;

public class MediaTitle
{

    internal static GqlSelection[] Selections =>
        new GqlSelection[]
        {
            new("romaji"),
            new("english"),
            new("native"),
            new("userPreferred")
        };

    [JsonProperty("romaji")] public string Romaji { get; init; }
    [JsonProperty("english")] public string? English { get; init; }
    [JsonProperty("native")] public string Native { get; init; }
    [JsonProperty("userPreferred")] public string Preferred { get; init; }

}