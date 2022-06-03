using AniListNet.Objects;
using CommunityToolkit.Mvvm.Input;
using Humanizer;
using Otakulore.Pages;

namespace Otakulore.Models;

public partial class MediaItemModel
{

    public Uri ImageUrl { get; }
    public string Title { get; }
    public string Format { get; }
    public Media Data { get; }

    public MediaItemModel(Media data)
    {
        ImageUrl = data.Cover.ExtraLargeImageUrl;
        Title = data.Title.PreferredTitle;
        Format = data.Format.Humanize(LetterCasing.Title);
        Data = data;
    }

    [ICommand]
    private async Task Open()
    {
        await Shell.Current.GoToAsync(nameof(DetailsPage), new Dictionary<string, object>
        {
            { "id", Data.Id }
        });
    }

}