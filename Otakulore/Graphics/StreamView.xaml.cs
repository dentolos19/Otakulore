using System;
using System.ComponentModel;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;
using Otakulore.Core;
using Otakulore.Core.Services.Scrapers;
using Otakulore.Models;
using AdonisMessageBox = AdonisUI.Controls.MessageBox;

namespace Otakulore.Graphics
{

    public partial class StreamDetailsView
    {

        private readonly string _url;
        private readonly StreamingService _service;
        private readonly BackgroundWorker _worker;
        private readonly DispatcherTimer _timer;

        private bool _isVideoSeeking;

        public StreamDetailsView(string url, StreamingService service)
        {
            InitializeComponent();
            _url = url;
            _service = service;
            _worker = new BackgroundWorker();
            _worker.DoWork += LoadEpisodes;
            _worker.RunWorkerAsync();
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += UpdateStatus;
            _timer.Start();
        }

        private void LoadEpisodes(object sender, DoWorkEventArgs args)
        {
            switch (_service)
            {
                case StreamingService.FourAnime:
                {
                    var episodes = FourAnimeScraper.ScrapeEpisodes(_url);
                    if (episodes == null)
                        return; // TODO: add null indictator
                    foreach (var episode in episodes)
                    {
                        Dispatcher.BeginInvoke(() => EpisodeList.Items.Add(new EpisodeItemModel
                        {
                            EpisodeNumber = "Episode " + (episode.EpisodeNumber.HasValue ? episode.EpisodeNumber : "Unknown"),
                            WatchUrl = episode.WatchUrl
                        }));
                    }
                    break;
                }
                case StreamingService.Gogoanime:
                {
                    var episodes = GogoanimeScraper.ScrapeEpisodes(_url);
                    if (episodes == null)
                        return; // TODO: add null indictator
                    foreach (var episode in episodes)
                    {
                        Dispatcher.BeginInvoke(() => EpisodeList.Items.Add(new EpisodeItemModel
                        {
                            EpisodeNumber = "Episode " + (episode.EpisodeNumber.HasValue ? episode.EpisodeNumber : "Unknown"),
                            WatchUrl = episode.WatchUrl
                        }));
                    }
                    break;
                }
            }
        }

        private void PlayEpisode(object sender, MouseButtonEventArgs args)
        {
            if (EpisodeList.SelectedItem is not EpisodeItemModel model)
                return;
            switch (_service)
            {
                case StreamingService.FourAnime:
                {
                    var sourceUrl = FourAnimeScraper.ScrapeVideoSource(model.WatchUrl);
                    if (string.IsNullOrEmpty(sourceUrl))
                    {
                        AdonisMessageBox.Show("Sorry, unable to find video source.", "Otakulore");
                        return;
                    }
                    MediaPlayer.Source = new Uri(sourceUrl);
                    break;
                }
                case StreamingService.Gogoanime:
                {
                    var sourceUrl = FourAnimeScraper.ScrapeVideoSource(model.WatchUrl);
                    if (string.IsNullOrEmpty(sourceUrl))
                    {
                        AdonisMessageBox.Show("Sorry, unable to find video source.", "Otakulore");
                        return;
                    }
                    MediaPlayer.Source = new Uri(sourceUrl);
                    break;
                }
            }
        }

        private void UpdateStatus(object sender, EventArgs args)
        {
            if (!MediaPlayer.HasVideo || _isVideoSeeking)
                return;
            if (MediaPlayer.IsBuffering)
            {
                StatusText.Text = "Buffering";
            }
            else
            {
                if (!MediaPlayer.NaturalDuration.HasTimeSpan)
                    return;
                VideoProgress.Maximum = MediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                VideoProgress.Value = MediaPlayer.Position.TotalSeconds;
                StatusText.Text = MediaPlayer.Position.ToString(@"hh\:mm\:ss");
                DurationText.Text = MediaPlayer.NaturalDuration.TimeSpan.ToString(@"hh\:mm\:ss");
            }
        }

        private void PlayMedia(object sender, ExecutedRoutedEventArgs args)
        {
            MediaPlayer.Play();
        }

        private void PauseMedia(object sender, ExecutedRoutedEventArgs args)
        {
            MediaPlayer.Pause();
        }

        private void VideoSeekingStarted(object sender, DragStartedEventArgs args)
        {
            _isVideoSeeking = true;
        }

        private void VideoSeekingCompleted(object sender, DragCompletedEventArgs args)
        {
            _isVideoSeeking = false;
            MediaPlayer.Position = TimeSpan.FromSeconds(VideoProgress.Value);
        }

    }

}