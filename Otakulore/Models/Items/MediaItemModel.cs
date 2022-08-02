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
    public string Tag { get; protected init; }

    public MediaItemModel(Media data)
    {
        Id = data.Id;
        ImageUrl = data.Cover.ExtraLargeImageUrl;
        Title = data.Title.PreferredTitle;
        Tag = data.Format?.Humanize(LetterCasing.Title) ?? "Unknown";
        if (data.Entry is null)
            return;
        Tag += $" • {data.Entry.Status} • {data.Entry.Progress}/"
            + (data.Entry.MaxProgress.HasValue ? data.Entry.MaxProgress.Value : "?");
    }

    [ICommand]
    private void Open()
    {
        MauiHelper.NavigateTo(
            typeof(DetailsPage),
            new Dictionary<string, object>
            {
                { "id", Id }
            }
        );
    }

}