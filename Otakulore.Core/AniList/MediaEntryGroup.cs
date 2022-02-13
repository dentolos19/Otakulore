using System.Text.Json.Serialization;
using Otakulore.Core.Helpers;

namespace Otakulore.Core.AniList;

public class MediaEntryGroup
{

    public static GqlSelection[] Selections =>
        new GqlSelection[]
        {
            new("entries", MediaEntry.Selections),
            new("name"),
            new("isCustomList"),
            new("status")
        };

    [JsonPropertyName("entries")] public MediaEntry[] Entries { get; init; }
    [JsonPropertyName("name")] public string Name { get; init; }
    [JsonPropertyName("isCustomList")] public bool IsCustom { get; init; }
    [JsonPropertyName("status")] public MediaEntryStatus Status { get; init; }

}