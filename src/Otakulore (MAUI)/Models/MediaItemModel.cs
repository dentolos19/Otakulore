using AniListNet.Objects;
using Humanizer;

namespace Otakulore.Models;

public class MediaItemModel
{

    public Uri ImageUrl { get; }
    public string Title { get; }
    public string Format { get; }
    public Media Data { get; }

    public MediaItemModel(Media data)
    {
        ImageUrl = data.Cover.ExtraLargeImageUrl;
        Title = data.Title.PreferredTitle;
        Format = data.Format.Humanize(LetterCasing.Title);
        Data = data;
    }

}