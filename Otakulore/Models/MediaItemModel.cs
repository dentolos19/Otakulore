using System.Collections.Generic;
using CommunityToolkit.WinUI.UI.Controls;
using Otakulore.Core;
using Otakulore.Core.AniList;

namespace Otakulore.Models;

public class MediaItemModel
{

    public string ImageUrl { get; }
    public string? BannerImageUrl { get; }
    public string Title { get; }
    public IList<MetadataItem> Meta { get; } = new List<MetadataItem>();
    public double Score { get; }
    public string ScoreCaption { get; }
    public Media Media { get; }

    public MediaItemModel(Media media)
    {
        Media = media;
        ImageUrl = Media.CoverImage.LargeImageUrl;
        BannerImageUrl = Media.BannerImageUrl;
        Title = Media.Title.Romaji;
        Meta.Add(new MetadataItem { Label = Media.Format != null ? Media.Format.GetEnumDescription(true) : "Unknown Format" });
        Meta.Add(new MetadataItem { Label = Media.StartDate.Year.HasValue ? Media.StartDate.Year.Value.ToString() : "????" });
        if (Media.Type == MediaType.Anime)
            Meta.Add(new MetadataItem { Label = Media.Season != null ? Media.Season.GetEnumDescription(true) : "Unknown Season" });
        Score = Media.Score.HasValue ? Media.Score.Value / 20 : 0;
        ScoreCaption = media.Score?.ToString() ?? "Unknown";
    }

}