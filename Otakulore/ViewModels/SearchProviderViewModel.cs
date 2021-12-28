using System;

namespace Otakulore.ViewModels;

public class SearchProviderViewModel : BaseViewModel
{

    private Uri _imageUrl;
    private bool _hasSearched = true;

    public Uri ImageUrl
    {
        get => _imageUrl;
        set => UpdateProperty(ref _imageUrl, value);
    }

    public bool HasSearched
    {
        get => _hasSearched;
        set => UpdateProperty(ref _hasSearched, value);
    }

}