using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Core;
using Otakulore.Core.Providers;

namespace Otakulore.Models;

public partial class SearchProviderViewModel : ObservableObject, IQueryAttributable
{

    private readonly IProvider _provider = new GogoanimeProvider();

    [ObservableProperty] private string _query;
    [ObservableProperty] private bool _isLoading = true;
    [ObservableProperty] private ObservableCollection<SourceItemModel> _items = new();

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.ContainsKey("query"))
            return;
        if (query["query"] is not string searchQuery)
            return;
        Query = searchQuery;
        await SearchCommand.ExecuteAsync(null);
    }

    [ICommand]
    private async Task Search()
    {
        IsLoading = true;
        var sources = await new GogoanimeProvider().GetSources(Query);
        Items.Clear();
        foreach (var item in sources)
            Items.Add(new SourceItemModel(item, _provider));
        IsLoading = false;
    }

}