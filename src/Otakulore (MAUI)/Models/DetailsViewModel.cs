using AniListNet;
using AniListNet.Objects;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Humanizer;
using Otakulore.Pages;

namespace Otakulore.Models;

public partial class DetailsViewModel : ObservableObject, IQueryAttributable
{

    [ObservableProperty] private Uri _imageUrl;
    [ObservableProperty] private string _title;
    [ObservableProperty] private string _subtitle;
    [ObservableProperty] private string _description;
    [ObservableProperty] private string _format;
    [ObservableProperty] private string _contentLabel;
    [ObservableProperty] private string _content;
    [ObservableProperty] private string _startDate;
    [ObservableProperty] private string _endDate;
    [ObservableProperty] private bool _isLoading = true;

    [ICommand]
    private async Task Play()
    {
        await Shell.Current.GoToAsync(nameof(SearchProviderPage), new Dictionary<string, object>
        {
            { "query", _title }
        });
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.ContainsKey("id"))
            return;
        var id = int.Parse(query["id"].ToString());
        var data = await App.Client.GetMediaAsync(id);
        ImageUrl = data.Cover.ExtraLargeImageUrl;
        Title = data.Title.PreferredTitle;
        Subtitle = data.StartDate.Year.HasValue ? data.StartDate.Year.Value.ToString() : "????";
        Description = data.Description;
        Format = data.Format.Humanize(LetterCasing.Title);
        if (data.Format == MediaFormat.Movie && data.Episodes.GetValueOrDefault(0) == 1)
        {
            ContentLabel = "Duration";
            Content = data.Duration.HasValue ? data.Duration.Value.Humanize() : "???";
        }
        else
        {
            switch (data.Type)
            {
                case MediaType.Anime:
                    ContentLabel = "Episodes";
                    Content = data.Episodes.HasValue ? data.Episodes.Value.ToString() : "???";
                    break;
                case MediaType.Manga:
                    ContentLabel = "Chapters";
                    Content = data.Chapters.HasValue ? data.Chapters.Value.ToString() : "???";
                    break;
            }
        }
        var startDate = data.StartDate.ToDateOnly();
        if (startDate.HasValue)
            StartDate = startDate.Value.ToShortDateString();
        else
            StartDate = data.StartDate.Year.HasValue ? data.StartDate.Year.Value.ToString() : "???";
        var endDate = data.EndDate.ToDateOnly();
        if (endDate.HasValue)
            EndDate = endDate.Value.ToShortDateString();
        else
            EndDate = data.EndDate.Year.HasValue ? data.EndDate.Year.Value.ToString() : "???";
        IsLoading = false;
    }

}