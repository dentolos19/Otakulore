using AniListNet;
using AniListNet.Objects;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Humanizer;
using Otakulore.Pages;

namespace Otakulore.Models;

public partial class DetailsViewModel : ObservableObject, IQueryAttributable
{

    private readonly AniClient _client;

    private bool _queryApplied;

    [ObservableProperty] private Uri _imageUrl;
    [ObservableProperty] private string _title;
    [ObservableProperty] private string _subtitle;
    [ObservableProperty] private string _description;
    [ObservableProperty] private string _format;
    [ObservableProperty] private string _status;
    [ObservableProperty] private string _content;
    [ObservableProperty] private string _contentLabel;
    [ObservableProperty] private string _startDate;
    [ObservableProperty] private string _endDate;
    [ObservableProperty] private string _popularity;
    [ObservableProperty] private string _score;
    [ObservableProperty] private string _favorites;
    [ObservableProperty] private string[] _genres;
    [ObservableProperty] private string[] _tags;
    [ObservableProperty] private bool _isLoading = true;

    public DetailsViewModel(AniClient client)
    {
        _client = client;
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (_queryApplied)
            return;
        _queryApplied = true;
        if (!query.ContainsKey("id"))
            return;
        if (query["id"] is not int id)
            return;
        var media = await _client.GetMediaAsync(id);
        ImageUrl = media.Cover.ExtraLargeImageUrl;
        Title = media.Title.PreferredTitle;
        Subtitle = media.Type == MediaType.Anime && media.Season.HasValue && media.SeasonYear.HasValue
            ? $"{media.Season} {media.SeasonYear}"
            : media.StartDate.Year.HasValue
                ? media.StartDate.Year.Value.ToString()
                : media.Type.ToString();
        Description = media.Description ?? "No description provided.";
        Format = media.Format?.Humanize(LetterCasing.Title) ?? "Unknown";
        Status = media.Status.Humanize(LetterCasing.Title);
        Content = media.Episodes.HasValue
            ? media.Episodes.Value.ToString()
            : media.Chapters.HasValue
                ? media.Chapters.Value.ToString()
                : "Unknown";
        ContentLabel = media.Type == MediaType.Anime ? "Episodes" : "Chapters";
        StartDate = media.StartDate.ToDateTime()?.ToShortDateString() ?? "Unknown";
        EndDate = media.EndDate.ToDateTime()?.ToShortDateString() ?? "Unknown";
        Popularity = media.Popularity.ToString();
        Score = media.MeanScore.HasValue ? media.MeanScore.Value + "%" : "Unknown";
        Favorites = media.Favorites.ToString();
        Genres = media.Genres;
        var tags = await _client.GetMediaTagsAsync(id);
        Tags = tags.Select(item => item.Name).ToArray();
        IsLoading = false;
    }

    [ICommand]
    private Task Play()
    {
        return MauiHelper.NavigateTo(
            typeof(SearchProviderPage),
            new Dictionary<string, object>
            {
                { "query", Title }
            }
        );
    }

    [ICommand]
    private async Task Track()
    {
        await Toast.Make("This feature is not implemented yet!").Show(); // TODO: implement feature
    }

}