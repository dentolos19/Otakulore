using System.Collections.ObjectModel;
using AniListNet;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Otakulore.Models;

public partial class SearchViewModel : ObservableObject, IQueryAttributable
{

    private readonly AniClient _client;

    [ObservableProperty] private bool _isLoading;
    [ObservableProperty] private string _query;
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
        Query = searchQuery;
        await Search();
    }

    [ICommand]
    private async Task Search()
    {
        Items.Clear();
        IsLoading = true;
        var results = await _client.SearchMediaAsync(Query);
        if (results.Data is not { Length: > 0 })
            return;
        foreach (var item in results.Data)
            Items.Add(new MediaItemModel(item));
        IsLoading = false;
    }

}