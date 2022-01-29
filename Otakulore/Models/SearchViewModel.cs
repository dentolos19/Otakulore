using System.Collections.ObjectModel;
using Otakulore.Core.AniList;

namespace Otakulore.Models;

public class SearchViewModel : BaseViewModel
{

    private string _query;
    private MediaType _type = MediaType.Anime;
    private bool _isLoading;

    public string Query
    {
        get => _query;
        set => UpdateProperty(ref _query, value);
    }

    public int Type
    {
        get => (int)_type;
        set => UpdateProperty(ref _type, (MediaType)value);
    }

    public bool IsLoading
    {
        get => _isLoading;
        set => UpdateProperty(ref _isLoading, value);
    }

    public ObservableCollection<MediaItemModel> Items { get; } = new();

    public async void Search()
    {
        IsLoading = true;
        Items.Clear();
        var result = await App.Client.SearchMedia(_query, _type);
        IsLoading = false;
        foreach (var entry in result)
            Items.Add(new MediaItemModel(entry));
    }

}