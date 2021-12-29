using JikanDotNet;
using Otakulore.Core;
using Otakulore.Models;
using Otakulore.ViewModels;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Otakulore.Views;

public partial class DetailsPage
{

    private Anime? _anime;
    private Manga? _manga;

    private DetailsViewModel ViewModel => (DetailsViewModel)DataContext;

    public DetailsPage(MediaType type, long id)
    {
        var detailsLoader = new BackgroundWorker();
        detailsLoader.DoWork += async (_, _) =>
        {
            try
            {
                var viewModel = new DetailsViewModel
                {
                    Type = type,
                    Id = id,
                    IsFavorite = App.Settings.Favorites.FirstOrDefault(item => item.Type == type && item.Id == id) != null
                };
                switch (type)
                {
                    case MediaType.Anime:
                        _anime = await App.Jikan.GetAnime(id);
                        viewModel.ImageUrl = new Uri(_anime.ImageURL);
                        viewModel.Title = _anime.Title;
                        viewModel.Subtitle = _anime.Premiered;
                        viewModel.Synopsis = _anime.Synopsis;
                        viewModel.Background = _anime.Background;
                        viewModel.Format = _anime.Type;
                        viewModel.Status = _anime.Status;
                        viewModel.Contents = _anime.Episodes.HasValue ? _anime.Episodes.Value.ToString() : "Unknown";
                        break;
                    case MediaType.Manga:
                        _manga = await App.Jikan.GetManga(id);
                        viewModel.ImageUrl = new Uri(_manga.ImageURL);
                        viewModel.Title = _manga.Title;
                        viewModel.Subtitle = _manga.Published.From.HasValue ? _manga.Published.From.Value.Year.ToString() : "Unknown Year";
                        viewModel.Synopsis = _manga.Synopsis;
                        viewModel.Background = _manga.Background;
                        viewModel.Format = _manga.Type;
                        viewModel.Status = _manga.Status;
                        viewModel.Contents = _manga.Chapters.HasValue ? _manga.Chapters.Value.ToString() : "Unknown";
                        break;
                }
                Dispatcher.Invoke(() => DataContext = viewModel);
            }
            catch
            {
                // TODO: notify user of exception and send them back
            }
        };
        InitializeComponent();
        switch (type)
        {
            case MediaType.Anime:
                foreach (var provider in App.Providers)
                    if (provider is IAnimeProvider)
                        ProviderSelection.Items.Add(new ProviderItemModel(provider));
                break;
            case MediaType.Manga:
                foreach (var provider in App.Providers)
                    if (provider is IMangaProvider)
                        ProviderSelection.Items.Add(new ProviderItemModel(provider));
                break;
        }
        ProviderSelection.SelectedIndex = 0;
        detailsLoader.RunWorkerAsync();
    }

    private void OnFavorite(object sender, RoutedEventArgs args)
    {
        if (!ViewModel.IsFavorite)
        {
            var item = App.Settings.Favorites.FirstOrDefault(item => item.Type == ViewModel.Type && item.Id == ViewModel.Id);
            if (item != null)
                return;
            switch (ViewModel.Type)
            {
                case MediaType.Anime:
                    if (_anime != null)
                        App.Settings.Favorites.Add(MediaItemModel.Create(_anime));
                    break;
                case MediaType.Manga:
                    if (_manga != null)
                        App.Settings.Favorites.Add(MediaItemModel.Create(_manga));
                    break;
            }
            ViewModel.IsFavorite = true;
        }
        else
        {
            var item = App.Settings.Favorites.FirstOrDefault(item => item.Type == ViewModel.Type && item.Id == ViewModel.Id);
            if (item != null)
                App.Settings.Favorites.Remove(item);
            ViewModel.IsFavorite = false;
        }
    }

    private void OnPlay(object sender, RoutedEventArgs args)
    {
        if (ProviderSelection.SelectedItem is ProviderItemModel provider)
            NavigationService.Navigate(new SearchProviderPage(provider.Provider, ViewModel.Title));
    }

}