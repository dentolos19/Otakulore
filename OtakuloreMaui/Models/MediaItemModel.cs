using Otakulore.Core.AniList;

namespace Otakulore.Models;

public class MediaItemModel
{

    public int Id { get; }
    public string ImageUrl { get; }
    public string BannerImageUrl { get; }
    public string Title { get; }

    public MediaItemModel(Media media)
    {
        Id = media.Id;
        ImageUrl = media.Cover.ExtraLargeImageUrl;
        BannerImageUrl = media.BannerImageUrl;
        Title = media.Title.Preferred;
    }

}