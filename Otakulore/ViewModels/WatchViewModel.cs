using Humanizer;
using Otakulore.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Otakulore.ViewModels
{

    public class WatchViewModel : INotifyPropertyChanged
    {

        private bool _isLoading = true;

        public string Title { get; set; }
        public ObservableCollection<EpisodeItemModel> Episodes { get; set; } = new ObservableCollection<EpisodeItemModel>();

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsLoading)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public static WatchViewModel CreateViewModel(WatchItemModel model)
        {
            return new WatchViewModel
            {
                Title = model.Title + " | " + model.Provider.Humanize()
            };
        }

    }

}