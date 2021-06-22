using System.ComponentModel;

namespace Otakulore.ViewModels
{

    public class LoadingViewModel : INotifyPropertyChanged
    {

        private bool _isLoading = true;

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
        
    }

}