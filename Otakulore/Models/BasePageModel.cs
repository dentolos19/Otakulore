using CommunityToolkit.Mvvm.ComponentModel;

namespace Otakulore.Models;

public class BasePageModel : ObservableObject
{

    public virtual void Initialize(object? args = null) { }

}