using Otakulore.Core;

namespace Otakulore.Models;

public class SourceItemModel
{

    public MediaSource Source { get; }

    public SourceItemModel(MediaSource source)
    {
        Source = source;
    }

}