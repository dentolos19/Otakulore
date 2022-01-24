using Otakulore.Core;

namespace Otakulore.Models;

public class ContentItemModel
{

    public ContentItemModel(MediaContent content)
    {
        Content = content;
    }

    public MediaContent Content { get; }

}