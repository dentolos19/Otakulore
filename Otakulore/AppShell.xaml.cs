using Otakulore.Pages;

namespace Otakulore;

public partial class AppShell
{

    public AppShell()
    {
        InitializeComponent();
        MauiHelper.AddRoute(typeof(ContentViewerPage));
        MauiHelper.AddRoute(typeof(DetailsPage));
        MauiHelper.AddRoute(typeof(SearchPage));
        MauiHelper.AddRoute(typeof(SearchProviderPage));
        MauiHelper.AddRoute(typeof(SourceViewerPage));
    }

}