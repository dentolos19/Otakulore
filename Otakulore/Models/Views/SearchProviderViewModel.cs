using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Core;
using Otakulore.Services;

namespace Otakulore.Models;

public partial class SearchProviderViewModel : ObservableObject, IQueryAttributable
{

    private bool _queryApplied;

    [ObservableProperty] private bool _isLoading;
    [ObservableProperty] private string _query;
    [ObservableProperty] private ProviderItemModel _selectedProvider;
    [ObservableProperty] private ObservableCollection<SourceItemModel> _items = new();
    [ObservableProperty] private ObservableCollection<ProviderItemModel> _providers = new();

    public SearchProviderViewModel(VariableService variableService)
    {
        foreach (var provider in variableService.Providers)
            Providers.Add(new ProviderItemModel(provider));
        SelectedProvider = Providers.First();
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (_queryApplied)
            return;
        _queryApplied = true;
        if (query.ContainsKey("provider") && query["provider"] is IProvider provider)
            SelectedProvider = Providers.FirstOrDefault(item => item.Provider == provider) ?? SelectedProvider;
        if (!query.ContainsKey("query") || query["query"] is not string searchQuery)
            return;
        Query = searchQuery;
        await Search();
    }

    [RelayCommand]
    private async Task Search()
    {
        Items.Clear();
        IsLoading = true;
        var sources = await SelectedProvider.Provider.GetSources(Query);
        if (sources is not { Length: > 0 })
        {
            IsLoading = false;
            return;
        }
        foreach (var item in sources)
            Items.Add(new SourceItemModel(item, SelectedProvider.Provider));
        IsLoading = false;
    }

}