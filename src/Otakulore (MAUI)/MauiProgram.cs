namespace Otakulore;

public static class MauiProgram
{

    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>().ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans.ttf", "OpenSans");
            fonts.AddFont("SegoeAssets.ttf", "SegoeAssets");
        });
        return builder.Build();
    }

}