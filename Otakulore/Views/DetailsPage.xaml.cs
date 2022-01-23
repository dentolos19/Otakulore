using System;
using System.Collections.Generic;
using System.Linq;
using Humanizer;
using JikanDotNet;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.AniList;
using Otakulore.Core;
using Otakulore.Models;
using Otakulore.Views.Dialogs;

namespace Otakulore.Views;

public sealed partial class DetailsPage
{

    private Media _media;

    private DetailsViewModel ViewModel => (DetailsViewModel)DataContext;

    public DetailsPage()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        if (args.Parameter is not Media data)
            return;
        _media = data;
        DataContext = new DetailsViewModel
        {
            Id = _media.Id,
            ImageUrl = _media.Cover.ExtraLargeImageUrl,
            Title = _media.Title.Romaji,
            Subtitle = _media.StartDate.Year != null ? _media.StartDate.Year.ToString() : "Unknown Year",
            Description = _media.Description,
            Format = _media.Format.Humanize(),
            Status = _media.Status.Humanize(),
            Episodes = _media.Episodes?.ToString() ?? "Unknown Count",
            IsFavorite = App.Settings.Favorites.FirstOrDefault(item => item.Id == _media.Id) != null
        };
    }

    private async void OnPlayRequested(object sender, RoutedEventArgs args)
    {
        var selectDialog = new SelectProviderDialog(_media.Type);
        await selectDialog.ShowAsync();
        if (selectDialog.Result == null)
            return;
        var searchDialog = new SearchProviderDialog(selectDialog.Result, ViewModel.Title);
        await searchDialog.ShowAsync();
        if (searchDialog.Result != null)
            Frame.Navigate(typeof(CinemaPage), new KeyValuePair<IProvider, object>(selectDialog.Result, searchDialog.Result));
    }

    private async void OnTrackRequested(object sender, RoutedEventArgs args)
    {
        var dialog = new ManageTrackerDialog();
        await dialog.ShowAsync();
    }

    private void OnUpdateFavorite(object sender, RoutedEventArgs args)
    {
        var item = App.Settings.Favorites.FirstOrDefault(item => item.Id == _media.Id);
        if (ViewModel.IsFavorite)
        {
            if (item == null)
                App.Settings.Favorites.Add(_media);
        }
        else
        {
            if (item != null)
                App.Settings.Favorites.Remove(item);
        }
    }

}