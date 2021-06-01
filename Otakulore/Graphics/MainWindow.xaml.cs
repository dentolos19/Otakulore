using System.Windows;

namespace Otakulore.Graphics
{

    public partial class MainWindow
    {

        public MainWindow()
        {
            InitializeComponent();
            NavigateTrending(null, null);
        }

        private void NavigateBack(object sender, RoutedEventArgs args)
        {
            View.GoBack(); // TODO: use commands instead
        }

        private void NavigateForward(object sender, RoutedEventArgs args)
        {
            View.GoForward(); // TODO: use commands instead
        }

        private void NavigateSearch(object sender, RoutedEventArgs args)
        {
            var query = QueryInput.Text;
            if (string.IsNullOrEmpty(query))
                return;
            App.NavigateSinglePage(new SearchView(query));
        }

        private void NavigateTrending(object sender, RoutedEventArgs args)
        {
            App.NavigateSinglePage(new TrendingView());
        }

        private void NavigateFavorites(object sender, RoutedEventArgs args)
        {
            App.NavigateSinglePage(new FavoritesView());
        }

    }

}