using Newtonsoft.Json;
using Otakulore.Core.Helpers;

namespace Otakulore.Core.AniList;

public class MediaEntry
{

    public static GqlSelection[] Selections =>
        new GqlSelection[]
        {
            new("id"),
            new("mediaId"),
            new("status"),
            new("progress"),
            new("media", Media.Selections)
        };

    public static GqlSelection[] MediaSelections =>
        new GqlSelection[]
        {
            new("id"),
            new("mediaId"),
            new("status"),
            new("progress")
        };

    [JsonProperty("id")] public int Id { get; init; }
    [JsonProperty("mediaId")] public int MediaId { get; init; }
    [JsonProperty("status")] public MediaEntryStatus Status { get; init; }
    [JsonProperty("progress")] public int Progress { get; init; }
    [JsonProperty("media")] public Media? Media { get; init; }

}