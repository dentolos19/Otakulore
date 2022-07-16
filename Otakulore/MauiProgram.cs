using AniListNet;
using CommunityToolkit.Maui;
using Otakulore.Models;
using Otakulore.Services;

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
                fonts.AddFont("SegoeAssets.ttf", "SegoeAssets");
            });

        builder.Services.AddSingleton<AniClient>();
        builder.Services.AddSingleton(ResourceService.Initialize());
        builder.Services.AddSingleton(SettingsService.Initialize());
        builder.Services.AddSingleton(VariableService.Initialize());

        builder.Services.AddSingleton<HomeViewModel>();
        builder.Services.AddSingleton<SettingsViewModel>();
        builder.Services.AddTransient<SearchViewModel>();
        builder.Services.AddTransient<DetailsViewModel>();
        builder.Services.AddTransient<SearchProviderViewModel>();
        builder.Services.AddTransient<SourceViewerViewModel>();
        builder.Services.AddTransient<ContentViewerViewModel>();

        return builder.Build();
    }

}