using Microsoft.UI.Xaml;

namespace Otakulore.Views.Dialogs;

public sealed partial class ManageTrackerDialog
{

    public ManageTrackerDialog()
    {
        XamlRoot = App.Window.Content.XamlRoot;
        InitializeComponent();
    }

    private void OnSave(object sender, RoutedEventArgs args)
    {
        Hide();
    }

    private void OnCancel(object sender, RoutedEventArgs args)
    {
        Hide();
    }

}