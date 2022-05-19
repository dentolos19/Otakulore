using AniListNet.Objects;
using Humanizer;

namespace Otakulore.Models;

public class MediaItemModel
{

    public int Id { get; }
    public string ImageUrl { get; }
    public string BannerImageUrl { get; }
    public string Title { get; }
    public string Format { get; }

    public MediaItemModel(Media media)
    {
        Id = media.Id;
        ImageUrl = media.Cover.ExtraLargeImageUrl.ToString();
        BannerImageUrl = media.BannerImageUrl.ToString();
        Title = media.Title.PreferredTitle;
        Format = media.Format?.Humanize() ?? "???";
    }

}