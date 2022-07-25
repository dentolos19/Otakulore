using System.Collections.ObjectModel;
using AniListNet;
using AniListNet.Objects;
using AniListNet.Parameters;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Services;

namespace Otakulore.Models;

public partial class SearchViewModel : ObservableObject, IQueryAttributable
{

    private readonly DataService _data;

    private bool _queryApplied;
    private SearchMediaFilter _accumulationFilter = new();
    private int _currentPageIndex;
    private bool _hasNextPage;

    [ObservableProperty] private string _query;
    [ObservableProperty] private bool _isLoading;
    [ObservableProperty] private MediaSort _selectedSort = MediaSort.Relevance;
    [ObservableProperty] private ObservableCollection<MediaSort> _sorts = new();
    [ObservableProperty] private ObservableCollection<MediaItemModel> _items = new();

    public SearchViewModel()
    {
        _data = MauiHelper.GetService<DataService>();
        var sorts = (MediaSort[])Enum.GetValues(typeof(MediaSort));
        foreach (var sort in sorts)
            Sorts.Add(sort);
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (_queryApplied)
            return;
        _queryApplied = true;
        if (!query.ContainsKey("filter"))
            return;
        if (query["filter"] is not SearchMediaFilter filter)
            return;
        _accumulationFilter = filter;
        if (!string.IsNullOrEmpty(filter.Query))
            Query = _accumulationFilter.Query;
        SelectedSort = _accumulationFilter.Sort;
        await Search();
    }

    [ICommand]
    private async Task Search()
    {
        if (IsLoading)
            return;
        Items.Clear();
        IsLoading = true;
        _accumulationFilter.Query = Query;
        _accumulationFilter.Sort = SelectedSort;
        var results = await _data.Client.SearchMediaAsync(_accumulationFilter);
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
        var results = await _data.Client.SearchMediaAsync(_accumulationFilter, new AniPaginationOptions(++_currentPageIndex));
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
        await Toast.Make("This feature is not implemented yet!").Show(); // TODO: implement filtering
    }

    [ICommand]
    private async Task Back()
    {
        _queryApplied = false;
        await MauiHelper.NavigateBack();
    }

}