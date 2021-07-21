using Otakulore.Core.Services.Anime;
using Otakulore.Models;
using Otakulore.ViewModels;
using System;
using System.Threading;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Otakulore.Views
{

    public sealed partial class AnimePlayerView
    {

        private IAnimeProvider _provider;

        public AnimePlayerView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            if (!(args.Parameter is ChannelItemModel model))
                return;
            _provider = model.AnimeProvider;
            DataContext = PlayerReaderViewModel.CreateViewModel(model);
            ThreadPool.QueueUserWorkItem(async _ =>
            {
                var episodeList = _provider.ScrapeAnimeEpisodes(model.Url);
                if (episodeList != null && episodeList.Length > 0)
                {
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        foreach (var episode in episodeList)
                        {
                            ((PlayerReaderViewModel)DataContext).EpisodeChapterList.Add(EpisodeChapterItemModel.CreateModel(episode));
                        }
                    });
                }
                else
                {
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                    {
                        if (Frame.CanGoBack)
                            Frame.GoBack();
                        await new MessageDialog("Unable to scrape episodes with the current provider.").ShowAsync();
                    });
                    return;
                }
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    ((PlayerReaderViewModel)DataContext).IsLoading = false;
                    EpisodeList.SelectedIndex = 0;
                });
            });
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs args)
        {
            if (MediaElement.MediaPlayer.PlaybackSession.CanPause)
                MediaElement.MediaPlayer.Pause();
        }

        private void EpisodeChanged(object sender, SelectionChangedEventArgs args)
        {
            if (!(EpisodeList.SelectedItem is EpisodeChapterItemModel model))
                return;
            ((PlayerReaderViewModel)DataContext).IsLoading = true;
            ThreadPool.QueueUserWorkItem(async _ =>
            {
                var episodeSource = _provider.ScrapeEpisodeSource(model.Url);
                if (!string.IsNullOrEmpty(episodeSource))
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => MediaElement.SetMediaPlayer(new MediaPlayer { Source = MediaSource.CreateFromUri(new Uri(episodeSource)) }));
                else
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () => await new MessageDialog("Unable to scrape episodes with the current provider.").ShowAsync());
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => ((PlayerReaderViewModel)DataContext).IsLoading = false);
            });
        }

    }

}