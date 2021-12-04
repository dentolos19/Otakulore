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
    public float Score { get; init; }

    public static MediaItemModel Create(Anime anime)
    {
        return new MediaItemModel
        {
            Type = MediaType.Anime,
            Id = anime.MalId,
            ImageUrl = anime.ImageURL,
            Title = anime.Title,
            Description = anime.Synopsis,
            Year = anime.Aired.From.HasValue ? anime.Aired.From.Value.Year.ToString() : "????",
            Contents = anime.Episodes.HasValue ? $"{anime.Episodes.Value} Episode(s)" : "No Episodes",
            Status = anime.Airing ? "Airing" : "Finished",
            Score = anime.Score.HasValue ? anime.Score.Value / 2 : -1
        };
    }

    public static MediaItemModel Create(AnimeSearchEntry animeEntry)
    {
        return new MediaItemModel
        {
            Type = MediaType.Anime,
            Id = animeEntry.MalId,
            ImageUrl = animeEntry.ImageURL,
            Title = animeEntry.Title,
            Description = animeEntry.Description,
            Year = animeEntry.StartDate.HasValue ? animeEntry.StartDate.Value.Year.ToString() : "????",
            Contents = animeEntry.Episodes.HasValue ? $"{animeEntry.Episodes.Value} Episode(s)" : "No Episodes",
            Status = animeEntry.Airing ? "Airing" : "Finished",
            Score = animeEntry.Score.HasValue ? animeEntry.Score.Value / 2 : -1
        };
    }

    public static MediaItemModel Create(Manga manga)
    {
        return new MediaItemModel
        {
            Type = MediaType.Manga,
            Id = manga.MalId,
            ImageUrl = manga.ImageURL,
            Title = manga.Title,
            Description = manga.Synopsis,
            Year = manga.Published.From.HasValue ? manga.Published.From.Value.Year.ToString() : "????",
            Contents = !manga.Chapters.HasValue ? "No Chapters" : manga.Publishing ? "Progressing Chapters" : $"{manga.Chapters} Chapter(s)",
            Status = manga.Publishing ? "Publishing" : "Finished",
            Score = manga.Score.HasValue ? manga.Score.Value / 2 : -1
        };
    }

    public static MediaItemModel Create(MangaSearchEntry mangaEntry)
    {
        return new MediaItemModel
        {
            Type = MediaType.Manga,
            Id = mangaEntry.MalId,
            ImageUrl = mangaEntry.ImageURL,
            Title = mangaEntry.Title,
            Description = mangaEntry.Description,
            Year = mangaEntry.StartDate.HasValue ? mangaEntry.StartDate.Value.Year.ToString() : "????",
            Contents = !mangaEntry.Chapters.HasValue ? "No Chapters" : mangaEntry.Publishing ? "Progressing Chapters" : $"{mangaEntry.Chapters} Chapter(s)",
            Status = mangaEntry.Publishing ? "Publishing" : "Finished",
            Score = mangaEntry.Score.HasValue ? mangaEntry.Score.Value / 2 : -1
        };
    }

}