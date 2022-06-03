namespace Otakulore;

public static class MauiExtensions
{

    public static TService? GetService<TService>()
    {
        #if WINDOWS10_0_17763_0_OR_GREATER
        return MauiWinUIApplication.Current.Services.GetService<TService>();
        #elif ANDROID
        return MauiApplication.Current.Services.GetService<TService>();
        #else
        return default(TService);
        #endif
    }

}