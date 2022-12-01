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
            .ConfigureSyncfusionCore()
            .SetupFonts()
            .SetupServices()
            .SetupRoutes();
        return builder.Build();
    }

}