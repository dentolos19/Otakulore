using Newtonsoft.Json;
using Otakulore.Core.Helpers;

namespace Otakulore.Core.AniList;

public class Character
{

    internal static GqlSelection[] Selections =>
        new GqlSelection[]
        {
            new("id"),
            new("image", Image.Selections),
            new("name", Name.Selections),
            new("gender"),
            new("age"),
            new("description") { Parameters = { { "asHtml", false } } }
        };

    [JsonProperty("id")] public int Id { get; init; }
    [JsonProperty("image")] public Image Image { get; init; }
    [JsonProperty("name")] public Name Name { get; init; }
    [JsonProperty("gender")] public string? Gender { get; init; }
    [JsonProperty("age")] public string? Age { get; init; }
    [JsonProperty("description")] public string Description { get; init; }

}