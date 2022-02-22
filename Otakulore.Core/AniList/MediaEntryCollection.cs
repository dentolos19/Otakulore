using Newtonsoft.Json;
using Otakulore.Core.Helpers;

namespace Otakulore.Core.AniList;

public class MediaEntryCollection
{

    public static GqlSelection[] Selections =>
        new GqlSelection[]
        {
            new("lists", MediaEntryGroup.Selections),
            new("hasNextChunk")
        };

    [JsonProperty("lists")] public MediaEntryGroup[] Groups { get; init; }
    [JsonProperty("hasNextChunk")] public bool HasNextChunk { get; init; }

}