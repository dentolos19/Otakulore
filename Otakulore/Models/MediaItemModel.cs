using System.Collections.Generic;
using CommunityToolkit.WinUI.UI.Controls;
using Otakulore.Core.AniList;
using Otakulore.Core.Helpers;

namespace Otakulore.Models;

public class MediaItemModel
{

    public Media Media { get; }
    public string Tag { get; }
    public IList<MetadataItem> Meta { get; } = new List<MetadataItem>();

    public MediaItemModel(Media media, string? tag = null)
    {
        Media = media;
        Tag = tag ?? Media.Format.ToEnumDescription(true);
        Meta.Add(new MetadataItem { Label = Media.Format != null ? Media.Format.ToEnumDescription(true) : "Unknown Format" });
        Meta.Add(new MetadataItem { Label = Media.StartDate.HasValue ? Media.StartDate.Value.Year.ToString() : "????" });
    }

}