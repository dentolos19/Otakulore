using System.Windows.Navigation;
using Otakulore.Models;

namespace Otakulore.Views;

public partial class SettingsPage
{

    public SettingsPage()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        foreach (var animeProvider in App.AnimeProviders)
            AnimeProviderList.Items.Add(new ProviderItemModel { Name = animeProvider.Name, Author = animeProvider.Author });
        foreach (var mangaProvider in App.MangaProviders)
            MangaProviderList.Items.Add(new ProviderItemModel { Name = mangaProvider.Name, Author = mangaProvider.Author });
    }

}