using System.Collections.ObjectModel;
using AniListNet;
using AniListNet.Objects;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Humanizer;
using Otakulore.Helpers;
using Otakulore.Pages;
using Otakulore.Services;

namespace Otakulore.Models;

[TransientService]
public partial class MediaDetailsPageModel : BasePageModel
{

    private int _id;

    [ObservableProperty] private Uri _imageUrl;
    [ObservableProperty] private string _title;
    [ObservableProperty] private string _subtitle;
    [ObservableProperty] private string _popularity;
    [ObservableProperty] private string _score;
    [ObservableProperty] private string _favorites;
    [ObservableProperty] private string _description;
    [ObservableProperty] private string _format;
    [ObservableProperty] private string _status;
    [ObservableProperty] private string _content;
    [ObservableProperty] private string _contentLabel;
    [ObservableProperty] private string _startDate;
    [ObservableProperty] private string _endDate;

    [ObservableProperty] private string[] _synonyms;
    [ObservableProperty] private string[] _genres;
    [ObservableProperty] private string[] _tags;

    [ObservableProperty] private AccumulableCollection<CharacterItemModel> _characterItems = new();
    [ObservableProperty] private ObservableCollection<MediaItemModel> _relationItems = new();

    protected override void Initialize(object? args = null)
    {
        if (args is not int id)
            return;
        _id = id;
        RefreshOverviewDataCommand.Execute(null);
        RefreshCharactersDataCommand.Execute(null);
        RefreshRelationsDataCommand.Execute(null);
    }

    [RelayCommand]
    private Task Play()
    {
        return MauiHelper.Navigate(typeof(SearchProviderPage), Title);
    }

    [RelayCommand]
    private async Task Track()
    {
        if (!DataService.Instance.Client.IsAuthenticated)
        {
            var wantLogin = await ParentPage.DisplayAlert(
                "Otakulore",
                "You need an AniList account to use this feature.",
                "Login",
                "Close"
            );
            if (wantLogin)
                await MauiHelper.Navigate(typeof(LoginPage));
            return;
        }
        await MauiHelper.Navigate(typeof(TrackPage), _id);
    }

    [RelayCommand]
    private async Task RefreshOverviewData()
    {
        var media = await DataService.Instance.Client.GetMediaAsync(_id);
        ImageUrl = media.Cover.ExtraLargeImageUrl;
        Title = media.Title.PreferredTitle;
        Subtitle = media is { Type: MediaType.Anime, Season: { }, SeasonYear: { } }
            ? $"{media.Season} {media.SeasonYear}"
            : media.StartDate.Year.HasValue
                ? media.StartDate.Year.Value.ToString()
                : media.Type.ToString();
        Popularity = media.Popularity.ToString();
        Score = media.MeanScore.HasValue ? media.MeanScore.Value + "%" : "Unknown";
        Favorites = media.Favorites.ToString();
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
        Synonyms = media.Synonyms;
        Genres = media.Genres;
        Tags = (await DataService.Instance.Client.GetMediaTagsAsync(_id)).Select(item => item.Name).ToArray();
    }

    [RelayCommand]
    private Task RefreshCharactersData()
    {
        CharacterItems = new AccumulableCollection<CharacterItemModel>();
        CharacterItems.AccumulationFunc += async index =>
        {
            var result = await DataService.Instance.Client.GetMediaCharactersAsync(_id, new AniPaginationOptions(index));
            return (
                result.HasNextPage,
                result.Data.Select(CharacterItemModel.Map).ToList()
            );
        };
        CharacterItems.AccumulateCommand.Execute(null);
        return Task.CompletedTask;
    }

    [RelayCommand]
    private async Task RefreshRelationsData()
    {
        var result = await DataService.Instance.Client.GetMediaRelationsAsync(_id);
        foreach (var item in result)
            RelationItems.Add(MediaItemModel.Map(item));
    }

}