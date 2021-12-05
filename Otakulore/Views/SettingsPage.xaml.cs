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
            ViewModel.AnimeProviders.Add(new ProviderItemModel { Name = animeProvider.Name, Author = animeProvider.Author });
        foreach (var mangaProvider in App.MangaProviders)
            ViewModel.MangaProviders.Add(new ProviderItemModel { Name = mangaProvider.Name, Author = mangaProvider.Author });
    }

}