using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Navigation;
using Otakulore.Core;
using Otakulore.ViewModels;

namespace Otakulore.Views;

public partial class DetailsPage
{

    private readonly BackgroundWorker _detailsWorker;

    private DetailsViewModel ViewModel => (DetailsViewModel)DataContext;

    public DetailsPage()
    {
        _detailsWorker = new BackgroundWorker();
        _detailsWorker.WorkerSupportsCancellation = true;
        _detailsWorker.DoWork += async (_, args) =>
        {
            if (args.Argument is not KeyValuePair<MediaType, long>(var type, var id))
                return;
            var viewModel = new DetailsViewModel { Type = type, Id = id };
            if (type == MediaType.Anime)
            {
                try
                {
                    var details = await App.Jikan.GetAnime(id);
                    viewModel.ImageUrl = details.ImageURL;
                    viewModel.Title = details.Title;
                    viewModel.Subtitle = details.Premiered;
                    viewModel.Synopsis = details.Synopsis;
                    viewModel.Background = details.Background;
                    viewModel.Format = details.Type;
                    viewModel.Status = details.Status;
                    viewModel.Contents = details.Episodes.HasValue ? details.Episodes.Value.ToString() : "Unknown";
                    viewModel.IsFavorite = App.Settings.AnimeFavorites.Contains(id);
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
                    var details = await App.Jikan.GetManga(id);
                    viewModel.ImageUrl = details.ImageURL;
                    viewModel.Title = details.Title;
                    viewModel.Subtitle = details.Published.From.HasValue ? details.Published.From.Value.Year.ToString() : "????";
                    viewModel.Synopsis = details.Synopsis;
                    viewModel.Background = details.Background;
                    viewModel.Format = details.Type;
                    viewModel.Status = details.Status;
                    viewModel.Contents = details.Chapters.HasValue ? details.Chapters.Value.ToString() : "Unknown";
                    viewModel.IsFavorite = App.Settings.MangaFavorites.Contains(id);
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
        _detailsWorker.RunWorkerAsync(data);
    }

    protected override void OnNavigatedFrom(NavigationEventArgs args)
    {
        _detailsWorker.CancelAsync();
    }

    private void OnFavorite(object sender, RoutedEventArgs args)
    {
        if (ViewModel.IsFavorite)
        {
            switch (ViewModel.Type)
            {
                case MediaType.Anime:
                {
                    if (!App.Settings.AnimeFavorites.Contains((long)ViewModel.Id))
                        App.Settings.AnimeFavorites.Add((long)ViewModel.Id);
                    break;
                }
                case MediaType.Manga:
                {
                    if (!App.Settings.MangaFavorites.Contains((long)ViewModel.Id))
                        App.Settings.MangaFavorites.Add((long)ViewModel.Id);
                    break;
                }
            }
        }
        else
        {
            switch (ViewModel.Type)
            {
                case MediaType.Anime:
                {
                    if (App.Settings.AnimeFavorites.Contains((long)ViewModel.Id))
                        App.Settings.AnimeFavorites.Remove((long)ViewModel.Id);
                    break;
                }
                case MediaType.Manga:
                {
                    if (App.Settings.MangaFavorites.Contains((long)ViewModel.Id))
                        App.Settings.MangaFavorites.Remove((long)ViewModel.Id);
                    break;
                }
            }
        }
    }

}