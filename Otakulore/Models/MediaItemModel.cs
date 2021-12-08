using System;
using JikanDotNet;
using Otakulore.Core;

namespace Otakulore.Models;

public class MediaItemModel
{

    public MediaType Type { get; init; }
    public long Id { get; set; }

    public string ImageUrl { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public string Year { get; init; }
    public string Contents { get; init; }
    public string Status { get; init; }
    public float? Score { get; init; }

    public static MediaItemModel Create(Anime anime)
    {
        var synopsis = anime.Synopsis;
        if (synopsis.Length > 200)
            synopsis = synopsis[..Math.Min(synopsis.Length, 200)] + "...";
        return new MediaItemModel
        {
            Type = MediaType.Anime,
            Id = anime.MalId,
            ImageUrl = anime.ImageURL,
            Title = anime.Title,
            Description = synopsis,
            Year = anime.Aired.From.HasValue ? anime.Aired.From.Value.Year.ToString() : "Unknown Year",
            Contents = anime.Episodes.HasValue ? $"{anime.Episodes.Value} Episode(s)" : "No Episodes",
            Status = anime.Airing ? "Airing" : anime.Aired.From.HasValue ? "Finished" : "Unreleased",
            Score = anime.Score / 2
        };
    }

    public static MediaItemModel Create(AnimeSearchEntry anime)
    {
        return new MediaItemModel
        {
            Type = MediaType.Anime,
            Id = anime.MalId,
            ImageUrl = anime.ImageURL,
            Title = anime.Title,
            Description = anime.Description,
            Year = anime.StartDate.HasValue ? anime.StartDate.Value.Year.ToString() : "Unknown Year",
            Contents = anime.Episodes.HasValue ? $"{anime.Episodes.Value} Episode(s)" : "No Episodes",
            Status = anime.Airing ? "Airing" : anime.StartDate.HasValue ? "Finished" : "Unreleased",
            Score = anime.Score / 2
        };
    }

    public static MediaItemModel Create(AnimeTopEntry anime)
    {
        return new MediaItemModel
        {
            Type = MediaType.Anime,
            Id = anime.MalId,
            ImageUrl = anime.ImageURL,
            Title = anime.Title,
        };
    }

    public static MediaItemModel Create(Manga manga)
    {
        var synopsis = manga.Synopsis;
        if (synopsis.Length > 200)
            synopsis = synopsis[..Math.Min(synopsis.Length, 200)] + "...";
        return new MediaItemModel
        {
            Type = MediaType.Manga,
            Id = manga.MalId,
            ImageUrl = manga.ImageURL,
            Title = manga.Title,
            Description = synopsis,
            Year = manga.Published.From.HasValue ? manga.Published.From.Value.Year.ToString() : "Unknown Year",
            Contents = !manga.Chapters.HasValue ? "No Chapters" : manga.Publishing ? "Progressing Chapters" : $"{manga.Chapters} Chapter(s)",
            Status = manga.Publishing ? "Publishing" : manga.Published.From.HasValue ? "Finished" : "Unreleased",
            Score = manga.Score / 2
        };
    }

    public static MediaItemModel Create(MangaSearchEntry manga)
    {
        return new MediaItemModel
        {
            Type = MediaType.Manga,
            Id = manga.MalId,
            ImageUrl = manga.ImageURL,
            Title = manga.Title,
            Description = manga.Description,
            Year = manga.StartDate.HasValue ? manga.StartDate.Value.Year.ToString() : "Unknown Year",
            Contents = !manga.Chapters.HasValue ? "No Chapters" : manga.Publishing ? "Progressing Chapters" : $"{manga.Chapters} Chapter(s)",
            Status = manga.Publishing ? "Publishing" : manga.StartDate.HasValue ? "Finished" : "Unreleased",
            Score = manga.Score / 2
        };
    }

    public static MediaItemModel Create(MangaTopEntry manga)
    {
        return new MediaItemModel
        {
            Type = MediaType.Manga,
            Id = manga.MalId,
            ImageUrl = manga.ImageURL,
            Title = manga.Title
        };
    }

}