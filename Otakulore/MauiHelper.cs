namespace Otakulore;

public static class MauiHelper
{

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