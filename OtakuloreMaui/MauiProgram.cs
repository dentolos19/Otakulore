namespace Otakulore;

public static class MauiProgram
{

    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>().ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans.ttf", "OpenSans");
            fonts.AddFont("OpenSans-Italic.ttf", "OpenSans Italic");
        });
        return builder.Build();
    }

}