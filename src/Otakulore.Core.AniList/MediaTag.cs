using Newtonsoft.Json;
using Otakulore.Core.Helpers;

namespace Otakulore.Core.AniList;

public class MediaTag
{

    internal static GqlSelection[] Selections =>
        new GqlSelection[]
        {
            new("id"),
            new("name"),
            new("description")
        };

    [JsonProperty("id")] public int Id { get; init; }
    [JsonProperty("name")] public string Name { get; init; }
    [JsonProperty("description")] public string Description { get; init; }

}