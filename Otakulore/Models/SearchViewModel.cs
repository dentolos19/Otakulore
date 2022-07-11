using System.Collections.ObjectModel;
using AniListNet;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Otakulore.Models;

public partial class SearchViewModel : ObservableObject, IQueryAttributable
{

    private readonly AniClient _client;

    [ObservableProperty] private ObservableCollection<MediaItemModel> _items = new();

    public SearchViewModel(AniClient client)
    {
        _client = client;
    }
    
    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.ContainsKey("query"))
            return;
        if (query["query"] is not string searchQuery)
            return;
        var results = await _client.SearchMediaAsync(searchQuery);
        Items.Clear();
        foreach (var item in results.Data)
            Items.Add(new MediaItemModel(item));
    }

}