using JikanDotNet;
using System.Windows;
using System.Windows.Threading;
using Otakulore.Core;

namespace Otakulore;

public partial class App
{

    public static Settings Settings { get; } = Settings.Load();
    public static IJikan Jikan { get; } = new Jikan();

    private void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs args)
    {
        MessageBox.Show("An unhandled exception occurred! " + args.Exception.Message, "Libjector");
        args.Handled = true;
    }

    private void OnExit(object sender, ExitEventArgs args)
    {
        Settings.Save();
    }

}