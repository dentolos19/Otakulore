using Otakulore.Services;

namespace Otakulore.Models;

public class ContentItemModel
{

    public int Index { get; init; }
    public string Title { get; init; }
    public IMediaContent Content { get; init; }

    public static ContentItemModel Create(IMediaContent content)
    {
        return new ContentItemModel
        {
            Index = content.Index,
            Title = content.Title,
            Content = content
        };
    }

}