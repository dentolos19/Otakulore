using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Otakulore.Core.Helpers;

namespace Otakulore.Core.AniList;

public class MediaExtra : Media
{

    [JsonProperty("characters")] private readonly JObject _characters;
    [JsonProperty("staff")] private readonly JObject _staff;
    [JsonProperty("relations")] private readonly JObject _relations;

    internal static GqlSelection[] Selections =>
        Media.Selections.Concat(new GqlSelection[]
        {
            new("tags", MediaTag.Selections),
            new("mediaListEntry", MediaEntry.MediaSelections),
            new("characters", new GqlSelection[] { new("edges", CharacterEdge.Selections) }) { Parameters = { { "sort", "$ROLE" } } },
            new("staff", new GqlSelection[] { new("edges", StaffEdge.Selections) }),
            new("relations", new GqlSelection[] { new("edges", MediaEdge.Selections) })
        }).ToArray();

    public CharacterEdge[] Characters => _characters["edges"].ToObject<CharacterEdge[]>(); // TODO: add pagination compatibilities
    public StaffEdge[] Staff => _staff["edges"].ToObject<StaffEdge[]>(); // TODO: add pagination compatibilities
    public MediaEdge[] Relations => _relations["edges"].ToObject<MediaEdge[]>(); // TODO: add pagination compatibilities

    [JsonProperty("tags")] public MediaTag[]? Tags { get; init; }
    [JsonProperty("mediaListEntry")] public MediaEntry? Entry { get; init; }

}