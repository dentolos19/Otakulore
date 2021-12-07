using System.Collections.ObjectModel;
using Otakulore.Models;

namespace Otakulore.ViewModels;

public class AnimePlayerViewModel : BaseViewModel
{

    private bool _hasLoaded;
    private string _title;
    private string _videoSource;

    public bool HasLoaded
    {
        get => _hasLoaded;
        set => UpdateProperty(ref _hasLoaded, value);
    }

    public string Title
    {
        get => _title;
        set => UpdateProperty(ref _title, value);
    }

    public string VideoSource
    {
        get => _videoSource;
        set => UpdateProperty(ref _videoSource, value);
    }

    public ObservableCollection<ContentItemModel> Episodes { get; } = new();

}