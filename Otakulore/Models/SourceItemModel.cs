using Otakulore.Core;

namespace Otakulore.Models;

public class SourceItemModel
{

    public MediaType Type { get; }
    public MediaSource Source { get; }

    public SourceItemModel(MediaType type, MediaSource source)
    {
        Type = type;
        Source = source;
    }

}