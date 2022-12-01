using AniListNet.Objects;
using CommunityToolkit.Mvvm.Input;
using Humanizer;
using Otakulore.Pages;

namespace Otakulore.Models;

public partial class MediaItemModel
{

    public int Id { get; }
    public Uri ImageUrl { get; }
    public string Title { get; }
    public string Subtitle { get; protected init; }
    public string Tag { get; protected init; }

    public MediaItemModel(Media data)
    {
        Id = data.Id;
        ImageUrl = data.Cover.ExtraLargeImageUrl;
        Title = data.Title.PreferredTitle;
        Subtitle = data.Format?.Humanize(LetterCasing.Title) ?? "Unknown";
        Tag = Subtitle;
        if (data.Entry is not null)
            Subtitle += $" • {data.Entry.Status} • {data.Entry.Progress}/"
                        + (data.Entry.MaxProgress.HasValue ? data.Entry.MaxProgress.Value : "?");
    }

    [RelayCommand]
    private void Open()
    {
        MauiHelper.Navigate(
            typeof(DetailsPage),
            new Dictionary<string, object>
            {
                { "id", Id }
            }
        );
    }

}