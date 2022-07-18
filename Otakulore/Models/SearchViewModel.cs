using System.Collections.ObjectModel;
using System.Diagnostics;
using AniListNet;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Otakulore.Models;

public partial class SearchViewModel : ObservableObject, IQueryAttributable
{

    private readonly AniClient _client;

    private string _accumulationQuery;
    private int _currentPageIndex;
    private bool _hasNextPage;

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
        if (IsLoading)
            return;
        Items.Clear();
        IsLoading = true;
        var results = await _client.SearchMediaAsync(Query);
        if (results.Data is not { Length: > 0 })
        {
            IsLoading = false;
            return;
        }
        _accumulationQuery = Query;
        _currentPageIndex = results.CurrentPageIndex;
        _hasNextPage = results.HasNextPage;
        foreach (var item in results.Data)
            Items.Add(new MediaItemModel(item));
        IsLoading = false;
    }

    [ICommand]
    private async Task Accumulate()
    {
        if (IsLoading || !_hasNextPage)
            return;
        IsLoading = true;
        var results = await _client.SearchMediaAsync(_accumulationQuery, new AniPaginationOptions(++_currentPageIndex));
        if (results.Data is not { Length: > 0 })
        {
            _hasNextPage = false;
            IsLoading = false;
            return;
        }
        _hasNextPage = results.HasNextPage;
        foreach (var item in results.Data)
            Items.Add(new MediaItemModel(item));
        IsLoading = false;
    }

}