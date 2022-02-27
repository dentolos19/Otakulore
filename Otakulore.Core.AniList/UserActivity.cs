using Newtonsoft.Json;
using Otakulore.Core.Helpers;

namespace Otakulore.Core.AniList;

public class UserActivity
{

    internal static GqlSelection[] Selections =>
        new GqlSelection[]
        {
            new("id"),
            new("status"),
            new("progress"),
            new("media", Media.Selections)
        };

    [JsonProperty("id")] public int Id { get; init; }
    [JsonProperty("status")] public string Status { get; init; }
    [JsonProperty("progress")] public string? Progress { get; init; }
    [JsonProperty("media")] public Media Media { get; init; }

}