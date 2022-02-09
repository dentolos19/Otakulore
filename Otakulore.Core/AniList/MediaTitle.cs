using System.Text.Json.Serialization;
using Otakulore.Core.Helpers;

namespace Otakulore.Core.AniList;

public class MediaTitle
{

    internal static GqlSelection[] Selections =>
        new GqlSelection[]
        {
            new("romaji"),
            new("english"),
            new("native"),
            new("userPreferred")
        };

    [JsonPropertyName("romaji")] public string Romaji { get; init; }
    [JsonPropertyName("english")] public string? English { get; init; }
    [JsonPropertyName("native")] public string Native { get; init; }
    [JsonPropertyName("userPreferred")] public string Preferred { get; init; }

}