using System.Reflection;
using Otakulore.Helpers;
using Otakulore.Services;

namespace Otakulore;

public static class MauiHelper
{

    public static MauiAppBuilder SetupFonts(this MauiAppBuilder builder)
    {
        builder.ConfigureFonts(fonts =>
        {
            fonts.AddFont("FontAwesomeRegular.ttf", "FontAwesomeRegular");
            fonts.AddFont("FontAwesomeRegularBrands.ttf", "FontAwesomeRegularBrands");
            fonts.AddFont("FontAwesomeSolid.ttf", "FontAwesomeSolid");
            fonts.AddFont("MaterialIcons.ttf", "MaterialIcons");
            fonts.AddFont("Poppins.ttf", "Poppins");
            fonts.AddFont("SegoeIcons.ttf", "SegoeIcons");
            fonts.AddFont("SpaceGrotesk.ttf", "SpaceGrotesk");
        });
        return builder;
    }

    public static MauiAppBuilder SetupServices(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton(DataService.Initialize());
        builder.Services.AddSingleton(ResourceService.Initialize());
        builder.Services.AddSingleton(SettingsService.Initialize());
        builder.Services.AddSingleton(VariableService.Initialize());

        var types =
            from type in Assembly.GetExecutingAssembly().GetTypes()
            where type.Namespace == "Otakulore.Models"
            select type;
        foreach (var type in types)
        {
            if (type.GetCustomAttribute<SingletonServiceAttribute>() is not null)
                builder.Services.AddSingleton(type);
            if (type.GetCustomAttribute<TransientServiceAttribute>() is not null)
                builder.Services.AddTransient(type);
        }

        return builder;
    }

    public static MauiAppBuilder SetupRoutes(this MauiAppBuilder builder)
    {
        var types =
            from type in Assembly.GetExecutingAssembly().GetTypes()
            where
                type.Namespace == "Otakulore.Pages" &&
                type.GetCustomAttribute(typeof(PageRouteAttribute)) is not null
            select type;
        foreach (var type in types)
            Routing.RegisterRoute(type.Name, type);
        return builder;
    }

    public static Task Navigate(Type type, IDictionary<string, object>? parameters = null)
    {
        return parameters is { Count: > 0 }
            ? Shell.Current.GoToAsync(type.Name, parameters)
            : Shell.Current.GoToAsync(type.Name);
    }

    public static Task NavigateBack()
    {
        return Shell.Current.GoToAsync("..");
    }

    public static TService? GetService<TService>()
    {
        #if WINDOWS
        return MauiWinUIApplication.Current.Services.GetService<TService>();
        #elif ANDROID
        return MauiApplication.Current.Services.GetService<TService>();
        #else
        return default;
        #endif
    }

}