using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Services;
using Otakulore.Services.Providers;

namespace Otakulore.Models;

public partial class SearchProviderViewModel : ObservableObject, IQueryAttributable
{

    private readonly IProvider _provider = new GogoanimeProvider();

    [ObservableProperty] private string? _query;
    [ObservableProperty] private bool _isLoading;
    [ObservableProperty] private ObservableCollection<SourceItemModel> _items = new();

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.ContainsKey("query"))
            return;
        Query = query["query"].ToString();
        SearchCommand.Execute(null);
    }

    [ICommand]
    private async Task Search()
    {
        if (string.IsNullOrEmpty(Query))
            return;
        IsLoading = true;
        Items.Clear();
        var results = await _provider.SearchAsync(Query);
        if (results == null)
        {
            IsLoading = false;
            return;
        }
        foreach (var item in results)
            Items.Add(new SourceItemModel(item));
        IsLoading = false;
    }

}