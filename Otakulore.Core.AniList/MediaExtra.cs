using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Otakulore.Core.Helpers;

namespace Otakulore.Core.AniList;

public class MediaExtra : Media
{

    [JsonProperty("relations")] private readonly JObject _relations;
    [JsonProperty("characters")] private readonly JObject _characters;

    public static GqlSelection[] Selections =>
        Media.Selections.Concat(new GqlSelection[]
        {
            new("tags", MediaTag.Selections),
            new("mediaListEntry", MediaEntry.MediaSelections),
            new("relations", new GqlSelection[] { new("edges", MediaEdge.Selections) }),
            new("characters", new GqlSelection[] { new("edges", CharacterEdge.Selections) }) { Parameters = { { "sort", "$ROLE" } } }
        }).ToArray();

    [JsonProperty("tags")] public MediaTag[]? Tags { get; init; }
    [JsonProperty("mediaListEntry")] public MediaEntry? Entry { get; init; }

    public MediaEdge[] Relations => _relations["edges"].ToObject<MediaEdge[]>();
    public CharacterEdge[] Characters => _characters["edges"].ToObject<CharacterEdge[]>();

}