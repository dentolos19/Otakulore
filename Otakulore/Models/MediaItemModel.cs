using JikanDotNet;
using Otakulore.Core;

namespace Otakulore.Models;

public class MediaItemModel
{

    public MediaType Type { get; init; }
    public long Id { get; init; }
    public string ImageUrl { get; init; }
    public string Title { get; init; }

    public static MediaItemModel Create(Anime anime) => new()
    {
        Type = MediaType.Anime,
        Id = anime.MalId,
        ImageUrl = anime.ImageURL,
        Title = anime.Title
    };

    public static MediaItemModel Create(Manga manga) => new()
    {
        Type = MediaType.Manga,
        Id = manga.MalId,
        ImageUrl = manga.ImageURL,
        Title = manga.Title
    };

    public static MediaItemModel Create(AnimeSearchEntry anime) => new()
    {
        Type = MediaType.Anime,
        Id = anime.MalId,
        ImageUrl = anime.ImageURL,
        Title = anime.Title
    };

    public static MediaItemModel Create(MangaSearchEntry manga) => new()
    {
        Type = MediaType.Manga,
        Id = manga.MalId,
        ImageUrl = manga.ImageURL,
        Title = manga.Title
    };

    public static MediaItemModel Create(AnimeSubEntry anime) => new()
    {
        Type = MediaType.Anime,
        Id = anime.MalId,
        ImageUrl = anime.ImageURL,
        Title = anime.Title
    };

}