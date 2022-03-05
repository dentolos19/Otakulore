using Newtonsoft.Json;
using Otakulore.Core.Helpers;

namespace Otakulore.Core.AniList;

public class StaffEdge
{

    internal static GqlSelection[] Selections =>
        new GqlSelection[]
        {
            new("node", Character.Selections),
            new("role")
        };

    [JsonProperty("node")] public Staff Details { get; init; }
    [JsonProperty("role")] public string Role { get; init; }

}