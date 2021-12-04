using Otakulore.Models;
using System.Collections.ObjectModel;

namespace Otakulore.ViewModels;

public class SearchViewModel : BaseViewModel
{

    private bool _hasFinishedSearching;
    private ObservableCollection<MediaItemModel> _searchItems = new();

    public bool HasFinishedSearching
    {
        get => _hasFinishedSearching;
        set => UpdateProperty(ref _hasFinishedSearching, value);
    }

    public ObservableCollection<MediaItemModel> SearchItems
    {
        get => _searchItems;
        set => UpdateProperty(ref _searchItems, value);
    }

}