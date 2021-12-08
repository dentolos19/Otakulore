using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using JikanDotNet;
using Otakulore.Core;
using Otakulore.Services;
using Otakulore.Views;

namespace Otakulore;

public partial class App
{

    public static readonly IList<IAnimeProvider> AnimeProviders = new List<IAnimeProvider>();
    public static readonly IList<IMangaProvider> MangaProviders = new List<IMangaProvider>();

    public static Settings Settings { get; } = Settings.Load();
    public static IJikan Jikan { get; } = new Jikan();

    private void OnStartup(object sender, StartupEventArgs args)
    {
        var providers = Assembly.GetExecutingAssembly().GetTypes().Where(type => type.IsClass && type.GetInterfaces().Contains(typeof(IProvider)));
        foreach (var provider in providers)
        {
            var interfaces = provider.GetInterfaces();
            if (interfaces.Contains(typeof(IAnimeProvider)))
                AnimeProviders.Add((IAnimeProvider)Activator.CreateInstance(provider));
            else if (interfaces.Contains(typeof(IMangaProvider)))
                MangaProviders.Add((IMangaProvider)Activator.CreateInstance(provider));
        }
        MainWindow = new MainWindow();
        MainWindow.Show();
    }

    private void OnExit(object sender, ExitEventArgs args)
    {
        Settings.Save();
    }

}