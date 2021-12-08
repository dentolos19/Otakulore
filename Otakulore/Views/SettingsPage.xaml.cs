using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Navigation;
using Otakulore.Models;
using Otakulore.ViewModels;

namespace Otakulore.Views;

public partial class SettingsPage
{

    private SettingsViewModel ViewModel => (SettingsViewModel)DataContext;

    public SettingsPage()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        foreach (var animeProvider in App.AnimeProviders)
            ViewModel.Providers.Add(ProviderItemModel.Create(animeProvider));
        foreach (var mangaProvider in App.MangaProviders)
            ViewModel.Providers.Add(ProviderItemModel.Create(mangaProvider));
    }

    private void OnOpenProvider(object sender, MouseButtonEventArgs args)
    {
        if (ProviderList.SelectedItem is ProviderItemModel item)
            Process.Start(new ProcessStartInfo
            {
                FileName = item.Provider.Website,
                UseShellExecute = true
            });
    }

}