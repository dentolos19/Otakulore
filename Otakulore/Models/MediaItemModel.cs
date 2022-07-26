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
    public string Tag { get; set; }

    public MediaItemModel(Media data)
    {
        Id = data.Id;
        ImageUrl = data.Cover.ExtraLargeImageUrl;
        Title = data.Title.PreferredTitle;
        Tag = data.Format?.Humanize(LetterCasing.Title) ?? "Unknown";
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