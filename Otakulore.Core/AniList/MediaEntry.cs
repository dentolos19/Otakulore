using System.Text.Json.Serialization;
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

    [JsonPropertyName("id")] public int Id { get; init; }
    [JsonPropertyName("mediaId")] public int MediaId { get; init; }
    [JsonPropertyName("status")] public MediaEntryStatus Status { get; init; }
    [JsonPropertyName("progress")] public int Progress { get; init; }
    [JsonPropertyName("media")] public Media? Media { get; init; }

}