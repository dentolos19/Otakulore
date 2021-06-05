using System.ComponentModel;
using System.Windows;

namespace Otakulore.ViewModels
{

    public class StreamViewModel : INotifyPropertyChanged
    {

        public string? EpisodeLoadingText { get; private set; }
        public Visibility EpisodeLoadingPanelVisibility { get; private set; } = Visibility.Visible;
        public Visibility EpisodeLoadingTextVisibility { get; private set; } = Visibility.Collapsed;
        public Visibility EpisodeLoadingIndicatorVisibility { get; private set; } = Visibility.Visible;

        public string? VideoLoadingText { get; private set; }
        public Visibility VideoLoadingPanelVisibility { get; private set; } = Visibility.Collapsed;
        public Visibility VideoLoadingTextVisibility { get; private set; } = Visibility.Collapsed;
        public Visibility VideoLoadingIndicatorVisibility { get; private set; } = Visibility.Collapsed;

        public event PropertyChangedEventHandler? PropertyChanged;

        public void CollapseEpisodeLoading()
        {
            EpisodeLoadingPanelVisibility = Visibility.Collapsed;
            EpisodeLoadingTextVisibility = Visibility.Collapsed;
            EpisodeLoadingIndicatorVisibility = Visibility.Collapsed;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EpisodeLoadingPanelVisibility)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EpisodeLoadingTextVisibility)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EpisodeLoadingIndicatorVisibility)));
        }

        public void NotifyEpisodeLoading(string message)
        {
            EpisodeLoadingText = message;
            EpisodeLoadingPanelVisibility = Visibility.Visible;
            EpisodeLoadingTextVisibility = Visibility.Visible;
            EpisodeLoadingIndicatorVisibility = Visibility.Collapsed;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EpisodeLoadingText)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EpisodeLoadingPanelVisibility)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EpisodeLoadingTextVisibility)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EpisodeLoadingIndicatorVisibility)));
        }

        public void ShowEpisodeLoading()
        {
            EpisodeLoadingPanelVisibility = Visibility.Visible;
            EpisodeLoadingTextVisibility = Visibility.Collapsed;
            EpisodeLoadingIndicatorVisibility = Visibility.Visible;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EpisodeLoadingPanelVisibility)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EpisodeLoadingTextVisibility)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EpisodeLoadingIndicatorVisibility)));
        }

        public void CollapseVideoLoading()
        {
            VideoLoadingPanelVisibility = Visibility.Collapsed;
            VideoLoadingTextVisibility = Visibility.Collapsed;
            VideoLoadingIndicatorVisibility = Visibility.Collapsed;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VideoLoadingPanelVisibility)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VideoLoadingTextVisibility)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VideoLoadingIndicatorVisibility)));
        }

        public void NotifyVideoLoading(string message)
        {
            VideoLoadingText = message;
            VideoLoadingPanelVisibility = Visibility.Visible;
            VideoLoadingTextVisibility = Visibility.Visible;
            VideoLoadingIndicatorVisibility = Visibility.Collapsed;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VideoLoadingText)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VideoLoadingPanelVisibility)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VideoLoadingTextVisibility)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VideoLoadingIndicatorVisibility)));
        }

        public void ShowVideoLoading()
        {
            VideoLoadingPanelVisibility = Visibility.Visible;
            VideoLoadingTextVisibility = Visibility.Collapsed;
            VideoLoadingIndicatorVisibility = Visibility.Visible;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VideoLoadingPanelVisibility)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VideoLoadingTextVisibility)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VideoLoadingIndicatorVisibility)));
        }

    }

}