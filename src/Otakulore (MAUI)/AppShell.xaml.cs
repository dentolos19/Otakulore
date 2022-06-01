using Otakulore.Pages;

namespace Otakulore;

public partial class AppShell
{

    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));
        Routing.RegisterRoute(nameof(SearchProviderPage), typeof(SearchProviderPage));
        Routing.RegisterRoute(nameof(SearchPage), typeof(SearchPage));
    }

}