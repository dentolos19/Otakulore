using System.Collections.ObjectModel;
using Otakulore.Models;

namespace Otakulore.ViewModels;

public class SettingsViewModel : BaseViewModel
{

    public ObservableCollection<ProviderItemModel> AnimeProviders { get; } = new();
    public ObservableCollection<ProviderItemModel> MangaProviders { get; } = new();

}