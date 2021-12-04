using System.Collections.ObjectModel;
using Otakulore.Models;

namespace Otakulore.ViewModels;

public class FavoriteViewModel : BaseViewModel
{

    private ObservableCollection<MediaItemModel> _favoriteList = new();

    public ObservableCollection<MediaItemModel> FavoriteList
    {
        get => _favoriteList;
        set => UpdateProperty(ref _favoriteList, value);
    }

}