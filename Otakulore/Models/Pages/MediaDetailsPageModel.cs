﻿using System.Collections.ObjectModel;
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

    [ObservableProperty] private AccumulableCollection<CharacterItemModel> _characterItems = new();
    [ObservableProperty] private ObservableCollection<MediaItemModel> _relationItems = new();

    protected override void Initialize(object? args = null)
    {
        if (args is not int id)
            return;
        _id = id;
        LoadOverviewDataCommand.Execute(null);
        LoadCharactersDataCommand.Execute(null);
        LoadRelationsDataCommand.Execute(null);
    }

    [RelayCommand]
    private async Task LoadOverviewData()
    {
        var media = await DataService.Instance.Client.GetMediaAsync(_id);
        ImageUrl = media.Cover.ExtraLargeImageUrl;
        Title = media.Title.PreferredTitle;
        Subtitle = media.Type == MediaType.Anime && media.Season.HasValue && media.SeasonYear.HasValue
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
    }

    [RelayCommand]
    private Task LoadCharactersData()
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
    private async Task LoadRelationsData()
    {
        var result = await DataService.Instance.Client.GetMediaRelationsAsync(_id);
        foreach (var item in result)
            RelationItems.Add(MediaItemModel.Map(item));
    }

    [RelayCommand]
    private Task Play()
    {
        return MauiHelper.Navigate(typeof(SearchProviderPage), Title);
    }

    [RelayCommand]
    private Task Track()
    {
        return Task.CompletedTask;
    }

}