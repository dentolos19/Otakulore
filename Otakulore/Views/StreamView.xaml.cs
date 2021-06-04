using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;
using Humanizer;
using Otakulore.Core.Anime;
using Otakulore.Core.Anime.Providers;
using Otakulore.Models;
using AdonisMessageBox = AdonisUI.Controls.MessageBox;

namespace Otakulore.Views
{

    public partial class StreamDetailsView
    {

        private readonly string _title;
        private readonly string _url;
        private readonly AnimeProvider _service;
        private readonly BackgroundWorker _worker;
        private readonly DispatcherTimer _timer;
        
        private bool _isVideoSeeking;
        private DateTime _videoStartTime;

        public StreamDetailsView(string title, string url, AnimeProvider service)
        {
            InitializeComponent();
            MediaControl.Header = $"{title} | {service.Humanize()} | Select a episode to stream from the list on the left.";
            _title = title;
            _url = url;
            _service = service;
            _worker = new BackgroundWorker();
            _worker.DoWork += LoadEpisodes;
            _worker.RunWorkerAsync();
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += UpdateMediaProgress;
            _timer.Start();
        }

        private void LoadEpisodes(object? sender, DoWorkEventArgs args)
        {
            switch (_service)
            {
                case AnimeProvider.FourAnime:
                {
                    var episodes = FourAnimeProvider.ScrapeEpisodes(_url);
                    if (episodes == null)
                    {
                        Dispatcher.BeginInvoke(() => EpisodeLoadingIndicator.Text = "Failed to scrape content.");
                        return;
                    }
                    foreach (var episode in episodes)
                    {
                        Dispatcher.BeginInvoke(() => EpisodeList.Items.Add(new EpisodeItemModel
                        {
                            EpisodeName = "Episode " + (episode.EpisodeNumber.HasValue ? episode.EpisodeNumber : "Unknown"),
                            WatchUrl = episode.WatchUrl
                        }));
                    }
                    break;
                }
                case AnimeProvider.Gogoanime:
                {
                    var episodes = GogoanimeProvider.ScrapeEpisodes(_url);
                    if (episodes == null)
                    {
                        Dispatcher.BeginInvoke(() => EpisodeLoadingIndicator.Text = "Failed to scrape content.");
                        return;
                    }
                    foreach (var episode in episodes)
                    {
                        Dispatcher.BeginInvoke(() => EpisodeList.Items.Add(new EpisodeItemModel
                        {
                            EpisodeName = "Episode " + (episode.EpisodeNumber.HasValue ? episode.EpisodeNumber : "Unknown"),
                            WatchUrl = episode.WatchUrl
                        }));
                    }
                    break;
                }
            }
            Dispatcher.BeginInvoke(() => EpisodeLoadingPanel.Visibility = Visibility.Collapsed);
        }

        private void PlayEpisode(object sender, MouseButtonEventArgs args)
        {
            if (EpisodeList.SelectedItem is not EpisodeItemModel model)
                return;
            ThreadPool.QueueUserWorkItem(_ =>
            {
                string? sourceUrl = _service switch
                {
                    AnimeProvider.FourAnime => FourAnimeProvider.ScrapeVideoSource(model.WatchUrl),
                    AnimeProvider.Gogoanime => GogoanimeProvider.ScrapeVideoSource(model.WatchUrl),
                    _ => null
                };
                Dispatcher.BeginInvoke(() =>
                {
                    if (string.IsNullOrEmpty(sourceUrl))
                    {
                        AdonisMessageBox.Show("Sorry, the providers unable to find a working video source.", "Otakulore");
                        return;
                    }
                    MediaPlayer.Source = new Uri(sourceUrl);
                    MediaControl.Header = $"{_title} | {_service.Humanize()} | {model.EpisodeName}";
                    MediaPlayer.Play();
                    _videoStartTime = DateTime.Now;
                    App.RichPresence?.SetWatchingState(_title, model.EpisodeName);
                });
            });
        }

        private void UpdateMediaProgress(object? sender, EventArgs args)
        {
            if ((!MediaPlayer.HasVideo || _isVideoSeeking) && !MediaPlayer.NaturalDuration.HasTimeSpan)
                return;
            VideoProgress.Maximum = MediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
            VideoProgress.Value = MediaPlayer.Position.TotalSeconds;
            VideoProgressText.Text = MediaPlayer.Position.ToString(@"hh\:mm\:ss");
            DurationText.Text = MediaPlayer.NaturalDuration.TimeSpan.ToString(@"hh\:mm\:ss");
        }

        private void PlayVideo(object sender, ExecutedRoutedEventArgs args)
        {
            MediaPlayer.Play();
        }

        private void PauseVideo(object sender, ExecutedRoutedEventArgs args)
        {
            MediaPlayer.Pause();
        }
        
        private void CanInteractVideo(object sender, CanExecuteRoutedEventArgs args)
        {
            if (!IsInitialized)
                return;
            args.CanExecute = MediaPlayer.HasVideo;
        }

        private void VideoSeeking(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            if (_isVideoSeeking)
                VideoProgressText.Text = TimeSpan.FromSeconds(VideoProgress.Value).ToString(@"hh\:mm\:ss");
        }

        private void VideoSeekingStarted(object sender, DragStartedEventArgs args)
        {
            MediaPlayer.Pause();
            _isVideoSeeking = true;
        }

        private void VideoSeekingCompleted(object sender, DragCompletedEventArgs args)
        {
            _isVideoSeeking = false;
            MediaPlayer.Position = TimeSpan.FromSeconds(VideoProgress.Value);
            MediaPlayer.Play();
        }

        private void StopVideo(object sender, RoutedEventArgs args)
        {
            MediaPlayer.Pause();
            App.RichPresence?.SetInitialState();
        }

    }

}