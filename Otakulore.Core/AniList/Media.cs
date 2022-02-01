using System.Text.Json.Serialization;
using Otakulore.Core.Helpers;

namespace Otakulore.Core.AniList;

public class Media
{

    public static GqlSelection[] Selections =>
        new GqlSelection[]
        {
            new("id"),
            new("idMal"),
            new("coverImage", new GqlSelection[]
            {
                new("extraLarge"),
                new("large"),
                new("medium"),
                new("color")
            }),
            new("bannerImage"),
            new("title", MediaTitle.Selections),
            new("description") { Parameters = { { "asHtml", false } } },
            new("type"),
            new("format"),
            new("status"),
            new("genres"),
            new("tags", MediaTag.Selections),
            new("averageScore"),
            new("startDate", Date.Selections),
            new("endDate", Date.Selections),
            new("isAdult"),
            new("episodes"),
            new("duration"),
            new("chapters")
        };

    [JsonPropertyName("id")] public int Id { get; init; }
    [JsonPropertyName("idMal")] public int? MalId { get; init; }

    [JsonPropertyName("coverImage")] public Cover CoverImage { get; init; }
    [JsonPropertyName("bannerImage")] public string BannerImageUrl { get; init; }
    [JsonPropertyName("title")] public MediaTitle Title { get; init; }
    [JsonPropertyName("description")] public string? Description { get; init; }
    [JsonPropertyName("type")] public MediaType Type { get; init; }
    [JsonPropertyName("format")] public MediaFormat? Format { get; init; }
    [JsonPropertyName("status")] public MediaStatus Status { get; init; }
    [JsonPropertyName("genres")] public string[]? Genres { get; init; }
    [JsonPropertyName("tags")] public MediaTag[]? Tags { get; init; }
    [JsonPropertyName("averageScore")] public int? Score { get; init; }
    [JsonPropertyName("startDate")] public Date StartDate { get; init; }
    [JsonPropertyName("endDate")] public Date EndDate { get; init; }
    [JsonPropertyName("isAdult")] public bool IsAdult { get; init; }

    [JsonPropertyName("episodes")] public int? Episodes { get; init; }
    [JsonPropertyName("duration")] public int? Duration { get; init; }
    [JsonPropertyName("chapters")] public int? Chapters { get; init; }

}