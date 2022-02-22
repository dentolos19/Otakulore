using Newtonsoft.Json;

namespace Otakulore.Core.AniList;

public class Connection<TNode, TEdge>
{

    [JsonProperty("edges")] public TEdge[] Edges { get; init; }
    [JsonProperty("nodes")] public TNode[] Nodes { get; init; }

}