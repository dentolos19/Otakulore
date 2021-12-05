using System.Collections.ObjectModel;
using Otakulore.Core;
using Otakulore.Models;

namespace Otakulore.ViewModels;

public class DetailsViewModel : BaseViewModel
{

    private bool _isFavorite;
    private bool _hasSourcesLoaded;

    public MediaType Type { get; init; }
    public long? Id { get; init; }

    public string ImageUrl { get; set; }
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string Synopsis { get; set; }
    public string? Background { get; set; }
    public string Format { get; set; }
    public string Status { get; set; }
    public string Contents { get; set; }

    public bool IsFavorite
    {
        get => _isFavorite;
        set => UpdateProperty(ref _isFavorite, value);
    }

    public bool HasSourcesLoaded
    {
        get => _hasSourcesLoaded;
        set => UpdateProperty(ref _hasSourcesLoaded, value);
    }

    public ObservableCollection<string> Titles { get; } = new();
    public ObservableCollection<ProviderItemModel> Providers { get; } = new();
    public ObservableCollection<SourceItemModel> Sources { get; } = new();

}