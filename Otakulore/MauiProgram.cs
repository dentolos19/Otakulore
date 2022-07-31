using CommunityToolkit.Maui;
using Syncfusion.Maui.Core.Hosting;

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
            .ConfigureSyncfusionCore()
            .SetupServices()
            .SetupViewModels();
        return builder.Build();
    }

}