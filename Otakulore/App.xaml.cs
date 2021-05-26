using System.Windows;
using System.Windows.Threading;
using Otakulore.Graphics;

namespace Otakulore
{

    public partial class App
    {

        internal static HomeView HomeViewPage { get; } = new();

        private void InitializeApp(object sender, StartupEventArgs args)
        {
            new MainWindow().Show();
        }

        private void HandleException(object sender, DispatcherUnhandledExceptionEventArgs args)
        {
            // TODO
        }

    }

}