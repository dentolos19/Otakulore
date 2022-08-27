using System.Reflection;
using Otakulore.Core.Attributes;
using Otakulore.Services;

namespace Otakulore;

public static class MauiHelper
{

    public static MauiAppBuilder SetupServices(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton(DataService.Initialize());
        builder.Services.AddSingleton(ResourceService.Initialize());
        builder.Services.AddSingleton(SettingsService.Initialize());
        builder.Services.AddSingleton(VariableService.Initialize());
        return builder;
    }

    public static MauiAppBuilder SetupViewModels(this MauiAppBuilder builder)
    {
        var types =
            from type in Assembly.GetExecutingAssembly().GetTypes()
            where type.Namespace == "Otakulore.Models"
            select type;
        foreach (var type in types)
        {
            if (type.GetCustomAttribute<AsSingletonServiceAttribute>() is not null)
                builder.Services.AddSingleton(type);
            if (type.GetCustomAttribute<AsTransientServiceAttribute>() is not null)
                builder.Services.AddTransient(type);
        }
        /*
        builder.Services.AddSingleton<AppShellModel>();
        builder.Services.AddTransient<CharacterDetailsPageModel>();
        builder.Services.AddTransient<ContentViewerPageModel>();
        builder.Services.AddTransient<DetailsPageModel>();
        builder.Services.AddSingleton<HomePageModel>();
        builder.Services.AddSingleton<LibraryPageModel>();
        builder.Services.AddTransient<MediaCharactersPageModel>();
        builder.Services.AddTransient<MediaRelationsPageModel>();
        builder.Services.AddTransient<MediaTrackPageModel>();
        builder.Services.AddTransient<MediaTrackPageModel>();
        builder.Services.AddSingleton<SchedulePageModel>();
        builder.Services.AddTransient<SearchFilterPageModel>();
        builder.Services.AddTransient<SearchProviderPageModel>();
        builder.Services.AddTransient<SearchPageModel>();
        builder.Services.AddTransient<SeasonalPageModel>();
        builder.Services.AddSingleton<SettingsPageModel>();
        builder.Services.AddTransient<SourceViewerPageModel>();
        */
        return builder;
    }

    public static void SetupRoutes()
    {
        var types =
            from type in Assembly.GetExecutingAssembly().GetTypes()
            where type.Namespace == "Otakulore.Pages" &&
                  type.GetCustomAttribute(typeof(IncludePageRoute)) is not null
            select type;
        foreach (var type in types)
            AddRoute(type);
        /*
        AddRoute(typeof(CharacterDetailsPage));
        AddRoute(typeof(ContentViewerPage));
        AddRoute(typeof(DetailsPage));
        AddRoute(typeof(LibraryPage));
        AddRoute(typeof(LoginPage));
        AddRoute(typeof(MediaCharactersPage));
        AddRoute(typeof(MediaRelationsPage));
        AddRoute(typeof(MediaTrackPage));
        AddRoute(typeof(SearchFilterPage));
        AddRoute(typeof(SearchPage));
        AddRoute(typeof(SearchProviderPage));
        AddRoute(typeof(SourceViewerPage));
        */
    }

    private static void AddRoute(Type type)
    {
        Routing.RegisterRoute(type.Name, type);
    }

    public static TService? GetService<TService>()
    {
        #if WINDOWS
        return MauiWinUIApplication.Current.Services.GetService<TService>();
        #elif ANDROID
        return MauiApplication.Current.Services.GetService<TService>();
        #else
        return default(TService);
        #endif
    }

    public static Task NavigateTo(Type type, IDictionary<string, object>? parameters = null)
    {
        return parameters is { Count: > 0 }
            ? Shell.Current.GoToAsync(type.Name, parameters)
            : Shell.Current.GoToAsync(type.Name);
    }

    public static Task NavigateBack()
    {
        return Shell.Current.GoToAsync("..");
    }

}