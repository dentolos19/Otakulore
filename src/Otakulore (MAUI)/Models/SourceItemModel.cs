using Otakulore.Services;

namespace Otakulore.Models;

public class SourceItemModel
{

    public Uri ImageUrl { get; init; }
    public string Title { get; init; }

    public SourceItemModel(MediaSource data)
    {
        ImageUrl = data.ImageUrl;
        Title = data.Title;
    }

}