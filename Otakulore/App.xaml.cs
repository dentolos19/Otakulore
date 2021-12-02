using JikanDotNet;
using System.Windows;
using System.Windows.Threading;

namespace Otakulore;

public partial class App
{

    public static IJikan JikanService { get; } = new Jikan();

    private void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs args)
    {
        MessageBox.Show("An unhandled exception occurred! " + args.Exception.Message, "Libjector");
        args.Handled = true;
    }

}