using CommunityToolkit.Mvvm.ComponentModel;

namespace Otakulore.Models;

public class BasePageModel : ObservableObject
{

    protected bool IsInitialized { get; set; }

    public void Activate(object? args = null)
    {
        if (IsInitialized)
            return;
        Initialize(args);
        IsInitialized = true;
    }

    protected virtual void Initialize(object? args = null) { }

}