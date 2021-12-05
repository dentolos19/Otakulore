using Otakulore.Models;
using System.Collections.ObjectModel;

namespace Otakulore.ViewModels;

public class SearchViewModel : BaseViewModel
{

    private bool _hasSearchFinished;

    public bool HasSearchFinished
    {
        get => _hasSearchFinished;
        set => UpdateProperty(ref _hasSearchFinished, value);
    }

    public ObservableCollection<MediaItemModel> SearchResults { get; } = new();

}