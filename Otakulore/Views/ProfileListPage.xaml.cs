using Microsoft.UI.Xaml.Navigation;

namespace Otakulore.Views;

public sealed partial class ProfileListPage
{

    public ProfileListPage()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        if (args.Parameter is not int id)
            return;
        // TODO
    }

}