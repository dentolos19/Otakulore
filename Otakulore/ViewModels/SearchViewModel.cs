namespace Otakulore.ViewModels;

public class SearchViewModel : BaseViewModel
{

    private bool _hasAnimeSearched;
    private bool _hasMangaSearched;

    public bool HasAnimeSearched
    {
        get => _hasAnimeSearched;
        set => UpdateProperty(ref _hasAnimeSearched, value);
    }

    public bool HasMangaSearched
    {
        get => _hasMangaSearched;
        set => UpdateProperty(ref _hasMangaSearched, value);
    }

}