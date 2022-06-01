namespace Otakulore;

public static class MauiExtensions
{

    public static TService? GetService<TService>()
    {
        return MauiWinUIApplication.Current.Services.GetService<TService>();
    }

}