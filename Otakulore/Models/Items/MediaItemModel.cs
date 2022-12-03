using AniListNet.Objects;
using CommunityToolkit.Mvvm.Input;
using Humanizer;
using Otakulore.Pages;

namespace Otakulore.Models;

public partial class MediaItemModel
{

    public required int Id { get; init; }
    public required Uri ImageUrl { get; init; }
    public required string Title { get; init; }
    public required string Tag { get; init; }

    [RelayCommand]
    private Task Interact()
    {
        return MauiHelper.Navigate(typeof(MediaDetailsPage), Id);
    }

    public static MediaItemModel Map(Media media)
    {
        return new MediaItemModel
        {
            Id = media.Id,
            ImageUrl = media.Cover.ExtraLargeImageUrl,
            Title = media.Title.PreferredTitle,
            Tag = media.Format?.Humanize() ?? "Unknown"
        };
    }

}