using System;
using System.Collections.Generic;
using System.Linq;
using JikanDotNet;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.Core;
using Otakulore.Models;
using Otakulore.Views.Dialogs;

namespace Otakulore.Views;

public sealed partial class DetailsPage
{

    private Anime? _anime;
    private Manga? _manga;

    private DetailsViewModel ViewModel => (DetailsViewModel)DataContext;

    public DetailsPage()
    {
        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigationEventArgs args)
    {
        if (args.Parameter is not KeyValuePair<MediaType, long>(var type, var id))
            return;
        switch (type)
        {
            case MediaType.Anime:
            {
                _anime = await App.Jikan.GetAnime(id);
                DataContext = new DetailsViewModel
                {
                    Id = id,
                    ImageUrl = _anime.ImageURL,
                    Title = _anime.Title,
                    Subtitle = !string.IsNullOrEmpty(_anime.Premiered) ? _anime.Premiered : "Unknown Season/Year",
                    Synopsis = !string.IsNullOrEmpty(_anime.Synopsis) ? _anime.Synopsis : "No synopsis provided.",
                    Background = !string.IsNullOrEmpty(_anime.Background) ? _anime.Background : "No background information.",
                    Format = _anime.Type,
                    Status = _anime.Status,
                    Episodes = _anime.Episodes.HasValue ? _anime.Episodes.Value.ToString() : "Unknown",
                    IsFavorite = App.Settings.Favorites.FirstOrDefault(item => item.Id == id) != null
                };
                break;
            }
            case MediaType.Manga:
            {
                _manga = await App.Jikan.GetManga(id);
                DataContext = new DetailsViewModel
                {
                    Id = id,
                    ImageUrl = _manga.ImageURL,
                    Title = _manga.Title,
                    Subtitle = _manga.Published.From.HasValue ? _manga.Published.From.Value.Year.ToString() : "Unknown Year",
                    Synopsis = _manga.Synopsis,
                    Background = _manga.Background,
                    Format = _manga.Type,
                    Status = _manga.Status,
                    Episodes = _manga.Chapters.HasValue ? _manga.Chapters.Value.ToString() : "Unknown",
                    IsFavorite = App.Settings.Favorites.FirstOrDefault(item => item.Id == id) != null
                };
                break;
            }
        }
    }

    private async void OnPlayRequested(object sender, RoutedEventArgs args)
    {
        var type = MediaType.Anime;
        if (_manga != null)
            type = MediaType.Manga;
        var selectDialog = new SelectProviderDialog(type);
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
        if (_anime != null)
        {
            var item = App.Settings.Favorites.FirstOrDefault(item => item.Type == MediaType.Anime && item.Id == ViewModel.Id);
            if (ViewModel.IsFavorite)
            {
                if (item == null)
                    App.Settings.Favorites.Add(MediaItemModel.Create(_anime));
            }
            else
            {
                if (item != null)
                    App.Settings.Favorites.Remove(item);
            }
        }
        else if (_manga != null)
        {
            var item = App.Settings.Favorites.FirstOrDefault(item => item.Type == MediaType.Manga && item.Id == ViewModel.Id);
            if (ViewModel.IsFavorite)
            {
                if (item == null)
                    App.Settings.Favorites.Add(MediaItemModel.Create(_manga));
            }
            else
            {
                if (item != null)
                    App.Settings.Favorites.Remove(item);
            }
        }
    }

}