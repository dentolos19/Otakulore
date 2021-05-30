using System.Windows;
using System.Windows.Controls;
using Otakulore.Core;
using Otakulore.Graphics;

namespace Otakulore
{

    public partial class App
    {

        internal static Configuration Settings { get; } = Configuration.LoadConfig();

        internal static SearchView SearchViewPage { get; } = new();
        internal static DetailsView DetailsViewPage { get; } = new();
        internal static FavoritesView FavoritesViewPage { get; } = new();
        internal static SettingsView SettingsViewPage { get; } = new();

        private void InitializeApp(object sender, StartupEventArgs args)
        {
            new MainWindow().Show();
        }

        public static void NavigateSinglePage(Page view)
        {
            if (Current.MainWindow is MainWindow window)
                window.View.Navigate(view);
        }

    }

}