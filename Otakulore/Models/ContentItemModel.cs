using Otakulore.Services;

namespace Otakulore.Models;

public class ContentItemModel
{

    public string Name { get; init; }
    public IMediaContent Content { get; init; }

    public static ContentItemModel Create(IMediaContent content)
    {
        return new ContentItemModel
        {
            Name = content.Name,
            Content = content
        };
    }

}