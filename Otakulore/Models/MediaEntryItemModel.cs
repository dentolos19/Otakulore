using System.Collections.Generic;
using CommunityToolkit.WinUI.UI.Controls;
using Otakulore.Core;
using Otakulore.Core.AniList;

namespace Otakulore.Models;

public class MediaEntryItemModel
{

    public IList<MetadataItem> Meta { get; } = new List<MetadataItem>();

    public MediaEntry Entry { get; }

    public MediaEntryItemModel(MediaEntry entry)
    {
        Entry = entry;
        Meta.Add(new MetadataItem { Label = entry.Status.GetEnumDescription(true) });
        Meta.Add(new MetadataItem { Label = entry.Progress.ToString() });
    }

}