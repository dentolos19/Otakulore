using Otakulore.Models;
using System.Collections.ObjectModel;

namespace Otakulore.ViewModels;

public class SearchViewModel : BaseViewModel
{

    private bool _hasNoSearchResult;
    private ObservableCollection<SearchItemModel> _searchItems = new();

    public bool HasNoSearchResult
    {
        get => _hasNoSearchResult;
        set => UpdateProperty(ref _hasNoSearchResult, value);
    }

    public ObservableCollection<SearchItemModel> SearchItems
    {
        get => _searchItems;
        set => UpdateProperty(ref _searchItems, value);
    }

}