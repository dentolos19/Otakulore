using Newtonsoft.Json;
using Otakulore.Core.Helpers;

namespace Otakulore.Core.AniList;

public class CharacterName
{

    public static GqlSelection[] Selections =>
        new GqlSelection[]
        {
            new("full"),
            new("native"),
            new("userPreferred")
        };

    [JsonProperty("full")] public string Romaji { get; init; }
    [JsonProperty("native")] public string Native { get; init; }
    [JsonProperty("userPreferred")] public string Preferred { get; init; }

}