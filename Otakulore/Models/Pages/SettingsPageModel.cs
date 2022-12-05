using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Otakulore.Helpers;
using Otakulore.Services;

namespace Otakulore.Models;

[SingletonService]
public partial class SettingsPageModel : BasePageModel
{

    private readonly ContentService _contentService = MauiHelper.GetService<ContentService>();

    [ObservableProperty] private ObservableCollection<ProviderItemModel> _providers = new();

    protected override void Initialize(object? args = null)
    {
        foreach (var item in _contentService.Providers)
            Providers.Add(ProviderItemModel.Map(item));
    }

}