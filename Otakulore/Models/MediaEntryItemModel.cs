using AniListNet.Objects;

namespace Otakulore.Models;

public class MediaEntryItemModel : MediaItemModel
{

    public MediaEntryItemModel(MediaEntry data) : base(data.Media)
    {
        Tag = $"{data.Status} • {data.Progress}/{data.MaxProgress ?? '?'}";
    }

}