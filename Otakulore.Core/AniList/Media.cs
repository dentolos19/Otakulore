﻿using System.Text.Json.Serialization;
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

    [JsonPropertyName("id")] public int Id { get; init; }
    [JsonPropertyName("idMal")] public int? MalId { get; init; }

    [JsonPropertyName("coverImage")] public Cover CoverImage { get; init; }
    [JsonPropertyName("bannerImage")] public string BannerImageUrl { get; init; }
    [JsonPropertyName("title")] public MediaTitle Title { get; init; }
    [JsonPropertyName("description")] public string? Description { get; init; }
    [JsonPropertyName("type")] public MediaType Type { get; init; }
    [JsonPropertyName("format")] public MediaFormat? Format { get; init; }
    [JsonPropertyName("status")] public MediaStatus Status { get; init; }
    [JsonPropertyName("popularity")] public int? Popularity { get; init; }
    [JsonPropertyName("favourites")] public int? Favorites { get; init; }
    [JsonPropertyName("averageScore")] public int? Score { get; init; }
    [JsonPropertyName("genres")] public string[]? Genres { get; init; }
    [JsonPropertyName("startDate")] public Date StartDate { get; init; }
    [JsonPropertyName("endDate")] public Date EndDate { get; init; }

    [JsonPropertyName("season")] public MediaSeason? Season { get; init; }
    [JsonPropertyName("episodes")] public int? Episodes { get; init; }
    [JsonPropertyName("duration")] public int? Duration { get; init; }
    [JsonPropertyName("chapters")] public int? Chapters { get; init; }

}