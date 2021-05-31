using System.Windows;

namespace Otakulore.Graphics
{

    public partial class MainWindow
    {

        public MainWindow()
        {
            InitializeComponent();
            NavigateHome(null, null);
        }

        private void NavigateHome(object sender, RoutedEventArgs args)
        {
            App.NavigateSinglePage(new HomeView());
        }

        private void NavigateSearch(object sender, RoutedEventArgs args)
        {
            var query = QueryInput.Text;
            if (string.IsNullOrEmpty(query))
                return;
            App.NavigateSinglePage(new SearchView(query));
        }

        private void NavigateFavorites(object sender, RoutedEventArgs args)
        {
            App.NavigateSinglePage(new FavoritesView());
        }

        private void NavigateSettings(object sender, RoutedEventArgs args)
        {
            App.NavigateSinglePage(new SettingsView());
        }

    }

}