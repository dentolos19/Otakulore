using Otakulore.Views;
using System.Windows;

namespace Otakulore;

public partial class MainWindow
{

    private HomePage? _homePageInstance;
    private FavoritesPage? _favoritesPageInstance;
    private SchedulesPage? _schedulesPageInstance;
    private SettingsPage? _settingsPageInstance;

    public MainWindow()
    {
        InitializeComponent();
        OnNavigateHome(null, null);
    }

    private void OnNavigateBack(object sender, RoutedEventArgs args)
    {
        if (ContentView.CanGoBack)
            ContentView.GoBack();
    }

    private void OnNavigateSearch(object sender, RoutedEventArgs args)
    {
        var query = SearchInput.Text;
        if (!string.IsNullOrEmpty(query) && query.Length >= 3)
            ContentView.Navigate(new SearchPage(query));
    }

    private void OnNavigateHome(object sender, RoutedEventArgs args)
    {
        _homePageInstance ??= new HomePage();
        ContentView.Navigate(_homePageInstance);
    }

    private void OnNavigateFavorites(object sender, RoutedEventArgs args)
    {
        _favoritesPageInstance ??= new FavoritesPage();
        ContentView.Navigate(_favoritesPageInstance);
    }

    private void OnNavigateSchedules(object sender, RoutedEventArgs args)
    {
        _schedulesPageInstance ??= new SchedulesPage();
        ContentView.Navigate(_schedulesPageInstance);
    }

    private void OnNavigateSettings(object sender, RoutedEventArgs args)
    {
        _settingsPageInstance ??= new SettingsPage();
        ContentView.Navigate(_settingsPageInstance);
    }

}