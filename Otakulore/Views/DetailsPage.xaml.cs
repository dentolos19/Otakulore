using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;
using JikanDotNet;
using Otakulore.Core;
using Otakulore.Models;
using Otakulore.ViewModels;

namespace Otakulore.Views;

public partial class DetailsPage
{

    private readonly BackgroundWorker _detailsLoader;

    private Anime? _anime;
    private Manga? _manga;

    private DetailsViewModel ViewModel => (DetailsViewModel)DataContext;

    public DetailsPage()
    {
        _detailsLoader = new BackgroundWorker();
        _detailsLoader.DoWork += async (_, args) =>
        {
            if (args.Argument is not KeyValuePair<MediaType, long>(var type, var id))
                return;
            var viewModel = new DetailsViewModel { Type = type, Id = id };
            if (type == MediaType.Anime)
            {
                try
                {
                    _anime = await App.Jikan.GetAnime(id);
                    viewModel.ImageUrl = _anime.ImageURL;
                    viewModel.Title = _anime.Title;
                    viewModel.Subtitle = _anime.Premiered;
                    viewModel.Synopsis = _anime.Synopsis;
                    viewModel.Background = _anime.Background;
                    viewModel.Format = _anime.Type;
                    viewModel.Status = _anime.Status;
                    viewModel.Contents = _anime.Episodes.HasValue ? _anime.Episodes.Value.ToString() : "Unknown";
                    viewModel.IsFavorite = App.Settings.Favorites.FirstOrDefault(item => item.Type == type && item.Id == id) is not null;
                }
                catch
                {
                    // do nothing
                }
            }
            else if (type == MediaType.Manga)
            {
                try
                {
                    _manga = await App.Jikan.GetManga(id);
                    viewModel.ImageUrl = _manga.ImageURL;
                    viewModel.Title = _manga.Title;
                    viewModel.Subtitle = _manga.Published.From.HasValue ? _manga.Published.From.Value.Year.ToString() : "????";
                    viewModel.Synopsis = _manga.Synopsis;
                    viewModel.Background = _manga.Background;
                    viewModel.Format = _manga.Type;
                    viewModel.Status = _manga.Status;
                    viewModel.Contents = _manga.Chapters.HasValue ? _manga.Chapters.Value.ToString() : "Unknown";
                    viewModel.IsFavorite = App.Settings.Favorites.FirstOrDefault(item => item.Type == type && item.Id == id) is not null;
                }
                catch
                {
                    // do nothing
                }
            }
            Dispatcher.Invoke(() => DataContext = viewModel);
        };
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        if (args.ExtraData is not KeyValuePair<MediaType, long> data)
            return;
        _detailsLoader.RunWorkerAsync(data);
    }

    private void OnFavorite(object sender, RoutedEventArgs args)
    {
        if (ViewModel.IsFavorite)
        {
            var item = App.Settings.Favorites.FirstOrDefault(item => item.Type == ViewModel.Type && item.Id == ViewModel.Id);
            if (item is not null)
                return;
            switch (ViewModel.Type)
            {
                case MediaType.Anime:
                    App.Settings.Favorites.Add(MediaItemModel.Create(_anime));
                    break;
                case MediaType.Manga:
                    App.Settings.Favorites.Add(MediaItemModel.Create(_manga));
                    break;
            }
        }
        else
        {
            var item = App.Settings.Favorites.FirstOrDefault(item => item.Type == ViewModel.Type && item.Id == ViewModel.Id);
            if (item is not null)
                App.Settings.Favorites.Remove(item);
        }
    }

}