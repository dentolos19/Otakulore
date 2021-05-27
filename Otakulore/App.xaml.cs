using System.Windows;
using System.Windows.Controls;
using Otakulore.Graphics;

namespace Otakulore
{

    public partial class App
    {

        internal static HomeView HomeViewPage { get; } = new();
        internal static DetailsView DetailsViewPage { get; } = new();

        private void InitializeApp(object sender, StartupEventArgs args)
        {
            new MainWindow().Show();
        }

        public static void NavigateSinglePageApp(Page view)
        {
            if (Current.MainWindow is MainWindow window)
                window.View.Navigate(view);
        }

    }

}