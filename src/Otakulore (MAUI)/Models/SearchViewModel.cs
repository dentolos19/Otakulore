using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Otakulore.Models;

public partial class SearchViewModel : ObservableObject, IQueryAttributable
{

    private bool _alreadyAppliedQuery;

    [ObservableProperty] private string? _query;
    [ObservableProperty] private bool _isLoading;
    [ObservableProperty] private ObservableCollection<MediaItemModel> _items = new();

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.ContainsKey("query") || _alreadyAppliedQuery)
            return;
        Query = query["query"].ToString();
        SearchCommand.Execute(null);
        _alreadyAppliedQuery = true;
    }

    [ICommand]
    private async Task Search()
    {
        if (string.IsNullOrEmpty(Query))
            return;
        Items.Clear();
        IsLoading = true;
        var results = await App.Client.SearchMediaAsync(Query);
        foreach (var data in results.Data)
            Items.Add(new MediaItemModel(data));
        IsLoading = false;
    }

}