using Newtonsoft.Json;
using Otakulore.Core.Helpers;

namespace Otakulore.Core.AniList;

public class Media
{

    internal static GqlSelection[] Selections =>
        new GqlSelection[]
        {
            new("id"),
            new("idMal"),
            new("coverImage", Cover.Selections),
            new("bannerImage"),
            new("title", MediaTitle.Selections),
            new("description") { Parameters = { { "asHtml", false } } },
            new("type"),
            new("format"),
            new("status"),
            new("popularity"),
            new("favourites"),
            new("averageScore"),
            new("genres"),
            new("startDate", Date.Selections),
            new("endDate", Date.Selections),
            new("season"),
            new("episodes"),
            new("duration"),
            new("chapters")
        };

    [JsonProperty("startDate")] private readonly Date _startDate;
    [JsonProperty("endDate")] private readonly Date _endDate;
    [JsonProperty("episodes")] private readonly int? _episodes;
    [JsonProperty("chapters")] private readonly int? _chapters;

    [JsonProperty("id")] public int Id { get; init; }
    [JsonProperty("idMal")] public int? MalId { get; init; }
    [JsonProperty("coverImage")] public Cover Cover { get; init; }
    [JsonProperty("bannerImage")] public string BannerImageUrl { get; init; }
    [JsonProperty("title")] public MediaTitle Title { get; init; }
    [JsonProperty("description")] public string? Description { get; init; }
    [JsonProperty("type")] public MediaType Type { get; init; }
    [JsonProperty("format")] public MediaFormat? Format { get; init; }
    [JsonProperty("status")] public MediaStatus Status { get; init; }
    [JsonProperty("popularity")] public int? Popularity { get; init; }
    [JsonProperty("favourites")] public int? Favorites { get; init; }
    [JsonProperty("averageScore")] public int? Score { get; init; }
    [JsonProperty("genres")] public string[]? Genres { get; init; }
    [JsonProperty("season")] public MediaSeason? Season { get; init; }
    [JsonProperty("duration")] public int? Duration { get; init; }
    public int? Content => Type switch { MediaType.Anime => _episodes, MediaType.Manga => _chapters };
    public DateOnly? StartDate => _startDate.ToDateOnly();
    public DateOnly? EndDate => _endDate.ToDateOnly();

}