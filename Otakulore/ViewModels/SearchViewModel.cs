using Otakulore.Models;
using System.Collections.ObjectModel;

namespace Otakulore.ViewModels;

public class SearchViewModel : BaseViewModel
{

    private bool _hasSearchFinished;
    private ObservableCollection<MediaItemModel> _searchItems = new();

    public bool HasSearchFinished
    {
        get => _hasSearchFinished;
        set => UpdateProperty(ref _hasSearchFinished, value);
    }

    public ObservableCollection<MediaItemModel> SearchItems
    {
        get => _searchItems;
        set => UpdateProperty(ref _searchItems, value);
    }

}