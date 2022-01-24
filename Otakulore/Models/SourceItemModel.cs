using Otakulore.Core;

namespace Otakulore.Models;

public class SourceItemModel
{

    public SourceItemModel(MediaSource source)
    {
        Source = source;
    }

    public MediaSource Source { get; }

}