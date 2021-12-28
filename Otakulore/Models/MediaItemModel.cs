using JikanDotNet;
using Otakulore.Core;
using System;

namespace Otakulore.Models;

public class MediaItemModel
{

    public MediaType Type { get; init; }
    public long Id { get; init; }

    public Uri ImageUrl { get; init; }
    public string Title { get; init; }

    public static MediaItemModel Create(Anime anime)
    {
        return new MediaItemModel
        {
            Type = MediaType.Anime,
            Id = anime.MalId,
            ImageUrl = new Uri(anime.ImageURL),
            Title = anime.Title
        };
    }

    public static MediaItemModel Create(AnimeTopEntry anime)
    {
        return new MediaItemModel
        {
            Type = MediaType.Anime,
            Id = anime.MalId,
            ImageUrl = new Uri(anime.ImageURL),
            Title = anime.Title
        };
    }

    public static MediaItemModel Create(AnimeSearchEntry anime)
    {
        return new MediaItemModel
        {
            Type = MediaType.Anime,
            Id = anime.MalId,
            ImageUrl = new Uri(anime.ImageURL),
            Title = anime.Title
        };
    }

    public static MediaItemModel Create(Manga manga)
    {
        return new MediaItemModel
        {
            Type = MediaType.Manga,
            Id = manga.MalId,
            ImageUrl = new Uri(manga.ImageURL),
            Title = manga.Title
        };
    }

    public static MediaItemModel Create(MangaTopEntry manga)
    {
        return new MediaItemModel
        {
            Type = MediaType.Manga,
            Id = manga.MalId,
            ImageUrl = new Uri(manga.ImageURL),
            Title = manga.Title
        };
    }

    public static MediaItemModel Create(MangaSearchEntry manga)
    {
        return new MediaItemModel
        {
            Type = MediaType.Manga,
            Id = manga.MalId,
            ImageUrl = new Uri(manga.ImageURL),
            Title = manga.Title
        };
    }

}