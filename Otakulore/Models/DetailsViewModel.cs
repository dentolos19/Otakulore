using AniListNet;
using AniListNet.Objects;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Humanizer;
using Otakulore.Pages;

namespace Otakulore.Models;

public partial class DetailsViewModel : ObservableObject, IQueryAttributable
{

    private readonly AniClient _client;

    [ObservableProperty] private Uri _imageUrl;
    [ObservableProperty] private string _title;
    [ObservableProperty] private string _subtitle;
    [ObservableProperty] private string _description;
    [ObservableProperty] private string _format;
    [ObservableProperty] private string _content;
    [ObservableProperty] private string _contentLabel;
    [ObservableProperty] private string _startDate;
    [ObservableProperty] private string _endDate;
    [ObservableProperty] private bool _isLoading = true;

    public DetailsViewModel(AniClient client)
    {
        _client = client;
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.ContainsKey("id"))
            return;
        if (query["id"] is not int id)
            return;
        var data = await _client.GetMediaAsync(id);
        ImageUrl = data.Cover.ExtraLargeImageUrl;
        Title = data.Title.PreferredTitle;
        Subtitle = data.Type == MediaType.Anime && data.Season.HasValue && data.SeasonYear.HasValue
            ? $"{data.Season} {data.SeasonYear}"
            : data.StartDate.Year.HasValue
                ? data.StartDate.Year.Value.ToString()
                : data.Type.ToString();
        Description = data.Description ?? "No description provided.";
        Format = data.Format?.Humanize(LetterCasing.Title) ?? "Unknown";
        Content = data.Episodes.HasValue
            ? data.Episodes.Value.ToString()
            : data.Chapters.HasValue
                ? data.Chapters.Value.ToString()
                : "Unknown";
        ContentLabel = data.Type == MediaType.Anime ? "Episodes" : "Chapters";
        StartDate = data.StartDate.ToDateTime()?.ToShortDateString() ?? "Unknown";
        EndDate = data.EndDate.ToDateTime()?.ToShortDateString() ?? "Unknown";
        IsLoading = false;
    }

    [ICommand]
    private Task Play()
    {
        return MauiHelper.Navigate(
            typeof(SearchProviderPage),
            new Dictionary<string, object>
            {
                { "query", Title }
            }
        );
    }

}