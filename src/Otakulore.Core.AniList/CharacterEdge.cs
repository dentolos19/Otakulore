using Newtonsoft.Json;
using Otakulore.Core.Helpers;

namespace Otakulore.Core.AniList;

public class CharacterEdge
{

    internal static GqlSelection[] Selections =>
        new GqlSelection[]
        {
            new("node", Character.Selections),
            new("role")
        };

    [JsonProperty("node")] public Character Details { get; init; }
    [JsonProperty("role")] public CharacterRole Role { get; init; }

}