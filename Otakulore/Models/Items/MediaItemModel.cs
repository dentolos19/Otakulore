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
    public string Subtitle { get; init; }
    public string Tag { get; init; }

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

    public static MediaItemModel Map(MediaRelationEdge relation)
    {
        return new MediaItemModel
        {
            Id = relation.Media.Id,
            ImageUrl = relation.Media.Cover.ExtraLargeImageUrl,
            Title = relation.Media.Title.PreferredTitle,
            Tag = relation.Relation.Humanize()
        };
    }

    public static MediaItemModel Map(MediaSchedule schedule)
    {
        return new MediaItemModel()
        {
            Id = schedule.Media.Id,
            ImageUrl = schedule.Media.Cover.ExtraLargeImageUrl,
            Title = schedule.Media.Title.PreferredTitle + (
                schedule.Media.Type == MediaType.Anime && schedule.Media.Episodes.HasValue
                    ? schedule.Media.Format == MediaFormat.Movie
                        ? string.Empty
                        : $" (Episode {schedule.Media.Episodes})"
                    : string.Empty
            ),
            Subtitle = schedule.AiringTime.Humanize()
        };
    }

}