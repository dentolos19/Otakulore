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
        Meta.Add(new MetadataItem { Label = entry.Media.Format.ToEnumDescription(true) });
        Meta.Add(new MetadataItem { Label = entry.Status.ToEnumDescription(true) });
        var totalProgress = Entry.Media.Type switch
        {
            MediaType.Anime => Entry.Media.Episodes?.ToString() ?? "?",
            MediaType.Manga => Entry.Media.Chapters?.ToString() ?? "?"
        };
        Meta.Add(new MetadataItem { Label = $"{Entry.Progress}/{totalProgress}" });
    }

}