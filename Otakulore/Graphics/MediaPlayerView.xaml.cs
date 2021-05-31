using System;
using System.Windows;
using System.Windows.Threading;
using Otakulore.Core.AnimeServices;
using Otakulore.Core.AnimeServices.Scrapers;
using AdonisMessageBox = AdonisUI.Controls.MessageBox;

namespace Otakulore.Graphics
{

    public partial class MediaPlayerView
    {
        
        private DispatcherTimer _timer;

        public MediaPlayerView(string url, StreamingService service)
        {
            InitializeComponent();
            switch (service)
            {
                case StreamingService.FourAnime:
                {
                    var sourceUrl = FourAnimeScraper.ScrapeVideoSource(url);
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
                    // TODO: add gogoanime video streaming
                    break;
                }
            }
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += UpdateStatus;
            _timer.Start();
        }

        private void UpdateStatus(object? sender, EventArgs args)
        {
            if (MediaPlayer.IsBuffering)
            {
                StatusText.Text = "Buffering";
            }
            else
            {
                if (!MediaPlayer.NaturalDuration.HasTimeSpan)
                    return;
                PlaybackSlider.Minimum = 0;
                PlaybackSlider.Maximum = MediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                PlaybackSlider.Value = MediaPlayer.Position.TotalSeconds;
                StatusText.Text = MediaPlayer.Position.ToString(@"hh\:mm\:ss");
            }
        }

        private void PlayMedia(object sender, RoutedEventArgs args)
        {
            MediaPlayer.Play();
        }

        private void PauseMedia(object sender, RoutedEventArgs args)
        {
            MediaPlayer.Pause();
        }

    }

}