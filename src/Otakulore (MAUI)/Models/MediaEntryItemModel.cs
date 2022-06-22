using AniListNet.Objects;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Pages;

namespace Otakulore.Models;

public partial class MediaEntryItemModel
{

    public Uri ImageUrl { get; }
    public string Title { get; }
    public string Progress { get; }
    public MediaEntry Data { get; }

    public MediaEntryItemModel(MediaEntry data)
    {
        ImageUrl = data.Media.Cover.ExtraLargeImageUrl;
        Title = data.Media.Title.PreferredTitle;
        Progress = data.Progress + "/" + (data.MaxProgress.HasValue ? data.MaxProgress.ToString() : "?");
        Data = data;
    }

    [ICommand]
    private async Task Open(MediaEntry data)
    {
        await Shell.Current.GoToAsync(nameof(DetailsPage), new Dictionary<string, object>
        {
            { "id", Data.Media.Id }
        });
    }

}