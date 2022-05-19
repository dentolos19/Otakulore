using Otakulore.Core;

namespace Otakulore.Models;

public class ContentItemModel
{

    public MediaContent Content { get; }

    public ContentItemModel(MediaContent content)
    {
        Content = content;
    }

}