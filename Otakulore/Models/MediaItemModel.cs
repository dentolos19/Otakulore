using JikanDotNet;
using Otakulore.AniList;

namespace Otakulore.Models;

public class MediaItemModel
{

    public MediaType Type { get; init; }
    public long Id { get; init; }
    public string ImageUrl { get; init; }
    public string Title { get; init; }
    public Media Data { get; }

    public MediaItemModel(Media? data = null)
    {
        if (data == null)
            return;
        Data = data;
        Id = Data.Id;
        ImageUrl = Data.Cover.LargeImageUrl;
        Title = Data.Title.Romaji;
        if (App.Settings.UseEnglishTitles && Data.Title.English != null)
            Title = Data.Title.English;
    }

    public static MediaItemModel Create(AnimeSubEntry anime) => new()
    {
        Type = MediaType.Anime,
        Id = anime.MalId,
        ImageUrl = anime.ImageURL,
        Title = anime.Title
    };

}