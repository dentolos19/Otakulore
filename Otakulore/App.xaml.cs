using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JikanDotNet;
using System.Windows;
using System.Windows.Threading;
using Otakulore.Core;
using Otakulore.Services;

namespace Otakulore;

public partial class App
{

    public static Settings Settings { get; } = Settings.Load();
    public static IJikan Jikan { get; } = new Jikan();

    public static IList<IAnimeProvider> AnimeProviders = new List<IAnimeProvider>();
    public static IList<IMangaProvider> MangaProviders = new List<IMangaProvider>();

    private void OnStartup(object sender, StartupEventArgs args)
    {
        var providers = Assembly.GetEntryAssembly().GetReferencedAssemblies().Select(Assembly.Load)
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.IsClass && type.GetInterfaces().Contains(typeof(IProvider)));
        foreach (var provider in providers)
        {
            var interfaces = provider.GetInterfaces();
            if (interfaces.Contains(typeof(IAnimeProvider)))
            {
                var animeProvider = (IAnimeProvider)Activator.CreateInstance(provider);
                animeProvider.Initialize();
                AnimeProviders.Add(animeProvider);
            }
            else if (interfaces.Contains(typeof(IMangaProvider)))
            {
                var mangaProvider = (IMangaProvider)Activator.CreateInstance(provider);
                mangaProvider.Initialize();
                MangaProviders.Add(mangaProvider);
            }
        }
    }

    private void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs args)
    {
        MessageBox.Show("An unhandled exception occurred! " + args.Exception.Message, "Otakulore");
        args.Handled = true;
    }

    private void OnExit(object sender, ExitEventArgs args)
    {
        Settings.Save();
    }

}