using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Services;

namespace Otakulore.Models;

public partial class SettingsViewModel : ObservableObject
{

    [ObservableProperty] private string _credits;
    [ObservableProperty] private ObservableCollection<ProviderItemModel> _providers = new();

    public SettingsViewModel(ResourceService resourceService, VariableService variableService)
    {
        Credits = resourceService.Credits;
        foreach (var provider in variableService.Providers)
            Providers.Add(new ProviderItemModel(provider));
    }

}