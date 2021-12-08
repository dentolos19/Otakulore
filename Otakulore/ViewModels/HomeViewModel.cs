using System.Collections.ObjectModel;
using Otakulore.Models;

namespace Otakulore.ViewModels;

public class HomeViewModel : BaseViewModel
{

    private bool _hasLoaded;

    public bool HasLoaded
    {
        get => _hasLoaded;
        set => UpdateProperty(ref _hasLoaded, value);
    }

    public ObservableCollection<MediaItemModel> TopAnimes { get; } = new();
    public ObservableCollection<MediaItemModel> TopMangas { get; } = new();

}