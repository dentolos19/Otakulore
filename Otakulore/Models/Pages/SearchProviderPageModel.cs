using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Helpers;
using Otakulore.Services;

namespace Otakulore.Models;

[TransientService]
public partial class SearchProviderPageModel : BasePageModel
{

    [ObservableProperty] private ObservableCollection<MediaSourceItemModel> _items = new();
    [ObservableProperty] private ObservableCollection<ProviderItemModel> _providers = new();

    protected override void Initialize(object? args = null)
    {
        foreach (var item in ContentService.Instance.Providers)
            Providers.Add(ProviderItemModel.Map(item));
        if (args is not string query)
            return;
        SearchCommand.Execute(query);
    }

    [RelayCommand]
    private async Task Search(string query)
    {
        Items.Clear();
        var provider = Providers.First().Provider;
        var results = await provider.GetSources(query);
        foreach (var item in results)
            Items.Add(MediaSourceItemModel.Map(provider, item));
    }

}