using JikanDotNet;
using Otakulore.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace Otakulore;

public partial class App
{

    public static Settings Settings { get; } = Settings.Load();
    public static IJikan Jikan { get; } = new Jikan(true);

    public static IList<IProvider> Providers { get; } = new List<IProvider>();

    private void OnStartup(object sender, StartupEventArgs args)
    {
        var providers = Assembly.GetExecutingAssembly().GetTypes().Where(type => type.IsClass && type.GetInterfaces().Contains(typeof(IProvider)));
        foreach (var provider in providers)
            Providers.Add((IProvider)Activator.CreateInstance(provider));
        MainWindow = new MainWindow();
        MainWindow.Show();
    }

    private void OnExit(object sender, ExitEventArgs args)
    {
        Settings.Save();
    }

}