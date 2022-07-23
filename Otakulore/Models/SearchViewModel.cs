using System.Collections.ObjectModel;
using AniListNet;
using AniListNet.Objects;
using AniListNet.Parameters;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Otakulore.Models;

public partial class SearchViewModel : ObservableObject, IQueryAttributable
{

    private readonly AniClient _client;

    private bool _queryApplied;
    private SearchMediaFilter _accumulationFilter;
    private int _currentPageIndex;
    private bool _hasNextPage;

    [ObservableProperty] private string _query;
    [ObservableProperty] private bool _isLoading;
    [ObservableProperty] private MediaSort _selectedSort = MediaSort.Relevance;
    [ObservableProperty] private ObservableCollection<MediaSort> _sorts = new();
    [ObservableProperty] private ObservableCollection<MediaItemModel> _items = new();

    public SearchViewModel(AniClient client)
    {
        _client = client;
        var sorts = (MediaSort[])Enum.GetValues(typeof(MediaSort));
        foreach (var sort in sorts)
            Sorts.Add(sort);
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (_queryApplied)
            return;
        _queryApplied = true;
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
        _accumulationFilter = new SearchMediaFilter { Query = Query, Sort = SelectedSort };
        var results = await _client.SearchMediaAsync(_accumulationFilter);
        if (results.Data is not { Length: > 0 })
        {
            IsLoading = false;
            return;
        }
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
        var results = await _client.SearchMediaAsync(_accumulationFilter, new AniPaginationOptions(++_currentPageIndex));
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

    [ICommand]
    private async Task Filter()
    {
        await Toast.Make("This feature is not implemented yet!").Show();
    }

}