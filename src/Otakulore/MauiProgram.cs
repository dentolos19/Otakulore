using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace Otakulore;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .SetupFonts()
            .SetupServices();
#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}