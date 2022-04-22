using Otakulore.Pages;

namespace Otakulore;

public partial class AppShell
{

    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("details", typeof(DetailsPage));
        Routing.RegisterRoute("search", typeof(SearchPage));
    }

}