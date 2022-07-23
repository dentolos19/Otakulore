using Otakulore.Models;
using Otakulore.Services;

namespace Otakulore;

public static class MauiHelper
{

    public static MauiAppBuilder SetupServices(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton(ResourceService.Initialize());
        builder.Services.AddSingleton(SettingsService.Initialize());
        builder.Services.AddSingleton(VariableService.Initialize());
        return builder;
    }

    public static MauiAppBuilder SetupViewModels(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<ContentViewerViewModel>();
        builder.Services.AddTransient<DetailsViewModel>();
        builder.Services.AddSingleton<HomeViewModel>();
        builder.Services.AddTransient<SearchProviderViewModel>();
        builder.Services.AddSingleton<SearchViewModel>();
        builder.Services.AddTransient<SeasonalViewModel>();
        builder.Services.AddSingleton<SettingsViewModel>();
        builder.Services.AddTransient<SourceViewerViewModel>();
        return builder;
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

    public static void AddRoute(Type type)
    {
        Routing.RegisterRoute(type.Name, type);
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