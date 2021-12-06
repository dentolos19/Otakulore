using Otakulore.Core;
using Otakulore.Services;

namespace Otakulore.Models;

public class SourceItemModel
{

    public MediaType Type { get; init; }
    public string ImageUrl { get; init; }
    public string Title { get; init; }
    public IMediaInfo Info { get; init; }

    public static SourceItemModel Create(IMediaInfo info, MediaType type)
    {
        return new SourceItemModel
        {
            Type = type,
            ImageUrl = info.ImageUrl,
            Title = info.Title,
            Info = info
        };
    }

}