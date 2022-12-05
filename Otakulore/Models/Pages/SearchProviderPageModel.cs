using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Content;
using Otakulore.Helpers;
using Otakulore.Services;

namespace Otakulore.Models;

[TransientService]
public partial class SearchProviderPageModel : BasePageModel
{

    private readonly ContentService _contentService = MauiHelper.GetService<ContentService>();

    [ObservableProperty] private ObservableCollection<ProviderItemModel> _providers = new();
    [ObservableProperty] private ObservableCollection<MediaSourceItemModel> _items = new();

    protected override void Initialize(object? args = null)
    {
        foreach (var item in _contentService.Providers)
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