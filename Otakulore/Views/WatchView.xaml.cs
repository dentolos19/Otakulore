using Otakulore.Core.Services.Anime;
using Otakulore.Models;
using Otakulore.ViewModels;
using System;
using System.Linq;
using System.Threading;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Otakulore.Core.Services.Kitsu;

namespace Otakulore.Views
{

    public sealed partial class WatchView
    {

        private IAnimeProvider _provider;

        public WatchView()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs args)
        {
            if (!(args.Parameter is WatchItemModel model))
                return;
            _provider = model.Provider;
            DataContext = WatchViewModel.CreateViewModel(model);
            KitsuData<KitsuEpisodeAttributes>[] episodeInfoList = null;
            if (App.Settings.ShowEpisodeInfo)
                episodeInfoList = await KitsuApi.GetAnimeEpisodesAsync(model.Id);
            ThreadPool.QueueUserWorkItem(async _ =>
            {
                var episodeList = model.Provider.ScrapeAnimeEpisodes(model.EpisodesUrl);
                if (episodeList != null && episodeList.Length > 0)
                {
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        foreach (var episode in episodeList)
                        {
                            KitsuEpisodeAttributes episodeAttributes = null;
                            if (episodeInfoList != null)
                                episodeAttributes = episodeInfoList.FirstOrDefault(data => data.Attributes.Episode == episode.EpisodeNumber)?.Attributes;
                            ((WatchViewModel)DataContext).Episodes.Add(EpisodeItemModel.CreateModel(episode, episodeAttributes));
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
                var episodeSource = _provider.ScrapeEpisodeSource(model.WatchUrl);
                if (!string.IsNullOrEmpty(episodeSource))
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => MediaElement.SetMediaPlayer(new MediaPlayer { Source = MediaSource.CreateFromUri(new Uri(episodeSource)) }));
                else
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () => await new MessageDialog("Unable to scrape episodes with the current provider.").ShowAsync());
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => ((WatchViewModel)DataContext).IsLoading = false);
            });
        }

    }

}