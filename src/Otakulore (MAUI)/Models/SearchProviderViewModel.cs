using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Pages;
using Otakulore.Services;
using Otakulore.Services.Providers;

namespace Otakulore.Models;

public partial class SearchProviderViewModel : ObservableObject, IQueryAttributable
{

    [ObservableProperty] private string? _query;
    [ObservableProperty] private bool _isLoading;
    [ObservableProperty] private ProviderItemModel? _selectedProvider;
    [ObservableProperty] private ObservableCollection<ProviderItemModel> _providers = new();
    [ObservableProperty] private ObservableCollection<SourceItemModel> _items = new();

    public SearchProviderViewModel()
    {
        Providers.Add(new ProviderItemModel(new GogoanimeProvider()));
        Providers.Add(new ProviderItemModel(new ManganatoProvider()));
        SelectedProvider = Providers.First();
    }

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
        if (SelectedProvider == null)
            return;
        IsLoading = true;
        Items.Clear();
        var results = await SelectedProvider.Provider.SearchSourcesAsync(Query);
        if (results == null)
        {
            IsLoading = false;
            return;
        }
        foreach (var item in results)
            Items.Add(new SourceItemModel(item, SelectedProvider.Provider));
        IsLoading = false;
    }

    [ICommand]
    private async Task Open(MediaSource data)
    {
        await Shell.Current.GoToAsync(nameof(SourceViewerPage), new Dictionary<string, object>
        {
            { "data", data }
        });
    }

}