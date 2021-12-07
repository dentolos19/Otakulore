using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Otakulore.Core;
using Otakulore.Models;
using Otakulore.Services;
using Otakulore.ViewModels;

namespace Otakulore.Views;

public partial class AnimePlayerPage
{

    private readonly BackgroundWorker _contentLoader;
    private readonly BackgroundWorker _sourceLoader;

    private IAnimeProvider? _provider;

    private AnimePlayerViewModel ViewModel => (AnimePlayerViewModel)DataContext;

    public AnimePlayerPage()
    {
        _contentLoader = new BackgroundWorker();
        _sourceLoader = new BackgroundWorker { WorkerSupportsCancellation = true };
        _contentLoader.DoWork += (_, args) =>
        {
            if (args.Argument is not IMediaInfo info)
                return;
            try
            {
                var chapters = _provider.GetAnimeEpisodes(info);
                Dispatcher.Invoke(() =>
                {
                    foreach (var chapter in chapters)
                        ViewModel.Episodes.Add(ContentItemModel.Create(chapter));
                    EpisodeSelection.SelectedIndex = 0;
                    ViewModel.HasLoaded = true;
                });
            }
            catch (Exception exception)
            {
                Dispatcher.Invoke(async () => await Utilities.CreateExceptionDialog(exception, "The provider returned an exception!").ShowAsync());
            }
        };
        _sourceLoader.DoWork += (_, args) =>
        {
            if (args.Argument is not IMediaContent content)
                return;
            Dispatcher.Invoke(() => ViewModel.HasLoaded = false);
            try
            {
                var videoSource = _provider.GetAnimeEpisodeSource(content);
                Dispatcher.Invoke(() =>
                {
                    ViewModel.VideoSource = videoSource;
                    ViewModel.HasLoaded = true;
                });
            }
            catch (Exception exception)
            {
                Dispatcher.Invoke(async () => await Utilities.CreateExceptionDialog(exception, "The provider returned an exception!").ShowAsync());
            }
        };
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        if (args.ExtraData is not ObjectData data)
            return;
        ViewModel.Title = data.MediaInfo.Title;
        _provider = (IAnimeProvider)data.Provider;
        _contentLoader.RunWorkerAsync(data.MediaInfo);
    }

    private void OnEpisodeChanged(object sender, SelectionChangedEventArgs args)
    {
        if (EpisodeSelection.SelectedItem is not ContentItemModel content)
            return;
        _sourceLoader.CancelAsync();
        _sourceLoader.RunWorkerAsync(content.Content);
    }

    private void OnPlay(object sender, RoutedEventArgs args)
    {
        VideoPlayer.Play();
    }

    private void OnPause(object sender, RoutedEventArgs args)
    {
        VideoPlayer.Pause();
    }

}