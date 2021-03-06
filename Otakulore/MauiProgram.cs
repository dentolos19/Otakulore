using CommunityToolkit.Maui;

namespace Otakulore;

public static class MauiProgram
{

    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans.ttf", "OpenSans");
                fonts.AddFont("Poppins.ttf", "Poppins");
                fonts.AddFont("SegoeAssets.ttf", "SegoeAssets");
            })
            .SetupServices()
            .SetupViewModels();
        return builder.Build();
    }

}