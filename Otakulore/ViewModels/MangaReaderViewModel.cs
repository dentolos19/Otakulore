using System.Collections.ObjectModel;
using Otakulore.Models;

namespace Otakulore.ViewModels;

public class MangaReaderViewModel : BaseViewModel
{

    private string _title;
    private bool _hasLoaded;

    public string Title
    {
        get => _title;
        set => UpdateProperty(ref _title, value);
    }

    public bool HasLoaded
    {
        get => _hasLoaded;
        set => UpdateProperty(ref _hasLoaded, value);
    }

    public ObservableCollection<ContentItemModel> Chapters { get; } = new();
    public ObservableCollection<StringItemModel> ImageSources { get; } = new();

}