using Newtonsoft.Json;
using Otakulore.Core.Helpers;

namespace Otakulore.Core.AniList;

public class MediaEdge
{

    public static GqlSelection[] Selections =>
        new GqlSelection[]
        {
            new("node", Media.Selections),
            new("relationType") { Parameters = { { "version", 2 } } }
        };

    [JsonProperty("node")] public Media Details { get; init; }
    [JsonProperty("relationType")] public MediaRelation Relation { get; init; }

}