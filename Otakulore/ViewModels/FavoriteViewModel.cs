using System.Collections.ObjectModel;
using Otakulore.Models;

namespace Otakulore.ViewModels;

public class FavoriteViewModel : BaseViewModel
{

    private bool _hasFinishedLoadingAnime;
    private bool _hasFinishedLoadingManga;
    private ObservableCollection<MediaItemModel> _animeFavoriteList = new();
    private ObservableCollection<MediaItemModel> _mangaFavoriteList = new();

    public bool HasFinishedLoadingAnime
    {
        get => _hasFinishedLoadingAnime;
        set => UpdateProperty(ref _hasFinishedLoadingAnime, value);
    }

    public bool HasFinishedLoadingManga
    {
        get => _hasFinishedLoadingManga;
        set => UpdateProperty(ref _hasFinishedLoadingManga, value);
    }

    public ObservableCollection<MediaItemModel> AnimeFavoriteList
    {
        get => _animeFavoriteList;
        set => UpdateProperty(ref _animeFavoriteList, value);
    }

    public ObservableCollection<MediaItemModel> MangaFavoriteList
    {
        get => _mangaFavoriteList;
        set => UpdateProperty(ref _mangaFavoriteList, value);
    }

}