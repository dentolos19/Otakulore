using System.Text.Json.Serialization;
using Otakulore.Core.Helpers;

namespace Otakulore.Core.AniList;

public class MediaExtra : Media
{

    public static GqlSelection[] Selections =>
        Media.Selections.Concat(new GqlSelection[]
        {
            new("tags", MediaTag.Selections),
            new("mediaListEntry", MediaEntry.MediaSelections)
        }).ToArray();

    [JsonPropertyName("tags")] public MediaTag[]? Tags { get; init; }
    [JsonPropertyName("mediaListEntry")] public MediaEntry? Entry { get; init; }

}