using System.Collections.Generic;
using CommunityToolkit.WinUI.UI.Controls;
using Otakulore.Core;
using Otakulore.Core.AniList;

namespace Otakulore.Models;

public class MediaEntryItemModel
{

    public MediaEntry Entry { get; }
    public IList<MetadataItem> Meta { get; } = new List<MetadataItem>();

    public MediaEntryItemModel(MediaEntry entry)
    {
        Entry = entry;
        Meta.Add(new MetadataItem { Label = entry.Media.Format.ToEnumDescription(true) });
        Meta.Add(new MetadataItem { Label = entry.Status.ToEnumDescription(true) });
        Meta.Add(new MetadataItem { Label = $"{Entry.Progress}/{Entry.Media.Content?.ToString() ?? "?"}" });
    }

}