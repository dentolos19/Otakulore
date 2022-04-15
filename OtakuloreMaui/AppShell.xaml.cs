using Otakulore.Views;

namespace Otakulore;

public partial class AppShell
{

    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("details", typeof(DetailsPage));
    }

}