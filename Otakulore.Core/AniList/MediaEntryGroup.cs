using Newtonsoft.Json;
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

    [JsonProperty("entries")] public MediaEntry[] Entries { get; init; }
    [JsonProperty("name")] public string Name { get; init; }
    [JsonProperty("isCustomList")] public bool IsCustom { get; init; }
    [JsonProperty("status")] public MediaEntryStatus Status { get; init; }

}