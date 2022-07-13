using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Core;
using Otakulore.Core.Providers;
using Otakulore.Services;

namespace Otakulore.Models;

public partial class SearchProviderViewModel : ObservableObject, IQueryAttributable
{
    
    [ObservableProperty] private string _query;
    [ObservableProperty] private ProviderItemModel _selectedProvider;
    [ObservableProperty] private bool _isLoading = true;
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
        if (query.ContainsKey("provider") && query["provider"] is IProvider provider)
        {
            SelectedProvider = Providers.FirstOrDefault(item => item.Provider == provider) ?? SelectedProvider;
        }
        if (query.ContainsKey("query") && query["query"] is string searchQuery)
        {
            Query = searchQuery;
            await Search();
        }
    }

    [ICommand]
    private async Task Search()
    {
        IsLoading = true;
        var sources = await new GogoanimeProvider().GetSources(Query);
        Items.Clear();
        foreach (var item in sources)
            Items.Add(new SourceItemModel(item, SelectedProvider.Provider));
        IsLoading = false;
    }

}