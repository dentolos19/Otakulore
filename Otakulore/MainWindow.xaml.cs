using System.Windows;
using System.Windows.Input;
using Otakulore.Views;

namespace Otakulore
{

    public partial class MainWindow
    {

        public MainWindow()
        {
            InitializeComponent();
            NavigateTrending(null!, null!);
        }

        private void NavigateBack(object sender, ExecutedRoutedEventArgs args)
        {
            View.GoBack(); 
        }

        private void NavigateForward(object sender, ExecutedRoutedEventArgs args)
        {
            View.GoForward();
        }

        private void CanNavigateBack(object sender, CanExecuteRoutedEventArgs args)
        {
            if (!IsInitialized)
                return;
            args.CanExecute = View.CanGoBack;
        }

        private void CanNavigateForward(object sender, CanExecuteRoutedEventArgs args)
        {
            if (!IsInitialized)
                return;
            args.CanExecute = View.CanGoForward;
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
            App.NavigateSinglePage(App.TrendingViewPage);
        }

        private void NavigateFavorites(object sender, RoutedEventArgs args)
        {
            App.NavigateSinglePage(App.FavoritesViewPage);
        }

        private void NavigateDownloads(object sender, RoutedEventArgs args)
        {
            App.NavigateSinglePage(App.DownloadsViewPage);
        }

        private void NavigateSettings(object sender, RoutedEventArgs args)
        {
            App.NavigateSinglePage(App.PreferencesViewPage);
        }

    }

}