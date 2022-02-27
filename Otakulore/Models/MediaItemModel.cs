using System.Collections.Generic;
using CommunityToolkit.WinUI.UI.Controls;
using Humanizer;
using Otakulore.Core.AniList;

namespace Otakulore.Models;

public class MediaItemModel
{

    public object Media { get; }
    public string ImageUrl { get; }
    public string Title { get; }
    public string? Tag { get; }
    public IList<MetadataItem> Meta { get; } = new List<MetadataItem>();

    public MediaItemModel(Media media)
    {
        Media = media;
        ImageUrl = media.Cover.ExtraLargeImageUrl;
        Title = media.Title.Preferred;
        Tag = media.Format?.Humanize() ?? "Unknown";
        Meta.Add(new MetadataItem { Label = media.Format != null ? media.Format?.Humanize() : "Unknown Format" });
        Meta.Add(new MetadataItem { Label = media.StartDate.HasValue ? media.StartDate.Value.Year.ToString() : "????" });
    }

    public MediaItemModel(MediaEntry entry)
    {
        Media = entry;
        ImageUrl = entry.Media.Cover.ExtraLargeImageUrl;
        Title = entry.Media.Title.Preferred;
        Meta.Add(new MetadataItem { Label = entry.Media.Format?.Humanize() ?? "Unknown Format" });
        Meta.Add(new MetadataItem { Label = entry.Status.Humanize() });
        Meta.Add(new MetadataItem { Label = $"{entry.Progress}/{entry.Media.Content?.ToString() ?? "?"}" });
    }

    public MediaItemModel(MediaEdge media)
    {
        Media = media;
        ImageUrl = media.Details.Cover.ExtraLargeImageUrl;
        Title = media.Details.Title.Preferred;
        Tag = media.Relation.Humanize();
        Meta.Add(new MetadataItem { Label = media.Details.Format != null ? media.Details.Format?.Humanize() : "Unknown Format" });
        Meta.Add(new MetadataItem { Label = media.Details.StartDate.HasValue ? media.Details.StartDate.Value.Year.ToString() : "????" });
    }

    public MediaItemModel(CharacterEdge character)
    {
        Media = character;
        ImageUrl = character.Details.Image.LargeImageUrl;
        Title = character.Details.Name.Preferred;
        var role = character.Role.Humanize();
        Tag = role;
        Meta.Add(new MetadataItem { Label = role });
        if (character.Details.Gender != null)
            Meta.Add(new MetadataItem { Label = character.Details.Gender });
        if (character.Details.Age != null)
            Meta.Add(new MetadataItem { Label = character.Details.Age });
    }

    public MediaItemModel(StaffEdge staff)
    {
        Media = staff;
        ImageUrl = staff.Details.Image.LargeImageUrl;
        Title = staff.Details.Name.Preferred;
        Tag = staff.Role;
        Meta.Add(new MetadataItem { Label = staff.Role });
        if (staff.Details.Gender != null)
            Meta.Add(new MetadataItem { Label = staff.Details.Gender });
        if (staff.Details.Age != null)
            Meta.Add(new MetadataItem { Label = staff.Details.Age });
    }

    public MediaItemModel(UserActivity activity)
    {
        Media = activity;
        ImageUrl = activity.Media.Cover.ExtraLargeImageUrl;
        Title = activity.Media.Title.Preferred;
        Meta.Add(new MetadataItem { Label = activity.Status.Humanize(LetterCasing.Title) });
        if (activity.Progress != null)
            Meta.Add(new MetadataItem { Label = activity.Progress });
    }

}