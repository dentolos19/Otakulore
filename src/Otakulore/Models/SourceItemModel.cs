using Otakulore.Core;

namespace Otakulore.Models;

public class SourceItemModel
{

    public MediaSource Data { get; }

    public SourceItemModel(MediaSource data)
    {
        Data = data;
    }

}