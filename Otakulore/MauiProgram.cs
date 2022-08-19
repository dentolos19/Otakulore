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
                fonts.AddFont("MaterialIcons.ttf", "MaterialIcons");
                fonts.AddFont("Poppins.ttf", "Poppins");
                fonts.AddFont("SegoeIcons.ttf", "SegoeIcons");
            })
            .ConfigureSyncfusionCore()
            .SetupServices()
            .SetupViewModels();
        return builder.Build();
    }

}