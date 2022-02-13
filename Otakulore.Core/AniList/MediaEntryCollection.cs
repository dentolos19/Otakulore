using System.Text.Json.Serialization;
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

    [JsonPropertyName("lists")] public MediaEntryGroup[] Groups { get; init; }
    [JsonPropertyName("hasNextChunk")] public bool HasNextChunk { get; init; }

}