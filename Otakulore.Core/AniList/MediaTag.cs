using System.Text.Json.Serialization;
using Otakulore.Core.Helpers;

namespace Otakulore.Core.AniList;

public class MediaTag
{

    public static GqlSelection[] Selections =>
        new GqlSelection[]
        {
            new("id"),
            new("name"),
            new("description")
        };

    [JsonPropertyName("id")] public int Id { get; init; }
    [JsonPropertyName("name")] public string Name { get; init; }
    [JsonPropertyName("description")] public string Description { get; init; }

}