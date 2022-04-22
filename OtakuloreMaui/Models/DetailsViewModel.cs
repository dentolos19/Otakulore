using CommunityToolkit.Mvvm.ComponentModel;
using Humanizer;
using Otakulore.Core.AniList;

namespace Otakulore.Models;

public partial class DetailsViewModel : ObservableObject, IQueryAttributable
{

    [ObservableProperty] private string _imageUrl;
    [ObservableProperty] private string _title;
    [ObservableProperty] private string _description;
    [ObservableProperty] private string _format;
    [ObservableProperty] private string _content;
    [ObservableProperty] private string _startDate;
    [ObservableProperty] private string _endDate;
    [ObservableProperty] private bool _isLoading;

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!int.TryParse((string)query["id"], out var id))
            return;
        IsLoading = true;
        var media = await App.Client.GetMedia(id);
        ImageUrl = media.Cover.ExtraLargeImageUrl;
        Title = media.Title.Preferred;
        Description = media.Description ?? "No description has been provided.";
        Format = media.Format.Humanize();
        if (media.Content.HasValue)
        {
            Content += media.Content.Value + media.Type switch
            {
                MediaType.Anime => " episodes",
                MediaType.Manga => " chapters"
            };
        }
        else
        {
            Content = "???";
        }
        StartDate = media.StartDate.HasValue ? media.StartDate.Value.ToShortDateString() : "???";
        EndDate = media.EndDate.HasValue ? media.EndDate.Value.ToShortDateString() : "???";
        IsLoading = false;
    }

}