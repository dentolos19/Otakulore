using AniListNet.Objects;

namespace Otakulore.Models;

public class MediaItemModel
{

    public Uri ImageUrl { get; }
    public string Title { get; }
    public Media Data { get; }

    public MediaItemModel(Media data)
    {
        ImageUrl = data.Cover.ExtraLargeImageUrl;
        Title = data.Title.PreferredTitle;
        Data = data;
    }

}