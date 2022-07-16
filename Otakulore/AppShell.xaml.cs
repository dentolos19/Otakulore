using Otakulore.Pages;
using Otakulore.Resources.Themes;
using Otakulore.Services;

namespace Otakulore;

public partial class AppShell
{

    public AppShell()
    {
        InitializeComponent();
        var settings = MauiHelper.GetService<SettingsService>();
        switch (settings.ThemeIndex)
        {
            case 1:
                Application.Current?.Resources.MergedDictionaries.Add(new Lavender());
                break;
        }
        MauiHelper.AddRoute(typeof(ContentViewerPage));
        MauiHelper.AddRoute(typeof(DetailsPage));
        MauiHelper.AddRoute(typeof(SearchPage));
        MauiHelper.AddRoute(typeof(SearchProviderPage));
        MauiHelper.AddRoute(typeof(SourceViewerPage));
    }

}