using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Otakulore.Core;
using Otakulore.Core.Services.Scrapers;
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
                    var sourceUrl = FourAnimeScraper.ScrapeVideoSource(url);
                    if (string.IsNullOrEmpty(sourceUrl))
                    {
                        AdonisMessageBox.Show("Sorry, unable to find video source.", "Otakulore");
                        return;
                    }
                    MediaPlayer.Source = new Uri(sourceUrl);
                    break;
                }
            }
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += UpdateStatus;
            _timer.Start();
        }

        private void UpdateStatus(object sender, EventArgs args)
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

        private void PlayMedia(object sender, ExecutedRoutedEventArgs args)
        {
            MediaPlayer.Play(); // TODO: use commands instead
        }

        private void PauseMedia(object sender, ExecutedRoutedEventArgs args)
        {
            MediaPlayer.Pause(); // TODO: use commands instead
        }

        private void SeekPlayback(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            // TODO: add playback seeking
        }

    }

}