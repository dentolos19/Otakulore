using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Otakulore.Models;

public class BaseViewModel : INotifyPropertyChanged
{

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void UpdateProperty([CallerMemberName] string? propertyName = null)
    {
        OnPropertyChanged(propertyName);
    }

    protected void UpdateProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (Equals(field, value))
            return;
        field = value;
        UpdateProperty(propertyName);
    }

    private void OnPropertyChanged(string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}