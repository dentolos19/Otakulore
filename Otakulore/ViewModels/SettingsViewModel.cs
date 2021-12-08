using System.Collections.ObjectModel;
using Otakulore.Models;

namespace Otakulore.ViewModels;

public class SettingsViewModel : BaseViewModel
{

    public ObservableCollection<ProviderItemModel> Providers { get; } = new();

}