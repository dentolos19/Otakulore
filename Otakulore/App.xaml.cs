using System.Windows;
using System.Windows.Controls;
using Otakulore.Core;
using Otakulore.Graphics;

namespace Otakulore
{

    public partial class App
    {

        internal static UserData Settings { get; } = UserData.LoadData();

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