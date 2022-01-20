using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Otakulore.Models;

public class BaseViewModel : INotifyPropertyChanged
{

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void UpdateProperty([CallerMemberName] string? name = null)
    {
        OnPropertyChanged(name);
    }

    protected void UpdateProperty<T>(ref T storage, T value, [CallerMemberName] string? name = null)
    {
        if (Equals(storage, value))
            return;
        storage = value;
        UpdateProperty(name);
    }

    private void OnPropertyChanged(string? name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

}