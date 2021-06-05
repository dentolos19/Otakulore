using System.ComponentModel;
using System.Reflection;
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

        private void InvokeAllPropertiesChanged()
        {
            var properties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);
            foreach (var property in properties)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property.Name));
        }

        public void CollapseEpisodeLoading()
        {
            EpisodeLoadingPanelVisibility = Visibility.Collapsed;
            EpisodeLoadingTextVisibility = Visibility.Collapsed;
            EpisodeLoadingIndicatorVisibility = Visibility.Collapsed;
            InvokeAllPropertiesChanged();
        }

        public void NotifyEpisodeLoading(string message)
        {
            EpisodeLoadingText = message;
            EpisodeLoadingPanelVisibility = Visibility.Visible;
            EpisodeLoadingTextVisibility = Visibility.Visible;
            EpisodeLoadingIndicatorVisibility = Visibility.Collapsed;
            InvokeAllPropertiesChanged();
        }

        public void ShowEpisodeLoading()
        {
            EpisodeLoadingPanelVisibility = Visibility.Visible;
            EpisodeLoadingTextVisibility = Visibility.Collapsed;
            EpisodeLoadingIndicatorVisibility = Visibility.Visible;
            InvokeAllPropertiesChanged();
        }

        public void CollapseVideoLoading()
        {
            VideoLoadingPanelVisibility = Visibility.Collapsed;
            VideoLoadingTextVisibility = Visibility.Collapsed;
            VideoLoadingIndicatorVisibility = Visibility.Collapsed;
            InvokeAllPropertiesChanged();
        }

        public void NotifyVideoLoading(string message)
        {
            VideoLoadingText = message;
            VideoLoadingPanelVisibility = Visibility.Visible;
            VideoLoadingTextVisibility = Visibility.Visible;
            VideoLoadingIndicatorVisibility = Visibility.Collapsed;
            InvokeAllPropertiesChanged();
        }

        public void ShowVideoLoading()
        {
            VideoLoadingPanelVisibility = Visibility.Visible;
            VideoLoadingTextVisibility = Visibility.Collapsed;
            VideoLoadingIndicatorVisibility = Visibility.Visible;
            InvokeAllPropertiesChanged();
        }

    }

}