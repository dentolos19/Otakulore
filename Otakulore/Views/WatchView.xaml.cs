using Otakulore.Core.Services.Anime;
using Otakulore.Core.Services.Anime.Providers;
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

    public sealed partial class WatchView
    {

        private AnimeProvider _provider;

        public WatchView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            if (!(args.Parameter is WatchItemModel model))
                return;
            _provider = model.Provider;
            DataContext = WatchViewModel.CreateViewModel(model);
            ThreadPool.QueueUserWorkItem(async _ =>
            {
                AnimeEpisode[] episodes = null;
                if (model.Provider == AnimeProvider.FourAnime)
                    episodes = FourAnimeProvider.ScrapeAnimeEpisodes(model.EpisodesUrl);
                else if (model.Provider == AnimeProvider.Gogoanime)
                    episodes = GogoanimeProvider.ScrapeAnimeEpisodes(model.EpisodesUrl);
                else if (model.Provider == AnimeProvider.AnimeKisa)
                    episodes = AnimeKisaProvider.ScrapeAnimeEpisodes(model.EpisodesUrl);
                if (episodes != null && episodes.Length > 0)
                {
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        foreach (var episode in episodes)
                        {
                            var episodeName = "Episode ";
                            if (episode.EpisodeNumber == null)
                                episodeName += "Unknown";
                            else
                                episodeName += episode.EpisodeNumber;
                            ((WatchViewModel)DataContext).EpisodeList.Add(new EpisodeItemModel
                            {
                                EpisodeName = episodeName,
                                WatchUrl = episode.WatchUrl
                            });
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
                    ((WatchViewModel)DataContext).IsLoading = false;
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
            if (!(EpisodeList.SelectedItem is EpisodeItemModel model))
                return;
            ((WatchViewModel)DataContext).IsLoading = true;
            ThreadPool.QueueUserWorkItem(async _ =>
            {
                string videoUrl = null;
                if (_provider == AnimeProvider.FourAnime)
                    videoUrl = FourAnimeProvider.ScrapeVideoUrl(model.WatchUrl);
                else if (_provider == AnimeProvider.Gogoanime)
                    videoUrl = GogoanimeProvider.ScrapeVideoUrl(model.WatchUrl);
                else if (_provider == AnimeProvider.AnimeKisa)
                    videoUrl = AnimeKisaProvider.ScrapeVideoUrl(model.WatchUrl);
                if (!string.IsNullOrEmpty(videoUrl))
                {
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => MediaElement.SetMediaPlayer(new MediaPlayer { Source = MediaSource.CreateFromUri(new Uri(videoUrl)) }));
                }
                else
                {
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () => await new MessageDialog("Unable to scrape episodes with the current provider.").ShowAsync());
                }
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => ((WatchViewModel)DataContext).IsLoading = false);
            });
        }

    }

}