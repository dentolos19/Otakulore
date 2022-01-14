using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Otakulore.ViewModels;

public class BaseViewModel : INotifyPropertyChanged
{

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void UpdateProperty<T>(ref T storage, T value, [CallerMemberName] string? name = null)
    {
        if (Equals(storage, value))
            return;
        storage = value;
        OnPropertyChanged(name);
    }

    private void OnPropertyChanged(string? name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

}