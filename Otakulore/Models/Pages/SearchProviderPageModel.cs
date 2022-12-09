using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Content;
using Otakulore.Helpers;
using Otakulore.Pages;
using Otakulore.Services;

namespace Otakulore.Models;

[TransientService]
public partial class SearchProviderPageModel : BasePageModel
{

    [ObservableProperty] private ProviderItemModel _selectedProvider;

    [ObservableProperty] private ObservableCollection<MediaSourceItemModel> _items = new();
    [ObservableProperty] private ObservableCollection<ProviderItemModel> _providers = new();

    public SearchProviderPageModel()
    {
        foreach (var item in ContentService.Instance.Providers)
            Providers.Add(ProviderItemModel.Map(item));
        SelectedProvider = Providers.First();
    }

    protected override void Initialize(object? args = null)
    {
        if (args is string query)
        {
            if (ParentPage is SearchProviderPage page)
                page.SearchBox.Text = query;
            SearchCommand.Execute(query);
        }
        else if (args is IProvider provider)
        {
            SelectedProvider = Providers.FirstOrDefault(
                item => item.Provider == provider,
                Providers.First()
            );
        }
    }

    [RelayCommand]
    private async Task Search(string query)
    {
        Items.Clear();
        var provider = SelectedProvider.Provider;
        var results = await provider.GetSources(query);
        foreach (var item in results)
            Items.Add(MediaSourceItemModel.Map(provider, item));
    }

}