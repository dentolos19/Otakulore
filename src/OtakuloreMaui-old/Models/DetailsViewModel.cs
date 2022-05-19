using AniListNet.Objects;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Humanizer;

namespace Otakulore.Models;

public partial class DetailsViewModel : ObservableObject, IQueryAttributable
{

    [ObservableProperty] private string _imageUrl;
    [ObservableProperty] private string _title;
    [ObservableProperty] private string _subtitle;
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
        var media = await App.Client.GetMediaAsync(id);
        ImageUrl = media.Cover.ExtraLargeImageUrl.ToString();
        Title = media.Title.PreferredTitle;
        Subtitle = media.StartDate.Year.HasValue ? media.StartDate.Year.Value.ToString() : "????";
        Description = media.Description ?? "No description has been provided.";
        Format = media.Format.Humanize();
        /* TODO: update this
        if (media.Content.HasValue)
            Content += media.Content.Value + media.Type switch
            {
                MediaType.Anime => " episodes",
                MediaType.Manga => " chapters"
            };
        else
            Content = "???";
        */
        var startDate = media.StartDate.ToDateOnly();
        var endDate = media.EndDate.ToDateOnly();
        StartDate = startDate.HasValue ? startDate.Value.ToShortDateString() : "???";
        EndDate = endDate.HasValue ? endDate.Value.ToShortDateString() : "???";
        IsLoading = false;
    }

    [ICommand]
    private async void Play()
    {
        await Shell.Current.GoToAsync("sourceSearcher?query=" + Uri.EscapeDataString(_title));
    }

}