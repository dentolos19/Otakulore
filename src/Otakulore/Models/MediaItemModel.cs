using System;
using System.Collections.Generic;
using AniListNet;
using AniListNet.Objects;
using CommunityToolkit.WinUI.UI.Controls;
using Humanizer;

namespace Otakulore.Models;

public class MediaItemModel
{

    public Uri ImageUrl { get; }
    public Uri? BannerImageUrl { get; }
    public string Title { get; }
    public string? Tag { get; }
    public IList<MetadataItem> Meta { get;  } = new List<MetadataItem>();
    public object Data { get; }

    public MediaItemModel(Media media)
    {
        ImageUrl = media.Cover.ExtraLargeImageUrl;
        BannerImageUrl = media.BannerImageUrl;
        Title = media.Title.PreferredTitle;
        var format = media.Format.Humanize(LetterCasing.Title);
        Tag = format;
        Meta.Add(new MetadataItem { Label = format });
        var startDate = media.StartDate.ToDateOnly();
        if (startDate.HasValue)
            Meta.Add(new MetadataItem { Label = startDate.Value.Year.ToString() });
        Data = media;
    }

    public MediaItemModel(MediaEntry entry)
    {
        Data = entry;
        ImageUrl = entry.Media.Cover.ExtraLargeImageUrl;
        Title = entry.Media.Title.PreferredTitle;
        var format = entry.Media.Format.Humanize(LetterCasing.Title);
        Tag = format;
        Meta.Add(new MetadataItem { Label = format });
        // Meta.Add(new MetadataItem { Label = $"{entry.Progress}/{entry.Media.Content?.ToString() ?? "?"}" });
    }

    public MediaItemModel(MediaEdge media)
    {
        Data = media;
        ImageUrl = media.Media.Cover.ExtraLargeImageUrl;
        Title = media.Media.Title.PreferredTitle;
        var format = media.Relation.Humanize();
        Tag = format;
        Meta.Add(new MetadataItem { Label = format });
        var startDate = media.Media.StartDate.ToDateOnly();
        Meta.Add(new MetadataItem { Label = startDate.HasValue ? startDate.Value.Year.ToString() : "????" });
    }

    public MediaItemModel(CharacterEdge character)
    {
        Data = character;
        ImageUrl = character.Character.Image.LargeImageUrl;
        Title = character.Character.Name.PreferredName;
        var role = character.Role.Humanize();
        Tag = role;
        Meta.Add(new MetadataItem { Label = role });
        if (character.Character.Gender != null)
            Meta.Add(new MetadataItem { Label = character.Character.Gender });
        if (character.Character.Age != null)
            Meta.Add(new MetadataItem { Label = character.Character.Age });
    }

    public MediaItemModel(StaffEdge staff)
    {
        Data = staff;
        ImageUrl = staff.Staff.Image.LargeImageUrl;
        Title = staff.Staff.Name.PreferredName;
        Tag = staff.Role;
        Meta.Add(new MetadataItem { Label = staff.Role });
        if (staff.Staff.Gender != null)
            Meta.Add(new MetadataItem { Label = staff.Staff.Gender });
        if (staff.Staff.Age != null)
            Meta.Add(new MetadataItem { Label = staff.Staff.Age });
    }

    /*
    public MediaItemModel(UserActivity activity)
    {
        Media = activity;
        ImageUrl = activity.Media.Cover.ExtraLargeImageUrl;
        Title = activity.Media.Title.Preferred;
        Meta.Add(new MetadataItem { Label = activity.Status.Humanize(LetterCasing.Title) });
        if (activity.Progress != null)
            Meta.Add(new MetadataItem { Label = activity.Progress });
    }
    */

}